using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hys.Consultation.EntityFramework.Repositories;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;
using Hys.CrossCutting.Common.Extensions;
using Hys.CrossCutting.Common.Interfaces;
using Hys.CrossCutting.Common.Utils;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRisProContext _RisProContext;
        private readonly IConsultationContext _DBContext;
        private readonly ILoginUserService _LoginUserService;


        #region Implementation of IUserManagementService

        public UserManagementService(IRisProContext risProContext, IConsultationContext dbContext, ILoginUserService loginUserService)
        {
            _RisProContext = risProContext;
            _DBContext = dbContext;
            _LoginUserService = loginUserService;
        }


        public IQueryable<User> FilterUsers(UserSearchCriteriaDto searchCriteria)
        {
            var users = _RisProContext.Set<User>().Where(u => u.DeleteMark != 1);
            if (!_LoginUserService.IsSystemAdmin && !searchCriteria.ShowAllUser)
            {
                var excludeUserIds = _DBContext.Set<UserExtention>()
                        .Where(u => !String.IsNullOrEmpty(u.HospitalID) && u.HospitalID != _LoginUserService.DefaultSiteID)
                        .Select(u => u.UniqueID).ToList();

                users = users.Where(u => !excludeUserIds.Contains(u.UniqueID));
            }

            if (!String.IsNullOrEmpty(searchCriteria.Name))
            {
                users = users.Where(u => u.LoginName.Contains(searchCriteria.Name) || u.LocalName.Contains(searchCriteria.Name));
            }
            var hasDep = !String.IsNullOrEmpty(searchCriteria.DepartmentID);
            var hasHospital = !String.IsNullOrEmpty(searchCriteria.HospitalID);
            var hasRole = !String.IsNullOrEmpty(searchCriteria.RoleID);
            var hasArea = !String.IsNullOrEmpty(searchCriteria.ProvinceName);
            var hasInCenter = searchCriteria.IsInCenter != null && searchCriteria.IsInCenter == 1;
            if (hasDep || hasHospital || hasRole || hasArea || hasInCenter)
            {
                var query = _DBContext.Set<UserExtention>().AsQueryable();
                if (hasDep)
                {
                    query = query.Where(u => u.DepartmentID == searchCriteria.DepartmentID);
                }
                if (hasHospital)
                {
                    query = query.Where(u => u.HospitalID == searchCriteria.HospitalID);
                }
                if (hasRole)
                {
                    query = query.Where(u => u.Roles.Any(r => r.UniqueID == searchCriteria.RoleID));
                }

                if (hasArea)
                {
                    //get hospital ids
                    var queryHospital = _DBContext.Set<HospitalProfile>().Where(u => u.Province == searchCriteria.ProvinceName);
                    if (!String.IsNullOrEmpty(searchCriteria.CityName))
                    {
                        queryHospital = queryHospital.Where(u => u.City == searchCriteria.CityName);
                    }
                    var hospitalIds = queryHospital.Select(u => u.UniqueID).ToList();
                    query = query.Where(u => hospitalIds.Contains(u.HospitalID));
                }

                if (hasInCenter)
                {
                    var queryHospital = _DBContext.Set<HospitalProfile>().Where(u => u.IsConsultation == true);
                    var hospitalIds = queryHospital.Select(u => u.UniqueID).ToList();
                    query = query.Where(u => hospitalIds.Contains(u.HospitalID));
                }

                var userIds = query.Select(u => u.UniqueID).ToList();
                users = users.Where(u => userIds.Contains(u.UniqueID));
            }

            return users;
        }

        public PaginationResult SearchUsers(UserSearchCriteriaDto searchCriteria)
        {
            if (searchCriteria.PageIndex == 0)
            {
                searchCriteria.PageIndex = 1;
            }
            var searchResult = FilterUsers(searchCriteria);
            var users = searchResult.OrderBy(u => u.LocalName).Skip((searchCriteria.PageIndex - 1) * searchCriteria.PageSize).Take(searchCriteria.PageSize)
                .ToList().Select(user =>
                {
                    var userDto = Mapper.Map<User, UserDto>(user);
                    var extention = _DBContext.Set<UserExtention>().Include(u => u.Roles).Include(u => u.Hospital).Include(u => u.Department).FirstOrDefault(u => u.UniqueID == user.UniqueID);
                    if (extention != null)
                    {
                        extention.Roles = extention.Roles.Where(r => !r.IsDeleted && r.Status).ToList();
                        Mapper.Map(extention, userDto);
                    }
                    if (searchCriteria.IncludeMobile)
                    {
                        userDto.Mobile = GetUserMobile(userDto.UniqueID);
                    }
                    return userDto;
                });
            return new PaginationResult { Data = users, Total = searchResult.Count() };
        }


        public string GetRoleRelatedUserName(string roleID)
        {
            var userIDs = _DBContext.Set<UserExtention>().Where(u => u.Roles.Any(r => r.UniqueID == roleID)).Select(u => u.UniqueID).ToList();
            if (userIDs.Count != 0)
            {
                var userNames = _RisProContext.Set<User>().Where(u => userIDs.Contains(u.UniqueID)).Select(u => u.LocalName).ToList();
                return String.Join(",", userNames);
            }
            return String.Empty;
        }

        public bool SaveUser(UserDto user)
        {
            var users = _DBContext.Set<UserExtention>();
            var roles = _DBContext.Set<Domain.Entities.Role>();

            var existing = users.Include(u => u.Roles).Include(u => u.Hospital).Include(u => u.Department).FirstOrDefault(u => u.UniqueID == user.UniqueID);
            if (existing == null)
            {
                if (user.DepartmentID == null)
                {
                    // set default department
                    var data = _RisProContext.Set<User2Domain>().FirstOrDefault(u => u.UniqueID == user.UniqueID);
                    if (data != null)
                    {
                        var name = data.Department;
                        if (!String.IsNullOrEmpty(name))
                        {
                            var department = _DBContext.Set<Department>().FirstOrDefault(d => d.Name == name);
                            if (department == null)
                            {
                                // add department
                                department = new Department()
                                {
                                    UniqueID = Guid.NewGuid().ToString(),
                                    Name = name
                                };
                                _DBContext.Set<Department>().Add(department);
                            }
                            user.DepartmentID = department.UniqueID;
                        }
                    }
                }
                //add
                var newUserExtention = Mapper.Map<UserDto, UserExtention>(user);
                newUserExtention.LastStatus = "ris.consultation.requests";
                users.Attach(newUserExtention);
                users.Add(newUserExtention);
                existing = newUserExtention;
            }
            else
            {
                Mapper.Map(user, existing);
            }

            if (user.IsMobileChanged)
            {
                UpdateUserMobile(user);
            }

            // update roles
            existing.Roles.Clear();
            user.Roles.Select(r => roles.Find(r.UniqueID)).Where(role => role != null).ForEach(existing.Roles.Add);

            _DBContext.SaveChanges();
            return true;
        }

        public bool UpdateUser(string userID, Dictionary<string, object> properties)
        {
            var user = _DBContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID == userID);
            if (user != null)
            {
                properties.Overwrite(user);
            }
            _DBContext.SaveChanges();
            return true;
        }

        public IEnumerable<HospitalProfileDto> GetHospitals(bool isCenter)
        {
            var rep = new HospitalProfileRepository(_DBContext);
            if (isCenter)
            {
                return
                    rep.Get(p => p.IsConsultation.HasValue && p.IsConsultation.Value.Equals(isCenter))
                        .Select(Mapper.Map<HospitalProfile, HospitalProfileDto>);
            }
            return rep.Get().Select(Mapper.Map<HospitalProfile, HospitalProfileDto>);
        }

        public UserDto GetUser(string userID)
        {
            var user = _RisProContext.Set<User>().FirstOrDefault(u => u.UniqueID == userID);
            if (user != null)
            {
                var userDto = Mapper.Map<User, UserDto>(user);
                userDto.RisRole = GetRisRole(user);
                var extention = _DBContext.Set<UserExtention>()
                                .Include(u => u.Roles)
                                .Include(u => u.Hospital)
                                .Include(u => u.Department)
                                .FirstOrDefault(u => u.UniqueID == user.UniqueID);
                if (extention != null)
                {
                    extention.Roles = extention.Roles.Where(r => !r.IsDeleted && r.Status).ToList();
                    Mapper.Map(extention, userDto);
                }
                return userDto;
            }
            return null;
        }

        private string GetRisRole(User user)
        {
            var defaultRoleName = "DefaultRoleName";
            var risDefaultRole = _RisProContext.Set<UserProfile>().FirstOrDefault(profile => profile.UserID == user.UniqueID && profile.Name == defaultRoleName && profile.Domain == user.Domain);
            if (risDefaultRole != null)
            {
                return risDefaultRole.Value;
            }

            var defaultRole = _RisProContext.Set<RoleToUser>().FirstOrDefault(l => l.UserID == user.UniqueID);
            if (defaultRole != null)
            {
                _RisProContext.Set<UserProfile>().Add(new UserProfile
                {
                    UniqueID = Guid.NewGuid().ToString(),
                    UserID = user.UniqueID,
                    Value = defaultRole.RoleName,
                    ModuleID = "0000",
                    Name = defaultRoleName,
                    Domain = user.Domain,
                    RoleName = "",//defaultRole.RoleName,
                    PropertyType = 0,
                    IsExportable = 0,
                    CanBeInherited = 0,
                    IsHidden = 0,
                    OrderingPos = "0",
                });
                _RisProContext.SaveChangesAsync();
                return defaultRole.RoleName;
            }
            return "";
        }

        public string GetUserMobile(string userID)
        {
            var userData = _RisProContext.Set<User2Domain>().FirstOrDefault(u => u.UniqueID == userID);
            if (userData != null)
            {
                return userData.Mobile;
            }
            return String.Empty;
        }

        public void UpdateUserMobile(UserDto user)
        {
            var userData = _RisProContext.Set<User2Domain>().FirstOrDefault(u => u.UniqueID == user.UniqueID);
            if (userData != null)
            {
                userData.Mobile = user.Mobile;
            }
            else
            {
                _RisProContext.Set<User2Domain>().Add(new User2Domain
                {
                    UniqueID = user.UniqueID,
                    Domain = user.Domain,
                    Mobile = user.Mobile
                });
            }
            _RisProContext.SaveChanges();
        }

        public bool SaveHospital(HospitalProfileDto hospitalDto)
        {
            hospitalDto.LastEditTime = DateTime.Now;
            hospitalDto.LastEditUser = _LoginUserService.CurrentUserID;
            var rep = new HospitalProfileRepository(_DBContext);
            var existing = rep.Get(h => h.UniqueID == hospitalDto.UniqueID).FirstOrDefault();
            if (existing == null)
            {
                rep.Add(Mapper.Map<HospitalProfileDto, HospitalProfile>(hospitalDto));
            }
            else
            {
                Mapper.Map(hospitalDto, existing);
                rep.Update(existing);
            }
            rep.SaveChanges();
            return true;
        }

        #endregion
    }
}