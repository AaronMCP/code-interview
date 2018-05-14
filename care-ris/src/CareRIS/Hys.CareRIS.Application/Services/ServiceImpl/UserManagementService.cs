using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using Kendo.DynamicLinq;
using Hys.CrossCutting.Common;
using System.Text.RegularExpressions;
using Hys.CareRIS.EnterpriseLib;
using Newtonsoft.Json;
using Hys.CrossCutting.Common.Utils;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class UserManagementService : DisposableServiceBase, IUserManagementService
    {
        private IUserRepository _UserRepository;
        private IOnlineClientRepository _OnlineClientRepository;
        private IRisProContext _dbContext;
        private LicenseDataDto _license;

        public UserManagementService(IUserRepository userRepository, IOnlineClientRepository onlineClientRepository, IRisProContext dbContext, LicenseDataDto license)
        {
            _UserRepository = userRepository;
            _OnlineClientRepository = onlineClientRepository;
            _dbContext = dbContext;
            _license = license;

            AddDisposableObject(userRepository);
            AddDisposableObject(onlineClientRepository);
            AddDisposableObject(dbContext);
        }

        public UserDto GetUser(string userName)
        {
            var user = _UserRepository.Get(p => p.LoginName == userName && p.DeleteMark == 0).FirstOrDefault();

            if (user != null)
            {
                var result = Mapper.Map<User, UserDto>(user);
                var e = _UserRepository.GetUserExpiration(user.UniqueID);
                var d2 = DateTime.Now;
                if (e != null)
                {
                    result.HasValidPeriod = e.IsSetExpireDate.HasValue ? (e.IsSetExpireDate == 1 ? true : false) : false;
                    result.ValidStartDate = e.StartDate;
                    result.ValidEndDate = e.EndDate;
                }

                var site = _dbContext.Set<UserProfile>().
                    Where(u => u.Name == "BelongToSite" && u.UserID == user.UniqueID).
                    Select(p => p.Value).FirstOrDefault() ?? "";
                var siteList = site.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                result.Site = siteList.Length > 0 ? siteList[0] : "";
                result.RoleName = GetUserDefaultRole(result);

                return result;
            }
            return null;
        }
        public void UpdateUserPassword(string userId, string password)
        {
            _UserRepository.UpdatePassword(userId, password);

        }

        public UserDto GetUserByID(string userID)
        {
            var user = _UserRepository.Get(p => p.UniqueID == userID).FirstOrDefault();
            if (user != null)
            {
                var result = Mapper.Map<User, UserDto>(user);
                result.RoleName = GetUserDefaultRole(result);
                return result;
            }
            return null;
        }

        /// <summary>
        /// add or update online info when login
        /// </summary>
        /// <param name="onlineClientDto"></param>
        /// <returns>0:sucess;1:error, same user login on other location;2:error, max user; 3:same user login in other location 4: can not get license data 5:license expired</returns>
        public int LoginToOnline(OnlineClientDto onlineClientDto, string isForce, out string message)
        {
            message = "";
            List<OnlineClient> onlineClientList = _OnlineClientRepository.Get(o => o.MachineIP == onlineClientDto.MachineIP && o.IsOnline == 1).ToList();
            foreach (OnlineClient onlineClient in onlineClientList)
            {
                //not selfservice login user
                if ((onlineClient.Comments == null || onlineClient.Comments.ToLower() != "selfservice login user")
                    && string.Compare(onlineClient.UniqueID, onlineClientDto.UniqueID, true) != 0)
                {
                    //同一台机器上只能允许登陆1次，请退出后再登陆！
                    if (isForce == "0")
                    {
                        UserDto user = GetUserByID(onlineClient.UniqueID);
                        if (user != null)
                        {
                            message = user.LoginName;
                        }
                        return 1;
                    }
                    else
                    {
                        //onlineClient.IsOnline = 0;
                    }
                }
            }

            //get max value
            string countName = "OnlineUserCheckTimePeriod";
            string countValue = null;
            int currentOnline = 0;
            SystemProfile systemProfile = null;
            SiteProfile siteProfile = _dbContext.Set<SiteProfile>().Where(s => s.Name == countName && s.Domain == onlineClientDto.Domain && s.Site == onlineClientDto.Site).FirstOrDefault();
            if (siteProfile != null && siteProfile.Value != null)
            {
                countValue = siteProfile.Value;
                currentOnline = _OnlineClientRepository.Get(o => o.IsOnline == 1 && o.Domain == onlineClientDto.Domain && o.Site == onlineClientDto.Site).Count();
            }
            else
            {
                systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == countName && s.Domain == onlineClientDto.Domain).FirstOrDefault();
                if (systemProfile != null && systemProfile.Value != null)
                {
                    countValue = systemProfile.Value;
                    currentOnline = _OnlineClientRepository.Get(o => o.IsOnline == 1 && o.Domain == onlineClientDto.Domain).Count();
                }
            }
            if (countValue != null)
            {
                //Max user count reached
                int profileMaxUserNumber = GetMaxOnlineUserCount(countValue);

                if (currentOnline >= profileMaxUserNumber)
                {
                    return 2;
                }

            }

            List<OnlineClient> onlineClientList2 = _OnlineClientRepository.Get(o => o.UniqueID == onlineClientDto.UniqueID && o.IsOnline == 1).ToList();
            foreach (OnlineClient onlineClient in onlineClientList2)
            {
                //not selfservice login user
                //if ((onlineClient.Comments == null || onlineClient.Comments.ToLower() != "selfservice login user")
                //    && (string.Compare(onlineClient.MachineIP, onlineClientDto.MachineIP, true) != 0
                //    || string.Compare(onlineClient.Comments, onlineClientDto.Comments, true) != 0))


                if (onlineClient.Comments != null && onlineClient.Comments.ToLower() != "selfservice login user"
                    && string.Compare(onlineClient.MachineIP, onlineClientDto.MachineIP, true) != 0)
                {
                    //同一用户只能在一个地方登录
                    if (isForce == "0")
                    {
                        message = onlineClient.MachineName + "&" + onlineClient.MachineIP;
                        return 3;
                    }
                    else
                    {
                        //onlineClient.IsOnline = 0;
                    }
                }
            }

            //clear all offline
            List<OnlineClient> onlineClientList3 = _OnlineClientRepository.Get(o => o.UniqueID == onlineClientDto.UniqueID && o.IsOnline == 0).ToList();
            foreach (OnlineClient onlineClient in onlineClientList3)
            {
                _OnlineClientRepository.Delete(onlineClient);
            }
            _OnlineClientRepository.SaveChanges();

            //add or update online data
            string webUserFlag = "web login user";

            // check web license
            var webOnline = _OnlineClientRepository.Get(o => o.IsOnline == 1 && o.Domain == onlineClientDto.Domain && o.Comments == webUserFlag).Count();
            if (_license.IsSuccessed)
            {
                if (_license.IsExpired)
                {
                    return 5;
                }
                if (webOnline >= _license.MaxOnlineUserCount)
                {
                    return 2;
                }
            }
            else
            {
                return 4;
            }


            OnlineClient onlineClientOld = _OnlineClientRepository.Get(o => o.UniqueID == onlineClientDto.UniqueID && o.Comments != "selfservice login user").FirstOrDefault();
            if (onlineClientOld != null && onlineClientOld.IsOnline == 1)
            {

            }
            else
            {
                OnlineClient onlineClientNew = Mapper.Map<OnlineClientDto, OnlineClient>(onlineClientDto);
                onlineClientNew.RoleName = GetUserDefaultRole(new UserDto { UniqueID = onlineClientDto.UniqueID, Domain = onlineClientDto.Domain });
                _OnlineClientRepository.Add(onlineClientNew);

                _OnlineClientRepository.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// set to offline when user logout
        /// </summary>
        /// <param name="onlineClientDto"></param>
        /// <returns></returns>
        public bool LogoutToOffline(OnlineClientDto onlineClientDto)
        {
            string webUserFlag = "web login user";
            OnlineClient onlineClientOld = _OnlineClientRepository.Get(o => o.UniqueID == onlineClientDto.UniqueID && o.Comments == webUserFlag && o.MachineIP == onlineClientDto.MachineIP).FirstOrDefault();
            if (onlineClientOld != null)
            {
                onlineClientOld.IsOnline = 0;
                _OnlineClientRepository.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// set to offline when user logout
        /// </summary>
        /// <param name="onlineClientDto"></param>
        /// <returns></returns>
        public bool UserOffline(string userIDIP)
        {
            string webUserFlag = "web login user";
            string[] userIDs = userIDIP.Split('&');
            if (userIDs.Length == 2)
            {
                string userIDs1 = userIDs[0];
                string userIDs2 = userIDs[1];
                OnlineClient onlineClientOld = _dbContext.Set<OnlineClient>().Where(o => o.UniqueID == userIDs1 && o.Comments == webUserFlag
                    && o.MachineIP == userIDs2).FirstOrDefault();
                if (onlineClientOld != null)
                {
                    if (onlineClientOld.IsOnline == 0)
                    {
                        onlineClientOld.IsOnline = 1;
                    }
                    _dbContext.SaveChanges();

                    onlineClientOld = _dbContext.Set<OnlineClient>().Where(o => o.UniqueID == userIDs1 && o.Comments == webUserFlag
                    && o.MachineIP == userIDs2).FirstOrDefault();
                    if (onlineClientOld.IsOnline == 1)
                    {
                        onlineClientOld.IsOnline = 0;
                    }
                    _dbContext.SaveChanges();

                    return true;
                }
            }


            return false;
        }

        /// <summary>
        /// is online
        /// </summary>
        /// <param name="onlineClientDto"></param>
        /// <returns></returns>
        public bool IsOnline(OnlineClientDto onlineClientDto)
        {
            string webUserFlag = "web login user";
            OnlineClient onlineClientOld = _OnlineClientRepository.Get(o => o.UniqueID == onlineClientDto.UniqueID
                && o.Comments == webUserFlag && o.IsOnline == 1 && o.MachineIP == onlineClientDto.MachineIP).FirstOrDefault();
            if (onlineClientOld != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// set to offline when web application start
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool StartToOffline(string url)
        {
            try
            {
                List<OnlineClient> onlineClientList = _OnlineClientRepository.Get(o => o.IISUrl == url).ToList();
                foreach (OnlineClient onlineClient in onlineClientList)
                {
                    onlineClient.IsOnline = 0;
                }
                _OnlineClientRepository.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private int GetMaxOnlineUserCount(string onlineUserCountSettingString)
        {
            int count = int.MaxValue;
            string[] items = onlineUserCountSettingString.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (items != null && items.Length > 0)
            {
                foreach (string item in items)
                {
                    string[] periodCounts = item.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (periodCounts != null && periodCounts.Length == 2)
                    {
                        string ct = periodCounts[1];
                        string[] periods = periodCounts[0].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (periods != null && periods.Length == 2)
                        {
                            DateTime beginTime = new DateTime();
                            DateTime.TryParse(periods[0], out beginTime);
                            DateTime endTime = new DateTime();
                            DateTime.TryParse(periods[1], out endTime);
                            DateTime now = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                            if (string.Compare(now.ToString("yyyy-MM-dd HH:mm"), beginTime.ToString("yyyy-MM-dd HH:mm")) >= 0 && string.Compare(now.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")) <= 0)
                            {
                                int.TryParse(ct, out count);
                                break;
                            }
                        }
                    }
                }
            }
            return count;
        }

        public void UpdateInvalidCount(string userID, string domain)
        {
            var user = _UserRepository.Get(p => p.UniqueID == userID).FirstOrDefault();
            if (user != null)
            {
                SystemProfile systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == "InvalidLoginMaxCount" && s.Domain == domain).FirstOrDefault();

                if (systemProfile != null && systemProfile.Value != "")
                {
                    int maxCount = 0;
                    int.TryParse(systemProfile.Value, out maxCount);
                    if (maxCount > 0)
                    {
                        //set invalid count
                        if (user.InvalidLoginCount == null)
                        {
                            user.InvalidLoginCount = 1;
                        }
                        else if (user.InvalidLoginCount.HasValue)
                        {
                            user.InvalidLoginCount += 1;
                        }

                        //is lock

                        if (user.InvalidLoginCount >= maxCount)
                        {
                            user.IsLocked = 1;
                        }

                        _UserRepository.SaveChanges();
                    }
                }
            }
        }

        public void UpdateInvalidCount(string userID)
        {
            var user = _UserRepository.Get(p => p.UniqueID == userID).FirstOrDefault();
            if (user != null)
            {
                user.InvalidLoginCount = 0;
                _UserRepository.SaveChanges();
            }
        }



        public async Task<IEnumerable<RoleDto>> GetUserRolesAsync(string userID)
        {
            var query = from u in _dbContext.Set<RoleToUser>()
                        join role in _dbContext.Set<Role>() on u.RoleName equals role.RoleName
                        where u.UserID == userID
                        select role;
            return (await query.ToListAsync()).Select(Mapper.Map<Role, RoleDto>);
        }

        public string GetUserDefaultRole(UserDto user)
        {
            var defaultRoleName = "DefaultRoleName";
            var risDefaultRole = _dbContext.Set<UserProfile>().FirstOrDefault(profile => profile.UserID == user.UniqueID && profile.Name == defaultRoleName);
            if (risDefaultRole != null)
            {
                return risDefaultRole.Value;
            }

            var defaultRole = _dbContext.Set<RoleToUser>().FirstOrDefault(l => l.UserID == user.UniqueID);
            if (defaultRole != null)
            {
                _dbContext.Set<UserProfile>().Add(new UserProfile
                {
                    UniqueID = Guid.NewGuid().ToString(),
                    UserID = user.UniqueID,
                    Value = defaultRole.RoleName,
                    ModuleID = "0Z00", // 0Z00 for WEB_RISPro
                    Name = defaultRoleName,
                    Domain = user.Domain,
                    RoleName = "",//defaultRole.RoleName,
                    PropertyType = 0,
                    IsExportable = 0,
                    CanBeInherited = 0,
                    IsHidden = 0,
                    OrderingPos = "0",
                });
                _dbContext.SaveChanges();
                return defaultRole.RoleName;
            }
            return "";
        }

        public async Task<bool> UpdateRisUserDefaultRoleAsync(string userID, string domain, Dictionary<string, string> data)
        {
            var role = data["roleName"];
            var onlineClient = await _dbContext.Set<OnlineClient>().FirstOrDefaultAsync(p => p.UniqueID == userID && p.Domain == domain && p.IsOnline == 1);
            if (onlineClient != null)
            {
                onlineClient.RoleName = role;
            }

            var profile = await _dbContext.Set<UserProfile>().FirstOrDefaultAsync(p => p.UserID == userID && p.Domain == domain && p.Name == "DefaultRoleName");
            if (profile != null)
            {
                profile.Value = role;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public bool IsPublicAccount(string email)
        {
            var profile = (from u in _dbContext.Set<User>()
                           join p in _dbContext.Set<UserProfile>() on u.UniqueID equals p.UserID
                           where u.LoginName == email && p.Name == "IsIntern"
                           select p).FirstOrDefault();
            return profile != null && profile.Value == "1";
        }

        #region  用户管理
        /// <summary>
        /// 用户分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        //public DataSourceResult GetPageUsers(DataSourceRequest request, string[] roles)
        public DataSourceResult GetPageUsers(PageRequest<string[]> request)
        {
            var roles = request.Specify;
            var kendoRequest = request.Request;
            var empty = roles == null || roles.Length < 1;
            var query = from u in _dbContext.Set<User>()
                        join ud in _dbContext.Set<User2Domain>() on u.UniqueID equals ud.UniqueID
                        join r2 in _dbContext.Set<RoleToUser>() on ud.UniqueID equals r2.UserID
                        where u.DeleteMark == 0 && (empty || roles.Contains(r2.RoleName))
                        select new UserDto
                        {
                            UniqueID = u.UniqueID,
                            LoginName = u.LoginName,
                            LocalName = u.LocalName,
                            DisplayName = u.DisplayName,
                            EnglishName = u.EnglishName,
                            Comments = u.Comments,
                            //Password = u.Password,
                            Domain = u.Domain,
                            Title = u.Title,
                            Address = u.Address,
                            Department = ud.Department,
                            Telephone = ud.Telephone,
                            Mobile = ud.Mobile,
                            Email = ud.Email,
                            IsLocked=u.IsLocked.HasValue ? (u.IsLocked == 1) : false,
                            HasValidPeriod = ud.IsSetExpireDate.HasValue ? (ud.IsSetExpireDate == 0) : true,
                            IsSetExpireDate = ud.IsSetExpireDate == 0 ? false : true,
                            ValidEndDate = ud.EndDate,
                            ValidStartDate = ud.StartDate,
                            EndDate = ud.EndDate,
                            StartDate = ud.StartDate,
                            DomainLoginName = ud.DomainLoginName
                        };
            query = query.Distinct();
            var result = query.ToDataSourceResult(kendoRequest);
            return result;

        }

        public List<RoleProfile> GetRoleProfiles(string strDomain)
        {

            if (strDomain != null)
            {
                var rps = _dbContext.Set<RoleProfile>().Where(r => string.IsNullOrEmpty(r.RoleName) && r.Domain == strDomain && r.CanBeInherited > 0);

                return rps.ToList();
            }
            return null;

        }
        /// <summary>
        /// 本地名验证
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DisplayNameExist(UserDto userInfo)
        {
            var user = _UserRepository.Get(p => p.DisplayName == userInfo.LocalName && p.DeleteMark == 0 && p.UniqueID != userInfo.UniqueID).FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// true:通过
        /// </summary>
        /// <param name="logionName"></param>
        /// <returns></returns>
        public bool UserLoginNameExist(string logionName)
        {
            var userdb = _dbContext.Set<User>();
            var login = userdb.Where(u => u.LocalName == logionName).FirstOrDefault();
            if (login != null)
            {
                return false;
            }
            return true;
        }
        //新增用户
        public bool SaveUser(UserDto userInfo)
        {
            if (userInfo == null)
            {
                return false;
            }
            var userdb = _dbContext.Set<User>();
            var userDomaindb = _dbContext.Set<User2Domain>();

            if (!UserLoginNameExist(userInfo.LoginName))
            {
                //客户端有恢复删除用户的逻辑
                return false;
            }
            if (!DisplayNameExist(userInfo))
            {
                return false;
            }
            //add user    
            var user = Mapper.Map<UserDto, User>(userInfo);
            var c = new Cryptography("GCRIS2-20061025");
            user.Password = c.Encrypt(userInfo.Password);
            user.UniqueID = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(user.DisplayName))
            {
                user.DisplayName = user.LocalName;
            }
            user.DeleteMark = 0;
            userdb.Add(user);
            //add User2Domain
            var userDomain = new User2Domain();
            userDomain.UniqueID = user.UniqueID;
            userDomain.DomainLoginName = userInfo.DomainLoginName;
            userDomain.Department = userInfo.Department;
            userDomain.Telephone = userInfo.Telephone;
            userDomain.Mobile = userInfo.Mobile;
            userDomain.Email = userInfo.Email;
            userDomain.Domain = userInfo.Domain;
            userDomain.IsSetExpireDate = userInfo.IsSetExpireDate ? 1 : 0;
            userDomain.StartDate = userInfo.StartDate;
            userDomain.EndDate = userInfo.EndDate;
            userDomaindb.Add(userDomain);

            //add roletouser

            List<RoleToUserDto> listRoles = new List<RoleToUserDto>();
            if (userInfo.RolesName != null && userInfo.RolesName.Count > 0)
            {
                foreach (string strRoleName in userInfo.RolesName)
                {
                    RoleToUserDto role = new RoleToUserDto
                    {
                        RoleName = strRoleName,
                        UserID = user.UniqueID,
                        Domain = user.Domain
                    };
                    listRoles.Add(role);
                }
            }

            if (!UpdateRoleToUser(listRoles))
            {
                return false;
            }
            //add userprofile
            var rps = GetRoleProfiles(userInfo.Domain);
            var updb = _dbContext.Set<UserProfile>();
            if (rps != null)
            {
                string strPropertyOption = null;
                int canBeInherited = -1;
                foreach (var rp in rps)
                {
                    strPropertyOption = Convert.ToString(rp.PropertyOptions);
                    if (!strPropertyOption.Contains("|"))
                    {
                        strPropertyOption = strPropertyOption.Replace("''", "'");
                    }
                    canBeInherited = rp.CanBeInherited - 1;
                    var up = new UserProfile();
                    up.UniqueID = Guid.NewGuid().ToString();
                    up.Name = rp.Name;
                    up.ModuleID = rp.ModuleID;
                    up.RoleName = rp.RoleName;
                    up.UserID = user.UniqueID;
                    if (rp.Name.ToUpper() == "ACCESSSITE")
                    {
                        up.Value = userInfo.AccessSites!=null? userInfo.AccessSites: userInfo.Site;
                    }
                    else {
                        up.Value = rp.Name.ToUpper() == "BELONGTOSITE" ? userInfo.Site : rp.Value;
                    }
                    up.IsExportable = rp.IsExportable;
                    up.PropertyDesc = rp.PropertyDesc.Trim();
                    up.PropertyOptions = strPropertyOption;
                    up.CanBeInherited = canBeInherited;
                    up.PropertyType = rp.PropertyType;
                    up.IsHidden = 1;
                    up.OrderingPos = rp.OrderingPos;
                    up.Domain = user.Domain;
                    updb.Add(up);
                }
            }
            _dbContext.SaveChanges();
            return true;

        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool UpdateUser(UserDto userInfo)
        {
            if (userInfo == null)
            {
                return false;
            }
            var user = _UserRepository.Get(p => p.UniqueID == userInfo.UniqueID).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            //user
            user.LoginName = userInfo.LoginName;
            user.LocalName = userInfo.LocalName;
            user.DisplayName = userInfo.LocalName;
            user.EnglishName = userInfo.EnglishName;
            user.Title = userInfo.Title;
            user.Address = userInfo.Address;
            user.Comments = userInfo.Comments;
            user.IsLocked = userInfo.IsLocked.HasValue && userInfo.IsLocked == true ? 1 : 0;
            if (!string.IsNullOrEmpty(userInfo.Password))
            {
                var c = new Cryptography("GCRIS2-20061025");
                var psd = c.Encrypt(userInfo.Password);
                if (user.Password != psd)
                {
                    user.Password = psd;
                }
            }
            //userDomain
            var userDomian = _dbContext.Set<User2Domain>().FirstOrDefault(u => u.UniqueID == userInfo.UniqueID && u.Domain == userInfo.Domain);
            if (userDomian == null)
            {
                return false;
            }
            userDomian.Department = userInfo.Department;
            userDomian.Telephone = userInfo.Telephone;
            userDomian.DomainLoginName = userInfo.DomainLoginName;
            userDomian.Mobile = userInfo.Mobile;
            userDomian.Email = userInfo.Email;
            userDomian.IsSetExpireDate = userInfo.IsSetExpireDate ? 1 : 0;
            if (userInfo.IsSetExpireDate)
            {
                userDomian.EndDate = userInfo.EndDate;
                userDomian.StartDate = userInfo.StartDate;
            }
            else
            {
                userDomian.EndDate = null;
                userDomian.StartDate = null;
            }

            //user2role

            List<RoleToUserDto> listRoles = new List<RoleToUserDto>();
            if (userInfo.RolesName != null && userInfo.RolesName.Count > 0)
            {
                foreach (string strRoleName in userInfo.RolesName)
                {
                    RoleToUserDto role = new RoleToUserDto
                    {
                        RoleName = strRoleName,
                        UserID = user.UniqueID,
                        Domain = user.Domain
                    };
                    listRoles.Add(role);
                }
            }
            UpdateRoleToUser(listRoles);
            //UserProfile
            UpdateUserProfile(userInfo.UserProfileList, userInfo.UniqueID, userInfo.Domain);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetRoles()
        {
            var tempRole = _dbContext.Set<Role>().ToList();
            var roleList = tempRole.Select(r => Mapper.Map<Role, RoleDto>(r));
            return roleList;
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetUserRoles(string userID)
        {
            var roleToUsers = _dbContext.Set<RoleToUser>().Where(r => r.UserID == userID);

            List<RoleDto> roleList = new List<RoleDto>();
            foreach (var t in roleToUsers)
            {
                var tempRole = _dbContext.Set<Role>().FirstOrDefault(r => r.RoleName == t.RoleName);
                if (tempRole != null)
                {
                    var tempRoleDto = Mapper.Map<Role, RoleDto>(tempRole);
                    roleList.Add(tempRoleDto);
                }
            }

            return roleList;
        }

        /// <summary>
        /// 修改角色用户对应关系
        /// </summary>
        /// <param name="roleToUsers"></param>
        /// <returns></returns>
        public bool UpdateRoleToUser(List<RoleToUserDto> roleToUsers)
        {

            if (roleToUsers == null)
            {
                return false;
            }
            if (roleToUsers.Count < 1)
            {
                return true;
            }
            //删除角色用户对应关系
            var userID = roleToUsers[0].UserID;
            if (userID != null)
            {
                var rulist = _dbContext.Set<RoleToUser>().Where(r => r.UserID == userID).ToList();
                if (rulist != null && rulist.Count > 0)
                {
                    foreach (var ru in rulist)
                    {
                        _dbContext.Set<RoleToUser>().Remove(ru);
                    }
                    _dbContext.SaveChanges();
                }
            }
            //添加角色用户对应关系
            var rulistNew = roleToUsers.Select(r =>
            {

                var roleto = new RoleToUser();
                roleto.Domain = r.Domain;
                roleto.RoleName = r.RoleName;
                roleto.UserID = r.UserID;
                return roleto;
            });
            foreach (var ruNew in rulistNew)
            {
                _dbContext.Set<RoleToUser>().Add(ruNew);
            }
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取用户权限配置
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserProfiles(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return null;
            }
            var user = _dbContext.Set<User>().Where(u => u.UniqueID == userID).FirstOrDefault();
            if (user != null)
            {
                var reportDbService = new ReportDBService();
                var ds = reportDbService.GetUserProfDetDataSet(userID, user.Domain);
                string json = JsonConvert.SerializeObject(ds);
                return json;
            }

            //var query = (from u in _dbContext.Set<UserProfile>()
            //             join m in _dbContext.Set<Module>() on u.ModuleID equals m.ModuleID
            //             where (u.IsHidden & 1) == 1
            //             && u.UserID == userID
            //             orderby u.OrderingPos descending
            //             select new UserProDto
            //             {
            //                 UniqueID = u.UniqueID,
            //                 RoleName = u.RoleName,
            //                 Name = u.Name,
            //                 ModuleID = u.ModuleID,
            //                 //ModuleName = m.Title,
            //                 Value = u.Value,
            //                 UserID = u.UserID,
            //                 Domain = u.Domain,
            //                 IsExportable = u.IsExportable,
            //                 PropertyDesc = u.PropertyDesc,
            //                 CanBeInherited = u.CanBeInherited,
            //                 PropertyType = u.PropertyType,
            //                 IsHidden = u.IsHidden == 1,
            //                 OrderingPos = u.OrderingPos
            //             }).ToList();
            //var result = query.Distinct();
            //result.Select(p =>
            //{
            //    //根据PropertyOptions 获得下拉列表集 
            //    return p;
            //});
            return null;
        }

        /// <summary>
        /// 获取影像科室
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<DictionaryValueDto> GetDepart(string domain)
        {
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = dicValues.Where(d => d.Tag == 2 && d.Domain == domain).ToList();
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).ToList();
            return groupedDVs;
        }

        /// <summary>
        /// 更新权限配置 
        /// </summary>
        /// <param name="userRrofiles"></param>
        /// <returns></returns>
        public bool UpdateUserProfile(List<ProfileDto> userProfiles, string userID, string domain)
        {

            if (userProfiles == null)
            {
                return false;
            }
            if (userProfiles.Count < 1)
            {
                return true;
            }
            //更新权限配置 
            foreach (var upNew in userProfiles)
            {
                var up = _dbContext.Set<UserProfile>().FirstOrDefault(p => p.Name == upNew.Name && p.UserID == userID && p.Domain == domain);
                if (up != null)
                {
                    up.Value = upNew.Value;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取所有职称
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public IEnumerable<DictionaryValueDto> GetTitles(string domain)
        {
            try
            {
                var query = _dbContext.Set<DictionaryValue>().Where(d => d.Tag == 7 && d.Domain == domain).ToList();
                var result = query.Select(d => Mapper.Map<DictionaryValue, DictionaryValueDto>(d)).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 修改用户时锁死用户
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="strDomain"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public bool CheckSyncronization(string userGuid, string strDomain, string currentUserID)
        {
            if (IsUserDeleted(userGuid, strDomain))
            {
                return false;
            }
            //获取当前操作用户ID
            string ipADD = "";
            //IP地址
            var online = _dbContext.Set<OnlineClient>().Where(u => u.UniqueID == currentUserID && u.Domain == strDomain).FirstOrDefault();
            if (online != null)
            {
                ipADD = online.MachineIP;
            }
            var dbSync = _dbContext.Set<Sync>();
            var sync = new Sync
            {
                SyncType = 12,
                UniqueId = userGuid,
                Owner = currentUserID,
                OwnerIP = ipADD,
                Domain = strDomain
            };
            dbSync.Add(sync);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 删除锁 未写+已有
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="strDomain"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public bool DelSyncronization(string userGuid, string strDomain, string currentUserID)
        {

            return true;
        }

        //用户是否已删除
        private bool IsUserDeleted(string strUserGuid, string strDomain)
        {
            var user = _dbContext.Set<User>().Where(u => u.UniqueID == strUserGuid && u.Domain == strDomain).FirstOrDefault();
            if (user != null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="strUserGuid"></param>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        public bool DeleteUser(string strUserGuid, string strDomain)
        {
            var userdb = _dbContext.Set<User>();
            var umdb = _dbContext.Set<User2Domain>();
            var updb = _dbContext.Set<UserProfile>();
            var rudb = _dbContext.Set<RoleToUser>();

            var user = userdb.Where(u => u.UniqueID == strUserGuid && u.Domain == strDomain).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            //user2domain
            var ums = umdb.Where(u => u.UniqueID == strUserGuid && u.Domain == strDomain).ToList();
            if (ums != null && ums.Count > 0)
            {
                foreach (var um in ums)
                {
                    umdb.Remove(um);
                }
            }
            //user
            user.DeleteMark = 1;
            //UserProfile
            var ups = updb.Where(u => u.UserID == strUserGuid && u.Domain == strDomain).ToList();
            if (ups != null && ups.Count > 0)
            {
                foreach (var up in ups)
                {
                    updb.Remove(up);
                }
            }
            //RoleToUser
            var rus = rudb.Where(u => u.UserID == strUserGuid && u.Domain == strDomain).ToList();
            if (rus != null && rus.Count > 0)
            {
                foreach (var ru in rus)
                {
                    rudb.Remove(ru);
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetUserByRoleName(List<string> roleName, string domain)
        {

            if (roleName == null || roleName.Count < 1)
            {
                return null;
            }
            var query = await (from u in _dbContext.Set<User>()
                               join ud in _dbContext.Set<User2Domain>() on u.UniqueID equals ud.UniqueID
                               join r2 in _dbContext.Set<RoleToUser>() on u.UniqueID equals r2.UserID
                               where u.DeleteMark == 0 && (roleName.Contains(r2.RoleName)) && ud.Domain == domain
                               select new UserDto
                               {
                                   UniqueID = ud.UniqueID,
                                   LoginName = u.LoginName,
                                   LocalName = u.LocalName
                               }).ToListAsync();
            var result = query.Distinct().ToList();
            return result;
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public int UpdatePassword(UserPwdDto upwd)
        {
            if (upwd == null)
            {
                return -2;
            }
            var user = _UserRepository.Get(p => p.UniqueID == upwd.UserID).FirstOrDefault();
            if (user == null)
            {
                return -2;
            }
            var c = new Cryptography("GCRIS2-20061025");
            var oldpwd = c.Encrypt(upwd.OldPassword);
            var newpwd = c.Encrypt(upwd.NewPassword);
            if (user.Password != oldpwd)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(upwd.NewPassword) || user.Password == newpwd)
            {
                return -1;
            }

            user.Password = newpwd;
            _dbContext.SaveChanges();
            return 1;
        }

        #endregion
    }
}
