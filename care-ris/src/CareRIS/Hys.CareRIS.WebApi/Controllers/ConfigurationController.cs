using System.Threading.Tasks;
using System.Web.Http;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Utils;
using WebApi.OutputCache.V2;
using Newtonsoft.Json;
using Kendo.DynamicLinq;
using System.Collections.Generic;
using System.Linq;
using Hys.CrossCutting.Common;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Configuration Web Api
    /// </summary>
    [RoutePrefix("api/v1/configuration")]
    [AutoInvalidateCacheOutput]
    public class ConfigurationController : ApiControllerBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly IUserManagementService _userManagementService;
        private readonly LicenseDataDto _license;

        /// <summary>
        /// Configuration controller constructor
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="userManagementService"></param>
        /// <param name="license"></param>
        public ConfigurationController(IConfigurationService configurationService, IUserManagementService userManagementService, LicenseDataDto license)
        {
            _configurationService = configurationService;
            _userManagementService = userManagementService;
            _license = license;
        }

        /// <summary>
        /// Get modality data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("modalities")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetModalities()
        {
            var result = await _configurationService.GetModalities();
            return Ok(result);
        }

        /// <summary>
        /// Get site and global modality data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("modalities/{site}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteModalities(string site)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            var result = await _configurationService.GetModalities(site);
            return Ok(result);
        }

        /// <summary>
        /// Get site and global modality data via modalitytype and site.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sitemodalities")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteModalities(string site, string modalityType)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            var result = await _configurationService.GetModalities(site, modalityType);
            return Ok(result);
        }

        /// <summary>
        /// Get sit or global dictionary data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionaries/{site}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteDictionaries(string site)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            var result = await _configurationService.GetDictionaries(site);
            return Ok(result);
        }

        /// <summary>
        /// Get sit or global dictionary data by tags.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionaries/{site}/query")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteDictionaries(string site, [FromUri(Name = "tag")]int[] tags)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            var result = await _configurationService.GetDictionaries(site, tags);
            return Ok(result);
        }

        /// <summary>
        /// Get apply depts data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("applydepts")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetApplyDepts()
        {
            var result = await _configurationService.GetApplyDepts();
            return Ok(result);
        }

        /// <summary>
        /// Get site and global apply depts data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("applydepts/{site}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteApplyDepts(string site)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            //dept can be added automatically by the CanEditAppDepartmentAndAppDoctor, so not use cache
            var result = await _configurationService.GetApplyDepts(site);

            return Ok(result);
        }

        /// <summary>
        /// Get apply doctor data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("applydoctors")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetApplyDoctors()
        {
            var result = await _configurationService.GetApplyDoctors();
            return Ok(result);
        }

        /// <summary>
        /// Get site and global apply doctor data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("applydoctors/{site}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetSiteApplyDoctors(string site)
        {
            // in order to distiguish the cach for each site, we have to send site as parameter
            //doctor can be added automatically by the CanEditAppDepartmentAndAppDoctor, so not use cache
            var result = await _configurationService.GetApplyDoctors(site);

            return Ok(result);
        }

        /// <summary>
        /// Get profiles for user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("profiles/{userID}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetProfiles(string userID)
        {
            var user = base.CurrentUser();
            var result = await _configurationService.GetProfiles(userID, user.Site, "Administrator");
            return Ok(result);
        }

        /// <summary>
        /// Get profiles for user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="profileNames"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("profiles/{userID}/query")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetProfiles(string userID, [FromUri(Name = "name")]string[] profileNames)
        {
            if (profileNames == null)
            {
                return await GetProfiles(userID);
            }
            var user = base.CurrentUser();
            var result = await _configurationService.GetProfiles(userID, user.Site, "Administrator", profileNames);

            return Ok(result);
        }

        /// <summary>
        /// Get user name for user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getusername/{userID}")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public IHttpActionResult GetUserName(string userID)
        {
            UserDto userDto = _userManagementService.GetUserByID(userID);
            if (userDto != null)
            {
                var result = new
                {
                    LocalName = userDto.LocalName,
                    EnglishName = userDto.EnglishName
                };
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("cache/elimination")]
        public void ClearCache()
        {
            // do nothing here, if someone called this method, all cache will be cleared in this controller
        }

        /// <summary>
        /// update User ProfileDto data.
        /// </summary>
        /// <param name="userProfileDto">User Profile data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("userprofile")]
        public IHttpActionResult SaveUserProfiles([FromBody]UserProfileDto userProfileDto)
        {
            var user = base.CurrentUser();
            userProfileDto.UserID = user.UniqueID;
            userProfileDto.Domain = user.Domain;
            var result = _configurationService.SaveUserProfiles(userProfileDto);

            return Ok(result);
        }

        /// <summary>
        /// Get first py
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("convertfirstpy/{name}")]
        public IHttpActionResult ConvertFirstPY(string name)
        {
            return Ok(_configurationService.ConvertFirstPY(name));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/clientconfig/{id}")]
        public IHttpActionResult GetClientConfig(string id)
        {
            return Response(_configurationService.GetClientConfig(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/clientconfig/")]
        public IHttpActionResult SaveClientConfig(ClientConfigDto config)
        {
            return Response(_configurationService.SaveClientConfig(config));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("license")]
        public IHttpActionResult GetLicenseData()
        {
            return Response(_license);
        }
        #region 疾病编码

        /// <summary>
        /// 筛选分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/icds")]
        public async Task<IHttpActionResult> GetICDs([FromBody]DataSourceRequest request)
        {
            var result = await Task.Run<DataSourceResult>(() => _configurationService.SearchICD(request));
            return Ok(result);
        }
        /// <summary>
        /// 下拉框分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/selecticds")]
        public IHttpActionResult GetSelectICDs([FromUri]string query)
        {
            var criteria = JsonConvert.DeserializeObject<SearchCriteriaDto>(query);
            if (criteria == null)
            {
                criteria = new SearchCriteriaDto
                {
                    Pagination = new PaginationDto
                    {
                        PageIndex = 1,
                        PageSize = 20
                    }
                };
            }
            var user = base.CurrentUser();
            var result = _configurationService.SearchICDs(criteria, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 查询所有疾病
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("settings/allicd")]
        public async Task<IHttpActionResult> GetAllICD()
        {
            return Response(await _configurationService.GetAlICD());
        }
        /// <summary>
        /// 新增疾病编码
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/icd")]
        public IHttpActionResult AddICD([FromBody]ICDTenDto icdInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.SaveICD(icdInfo, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 修改疾病编码
        /// </summary>
        /// <param name="icdInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/icdupdate")]
        public IHttpActionResult UpdateICD([FromBody]ICDTenDto icdInfo)
        {
            var result = _configurationService.UpdateICD(icdInfo);
            return Ok(result);
        }

        /// <summary>
        /// 删除
        /// <returns></returns>
        [HttpPut, Route("settings/icddelete")]
        public IHttpActionResult DeleteICD(ICDTenDto icdInfo)
        {
            var result = _configurationService.DelICD(icdInfo);
            return Ok(result);
        }
        /// <summary>
        /// id疾病查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/icdbyid")]
        public IHttpActionResult GetICDByID(string id)
        {
            return Response(_configurationService.GetICDByID(id));
        }

        #endregion

        #region 检查代码
        /// <summary>
        /// 检查代码分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/procedurecodes")]
        public async Task<IHttpActionResult> GetProcedureCodes([FromBody]DataSourceRequest request)
        {
            var result = await Task.Run<DataSourceResult>(() => _configurationService.SearchProcedureCodeList(request));
            return Ok(result);

        }

        /// <summary>
        /// 新增检查代码
        /// </summary>
        /// <param name="pcInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/procedurecodeadd")]
        public IHttpActionResult AddProcedureCode([FromBody]ProcedureCodeDto pcInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.SaveProcedureCode(pcInfo, user.Site, user.Domain);
            return Ok(result);
        }
        /// <summary>
        /// 修改检查代码
        /// </summary>
        /// <param name="pcInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/procedurecodeupdate")]
        public IHttpActionResult UpdateProcedureCode([FromBody]ProcedureCodeDto pcInfo)
        {
            var result = _configurationService.UpdateProcedureCode(pcInfo);
            return Ok(result);
        }

        /// <summary>
        /// 获取检查频率
        /// </summary>
        /// <param name="pcInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/procedurefrequency")]
        public IHttpActionResult GetProcedureFrequency([FromBody]ProcedureCodeDto pcInfo)
        {
            var result = _configurationService.GetProcedureFrequency(pcInfo);
            return Ok(result);
        }
        /// <summary>
        /// 修改检擦频率单个
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updatefrequency")]
        public IHttpActionResult UpdateFrequency(ProcedureCodeDto model)
        {
            var result = _configurationService.UpdateFrequency(model);
            return Ok(result);
        }
        /// <summary>
        /// 修改检擦频率list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updatefrequencylist")]
        public IHttpActionResult UpdateFrequencyList(List<ProcedureCodeDto> models)
        {

            var result = _configurationService.UpdateFrequencyList(models);
            return Ok(result);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPut, Route("settings/procedurecodedelete")]
        public IHttpActionResult DeleteProcedureCode(ProcedureCodeDto icdInfo)
        {
            var result = _configurationService.DelProcedureCode(icdInfo);
            return Ok(result);
        }

        /// <summary>
        /// 新增BodySystemMap
        /// </summary>
        /// <param name="dsmInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/bodysystemmapadd")]
        public IHttpActionResult AddBodySystemMap([FromBody]BodySystemMapDto dsmInfo)
        {
            var user = base.CurrentUser();
            dsmInfo.Domain = user.Domain;
            dsmInfo.Site = "";
            var result = _configurationService.AddBodySystemMap(dsmInfo);
            return Ok(result);
        }
        /// <summary>
        /// 唯一验证BodySystemMap
        /// true:已存在
        /// ModalityType
        /// BodyPart
        /// ExamSystem
        /// </summary>
        /// <param name="dsmInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/isbodypartexist")]
        public IHttpActionResult IsBodyPartExist([FromBody]BodySystemMapDto dsmInfo)
        {
            var result = _configurationService.IsBodyPartExist(dsmInfo);
            return Ok(result);
        }

        /// <summary>
        /// 获取检查系统 by
        /// ModalityType
        /// BodyPart
        /// </summary>
        /// <param name="dsmInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/bodysystem")]
        public IHttpActionResult GetBodySystemMap([FromBody]BodySystemMapDto dsmInfo)
        {
            var result = _configurationService.GetBodySystemMap(dsmInfo);
            return Ok(result);
        }
        /// <summary>
        /// 获取部位分类text
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/procedurecodetext")]
        public IHttpActionResult GetAllProcedureCodeText()
        {
            var result = _configurationService.GetAllProcedureCode();
            if (result != null)
            {
                var procedureCodeList = result.Select(c =>
                    new
                    {
                        Text = c.BodyCategory,
                        Frequency = c.Frequency

                    }).Distinct().ToList();
                return Ok(procedureCodeList);
            }
            return Ok(result);
        }

        /// <summary>
        /// 获取检查项目text
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/checkingitemtext")]
        public IHttpActionResult GetCheckingItemText()
        {
            var result = _configurationService.GetAllProcedureCode();
            if (result != null)
            {
                var checkingItemList = result.Select(c =>
                    new
                    {
                        Text = c.CheckingItem,
                        CheckingItemFrequency = c.CheckingItemFrequency

                    }).Distinct().ToList();
                return Ok(checkingItemList);
            }
            return Ok(result);
        }

        /// <summary>
        /// 获取全部检查项目的全部信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/allprocedurecode")]
        public IHttpActionResult GetAllProcedureCode()
        {
            var result = _configurationService.GetAllProcedureCode();
            return Ok(result);
        }


        /// <summary>
        /// tag:4 图片尺寸
        /// 15：持续检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/dictionaries/query")]
        public async Task<IHttpActionResult> GetSiteDictionaries([FromUri(Name = "tag")]int[] tags)
        {
            var user = base.CurrentUser();
            var result = await _configurationService.GetDictionariesList(user.Site, tags);
            return Ok(result);
        }

        /// <summary>
        /// 获取默认设备
        /// </summary>
        /// <param name="cType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getmodality/{cType}")]
        public IHttpActionResult GetModality(string cType)
        {
            var user = base.CurrentUser();
            var result = _configurationService.GetModality(cType);
            return Ok(result);
        }

        #endregion

        #region 用户管理
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [Route("userlist"), HttpPost]
        public async Task<IHttpActionResult> GetUserList([FromBody]PageRequest<string[]> request)
        {
            var user = CurrentUser();
            var result = await Task.Run<DataSourceResult>(() => _userManagementService.GetPageUsers(request));
            return Ok(result);
        }

        /// <summary>
        ///本地显示名验证
        /// </summary>
        /// <param name="userInfo"> DisplayName,userGuid</param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/isdisplaynameexist")]
        public IHttpActionResult DisplayNameExist([FromBody]UserDto userInfo)
        {
            var result = _userManagementService.DisplayNameExist(userInfo);
            return Ok(result);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/saveuser")]
        public IHttpActionResult SaveUser([FromBody]UserDto userInfo)
        {
            var user = base.CurrentUser();
            userInfo.Site = user.Site;
            userInfo.Domain = string.IsNullOrEmpty(userInfo.Domain) ? user.Domain : userInfo.Domain;
            var result = _userManagementService.SaveUser(userInfo);
            return Ok(result);
        }

        /// <summary>
        /// 登录名验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/loginnameexist/{loginName}")]
        public IHttpActionResult LoginNameExist(string loginName)
        {

            var result = _userManagementService.UserLoginNameExist(loginName);
            return Ok(result);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userInfo"> LoginName,DisplayName,EnglishName,Title,Address,Comments,IsLocked,Department,Telephone,Mobile,Email,IsSetExpireDate,EndDate,StartDate</param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updateuser")]
        public IHttpActionResult UpdateUser([FromBody]UserDto userInfo)
        {
            var result = _userManagementService.UpdateUser(userInfo);
            return Ok(result);
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/allroles")]
        public IHttpActionResult GetRoles()
        {
            var result = _userManagementService.GetRoles();
            return Ok(result);

        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/userroles/{userID}")]
        public IHttpActionResult GetUserRoles(string userID)
        {
            var result = _userManagementService.GetUserRoles(userID);
            return Ok(result);
        }
        /// <summary>
        /// 修改角色用户关系
        /// </summary>
        /// <param name="roleToUsers"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updateroletouser")]
        public IHttpActionResult UpdateRoleToUser(List<RoleToUserDto> roleToUsers)
        {
            var result = _userManagementService.UpdateRoleToUser(roleToUsers);
            return Ok(result);
        }

        ///// <summary>
        ///// 修改用户权限配置
        ///// </summary>
        ///// <param name="userProfiles"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("settings/updateuserrrofiles")]
        //public IHttpActionResult UpdateUserProfile(List<UserProfileDto> userProfiles)
        //{
        //    var result = _userManagementService.UpdateUserProfile(userProfiles);
        //    return Ok(result);
        //}
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/deleteuser/{userId}")]
        public IHttpActionResult DeleteUser(string userId)
        {
            var user = base.CurrentUser();
            var result = _userManagementService.DeleteUser(userId, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 获取用户权限配置
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/userprofilesbyuserid/{userID}")]
        public IHttpActionResult GetUserProfiles(string userID)
        {
            var result = _userManagementService.GetUserProfiles(userID);
            return Ok(result);
        }

        /// <summary>
        /// 获取影像科室
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/departs")]
        public IHttpActionResult GetDepart()
        {
            var user = base.CurrentUser();
            var result = _userManagementService.GetDepart(user.Domain);
            return Ok(result);
        }
        /// <summary>
        /// 获取所有职称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/titles")]
        public IHttpActionResult GetTitles()
        {
            var user = base.CurrentUser();
            var result = _userManagementService.GetTitles(user.Domain);
            return Ok(result);
        }
        /// <summary>
        /// 根据角色s获取用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/usersbyrolenames")]
        public async Task<IHttpActionResult> UsersByRoleNames(List<string> roleNames)
        {
            var user = base.CurrentUser();
            var result = await _userManagementService.GetUserByRoleName(roleNames, user.Domain);
            if (result != null)
            {
                var userList = result.Select(c =>
                new
                {
                    UniqueID = c.UniqueID,
                    LoginName = c.LoginName,
                    LocalName = c.LocalName
                }).ToList();

                return Ok(userList.Distinct());
            }
            return Ok(result);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="upwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updatepwd")]
        public IHttpActionResult UpdatePassword([FromBody]UserPwdDto upwd)
        {
            var user = base.CurrentUser();
            var result = _userManagementService.UpdatePassword(upwd);
            return Ok(result);
        }

        #endregion

        #region 角色管理
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/rolenodes")]
        public async Task<IHttpActionResult> GetRoleNodes()
        {
            var user = base.CurrentUser();
            var result = await _configurationService.GetRoleNodes(user.Domain);
            return Ok(result);
        }
        /// <summary>
        /// 获取角色配置信息
        /// roleInfo.RoleName,roleInfo.Domain
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/roleprofiles")]
        public IHttpActionResult RoleProfiles([FromBody]RoleDto roleInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.RoleProfile(roleInfo, user.UniqueID);
            return Ok(result);
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/roleprofiles{roleName}")]
        public IHttpActionResult GetRoleProfiles(string roleName)
        {
            var user = base.CurrentUser();
            var result = _configurationService.GetRoleProfile(roleName, user.Domain, user.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 保存角色
        /// RoleName:新增时角色名/拷贝时源角色名
        /// Description:新角色的描述
        /// CopyRoleName：拷贝时角色名
        /// IsCopy :bool true:是拷贝
        /// Site：添加站点角色时   站点
        /// Domain：医院
        /// ParentID:父级节点id
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/saverole")]
        public IHttpActionResult SaveRole([FromBody]RoleDto roleInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.AddRole(roleInfo);
            return Ok(result);
        }

        /// <summary>
        /// 更新角色
        /// Domain：医院
        /// RoleProfileList:角色配置信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updaterole")]
        public IHttpActionResult UpdateRole([FromBody]RoleDto roleInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.UpdateRole(roleInfo);
            return Ok(result);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/delrole")]
        public IHttpActionResult DelRole([FromBody]RoleDto roleInfo)
        {
            var user = base.CurrentUser();
            var result = _configurationService.DelRole(roleInfo);
            return Ok(result);
        }


        #endregion

        [HttpGet]
        [Route("usersites")]
        public async Task<IHttpActionResult> GetUserSites()
        {
            var user = base.CurrentUser();
            var allSites = await _configurationService.GetUserSites(user.UniqueID, user.Site, user.RoleName);
            return Ok(allSites);
        }

        #region 系统字典
        /// <summary>
        /// 根据站点获取系统字典 站点为空获取所有
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/sysdictionaries")]
        public async Task<IHttpActionResult> GetSysDictionaries(string site)
        {
            var allDictionaries = await _configurationService.GetSysDictionaries(site);
            return Ok(allDictionaries);
        }

        /// <summary>
        /// 保存字典
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/savedictionaries")]
        public IHttpActionResult SaveDictionaries(List<DictionaryDto> dics)
        {
            var result = _configurationService.SaveDictionaries(dics);
            return Ok(result);
        }
        /// <summary>
        /// tag's DictionaryValue
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/dictionariesbytag")]
        public async Task<IHttpActionResult> GetDictionariesList(int tag, string site)
        {
            var allDictionaries = await _configurationService.GetDictionariesList(tag, site);
            return Ok(allDictionaries);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dvDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/savedictionaryvalue")]
        public IHttpActionResult SaveDictionaryValue([FromBody]DictionaryValueDto dvDto)
        {
            var user = base.CurrentUser();
            dvDto.Domain = user.Domain;
            var result = _configurationService.AddDictionaryValue(dvDto);
            return Ok(result);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dvDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/updatedictionaryvalue")]
        public IHttpActionResult UpdateDictionaryValue([FromBody]DictionaryValueDto dvDto)
        {
            var result = _configurationService.UpdateDictionaryValue(dvDto);
            return Ok(result);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dvDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/deldictionaryvalue")]
        public IHttpActionResult DelDictionaryValue([FromBody]DictionaryValueDto dvDto)
        {
            var result = _configurationService.DeleteDictionaryValue(dvDto);
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getdicmappings")]
        public IHttpActionResult GetDicMappings(string site)
        {
            var dicMappings = _configurationService.GetDicMappings(site);
            return Ok(dicMappings);
        }
        /// <summary>
        /// 保存字典关联
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("settings/savedicmappings")]
        public IHttpActionResult SaveDicMappings(List<DictionaryMapDto> dics)
        {
            var result = _configurationService.UpdateDicMappings(dics);
            return Ok(result);
        }

        /// <summary>
        /// 配置》 获取所有申请部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allapplydepts")]
        public async Task<IHttpActionResult> GetAllDepts()
        {
            var result = await _configurationService.GetApplyDepts();
            return Ok(result);
        }

        /// <summary>
        /// 新增申请部门
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addapplydept")]
        public IHttpActionResult AddApplyDept(ApplyDeptDto deptDto)
        {
            var user = base.CurrentUser();
            var result = _configurationService.AddApplyDept(deptDto, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 修改申请部门
        /// </summary>
        /// <param name="deptDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updateapplydept")]
        public IHttpActionResult UpdateApplyDept(ApplyDeptDto deptDto)
        {
            var user = base.CurrentUser();
            var result = _configurationService.UpdateApplyDept(deptDto, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 删除申请部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/deleteapplydept/{id}")]
        public IHttpActionResult DeleteApplyDept(string id)
        {
            var dicMappings = _configurationService.DeleteApplyDept(id);
            return Ok(dicMappings);
        }

        /// <summary>
        /// 新增申请医生
        /// </summary>
        /// <param name="doctorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addapplydoctor")]
        public IHttpActionResult AddApplyDoctor(ApplyDoctorDto doctorDto)
        {
            var user = base.CurrentUser();
            var result = _configurationService.AddApplyDoctor(doctorDto, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 修改申请医生
        /// </summary>
        /// <param name="doctorDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updateapplydoctor")]
        public IHttpActionResult UpdateApplyDoctor(ApplyDoctorDto doctorDto)
        {
            var user = base.CurrentUser();
            var result = _configurationService.UpdateApplyDoctor(doctorDto, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 删除申请部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/deleteapplydoctor/{id}")]
        public IHttpActionResult DeleteApplyDoctor(string id)
        {
            var dicMappings = _configurationService.DeleteApplyDoctor(id);
            return Ok(dicMappings);
        }

        /// <summary>
        /// Get apply doctor data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allapplydoctors")]
        public async Task<IHttpActionResult> GetAllApplyDoctors()
        {
            var result = await _configurationService.GetApplyDoctors();
            return Ok(result);
        }


        #endregion

        #region  系统配置
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/systemProfiles/{domain}")]
        public IHttpActionResult SystemProfile(string domain)
        {

            var user = base.CurrentUser();
            if (string.IsNullOrEmpty(domain))
            {
                domain = user.Domain;
            }
            var result = _configurationService.SystemProfile(domain, user.Site);
            return Ok(result);
        }
        /// <summary>
        /// SystemProfileDto:
        /// Domain string
        /// SystemProfileList List ProfileDto
        /// ProfileDto:
        /// Name
        /// ModuleID
        /// Value 
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/savesystemprofile")]
        public IHttpActionResult SaveSystemProfile([FromBody]SystemProfileDto sysInfo)
        {
            var user = base.CurrentUser();
            if (sysInfo != null)
            {
                if (string.IsNullOrEmpty(sysInfo.Domain))
                {
                    sysInfo.Domain = user.Domain;
                }
            }
            var result = _configurationService.SaveSystemProfile(sysInfo);
            return Ok(result);
        }
        //系统配置保存

        #endregion

        #region 资源管理
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getmodalitytypenode")]
        public IHttpActionResult GetModalityTypeNode()
        {
            //
            var result = _configurationService.GetModalityTypeNode();
            if (result != null)
            {
                foreach (ModalityTypeDto mt in result)
                {
                    if (mt.Childrens != null && mt.Childrens.Count() > 0)
                    {
                        mt.HasChildren = true;
                    }
                    else
                    {
                        mt.HasChildren = false;
                    }
                }
                var templateList = result.Select(c =>
                new
                {
                    Name = c.ModalityType,
                    Type = 1,
                    HasChildren = c.HasChildren,
                    Items = c.Childrens.Select(m =>
                      new
                      {
                          Name = m.ModalityName,
                          Type = 2,
                          ModalityType = m.ModalityType,
                          HasChildren = false,
                          Items = new List<object>()
                      }
                    ),
                    enabled = true
                }).ToList();
                return Ok(templateList);
            }
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getmodalitynode/{type}")]
        public IHttpActionResult GetModalityNode(string type)
        {
            var result = _configurationService.GetModalityNode(type);
            if (result != null)
            {
                var templateList = result.Select(c =>
                               new
                               {
                                   Name = c.ModalityName,
                                   Type = c.ModalityType,
                                   HasChildren = false,
                                   enabled = true
                               }).ToList();
                return Ok(templateList);
            }
            return Ok(result);
        }
        /// <summary>
        ///获取设备信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getmodalitybyName")]
        public IHttpActionResult GetModalityByName(string type, string name)
        {
            var result = _configurationService.GetModalityByName(type, name);

            return Ok(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addmodality")]
        public IHttpActionResult AddModality(ModalityDto mod)
        {
            var user = base.CurrentUser();
            mod.Domain = user.Domain;
            var result = _configurationService.AddModality(mod);
            return Ok(result);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updatemodality")]
        public IHttpActionResult UpdateModality(ModalityDto mod)
        {
            var user = base.CurrentUser();
            var result = _configurationService.UpdateModality(mod);
            return Ok(result);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/deletemodality/{id}")]
        public IHttpActionResult DelModality(string id)
        {
            var result = _configurationService.DelModality(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取站点扫描技术
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getscanningtechs")]
        public IHttpActionResult GetScanningTechs(string type, string site)
        {
            var user = base.CurrentUser();
            var result = _configurationService.GetScanningTechs(type, site, user.Domain);
            return Ok(result);
        }

        /// <summary>
        /// 新增扫描技术
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addscanningtech")]
        public IHttpActionResult AddScanningTech(ScanningTechDto st)
        {
            var user = base.CurrentUser();
            st.Domain = user.Domain;
            var result = _configurationService.AddScanningTech(st);
            return Ok(result);
        }
        /// <summary>
        /// 修改扫描技术
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updatescanningtech")]
        public IHttpActionResult UpdateScanningTech(ScanningTechDto st)
        {
            var user = base.CurrentUser();
            st.Domain = user.Domain;
            var result = _configurationService.UpdateScanningTech(st);
            return Ok(result);
        }
        /// <summary>
        /// 删除扫描技术
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/deletescanningtech/{id}")]
        public IHttpActionResult DelScanningTech(string id)
        {
            var result = _configurationService.DelScanningTech(id);
            return Ok(result);
        }

        #endregion
        #region 集团医院
        /// <summary>
        /// 获取医院
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getdomainlist")]
        public IHttpActionResult GetDomainList()
        {
            var result = _configurationService.GetDomainList();
            return Ok(result);
        }
        /// <summary>
        /// 修改医院
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updatedomain")]
        public IHttpActionResult UpdateDomain(DomainListDto domain)
        {

            var result = _configurationService.UpdateDomain(domain);
            return Ok(result);
        }
        /// <summary>
        /// 获取站点s
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getsitelist")]
        public IHttpActionResult GetSiteList()
        {
            var result = _configurationService.GetSiteList();
            return Ok(result);
        }
        /// <summary>
        /// 获取站点详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getsitedli")]
        public IHttpActionResult GetSiteDli(string site, string domain)
        {
            var result = _configurationService.GetSite(site, domain);
            return Ok(result);
        }
        /// <summary>
        /// 新增站点
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addnewsite")]
        public IHttpActionResult AddNewSite(SiteDto st)
        {
            var user = base.CurrentUser();
            if (st.Domain == null)
            {
                st.Domain = user.Domain;
            }
            var result = _configurationService.AddSite(st);
            return Ok(result);
        }
        /// <summary>
        /// 修改战点
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("settings/updatesite")]
        public IHttpActionResult UpdateSite(SiteDto st)
        {
            var result = _configurationService.UpdateSite(st);
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/deletesite/{id}")]
        public IHttpActionResult DeleteSite(string id)
        {
            var result = _configurationService.DeleteSite(id);
            return Ok(result);
        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="site"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/siteprofile")]
        public IHttpActionResult SiteProfile(string site, string domain)
        {
            var result = _configurationService.SiteProfile(domain, site);
            return Ok(result);
        }

        /// <summary>
        /// 新增站点配置
        /// </summary>
        /// <param name="siteProDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/addprofile")]
        public IHttpActionResult AddProfile(SiteProfileDto siteProDto)
        {
            var user = base.CurrentUser();
            if (siteProDto.Domain == null)
            {
                siteProDto.Domain = user.Domain;
            }
            var result = _configurationService.AddProfile(siteProDto);
            return Ok(result);
        }

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        /// <param name="siteProList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/savesiteprofile")]
        public IHttpActionResult SaveSiteProfile(List<SiteProfileDto> siteProList)
        {

            var result = _configurationService.SaveSiteProfile(siteProList);
            return Ok(result);
        }

        /// <summary>
        /// 删除站点配置
        /// </summary>
        /// <param name="siteProDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/delsiteprofile")]
        public IHttpActionResult DelSiteProfile(SiteProfileDto siteProDto)
        {
            var result = _configurationService.DelSiteProfile(siteProDto);
            return Ok(result);
        }
        #endregion

        #region
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("settings/getlocks")]
        public IHttpActionResult SearchLocks([FromUri]string query)
        {
            var microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            var request = JsonConvert.DeserializeObject<SyncSearchCriteriaDto>(query, microsoftDateFormatSettings);
            if (request == null)
            {
                request = new SyncSearchCriteriaDto
                {
                    Pagination = new PaginationDto
                    {
                        PageIndex = 1,
                        PageSize = 20
                    }
                };
            }
            var result = _configurationService.SearchLocks(request);
            return Ok(result);
        }
        /// <summary>
        /// 解锁 单个
        /// </summary>
        /// <param name="lk"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("settings/dellock")]
        public IHttpActionResult DelLock(SyncDto lk)
        {
            var result = _configurationService.DelLock(lk);
            return Ok(result);
        }
        /// <summary>
        /// 解锁 多个
        /// </summary>
        /// <param name="lks"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("settings/dellocks")]
        public IHttpActionResult DelLocks(List<SyncDto> lks)
        {
            var result = _configurationService.DelLocks(lks);
            return Ok(result);
        }
    
        #endregion
    }
}
