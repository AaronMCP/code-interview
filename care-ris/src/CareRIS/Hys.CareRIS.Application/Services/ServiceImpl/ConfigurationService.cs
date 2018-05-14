using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Hys.CrossCutting.Common.Utils;
using Hys.CrossCutting.Common.Interfaces;
using Kendo.DynamicLinq;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.EnterpriseLib;
using Newtonsoft.Json;
using System.Transactions;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ConfigurationService : DisposableServiceBase, IConfigurationService
    {


        #region Fields

        private IRisProContext _dbContext;

        #endregion

        #region Constructors

        public ConfigurationService(IRisProContext dbContext)
        {
            _dbContext = dbContext;

            AddDisposableObject(dbContext);
        }

        #endregion

        #region Methods

        #region Modality

        // get all modalities
        public async Task<IEnumerable<ModalityDto>> GetModalities()
        {
            var modalityEntities = await _dbContext.Set<Modality>().ToListAsync();
            var modalities = modalityEntities.Select(m => Mapper.Map<Modality, ModalityDto>(m));
            return modalities.ToList();
        }

        // get those global and site matches modalities
        public async Task<IEnumerable<ModalityDto>> GetModalities(string site)
        {
            var modalityEntities = await _dbContext.Set<Modality>()
                .Where(m => String.IsNullOrEmpty(m.Site) || m.Site.Equals(site, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            var modalities = modalityEntities.Select(m => Mapper.Map<Modality, ModalityDto>(m));
            return modalities.ToList();
        }

        // get those global and site matches modalities via modalityType
        public async Task<IEnumerable<ModalityDto>> GetModalities(string site, string modalityType)
        {
            var modalityEntities = await _dbContext.Set<Modality>()
                .Where(m => (
                        (string.IsNullOrEmpty(site) && string.IsNullOrEmpty(m.Site))
                        || (!string.IsNullOrEmpty(site) && m.Site == site))
                    && m.ModalityType == modalityType)
                .OrderBy(m => m.ModalityName)
                .ToListAsync();

            var modalities = modalityEntities.Select(m => Mapper.Map<Modality, ModalityDto>(m));
            return modalities;
        }


        #endregion

        #region Dictionary

        // get all dictionary data
        public IEnumerable<DictionaryDto> GetDictionaries()
        {
            var dictionaries = _dbContext.Set<Dictionary>().ToList()
                .Select(d => Mapper.Map<Dictionary, DictionaryDto>(d)).ToList();
            var groupedDVs = _dbContext.Set<DictionaryValue>().ToList()
                .Select(dv => Mapper.Map<DictionaryValue, DictionaryValueDto>(dv)).GroupBy(d => d.Tag).ToList();
            dictionaries.ForEach(d =>
            {
                var group = groupedDVs.FirstOrDefault(g => g.Key == d.Tag);
                if (group != null)
                {
                    d.Values = group.Select(g => g).ToList();
                }
            });

            return dictionaries;
        }

        // if tag relevant site dictinoary data exists, get site dictinoary, otherwise, get global
        public async Task<IEnumerable<DictionaryDto>> GetDictionaries(string site)
        {
            var dictionaries = (await _dbContext.Set<Dictionary>().ToListAsync())
                .Select(Mapper.Map<Dictionary, DictionaryDto>).ToList();
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = await (from dv in dicValues
                                              //let siteTags = from dValues in dicValues
                                              //               where dValues.Site.Equals(site, StringComparison.OrdinalIgnoreCase)
                                              //               select dValues.Tag
                                          where dv.Site.Equals(site, StringComparison.OrdinalIgnoreCase) || String.IsNullOrEmpty(dv.Site) //&& !siteTags.Contains(dv.Tag)
                                          select dv).OrderBy(p => p.Text).ToListAsync();
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).GroupBy(d => d.Tag).ToList();
            dictionaries.ForEach(d =>
            {
                var group = groupedDVs.FirstOrDefault(g => g.Key == d.Tag);
                if (group != null)
                {
                    d.Values = group.Select(g => g).ToList();
                }
            });
            return dictionaries;
        }

        // if tag relevant site dictinoary data exists, get site dictinoary, otherwise, get global
        //获取系统字典 使用
        public async Task<IDictionary<int, DictionaryDto>> GetDictionaries(string site, int[] tags)
        {
            var result = new Dictionary<int, DictionaryDto>();

            var dictionaries = (await _dbContext.Set<Dictionary>().Where(d => tags.Contains(d.Tag)).ToListAsync()).Select(Mapper.Map<Dictionary, DictionaryDto>).ToList();
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = await (from dv in dicValues
                                              //let siteTags = from dValues in dicValues
                                              //               where dValues.Site.Equals(site, StringComparison.OrdinalIgnoreCase)
                                              //               select dValues.Tag
                                          where (dv.Site.Equals(site, StringComparison.OrdinalIgnoreCase) || String.IsNullOrEmpty(dv.Site)) && tags.Contains(dv.Tag)
                                          select dv).OrderBy(p => p.Text).ToListAsync();
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).GroupBy(d => d.Tag).ToList();

            foreach (var d in dictionaries)
            {
                var group = groupedDVs.FirstOrDefault(g => g.Key == d.Tag);
                if (group != null)
                {
                    d.Values = group.Select(g => g).ToList();
                }
                result.Add(d.Tag, d);
            }

            return result;
        }
        public async Task<IEnumerable<DictionaryValueDto>> GetDictionariesList(string site, int[] tags)
        {
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = await dicValues.Where(d => (d.Site.Equals(site, StringComparison.OrdinalIgnoreCase) || String.IsNullOrEmpty(d.Site)) && tags.Contains(d.Tag)).OrderBy(p => p.Text).ToListAsync();
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).ToList();
            return groupedDVs;
        }
        #endregion

        #region Apply Dept & doctor

        public async Task<IEnumerable<ApplyDeptDto>> GetApplyDepts()
        {
            var depts = _dbContext.Set<ApplyDept>();
            var applyDepts = (await depts.ToListAsync()).Select(Mapper.Map<ApplyDept, ApplyDeptDto>);
            return applyDepts.ToList();
        }

        public async Task<IEnumerable<ApplyDeptLiteDto>> GetApplyDepts(string site)
        {
            var applyDepts = from d in _dbContext.Set<ApplyDept>()
                             where d.Site.Equals(site, StringComparison.OrdinalIgnoreCase)
                             select new
                             {
                                 d.UniqueID,
                                 d.DeptName,
                                 d.ShortcutCode
                             };

            // if the site has no data,return overall data
            if (!applyDepts.Any())
            {
                applyDepts = from d in _dbContext.Set<ApplyDept>()
                             where string.IsNullOrEmpty(d.Site)
                             select new
                             {
                                 d.UniqueID,
                                 d.DeptName,
                                 d.ShortcutCode
                             };
            }

            var deptList = await applyDepts.ToListAsync();
            var result = deptList.Select(d => new ApplyDeptLiteDto
            {
                UniqueID = d.UniqueID,
                DeptName = d.DeptName,
                ShortcutCode = d.ShortcutCode
            }).ToList();

            foreach (var applyDeptDto in result)
            {
                string name = applyDeptDto.DeptName.Trim();
                if (name != "")
                {
                    applyDeptDto.FirstPingYinName = ConvertFirstPY(name);
                }
                if (string.IsNullOrEmpty(applyDeptDto.ShortcutCode))
                {
                    applyDeptDto.ShortcutCode = applyDeptDto.FirstPingYinName;
                }
            }

            result = result.OrderBy(o => o.ShortcutCode).ToList();
            return result;
        }

        public string ConvertFirstPY(string name)
        {
            string newName = "";
            for (int i = 0; i < name.Length; i++)
            {
                string pingyinAll = PinyinUtil.ToPinyin(name.Substring(i, 1), false, 0, ""); ;

                if (pingyinAll.Length > 0)
                {
                    newName += pingyinAll.Substring(0, 1);
                }
            }

            return newName;
        }

        public async Task<IEnumerable<ApplyDoctorDto>> GetApplyDoctors()
        {
            var depts = _dbContext.Set<ApplyDept>();
            var doctors = _dbContext.Set<ApplyDoctor>();
            var result = await (from doc in doctors
                                select new ApplyDoctorDto
                                {
                                    UniqueID = doc.UniqueID,
                                    ApplyDeptID = doc.ApplyDeptID,
                                    Gender = doc.Gender,
                                    DoctorName = doc.DoctorName,
                                    ShortcutCode = doc.ShortcutCode,
                                    Mobile = doc.Mobile,
                                    Telephone = doc.Telephone,
                                    StaffNo = doc.StaffNo,
                                    Email = doc.Email,
                                    Site = doc.Site,
                                    Domain = doc.Domain,
                                    DeptName = depts.Where(d => d.UniqueID == doc.ApplyDeptID).FirstOrDefault().DeptName
                                }).ToListAsync();

            return result;
            //return (await _dbContext.Set<ApplyDoctor>().ToListAsync()).Select(Mapper.Map<ApplyDoctor, ApplyDoctorDto>);
        }

        public async Task<IEnumerable<ApplyDoctorLiteDto>> GetApplyDoctors(string site)
        {
            var applyDoctors = from d in _dbContext.Set<ApplyDoctor>()
                               where d.Site.Equals(site, StringComparison.OrdinalIgnoreCase)
                               select new
                               {
                                   d.UniqueID,
                                   d.DoctorName,
                                   d.ShortcutCode
                               };

            // if the site has no data,return overall data
            if (!applyDoctors.Any())
            {
                applyDoctors = from d in _dbContext.Set<ApplyDoctor>()
                               where String.IsNullOrEmpty(d.Site)
                               select new
                               {
                                   d.UniqueID,
                                   d.DoctorName,
                                   d.ShortcutCode
                               };
            }

            var doctorList = await applyDoctors.ToListAsync();

            var result = doctorList.Select(d =>
                new ApplyDoctorLiteDto
                {
                    UniqueID = d.UniqueID,
                    DoctorName = d.DoctorName,
                    ShortcutCode = d.ShortcutCode
                }).ToList();

            foreach (var applyDoctorDto in result)
            {
                string name = applyDoctorDto.DoctorName.Trim();
                if (name != "")
                {
                    applyDoctorDto.FirstPingYinName = ConvertFirstPY(name);
                }

                if (string.IsNullOrEmpty(applyDoctorDto.ShortcutCode))
                {
                    applyDoctorDto.ShortcutCode = applyDoctorDto.FirstPingYinName;
                }
            }

            return result.OrderBy(o => o.ShortcutCode);
        }

        #endregion

        #region Profiles

        public async Task<IEnumerable<SystemProfileDto>> GetSystemProfiles(string[] profileNames = null)
        {
            var profiles = from p in _dbContext.Set<SystemProfile>()
                           select new
                           {
                               p.UniqueID,
                               p.Name,
                               p.Value
                           };

            if (profileNames != null && profileNames.Any())
            {
                profiles = profiles.Where(p => profileNames.Contains(p.Name));
            }
            var profileEntityList = await profiles.ToListAsync();
            var result = profileEntityList.Select(p => new SystemProfileDto { Name = p.Name, Value = p.Value }).ToList();
            return result;
        }

        public async Task<IEnumerable<SiteProfileDto>> GetSiteProfiles(string site, string[] profileNames = null)
        {
            var profiles = from p in _dbContext.Set<SiteProfile>()
                           where p.Site.Equals(site, StringComparison.OrdinalIgnoreCase)
                           select new
                           {
                               p.UniqueID,
                               p.Name,
                               p.Value
                           };

            if (profileNames != null && profileNames.Any())
            {
                profiles = profiles.Where(p => profileNames.Contains(p.Name));
            }

            var profilesDtoList = await profiles.ToListAsync();
            return profilesDtoList.Select(p => new SiteProfileDto { Name = p.Name, Value = p.Value }).ToList();
        }

        public async Task<IEnumerable<RoleProfileDto>> GetRoleProfiles(string roleName, string site, string[] profileNames = null)
        {
            var profiles = from p in _dbContext.Set<RoleProfile>()
                           where p.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase)
                           select new
                           {
                               p.UniqueID,
                               p.Name,
                               p.Value
                           };

            if (profileNames != null && profileNames.Any())
            {
                profiles = profiles.Where(p => profileNames.Contains(p.Name));
            }

            var profilesDtoList = (await profiles.ToListAsync())
                .Select(p => new RoleProfileDto { Name = p.Name, Value = p.Value }).ToList();

            // Special case for "AccessSite"
            if (profileNames != null && profileNames.Contains(Contants.ProfileKey.AccessSite))
            {
                var accessSites = await (from p in _dbContext.Set<RoleProfile>()
                                         where p.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase) && p.Name.Equals(site, StringComparison.OrdinalIgnoreCase)
                                         select p).FirstOrDefaultAsync();

                if (accessSites != null)
                {
                    accessSites.Name = Contants.ProfileKey.AccessSite;
                    profilesDtoList.Add(new RoleProfileDto { Name = accessSites.Name, Value = accessSites.Value });
                }
            }

            return profilesDtoList;
        }

        public async Task<IEnumerable<UserProfileDto>> GetUserProfiles(string userID, string[] profileNames = null)
        {
            var profiles = from p in _dbContext.Set<UserProfile>()
                           where p.UserID.Equals(userID, StringComparison.OrdinalIgnoreCase)
                           select new
                           {
                               p.UniqueID,
                               p.Name,
                               p.Value
                           };

            if (profileNames != null && profileNames.Any())
            {
                profiles = profiles.Where(p => profileNames.Contains(p.Name));
            }

            var profilesDtoList = (await profiles.ToListAsync())
                .Select(p => new UserProfileDto { Name = p.Name, Value = p.Value }).ToList();
            return profilesDtoList;
        }

        public bool SaveUserProfiles(UserProfileDto userProfileDto)
        {
            try
            {
                var profile = _dbContext.Set<UserProfile>().FirstOrDefault(p => p.UserID == userProfileDto.UserID && p.Name == userProfileDto.Name);
                if (profile != null)
                {
                    profile.Value = userProfileDto.Value;
                }
                else
                {
                    UserProfile userProfile = Mapper.Map<UserProfileDto, UserProfile>(userProfileDto);
                    userProfile.UniqueID = Guid.NewGuid().ToString();
                    userProfile.ModuleID = "0000";
                    userProfile.RoleName = "";
                    userProfile.PropertyType = 0;
                    userProfile.IsExportable = 0;
                    userProfile.CanBeInherited = 0;
                    userProfile.IsHidden = 0;
                    userProfile.OrderingPos = "0";

                    _dbContext.Set<UserProfile>().Add(userProfile);
                }

                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public SiteDto GetSite(string siteName)
        {
            var siteDto = _dbContext.Set<Site>().Where(p => p.SiteName.Equals(siteName, StringComparison.OrdinalIgnoreCase))
                .ToList().Select(d => Mapper.Map<Site, SiteDto>(d)).FirstOrDefault();
            return siteDto;
        }

        public async Task<ProfilesDto> GetProfiles(string userID, string site, string role)
        {
            var systemProfile = await GetSystemProfiles();
            var siteProfile = await GetSiteProfiles(site);
            var roleProfile = await GetRoleProfiles(role, site);
            var userProfile = await GetUserProfiles(userID);
            var result = new ProfilesDto
            {
                SystemProiles = systemProfile,
                SiteProiles = siteProfile,
                RoleProiles = roleProfile,
                UserProiles = userProfile
            };

            return result;
        }

        public async Task<Dictionary<string, IEnumerable<ProfileLiteDto>>> GetProfiles(string userID, string site, string role, string[] profileNames)
        {
            if (profileNames == null)
            {
                throw new ArgumentNullException("profileNames");
            }
            // user profile maybe modified!?

            var systemProfile = await GetSystemProfiles(profileNames);
            var siteProfile = await GetSiteProfiles(site, profileNames);
            var roleProfile = await GetRoleProfiles(role, site, profileNames);
            var userProfile = await GetUserProfiles(userID, profileNames);

            var result = new Dictionary<string, IEnumerable<ProfileLiteDto>>();

            foreach (var name in profileNames)
            {
                if (userProfile.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) && !userProfile.All(p => String.IsNullOrEmpty(p.Value)))
                {
                    result.Add(name.ToLower(), userProfile.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .Select(p => new ProfileLiteDto { UniqueID = p.UniqueID, Name = p.Name, Value = p.Value }).ToList());
                }
                else if (roleProfile.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) && !roleProfile.All(p => String.IsNullOrEmpty(p.Value)))
                {
                    result.Add(name.ToLower(), roleProfile.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .Select(p => new ProfileLiteDto { UniqueID = p.UniqueID, Name = p.Name, Value = p.Value }).ToList());
                }
                else if (siteProfile.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) && !siteProfile.All(p => String.IsNullOrEmpty(p.Value)))
                {
                    result.Add(name.ToLower(), siteProfile.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .Select(p => new ProfileLiteDto { UniqueID = p.UniqueID, Name = p.Name, Value = p.Value }).ToList());
                }
                else if (systemProfile.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) && !systemProfile.All(p => String.IsNullOrEmpty(p.Value)))
                {
                    result.Add(name.ToLower(), systemProfile.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .Select(p => new ProfileLiteDto { UniqueID = p.UniqueID, Name = p.Name, Value = p.Value }).ToList());
                }
                else
                {
                    result.Add(name.ToLower(), new List<ProfileLiteDto>());
                }
            }

            return result;
        }

        public async Task<IEnumerable<ProfileLiteDto>> GetProfile(string userID, string site, string role, string profileName)
        {
            var profiles = await GetProfiles(userID, site, role, new string[] { profileName });
            return profiles.First().Value;
        }


        #endregion

        #region ClientConfig
        public ClientConfigDto GetClientConfig(string id)
        {
            var config = _dbContext.Set<ClientConfig>().FirstOrDefault(c => c.UniqueID == id);
            if (config == null)
            {
                config = new ClientConfig
                {
                    UniqueID = id,
                    ScanQualityLevel = -1,
                    IntegrationType = 2,
                };

                _dbContext.Set<ClientConfig>().Add(config);
                _dbContext.SaveChanges();
            }
            return Mapper.Map<ClientConfig, ClientConfigDto>(config);
        }

        public bool SaveClientConfig(ClientConfigDto config)
        {
            var existingConfig = _dbContext.Set<ClientConfig>().FirstOrDefault(c => c.UniqueID == config.UniqueID);
            if (existingConfig != null)
            {
                Mapper.Map(config, existingConfig);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #endregion

        public IEnumerable<SiteDto> GetAllSites()
        {
            var allSites = _dbContext.Set<Site>().Where(s => true);
            var allSitesDto = Mapper.Map<IEnumerable<Site>, IEnumerable<SiteDto>>(allSites);
            return allSitesDto;
        }

        public async Task<List<string>> GetUserSites(string userID, string site, string role)
        {
            var userSites = new List<string>();
            var belongToSiteProfile = (await GetProfile(userID, site, role, Contants.ProfileKey.BelongToSite)).FirstOrDefault();

            if (belongToSiteProfile != null && !String.IsNullOrEmpty(belongToSiteProfile.Value))
            {
                userSites = belongToSiteProfile.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            }
            var accessSitesProfile = (await GetProfile(userID, site, role, Contants.ProfileKey.AccessSite)).FirstOrDefault();

            if (accessSitesProfile != null && !String.IsNullOrEmpty(accessSitesProfile.Value))
            {
                var sites = accessSitesProfile.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                userSites = userSites.Union(sites).ToList<string>();
            }
            if (userSites.Count() > 0)
            {
                return userSites;
            }
            return new List<string>();
        }
        #region ICDTen疾病编码


        /// <summary>
        /// 疾病分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataSourceResult SearchICD(DataSourceRequest request)
        {
            if (request == null)
            {
                return null;
            }
            var query = _dbContext.Set<ICDTen>().
                Select(p => new ICDTenDto
                {
                    ID = p.ID,
                    Name = p.Name,
                    PY = p.PY,
                    WB = p.WB,
                    Memo = p.Memo,
                    Domain = p.Domain,
                    UniqueId = p.UniqueID
                });
            return query.ToDataSourceResult(request);
        }
        /// <summary>
        /// 下拉框分页
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public SearchResultDto SearchICDs(SearchCriteriaDto criteria, string domain)
        {
            if (criteria == null || criteria.Pagination == null)
            {
                return null;
            }

            var dbquery = _dbContext.Set<ICDTen>().Where(i => i.Domain == domain || string.IsNullOrEmpty(domain));
            var query = dbquery.Where(i => i.ID.Contains(criteria.Code) || i.WB.Contains(criteria.Code) || i.Name.Contains(criteria.Code) || i.PY.Contains(criteria.Code)).
                Select(p => new ICDTenDto
                {
                    ID = p.ID,
                    Name = p.Name,
                    UniqueId = p.UniqueID
                }).ToList();

            var getTwoPagesItemsTask = !criteria.Pagination.NeedNoPagination ?
              query.OrderByDescending(q => q.ID).Skip(criteria.Pagination.PageSize * (criteria.Pagination.PageIndex - 1)).Take(criteria.Pagination.PageSize * 2).ToList() :
              query.OrderByDescending(q => q.ID).ToList();
            var twoPagesItems = getTwoPagesItemsTask;

            var firstPageItems = twoPagesItems.Take(criteria.Pagination.PageSize);
            var result = new SearchResultDto
            {
                Pagination = new PaginationDto
                {
                    PageIndex = criteria.Pagination.PageIndex,
                    PageSize = criteria.Pagination.PageSize,
                    HasNextPage = twoPagesItems.Count > criteria.Pagination.PageSize
                },
                Codes = firstPageItems
            };
            return result;
        }
        /// <summary>
        /// 查询所有疾病
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ICDTenDto>> GetAlICD()
        {
            var query = _dbContext.Set<ICDTen>().Take(1000);
            var data = Mapper.Map<IEnumerable<ICDTen>, IEnumerable<ICDTenDto>>(query);
            return data;
        }
        /// <summary>
        /// 新增疾病
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        public bool SaveICD(ICDTenDto icdInfo, string domain)
        {
            if (_dbContext.Set<ICDTen>().FirstOrDefault(i => i.ID.Trim() == icdInfo.ID.Trim()) != null)
            {
                return false;
            }
            else
            {
                ICDTen icd = Mapper.Map<ICDTenDto, ICDTen>(icdInfo);
                icd.UniqueID = Guid.NewGuid().ToString();

                icd.TJM = string.IsNullOrEmpty(icdInfo.TJM) ? "161" : icdInfo.TJM;//？？
                icd.Domain = string.IsNullOrEmpty(icdInfo.Domain) ? domain : icdInfo.Domain;//

                _dbContext.Set<ICDTen>().Add(icd);
                _dbContext.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 更具编号获取疾病
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICDTenDto GetICDByID(string id)
        {
            var icd = _dbContext.Set<ICDTen>().FirstOrDefault(i => i.ID.Trim() == id.Trim());
            if (icd != null)
            {
                var icdDto = Mapper.Map<ICDTen, ICDTenDto>(icd);
                return icdDto;
            }
            return null;
        }

        /// <summary>
        /// 修改疾病
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        public bool UpdateICD(ICDTenDto icdInfo)
        {

            var query = _dbContext.Set<ICDTen>();
            var icd = query.Where(i => i.UniqueID.Trim() == icdInfo.UniqueId.Trim()).FirstOrDefault();
            if (icd != null)
            {
                icd.Name = icdInfo.Name;
                icd.PY = icdInfo.PY;
                icd.WB = icdInfo.WB;
                icd.Memo = icdInfo.Memo;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除疾病 UniqueID
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DelICD(ICDTenDto icdInfo)
        {
            try
            {
                if (icdInfo != null)
                {
                    var icd = _dbContext.Set<ICDTen>().Where(i => i.UniqueID == icdInfo.UniqueId).FirstOrDefault();
                    if (icd != null)
                    {
                        _dbContext.Set<ICDTen>().Remove(icd);
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 检查代码
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public DataSourceResult SearchProcedureCodeList(DataSourceRequest request)
        {
            if (request == null)
            {
                return null;
            }
            var bquery = _dbContext.Set<BodySystemMap>();
            var query = _dbContext.Set<Procedurecode>()
                .Select(d => new ProcedureCodeDto
                {
                    UniqueId = d.UniqueID,
                    ProcedureCode = d.ProcedureCode,
                    Description = d.Description,
                    EnglishDescription = d.EnglishDescription,
                    ModalityType = d.ModalityType,
                    BodyPart = d.BodyPart,
                    CheckingItem = d.CheckingItem,
                    Charge = d.Charge,
                    Preparation = d.Preparation,
                    Frequency = d.Frequency,
                    BodyCategory = d.BodyCategory,
                    Duration = d.Duration,
                    FilmSpec = d.FilmSpec,
                    FilmCount = d.FilmCount,
                    ContrastName = d.ContrastName,
                    ContrastDose = d.ContrastDose,
                    ImageCount = d.ImageCount,
                    ExposalCount = d.ExposalCount,
                    BookingNotice = d.BookingNotice,
                    ShortcutCode = d.ShortcutCode,
                    Enhance = d.Enhance,
                    Effective = d.Effective,
                    Domain = d.Domain,
                    BodypartFrequency = d.BodypartFrequency,
                    CheckingItemFrequency = d.CheckingItemFrequency,
                    TechnicianWeight = d.TechnicianWeight,
                    RadiologistWeight = d.RadiologistWeight,
                    ApprovedRadiologistWeight = d.ApprovedRadiologistWeight,
                    DefaultModality = d.DefaultModality,
                    Site = d.Site,
                    Puncture = d.Puncture,
                    Radiography = d.Radiography,
                    ExamSystem = bquery.Where(b => b.ModalityType == b.ModalityType && b.BodyPart == b.BodyPart).FirstOrDefault() != null ? bquery.Where(b => b.ModalityType == b.ModalityType && b.BodyPart == b.BodyPart).FirstOrDefault().ExamSystem : ""
                });
            return query.ToDataSourceResult(request);
        }

        /// <summary>
        /// 获取所有检查
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProcedureCodeDto> GetAllProcedureCode()
        {

            var query = _dbContext.Set<Procedurecode>();
            var data = Mapper.Map<IEnumerable<Procedurecode>, IEnumerable<ProcedureCodeDto>>(query);
            return data;
        }
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModalityTypeDto> GetAllModalityType()
        {

            var query = _dbContext.Set<ModalityType>();
            var data = Mapper.Map<IEnumerable<ModalityType>, IEnumerable<ModalityTypeDto>>(query);
            return data;
        }
        /// <summary>
        /// 根据设备类型获默认设备
        /// </summary>
        /// <param name="cType"></param>
        /// <returns></returns>
        public IEnumerable<ModalityDto> GetModality(string cType)
        {

            var query = _dbContext.Set<Modality>().Where(c => c.ModalityType == cType);
            var data = Mapper.Map<IEnumerable<Modality>, IEnumerable<ModalityDto>>(query);
            return data;
        }

        /// <summary>
        /// 新增检查
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        public bool SaveProcedureCode(ProcedureCodeDto pcInfo, string site, string domain)
        {
            var query = _dbContext.Set<Procedurecode>();
            if (query.FirstOrDefault(p => p.ProcedureCode == pcInfo.ProcedureCode) != null)
            {
                return false;
            }
            else
            {
                pcInfo.ApprovedRadiologistWeight = pcInfo.ApprovedRadiologistWeight < 1 ? 1 : pcInfo.ApprovedRadiologistWeight;
                pcInfo.RadiologistWeight = pcInfo.RadiologistWeight < 1 ? 1 : pcInfo.RadiologistWeight;
                pcInfo.TechnicianWeight = pcInfo.TechnicianWeight < 1 ? 1 : pcInfo.TechnicianWeight;
                Procedurecode pc = Mapper.Map<ProcedureCodeDto, Procedurecode>(pcInfo);
                pc.UniqueID = Guid.NewGuid().ToString();
                //pc.Site = site;
                pc.Domain = domain;
                query.Add(pc);
                _dbContext.SaveChanges();
                return true;
            }
        }


        public ProcedureCodeDto GetProcedureFrequency(ProcedureCodeDto pcInfo)
        {
            var pf = _dbContext.Set<Procedurecode>().Where(p => p.ModalityType == pcInfo.ModalityType && p.BodyCategory == pcInfo.BodyCategory).FirstOrDefault();
            if (pf == null)
            {
                pcInfo.Frequency = 0;
            }
            else
            {
                pcInfo.Frequency = pf.Frequency;
            }
            var pbcf = _dbContext.Set<Procedurecode>().Where(p => p.ModalityType == pcInfo.ModalityType && p.BodyCategory == pcInfo.BodyCategory && p.BodyPart == pcInfo.BodyPart).FirstOrDefault();
            if (pbcf == null)
            {
                pcInfo.BodypartFrequency = 0;
            }
            else
            {
                pcInfo.BodypartFrequency = pbcf.BodypartFrequency;
            }
            var pcif = _dbContext.Set<Procedurecode>().Where(p => p.ModalityType == pcInfo.ModalityType && p.BodyCategory == pcInfo.BodyCategory && p.BodyPart == pcInfo.BodyPart && p.CheckingItem == pcInfo.CheckingItem).FirstOrDefault();
            if (pcif == null)
            {
                pcInfo.CheckingItemFrequency = 0;
            }
            else
            {
                pcInfo.CheckingItemFrequency = pcif.CheckingItemFrequency;
            }

            return pcInfo;
        }
        /// <summary>
        /// 修改检查
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        public bool UpdateProcedureCode(ProcedureCodeDto pcInfo)
        {
            if (pcInfo == null)
            {
                return false;
            }
            var pc = _dbContext.Set<Procedurecode>().FirstOrDefault(i => i.UniqueID == pcInfo.UniqueId);

            if (pc != null)
            {
                pc.Description = pcInfo.Description;
                pc.ModalityType = pcInfo.ModalityType;
                pc.BodyPart = pcInfo.BodyPart;
                pc.CheckingItem = pcInfo.CheckingItem;
                pc.BodyCategory = pcInfo.BodyCategory;
                pc.FilmSpec = pcInfo.FilmSpec;//胶片规格
                pc.FilmCount = pcInfo.FilmCount;
                pc.ContrastName = pcInfo.ContrastName;
                pc.ContrastDose = pcInfo.ContrastDose;
                pc.ImageCount = pcInfo.ImageCount;
                pc.ExposalCount = pcInfo.ExposalCount;
                pc.BookingNotice = pcInfo.BookingNotice;
                pc.DefaultModality = pcInfo.DefaultModality;

                pc.Duration = pcInfo.Duration;//持续检查
                pc.Enhance = pcInfo.Enhance;
                pc.Effective = pcInfo.Effective;
                pc.Charge = pcInfo.Charge;//费用
                pc.TechnicianWeight = pcInfo.TechnicianWeight;

                pc.CheckingItemFrequency = pcInfo.CheckingItemFrequency;
                pc.RadiologistWeight = pcInfo.RadiologistWeight;
                pc.ApprovedRadiologistWeight = pcInfo.ApprovedRadiologistWeight;
                pc.ShortcutCode = pcInfo.ShortcutCode;
                pc.Preparation = pcInfo.Preparation;
                pc.EnglishDescription = pcInfo.EnglishDescription;
                pc.Frequency = pcInfo.Frequency;
                pc.Puncture = pcInfo.Puncture;
                pc.Radiography = pcInfo.Radiography;
                pc.BodypartFrequency = pcInfo.BodypartFrequency;//部位频率
                //pc.Site = pcInfo.Site;
                // pc.ClinicalModality = pcInfo.ClinicalModality;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改检查频率单个
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFrequency(ProcedureCodeDto model)
        {
            if (model == null)
            {
                return false;
            }
            List<string> sqlList = new List<string>(3);
            if (!string.IsNullOrEmpty(model.CheckingItem))
            {
                sqlList.Add(string.Format("update tbProcedureCode set CheckingItemFrequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}' and BodyPart = '{3}' and CheckingItem = '{4}'", model.CheckingItemFrequency, model.ModalityType, model.BodyCategory, model.BodyPart, model.CheckingItem));
            }
            else if (!string.IsNullOrEmpty(model.BodyPart))
            {
                sqlList.Add(string.Format("update tbProcedureCode set BodyPartFrequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}' and BodyPart = '{3}'", model.BodypartFrequency, model.ModalityType, model.BodyCategory, model.BodyPart));
            }
            else if (!string.IsNullOrEmpty(model.BodyCategory))
            {
                sqlList.Add(string.Format("update tbProcedureCode set Frequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}'", model.Frequency, model.ModalityType, model.BodyCategory));
            }
            foreach (string sql in sqlList)
            {
                _dbContext.ExecuteSqlCommand(sql);
            }
            return true;
        }
        public bool UpdateFrequencyList(List<ProcedureCodeDto> models)
        {
            if (models == null || models.Count < 1)
            {
                return false;
            }
            var query = _dbContext.Set<Procedurecode>();
            foreach (var model in models)
            {
                //检查项目频率
                if (!string.IsNullOrEmpty(model.CheckingItem))
                {
                    var CheckingItems = query.Where(p => p.ModalityType == model.ModalityType && p.BodyCategory == model.BodyCategory && p.BodyPart == model.BodyPart && p.CheckingItem == model.CheckingItem).ToList();
                    if (CheckingItems.Count > 0)
                    {
                        foreach (var pro in CheckingItems)
                        {
                            pro.CheckingItemFrequency = model.CheckingItemFrequency;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(model.BodyPart))//检查部位频率
                {
                    var BodyParts = query.Where(p => p.ModalityType == model.ModalityType && p.BodyCategory == model.BodyCategory && p.BodyPart == model.BodyPart).ToList();
                    if (BodyParts.Count > 0)
                    {
                        foreach (var pro in BodyParts)
                        {
                            pro.BodypartFrequency = model.BodypartFrequency;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(model.BodyCategory))//检查分类频率
                {
                    var BodyCategories = query.Where(p => p.ModalityType == model.ModalityType && p.BodyCategory == model.BodyCategory).ToList();
                    if (BodyCategories.Count > 0)
                    {
                        foreach (var pro in BodyCategories)
                        {
                            pro.Frequency = model.Frequency;
                        }
                    }
                }
            }
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        /// <summary>
        /// 删除疾病 UniqueID
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DelProcedureCode(ProcedureCodeDto pcInfo)
        {
            try
            {
                if (pcInfo != null)
                {
                    var icd = _dbContext.Set<Procedurecode>().Where(i => i.UniqueID == pcInfo.UniqueId).FirstOrDefault();
                    if (icd != null)
                    {
                        _dbContext.Set<Procedurecode>().Remove(icd);
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        ///新增检查部位
        /// </summary>
        /// <param name="bsmInfo"></param>
        /// <returns></returns>
        public bool AddBodySystemMap(BodySystemMapDto bsmInfo)
        {
            var query = _dbContext.Set<BodySystemMap>();
            if (query.FirstOrDefault(b => b.ModalityType == bsmInfo.ModalityType && b.BodyPart == bsmInfo.BodyPart && b.ExamSystem == bsmInfo.ExamSystem) != null)
            {
                throw new Exception("Duplicated Body System Map");
            }
            BodySystemMap bsm = Mapper.Map<BodySystemMapDto, BodySystemMap>(bsmInfo);
            bsm.UniqueID = Guid.NewGuid().ToString();
            //Domain,site 在Controoler set
            query.Add(bsm);
            _dbContext.SaveChanges();
            return true;
        }

        public BodySystemMapDto GetBodySystemMap(BodySystemMapDto bsmInfo)
        {
            var query = _dbContext.Set<BodySystemMap>();
            BodySystemMap bsm = query.FirstOrDefault(b => b.ModalityType == bsmInfo.ModalityType && b.BodyPart == bsmInfo.BodyPart);
            if (bsm != null)
            {
                return Mapper.Map<BodySystemMap, BodySystemMapDto>(bsm);
            }
            return null;
        }
        /// <summary>
        /// true:存在
        /// </summary>
        /// <param name="bsmInfo"></param>
        /// <returns></returns>
        public bool IsBodyPartExist(BodySystemMapDto bsmInfo)
        {
            var query = _dbContext.Set<BodySystemMap>();
            var bsm = query.FirstOrDefault(b => b.ModalityType == bsmInfo.ModalityType && b.BodyPart == bsmInfo.BodyPart);
            if (bsm != null)
            {
                return false;
            }
            return true;
        }




        #endregion


        #region  角色管理
        /// <summary>
        /// 获取角色管理节点
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleDirDto>> GetRoleNodes(string domain)
        {

            var query = await _dbContext.Set<RoleDir>().Where(r => r.Domain == domain || string.IsNullOrEmpty(domain)).OrderBy(r => r.Name).ToListAsync();
            var roles = _dbContext.Set<Role>();
            var result = query.Select(p => Mapper.Map<RoleDir, RoleDirDto>(p));
            foreach (var rd in result)
            {
                if (!string.IsNullOrEmpty(rd.RoleID))
                {
                    var role = roles.Where(r => r.UniqueID == rd.RoleID).FirstOrDefault();

                    rd.RoleName = role.RoleName;
                    rd.Description = role.Description;
                    rd.IsSystem = role.IsSystem.HasValue ? (bool?)(role.IsSystem.Value == 1) : null;
                }
            }
            return result.ToList();
        }

        /// <summary>
        /// 获取角色配置信息post
        /// roleInfo.RoleName,roleInfo.Domain
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public string RoleProfile(RoleDto roleInfo, string userId)
        {
            if (roleInfo == null)
            {
                return null;
            }
            var role = _dbContext.Set<Role>().Where(r => r.Domain == roleInfo.Domain && r.RoleName == roleInfo.RoleName).FirstOrDefault();
            if (role == null)
            {
                return null;
            }
            bool isSiteAdmin = false;

            var siteAdmin = _dbContext.Set<Role>().Where(r => r.Domain == roleInfo.Domain && r.RoleName == "SiteAdmin").FirstOrDefault();
            if (siteAdmin != null)
            {
                if (siteAdmin.UniqueID == role.UniqueID)
                {
                    isSiteAdmin = true;
                }
            }
            var reportDbService = new ReportDBService();
            var ds = reportDbService.GetRoleProfDetDataSet(roleInfo.RoleName, roleInfo.Domain, userId, isSiteAdmin);
            string json = JsonConvert.SerializeObject(ds);
            return json;
        }

        public string GetRoleProfile(string roleName, string domain, string userId)
        {
            bool isSiteAdmin = false;
            if (roleName.ToUpper() == "SITEADMIN")
            {
                isSiteAdmin = true;
            }
            var reportDbService = new ReportDBService();
            var ds = reportDbService.GetRoleProfDetDataSet(roleName, domain, userId, isSiteAdmin);
            string json = JsonConvert.SerializeObject(ds);

            return json;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public bool AddRole(RoleDto roleInfo)
        {
            if (roleInfo == null)
            {
                return false;
            }
            var query = _dbContext.Set<Role>();
            var queryDir = _dbContext.Set<RoleDir>();
            var querySys = _dbContext.Set<SystemProfile>();
            string strRole = string.Empty;
            //不是拷贝
            if (!roleInfo.IsCopy)
            {
                var roleNameExist = query.Where(r => r.RoleName == roleInfo.RoleName && r.Domain == roleInfo.Domain).FirstOrDefault();
                var NameExist = queryDir.Where(r => r.Name == roleInfo.RoleName && r.Domain == roleInfo.Domain).FirstOrDefault();
                if (roleNameExist != null || NameExist != null)
                {
                    return false;
                }
                //role
                var role = Mapper.Map<RoleDto, Role>(roleInfo);
                role.IsSystem = 0;
                role.UniqueID = Guid.NewGuid().ToString();
                query.Add(role);

                //RoleProfile
                var sysProfiles = querySys.Where(s => s.CanBeInherited > 0 && s.Domain == roleInfo.Domain).ToList();
                if (sysProfiles != null && sysProfiles.Count() > 0)
                {
                    var queryRole = _dbContext.Set<RoleProfile>();
                    //站点
                    if (!string.IsNullOrEmpty(roleInfo.Site))
                    {
                        var cates = sysProfiles.Where(s => s.Name == "QueryCategories");
                        foreach (var cat in cates)
                        {
                            cat.PropertyOptions = string.Format("select QueryName from tQuery where len(ID)>0 and (Site = '' or Site ='{0}')", roleInfo.Site);
                        }
                        var descs = sysProfiles.Where(s => s.PropertyDesc == "Access site data");
                        foreach (var desc in descs)
                        {
                            desc.PropertyOptions = string.Format("if exists(select 1 from tbRoleProfile where 1=1 and Domain in (select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain'))else(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tbRoleProfile where Domain in(select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", desc.Name.Trim());
                            desc.Value = Convert.ToString(desc.Name).Trim();
                        }
                        var mDescs = sysProfiles.Where(s => s.PropertyDesc == "IM can access site");
                        foreach (var md in mDescs)
                        {
                            md.PropertyOptions = string.Format("if exists(select 1 from tbRoleProfile where 1=1 and Domain in (select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain'))else(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tbRoleProfile where Domain in(select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", md.Name.Trim());
                            md.Value = Convert.ToString(md.Name).Trim().Replace("IM_", "");
                        }
                    }
                    var sysInherited = 0;
                    //系统配置转换角色配置
                    foreach (var sp in sysProfiles)
                    {
                        if (!sp.PropertyOptions.Contains("|") || sp.PropertyOptions.Contains("'|'"))
                        {
                            //sp.PropertyOptions = sp.PropertyOptions.Replace("'", "''");
                        }
                        sysInherited = sp.CanBeInherited - 1;

                        var roleProfile = new RoleProfile();
                        roleProfile.RoleName = roleInfo.RoleName;
                        roleProfile.Name = sp.Name;
                        roleProfile.ModuleID = sp.ModuleID;
                        roleProfile.Value = sp.Value;
                        roleProfile.IsExportable = sp.IsExportable;
                        roleProfile.PropertyDesc = sp.PropertyDesc;
                        roleProfile.PropertyOptions = sp.PropertyOptions;
                        roleProfile.CanBeInherited = sysInherited;
                        roleProfile.PropertyType = sp.PropertyType;
                        roleProfile.IsHidden = sp.IsHidden;
                        roleProfile.OrderingPos = sp.OrderingPos;
                        roleProfile.Domain = roleInfo.Domain;
                        roleProfile.UniqueID = Guid.NewGuid().ToString();
                        queryRole.Add(roleProfile);
                    }
                }
                //RoleDir
                var roleDir = new RoleDir();
                roleDir.UniqueID = Guid.NewGuid().ToString();
                roleDir.Name = roleInfo.RoleName;
                roleDir.ParentID = roleInfo.ParentID;
                roleDir.Leaf = 1;
                roleDir.OrderID = 0;
                roleDir.RoleID = role.UniqueID;
                roleDir.Domain = roleInfo.Domain;
                queryDir.Add(roleDir);
            }
            else//拷贝
            {
                var roleNameExist = query.Where(r => r.RoleName == roleInfo.CopyRoleName && r.Domain == roleInfo.Domain).FirstOrDefault();

                var NameExist = queryDir.Where(r => r.Name == roleInfo.CopyRoleName && r.Domain == roleInfo.Domain).FirstOrDefault();
                if (roleNameExist != null || NameExist != null)
                {
                    return false;
                }
                //role
                var role = Mapper.Map<RoleDto, Role>(roleInfo);
                role.RoleName = roleInfo.CopyRoleName;
                role.IsSystem = 0;
                role.UniqueID = Guid.NewGuid().ToString();
                query.Add(role);
                //RoleProfile 被复制角色的配置信息
                var rolesProfiles = _dbContext.Set<RoleProfile>().Where(r => r.RoleName == roleInfo.RoleName && r.Domain == roleInfo.Domain);
                if (rolesProfiles != null && rolesProfiles.Count() > 0)
                {
                    var queryRole = _dbContext.Set<RoleProfile>();
                    //站点
                    if (!string.IsNullOrEmpty("roleInfo.Site"))
                    {
                        var cates = rolesProfiles.Where(s => s.Name == "QueryCategories");
                        foreach (var cat in cates)
                        {
                            cat.PropertyOptions = string.Format("select QueryName from tQuery where len(ID)>0 and (Site = '' or Site ='{0}')", roleInfo.Site);
                        }
                        var descs = rolesProfiles.Where(s => s.PropertyDesc == "Access site data");
                        foreach (var desc in descs)
                        {
                            desc.PropertyOptions = string.Format("if exists(select 1 from tbRoleProfile where 1=1 and Domain in (select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain'))else(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tbRoleProfile where Domain in(select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", desc.Name.Trim());
                            desc.Value = Convert.ToString(desc.Name).Trim();
                        }
                        var mDescs = rolesProfiles.Where(s => s.PropertyDesc == "IM can access site");
                        foreach (var md in mDescs)
                        {
                            md.PropertyOptions = string.Format("if exists(select 1 from tbRoleProfile where 1=1 and Domain in (select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain'))else(select site value,alias text from tbSiteList where Domain in(select Value from tbSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tbRoleProfile where Domain in(select Value from tbSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", md.Name.Trim());
                            md.Value = Convert.ToString(md.Name).Trim().Replace("IM_", "");
                        }

                    }
                    //角色配置copy
                    foreach (var rp in rolesProfiles)
                    {
                        if (!rp.PropertyOptions.Contains("|") || rp.PropertyOptions.Contains("'|'"))
                        {
                            // rp.PropertyOptions = rp.PropertyOptions.Replace("'", "''");
                        }
                        var roleProfile = new RoleProfile();
                        roleProfile.RoleName = roleInfo.CopyRoleName;
                        roleProfile.Name = rp.Name;
                        roleProfile.ModuleID = rp.ModuleID;
                        roleProfile.Value = rp.Value;
                        roleProfile.IsExportable = rp.IsExportable;
                        roleProfile.PropertyDesc = rp.PropertyDesc;
                        roleProfile.PropertyOptions = rp.PropertyOptions;
                        roleProfile.CanBeInherited = rp.CanBeInherited;
                        roleProfile.PropertyType = rp.PropertyType;
                        roleProfile.IsHidden = rp.IsHidden;
                        roleProfile.OrderingPos = rp.OrderingPos;
                        roleProfile.Domain = roleInfo.Domain;
                        roleProfile.UniqueID = Guid.NewGuid().ToString();
                        queryRole.Add(roleProfile);
                    }
                }
                //RoleDir
                var roleDir = new RoleDir();
                roleDir.UniqueID = Guid.NewGuid().ToString();
                roleDir.Name = roleInfo.CopyRoleName;
                roleDir.ParentID = roleInfo.ParentID;
                roleDir.Leaf = 1;
                roleDir.OrderID = 0;
                roleDir.RoleID = role.UniqueID;
                roleDir.Domain = roleInfo.Domain;
                queryDir.Add(roleDir);
            }
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                _dbContext.SaveChanges();
                ts.Complete();
            }
            return true;
        }

        //修改角色
        public bool UpdateRole(RoleDto roleInfo)
        {
            if (roleInfo == null)
            {
                return false;
            }

            //更新权限配置 
            foreach (var rpNew in roleInfo.RoleProfileList)
            {
                var up = _dbContext.Set<RoleProfile>().FirstOrDefault(p => p.Name == rpNew.Name && p.RoleName == rpNew.RoleName && p.ModuleID == rpNew.ModuleID && p.Domain == roleInfo.Domain);
                if (up != null)
                {
                    up.Value = rpNew.Value;
                }
            }
            _dbContext.SaveChanges();
            return true;

        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public bool DelRole(RoleDto roleInfo)
        {
            if (roleInfo == null)
            {
                return false;
            }
            var query = _dbContext.Set<Role>();
            var queryDir = _dbContext.Set<RoleDir>();
            var queryRp = _dbContext.Set<RoleProfile>();
            //RoleDir
            var rds = queryDir.Where(r => r.RoleID == roleInfo.UniqueID);
            foreach (var rd in rds)
            {
                if (rd.Leaf == 0)
                {
                    return false;
                }
                queryDir.Remove(rd);
            }
            //RoleProfile
            var rps = queryRp.Where(r => r.RoleName == roleInfo.RoleName && r.Domain == roleInfo.Domain);
            foreach (var rp in rps)
            {
                queryRp.Remove(rp);
            }
            //role
            var role = query.Where(r => r.UniqueID == roleInfo.UniqueID).FirstOrDefault();
            ////系统角色不删
            //if (role.IsSystem == 1)
            //{
            //    return false;
            //}
            query.Remove(role);
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                _dbContext.SaveChanges();
                ts.Complete();
            }
            return true;
        }

        #endregion


        #region 系统字典

        /// <summary>
        /// 获取非隐藏的字典 配置  根据Dictionary OrderID排序
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DictionaryDto>> GetSysDictionaries(string site)
        {
            //获取非隐藏字典
            var dictionaries = (await _dbContext.Set<Dictionary>().Where(d => d.IsHidden == 0).ToListAsync())
                .Select(Mapper.Map<Dictionary, DictionaryDto>).ToList();
            //字典值
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = await (from dv in dicValues
                                          where dv.Site.Equals(site, StringComparison.OrdinalIgnoreCase) || String.IsNullOrEmpty(dv.Site)
                                          select dv).OrderBy(p => p.OrderID).ToListAsync();
            //字典值根据tag 分组
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).GroupBy(d => d.Tag).ToList();
            //根据tag 匹配字典和值
            dictionaries.ForEach(d =>
            {
                var group = groupedDVs.FirstOrDefault(g => g.Key == d.Tag);
                if (group != null)
                {
                    d.Values = group.Select(g => g).ToList();
                }
            });
            return dictionaries;
        }

        public bool SaveDictionaries(List<DictionaryDto> dics)
        {
            if (dics != null)
            {
                var dicValueDb = _dbContext.Set<DictionaryValue>();
                foreach (var item in dics)
                {
                    if (!string.IsNullOrEmpty(item.value))//有默认值
                    {
                        var dicOldDefault = dicValueDb.Where(v => v.Tag == item.Tag && v.IsDefault == 1).FirstOrDefault();
                        var dicDefault = dicValueDb.Where(v => v.Tag == item.Tag && v.Value == item.value).FirstOrDefault();
                        if (dicOldDefault != null)
                        {
                            if (dicDefault != null && dicOldDefault.Value != dicDefault.Value)
                            {
                                dicOldDefault.IsDefault = 0;
                                dicDefault.IsDefault = 1;
                            }

                        }
                        else
                        {
                            if (dicDefault != null)
                            {
                                dicDefault.IsDefault = 1;
                            }
                        }
                    }
                    else
                    {//无默认值
                        var dicOldDefault = dicValueDb.Where(v => v.Tag == item.Tag && v.IsDefault == 1).FirstOrDefault();
                        if (dicOldDefault != null)
                        {
                            dicOldDefault.IsDefault = 0;
                        }
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取某个字典的值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DictionaryValueDto>> GetDictionariesList(int tag, string site)
        {
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dictionaryValues = await dicValues.Where(d => (d.Site.Equals(site, StringComparison.OrdinalIgnoreCase) || (String.IsNullOrEmpty(site) && string.IsNullOrEmpty(d.Site))) && d.Tag == tag).OrderBy(p => p.OrderID).ToListAsync();
            var groupedDVs = dictionaryValues.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).ToList();
            return groupedDVs;
        }
        //字典添加值
        public int AddDictionaryValue(DictionaryValueDto dvDto)
        {
            if (dvDto == null || dvDto.Tag == 0)
            {
                return -2;
            }
            var dicValues = _dbContext.Set<DictionaryValue>();
            //重复验证
            var cdv = dicValues.Where(d => d.Tag == dvDto.Tag && d.Value == dvDto.Value).FirstOrDefault();
            if (cdv != null)
            {
                return -1;
            }
            var maptag = dicValues.Where(d => d.Tag == dvDto.Tag && d.MapTag != null).FirstOrDefault();
            if (maptag != null)
            {
                dvDto.MapTag = maptag.MapTag;
            }
            var dv = Mapper.Map<DictionaryValueDto, DictionaryValue>(dvDto);
            dv.UniqueID = Guid.NewGuid().ToString();
            dv.Text = dvDto.Value;//相同
            dv.IsDefault = 0;
            dicValues.Add(dv);
            _dbContext.SaveChanges();
            return 1;
        }
        //字典修改值
        public int UpdateDictionaryValue(DictionaryValueDto dvDto)
        {
            if (dvDto == null)
            {
                return -2;
            }
            var dicValues = _dbContext.Set<DictionaryValue>();
            //重复验证
            var cdv = dicValues.Where(d => d.Tag == dvDto.Tag && d.Value == dvDto.Value).FirstOrDefault();
            if (cdv != null && dvDto.UniqueID != cdv.UniqueID)
            {
                return -1;
            }
            var dv = dicValues.Where(d => d.UniqueID == dvDto.UniqueID).FirstOrDefault();
            dv.Value = dvDto.Value;
            dv.Text = dvDto.Value;//相同
            dv.ShortcutCode = dvDto.ShortcutCode;
            //排序
            for (var i = 0; i < dvDto.Values.Count(); i++)
            {
                var item = dvDto.Values[i];
                var dvorder = dicValues.Where(d => d.UniqueID == item.UniqueID).FirstOrDefault();
                if (dvorder != null)
                {
                    dvorder.OrderID = i + 1;
                }
            }
            _dbContext.SaveChanges();
            return 1;
        }
        //排序修改 全部数据按顺序赋值orderid


        //删除字典值
        public bool DeleteDictionaryValue(DictionaryValueDto dvDto)
        {
            if (dvDto == null)
            {
                return false;
            }
            var dicValues = _dbContext.Set<DictionaryValue>();
            var dv = dicValues.Where(d => d.Tag == dvDto.Tag && d.Value == dvDto.Value && (d.Site == dvDto.Site || string.IsNullOrEmpty(dvDto.Site))).FirstOrDefault();
            if (dv == null)
            {
                return false;
            }
            dicValues.Remove(dv);
            _dbContext.SaveChanges();
            return true;
        }

        // 字典关联
        public IEnumerable<DictionaryMapDto> GetDicMappings(string site)
        {

            var dicValues = _dbContext.Set<DictionaryValue>();
            var dics = _dbContext.Set<Dictionary>();
            //字典关联项
            var queryList = (from dv in dicValues
                             join dic in dics on dv.Tag equals dic.Tag
                             where dv.MapTag > 0 && dic.IsHidden == 0 && (dv.Site == site || (string.IsNullOrEmpty(dv.Site) && string.IsNullOrEmpty(site)))
                             select new DictionaryMapDto
                             {
                                 UniqueID = dv.UniqueID,
                                 Name = dic.Name,
                                 Tag = dv.Tag,
                                 Value = dv.Value,
                                 Text = dv.Text,
                                 MapTag = dv.MapTag,
                                 MapValue = dv.MapValue
                             }).OrderBy(d => d.Tag).ToList();

            foreach (var item in queryList)
            {
                //关联 字典名
                item.MapName = dics.Where(d => d.Tag == item.MapTag).FirstOrDefault().Name;
                //关联字典值 
                var mapdvs = dicValues.Where(d => d.Tag == item.MapTag && (d.Site == site || string.IsNullOrEmpty(d.Site))).OrderBy(d => d.Value);
                if (mapdvs.Count() > 0)
                {
                    item.MapDicValues = mapdvs.Select(Mapper.Map<DictionaryValue, DictionaryValueDto>).ToList();
                }

            }
            var result = queryList;
            return result;
        }
        //保存字典关联
        public bool UpdateDicMappings(List<DictionaryMapDto> dicMaps)
        {
            if (dicMaps == null)
                return false;
            var dicValues = _dbContext.Set<DictionaryValue>();

            foreach (var item in dicMaps)
            {
                var dicv = dicValues.Where(d => d.UniqueID == item.UniqueID).FirstOrDefault();
                if (dicv != null)
                {
                    dicv.MapValue = item.MapValue;
                }

            }
            _dbContext.SaveChanges();
            return true;
        }

        #region 申请部门 &申请医生
        //新增申请部门
        public int AddApplyDept(ApplyDeptDto deptDto, string domain)
        {
            if (deptDto == null)
                return -2;//参数错误

            var query = _dbContext.Set<ApplyDept>();
            var dept = query.Where(d => d.DeptName == deptDto.DeptName && (d.Site == deptDto.Site || string.IsNullOrEmpty(deptDto.Site))).FirstOrDefault();
            if (dept != null)
                return -1;//重名

            var newDept = Mapper.Map<ApplyDeptDto, ApplyDept>(deptDto);
            newDept.UniqueID = Guid.NewGuid().ToString();
            newDept.Domain = domain;//用户医院
            if (string.IsNullOrEmpty(newDept.ShortcutCode))
            {
                newDept.ShortcutCode = ConvertFirstPY(newDept.DeptName);
            }
            query.Add(newDept);
            _dbContext.SaveChanges();
            return 1;
        }
        //修改申请部门
        public int UpdateApplyDept(ApplyDeptDto deptDto, string domain)
        {
            if (deptDto == null)
                return -2;//参数错误

            var query = _dbContext.Set<ApplyDept>();
            var dept = query.Where(d => d.DeptName == deptDto.DeptName && (d.Site == deptDto.Site || string.IsNullOrEmpty(deptDto.Site))).FirstOrDefault();
            if (dept != null && dept.UniqueID != deptDto.UniqueID)
                return -1;//重名
            var updateDept = query.Where(d => d.UniqueID == deptDto.UniqueID).FirstOrDefault();
            updateDept.DeptName = deptDto.DeptName;
            updateDept.Telephone = deptDto.Telephone;
            updateDept.ShortcutCode = deptDto.ShortcutCode;
            updateDept.Site = deptDto.Site;
            _dbContext.SaveChanges();
            return 1;
        }
        //删除申请部门
        public bool DeleteApplyDept(string deptId)
        {
            if (string.IsNullOrEmpty(deptId))
                return false;
            var query = _dbContext.Set<ApplyDept>();
            var dept = query.Where(d => d.UniqueID == deptId).FirstOrDefault();
            if (dept != null)
            {
                query.Remove(dept);
                _dbContext.SaveChanges();
            }
            return true;
        }



        //新增申请医生
        public int AddApplyDoctor(ApplyDoctorDto doctorDto, string domain)
        {
            if (doctorDto == null)
                return -2;//参数错误

            var query = _dbContext.Set<ApplyDoctor>();
            var doctor = query.Where(d => d.DoctorName == doctorDto.DoctorName && (d.Site == doctorDto.Site || string.IsNullOrEmpty(doctorDto.Site))).FirstOrDefault();
            if (doctor != null)
                return -1;//重名

            var newDoctor = Mapper.Map<ApplyDoctorDto, ApplyDoctor>(doctorDto);
            newDoctor.UniqueID = Guid.NewGuid().ToString();
            newDoctor.Domain = domain;//用户医院
            if (string.IsNullOrEmpty(newDoctor.ShortcutCode))
            {
                newDoctor.ShortcutCode = ConvertFirstPY(newDoctor.DoctorName);
            }
            query.Add(newDoctor);
            _dbContext.SaveChanges();
            return 1;
        }

        //修改申请医生
        public int UpdateApplyDoctor(ApplyDoctorDto doctorDto, string domain)
        {
            if (doctorDto == null)
                return -2;//参数错误

            var query = _dbContext.Set<ApplyDoctor>();
            var doctor = query.Where(d => d.DoctorName == doctorDto.DoctorName && (d.Site == doctorDto.Site || string.IsNullOrEmpty(doctorDto.Site))).FirstOrDefault();
            if (doctor != null && doctor.UniqueID != doctorDto.UniqueID)
                return -1;//重名
            var updateDoctor = query.Where(d => d.UniqueID == doctorDto.UniqueID).FirstOrDefault();

            updateDoctor.ApplyDeptID = doctorDto.ApplyDeptID;
            updateDoctor.DoctorName = doctorDto.DoctorName;
            updateDoctor.Telephone = doctorDto.Telephone;
            updateDoctor.Mobile = doctorDto.Mobile;
            updateDoctor.Email = doctorDto.Email;
            updateDoctor.Site = doctorDto.Site;
            updateDoctor.Gender = doctorDto.Gender;
            updateDoctor.StaffNo = doctorDto.StaffNo;
            updateDoctor.ShortcutCode = doctorDto.ShortcutCode;

            doctor = Mapper.Map<ApplyDoctorDto, ApplyDoctor>(doctorDto);
            _dbContext.SaveChanges();
            return 1;
        }
        //删除申请医生
        public bool DeleteApplyDoctor(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
                return false;
            var query = _dbContext.Set<ApplyDoctor>();
            var doctor = query.Where(d => d.UniqueID == doctorId).FirstOrDefault();
            if (doctor != null)
            {
                query.Remove(doctor);
                _dbContext.SaveChanges();
            }
            return true;
        }
        #endregion

        #endregion

        #region 系统配置

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public string SystemProfile(string domain, string site)
        {
            if (domain == null)
            {
                return null;
            }
            var reportDbService = new ReportDBService();
            var ds = reportDbService.GetSystemProfileDataSet(domain, site);
            string json = JsonConvert.SerializeObject(ds);
            return json;
        }
        //修改系统配置
        public bool SaveSystemProfile(SystemProfileDto spdto)
        {
            if (spdto == null || spdto.SystemProfileList.Count() < 1)
            {
                return false;
            }
            //更新权限配置 
            foreach (var spNew in spdto.SystemProfileList)
            {
                var up = _dbContext.Set<SystemProfile>().FirstOrDefault(p => p.Name == spNew.Name && p.ModuleID == spNew.ModuleID && p.Domain == spdto.Domain);
                if (up != null)
                {
                    up.Value = spNew.Value;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        #endregion

        #region 资源管理

        //获取设备类型节点
        public List<ModalityTypeDto> GetModalityTypeNode()
        {
            var mts = _dbContext.Set<ModalityType>().ToList().Select(p => Mapper.Map<ModalityType, ModalityTypeDto>(p)).ToList();

            foreach (var m in mts)
            {

                m.Childrens = GetModalityNode(m.ModalityType);
            }
            return mts;
        }
        //获取设备
        public List<ModalityDto> GetModalityNode(string type)
        {
            var mts = _dbContext.Set<Modality>().Where(m => m.ModalityType == type).ToList().Select(p => Mapper.Map<Modality, ModalityDto>(p)).ToList();
            return mts;
        }
        public ModalityDto GetModalityByName(string type, string name)
        {
            var mt = _dbContext.Set<Modality>().Where(m => m.ModalityType == type && m.ModalityName == name).FirstOrDefault();
            var mtd = Mapper.Map<Modality, ModalityDto>(mt);
            return mtd;
        }
        //新增设备
        public int AddModality(ModalityDto mod)
        {
            if (mod == null)
            {
                return -2;
            }
            var query = _dbContext.Set<Modality>();
            var modv = query.Where(m => m.ModalityName == mod.ModalityName).FirstOrDefault();
            if (modv != null)
            {
                return -1;
            }

            mod.MaxLoad = 10;//？？？
            var mo = Mapper.Map<ModalityDto, Modality>(mod);
            mo.UniqueID = Guid.NewGuid().ToString();
            query.Add(mo);
            _dbContext.SaveChanges();
            return 1;
        }
        //修改设备
        public int UpdateModality(ModalityDto mod)
        {
            if (mod == null)
            {
                return -2;
            }
            var query = _dbContext.Set<Modality>();
            var modv = query.Where(m => m.ModalityName == mod.ModalityName && m.UniqueID != mod.UniqueID).FirstOrDefault();
            if (modv != null)
            {
                return -1;
            }
            //没应用停机时间，默认值
            if (mod.ApplyHaltPeriod.HasValue && !mod.ApplyHaltPeriod.Value)
            {
                mod.StartDt = Convert.ToDateTime("1900-01-01 00:00:00");
                mod.EndDt = Convert.ToDateTime("1900-01-01 00:00:00");
            }
            mod.MaxLoad = 10;//？？？
            var moup = query.Where(m => m.UniqueID == mod.UniqueID).FirstOrDefault();
            var mo = Mapper.Map<ModalityDto, Modality>(mod);

            moup.Room = mo.Room;
            moup.ModalityName = mo.ModalityName;
            moup.ModalityType = mo.ModalityType;
            moup.BookingShowMode = mo.BookingShowMode;
            moup.IPAddress = mo.IPAddress;
            moup.WorkStationIP = mo.WorkStationIP;
            moup.ApplyHaltPeriod = mo.ApplyHaltPeriod;
            moup.StartDt = mo.StartDt;
            moup.EndDt = mo.EndDt;
            moup.MaxLoad = mo.MaxLoad;
            moup.Site = mo.Site;
            moup.Domain = mo.Domain;
            moup.Description = mo.Description;


            _dbContext.SaveChanges();
            return 1;
        }
        //删除设备
        public bool DelModality(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "null")
            {
                return false;
            }
            var query = _dbContext.Set<Modality>();
            var delmo = query.Where(m => m.UniqueID == id).FirstOrDefault();
            if (delmo != null)
            {
                query.Remove(delmo);
                _dbContext.SaveChanges();
            }
            return true;
        }

        //获取扫描技术
        public List<ScanningTechDto> GetScanningTechs(string type, string site, string domain)
        {
            var query = _dbContext.Set<ScanningTech>();
            var sts = query.Where(s => s.Domain == domain && s.ModalityType == type && (s.Site == site || (string.IsNullOrEmpty(site) && string.IsNullOrEmpty(s.Site)))).ToList();
            var stds = sts.Select(s => Mapper.Map<ScanningTech, ScanningTechDto>(s)).ToList();
            return stds;
        }
        //新增扫描技术
        public int AddScanningTech(ScanningTechDto mod)
        {
            if (mod == null)
            {
                return -2;
            }
            var query = _dbContext.Set<ScanningTech>();
            var modv = query.Where(m => m.ScanningTechName == mod.ScanningTechName).FirstOrDefault();
            if (modv != null)
            {
                return -1;
            }
            var sc = Mapper.Map<ScanningTechDto, ScanningTech>(mod);
            sc.UniqueID = Guid.NewGuid().ToString();
            sc.ParentId = mod.ModalityType;
            query.Add(sc);
            _dbContext.SaveChanges();
            return 1;
        }

        //修改
        public int UpdateScanningTech(ScanningTechDto std)
        {
            if (std == null)
            {
                return -2;
            }
            var query = _dbContext.Set<ScanningTech>();
            var scv = query.Where(s => s.ScanningTechName == std.ScanningTechName && s.UniqueID != std.UniqueID).FirstOrDefault();
            if (scv != null)
            {
                return -1;
            }
            var scup = query.Where(s => s.UniqueID == std.UniqueID).FirstOrDefault();

            scup.ScanningTechName = std.ScanningTechName;
            _dbContext.SaveChanges();
            return 1;
        }
        //删除
        public bool DelScanningTech(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "null")
            {
                return false;
            }
            var query = _dbContext.Set<ScanningTech>();
            var st = query.Where(m => m.UniqueID == id).FirstOrDefault();
            if (st != null)
            {
                query.Remove(st);
                _dbContext.SaveChanges();
            }
            return true;
        }

        #endregion

        #region 集团医院管理医院
        //获取医院
        public List<DomainListDto> GetDomainList()
        {
            var domainList = _dbContext.Set<DomainList>().ToList().Select(d => Mapper.Map<DomainList, DomainListDto>(d)).ToList();
            return domainList;
        }
        //修改医院
        public int UpdateDomain(DomainListDto domain)
        {
            if (domain == null)
            {
                return -2;
            }
            var domainup = _dbContext.Set<DomainList>().Where(d => d.DomainName == domain.DomainName).FirstOrDefault();
            if (domainup == null)
            {
                return -2;
            }
            var domainv = _dbContext.Set<DomainList>().Where(d => d.DomainPrefix == domain.DomainPrefix && d.DomainName != domain.DomainName).FirstOrDefault();
            if (domainv != null)
            {
                return -1;//前缀重复
            }
            var domainv2 = _dbContext.Set<DomainList>().Where(d => d.Alias == domain.Alias && d.DomainName != domain.DomainName).FirstOrDefault();
            if (domainv2 != null)
            {
                return -1;//简称重复
            }
            domainup.DomainPrefix = domain.DomainPrefix;
            domainup.Alias = domain.Alias;
            domainup.FtpServer = domain.FtpServer;
            domainup.FtpPort = domain.FtpPort;
            domainup.FtpUser = domain.FtpUser;
            domainup.FtpPassword = domain.FtpPassword;
            _dbContext.SaveChanges();
            return 1;
        }
        //获取所有站点
        public List<SiteDto> GetSiteList()
        {
            var siteList = _dbContext.Set<Site>().ToList().Select(s => Mapper.Map<Site, SiteDto>(s)).ToList();
            return siteList;
        }
        //根据站点名获取站点
        public SiteDto GetSite(string siteName, string domain)
        {
            var site = _dbContext.Set<Site>().Where(s => s.SiteName == siteName && s.Domain == domain).FirstOrDefault();
            var siteDto = Mapper.Map<Site, SiteDto>(site);
            return siteDto;
        }
        //添加站点
        public int AddSite(SiteDto site)
        {
            if (site == null)
            {
                return -2;
            }
            var query = _dbContext.Set<Site>();
            var stev = query.Where(s => s.SiteName == site.SiteName).FirstOrDefault();
            if (stev != null)
            {
                return -1;//站点名重复
            }
            var steAlias = query.Where(s => s.Alias == site.Alias).FirstOrDefault();
            if (steAlias != null)
            {
                return 0;//简称重复
            }
            if (site.Tab == null)
            {
                site.Tab = 0;
            }
            var newSite = Mapper.Map<SiteDto, Site>(site);
            query.Add(newSite);
            _dbContext.SaveChanges();
            //INSERT INTO dbo.tSiteList([Domain],DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Site,Alias,Tab) Values('HospitalA','','','',21,'','','title','136','127.0.0.1','127.0.0.2','127.0.0.3','Site4','Site4',0)
            return 1;
        }
        //修改站点
        public int UpdateSite(SiteDto site)
        {
            if (site == null)
            {
                return -2;
            }
            var query = _dbContext.Set<Site>();
            var siteUpdate = query.Where(s => s.SiteName == site.SiteName).FirstOrDefault();
            if (siteUpdate == null)
            {
                return -2;
            }
            var steAlias = query.Where(s => s.SiteName != site.SiteName && s.Alias == site.Alias).FirstOrDefault();
            if (steAlias != null)
            {
                return 0;//简称重复
            }
            var newSite = Mapper.Map<SiteDto, Site>(site);
            siteUpdate.Alias = site.Alias;
            siteUpdate.Domain = site.Domain;
            siteUpdate.PacsServer = site.PacsServer;
            siteUpdate.PacsWebServer = site.PacsWebServer;
            siteUpdate.Telephone = site.Telephone;
            siteUpdate.Address = site.Address;
            siteUpdate.PacsAETitle = site.PacsAETitle;
            siteUpdate.FtpPort = site.FtpPort;//和医院的FtpPort相同
            _dbContext.SaveChanges();
            return 1;
        }
        //删除站点
        public bool DeleteSite(string name)
        {
            var query = _dbContext.Set<Site>();
            var delsite = query.Where(s => s.SiteName == name).FirstOrDefault();
            if (delsite != null)
            {
                query.Remove(delsite);
                _dbContext.SaveChanges();
            }
            return true;
        }
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public string SiteProfile(string domain, string site)
        {
            if (domain == null)
            {
                return null;
            }
            var reportDbService = new ReportDBService();
            var ds = reportDbService.GetSiteProfileDataSet(domain, site);
            string json = JsonConvert.SerializeObject(ds);
            return json;
        }
        //添加站配置 
        public bool AddProfile(SiteProfileDto siteProDto)
        {
            if (siteProDto == null || string.IsNullOrEmpty(siteProDto.Site))
            {
                return false;
            }
            var sysquery = _dbContext.Set<SystemProfile>();
            var query = _dbContext.Set<SiteProfile>();
            var systemProfile = sysquery.Where(s => s.Domain == siteProDto.Domain && s.Name == siteProDto.Name && s.ModuleID == siteProDto.ModuleID).FirstOrDefault();

            var sitePro = new SiteProfile
            {
                Name = systemProfile.Name,
                ModuleID = systemProfile.ModuleID,
                Value = systemProfile.Value,
                PropertyDesc = systemProfile.PropertyDesc,
                PropertyOptions = systemProfile.PropertyOptions,
                IsExportable = systemProfile.IsExportable,
                CanBeInherited = systemProfile.CanBeInherited,
                PropertyType = systemProfile.PropertyType,
                IsHidden = systemProfile.IsHidden,
                OrderingPos = systemProfile.OrderingPos,
                Domain = systemProfile.Domain,
                Site = siteProDto.Site,
                UniqueID = Guid.NewGuid().ToString(),
            };
            query.Add(sitePro);
            _dbContext.SaveChanges();
            return true;
        }
        //修改站点配置
        public bool SaveSiteProfile(List<SiteProfileDto> siteProList)
        {
            if (siteProList == null || siteProList.Count() < 1)
            {
                return false;
            }
            //更新site配置 
            foreach (var sitepro in siteProList)
            {
                var siteproUpdate = _dbContext.Set<SiteProfile>().FirstOrDefault(s => s.Name == sitepro.Name && s.ModuleID == sitepro.ModuleID && s.Domain == sitepro.Domain && s.Site == sitepro.Site);
                if (siteproUpdate != null)
                {
                    siteproUpdate.Value = sitepro.Value;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }
        //删除站点单个配置
        public bool DelSiteProfile(SiteProfileDto siteProDto)
        {
            if (siteProDto == null || string.IsNullOrEmpty(siteProDto.Site))
            {
                return false;
            }
            var query = _dbContext.Set<SiteProfile>();
            var sitePro = query.Where(s => s.Name == siteProDto.Name && s.Site == siteProDto.Site && s.ModuleID == siteProDto.ModuleID).FirstOrDefault();
            if (sitePro != null)
            {
                query.Remove(sitePro);
                _dbContext.SaveChanges();
            }
            return true;
        }
        #endregion

        #region 管理员工具
        //解锁

        //锁查询
        public PaginationResult SearchLocks(SyncSearchCriteriaDto request)
        {
            if (request == null)
            {
                return null;
            }
            var query = (from s in _dbContext.Set<Sync>()
                         join u in _dbContext.Set<User>() on s.Owner equals u.UniqueID
                         join m in _dbContext.Set<Module>() on s.ModuleID equals m.ModuleID
                         select new SyncDto
                         {
                             OrderID = s.OrderID,
                             SyncType = s.SyncType,
                             Owner = s.Owner,
                             OwnerName = u.LocalName,
                             LoginName = u.LoginName,
                             OwnerIP = s.OwnerIP,
                             CreateTime = s.CreateTime,
                             ModuleID = s.ModuleID,
                             ModuleTitle=m.Title,
                             PatientID = s.PatientID,
                             AccNo = s.AccNo,
                             PatientName = s.PatientName,
                             Counter = s.Counter,
                             ProcedureIDs = s.ProcedureIDs,
                             Domain = s.Domain
                         });

            if (!string.IsNullOrEmpty(request.OwnerName))
            {
                query = query.Where(q => q.OwnerName.Contains(request.OwnerName));
            }
            if (request.HaveTime.HasValue && request.HaveTime == true)
            {
                if (request.CreateStartTime != null)
                {
                    query = query.Where(q => q.CreateTime > request.CreateStartTime);
                }
                if (request.CreateEndTime != null)
                {
                    request.CreateEndTime = ((DateTime)request.CreateEndTime).AddDays(1);
                    query = query.Where(q => q.CreateTime <= request.CreateEndTime);
                }
            }
            var getTwoPagesItemsTask = query.OrderByDescending(q => q.CreateTime).Skip(request.Pagination.PageSize * (request.Pagination.PageIndex - 1)).Take(request.Pagination.PageSize * 2).ToList();

            var result = new PaginationResult
            {
                Total = query.Count(),
                Data = getTwoPagesItemsTask,
            };

            return result;

        }
        //单个解锁
        public bool DelLock(SyncDto lk)
        {
            if (lk != null)
            {
                var query = _dbContext.Set<Sync>();
                var delItem = query.Where(s => s.SyncType == lk.SyncType && s.Owner == lk.Owner && s.OrderID == lk.OrderID).FirstOrDefault();
                if (delItem != null)
                {
                    query.Remove(delItem);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        //批量解锁
        public bool DelLocks(List<SyncDto> lks)
        {
            if (lks != null && lks.Count() > 0)
            {
                var query = _dbContext.Set<Sync>();
                foreach (var lk in lks)
                {
                    var delItem = query.Where(s => s.SyncType == lk.SyncType && s.Owner == lk.Owner && s.OrderID == lk.OrderID).FirstOrDefault();
                    if (delItem != null)
                    {
                        query.Remove(delItem);
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        //在线用户

        //工具栏
        #endregion
    }

}
