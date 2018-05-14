using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.OutputCache.V2;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Services;
using System.Web;
using Hys.CrossCutting.Common.Utils;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Configuration Web Api
    /// </summary>
    [RoutePrefix("api/v1/consultation/configuration")]
    [AutoInvalidateCacheOutput]
    public class ConsultationConfigurationController : ApiControllerBase
    {
        private readonly IConsultationConfigurationService _ConfigurationService;
        private readonly IUserManagementService _UserManagementService;
        private readonly INotificationService _NotificationService;

        /// <summary>
        /// Configuration controller constructor
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="userManagementService"></param>
        /// <param name="notificationService"></param>
        public ConsultationConfigurationController(IConsultationConfigurationService configurationService,
            IUserManagementService userManagementService, INotificationService notificationService)
        {
            _ConfigurationService = configurationService;
            _UserManagementService = userManagementService;
            _NotificationService = notificationService;
        }

        /// <summary>
        /// Get sit or global dictionary data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionaries/{type}")]
        public IHttpActionResult GetSiteDictionaries(int type)
        {
            var user = base.CurrentUser();
            var dic = (Dictionary<string, IEnumerable<ConsultationDictionaryDto>>)HttpContext.Current.Application["dic"];
            var result = dic[user.Language].Where(d => d.Type == type);
            return Ok(result);
        }

        /// <summary>
        /// Get sit or global dictionary data. NO USE andy more
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionaries/query")]
        public IHttpActionResult GetSiteDictionaries([FromUri(Name = "types")]int[] types)
        {
            var user = base.CurrentUser();
            var result = _ConfigurationService.GetDictionaryByTypes(types,user.Language);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpGet, Route("ExamModules")]
        public IHttpActionResult ExamModules(string owner)
        {
            var modules = _ConfigurationService.GetExamModule(owner);

            if (!modules.Any())
            {
                var user = base.CurrentUser();
                var defaultModules = (Dictionary<string, IEnumerable<ExamModuleDto>>)HttpContext.Current.Application["examModule"];
                modules = defaultModules[user.Language];

                return Ok(new { modules, isNew = true });
            }

            return Ok(new { modules, isNew = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateModule")]
        public IHttpActionResult UpdateModule([FromBody]ExamModuleDto module)
        {
            var user = base.CurrentUser();
            var result = _ConfigurationService.UpdateExamModule(module, user.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("roles")]
        [CacheOutput(ServerTimeSpan = 30, ClientTimeSpan = 30)]
        public async Task<IHttpActionResult> GetRolesAsync()
        {
            return Response(await _ConfigurationService.GetRolesAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("roles")]
        public IHttpActionResult SaveRole(RoleDto role)
        {
            return Ok(_ConfigurationService.SaveRole(role));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("roles/{roleID}/names/{roleName}")]
        public IHttpActionResult ValidateRoleName(string roleID, string roleName)
        {
            return Response(_ConfigurationService.ValidateRoleName(roleID, roleName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("roles/{roleID}/usernames")]
        public IHttpActionResult GetRoleRelatedUserName(string roleID)
        {
            return Ok(_UserManagementService.GetRoleRelatedUserName(roleID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("users")]
        public IHttpActionResult GetUsers(string query)
        {
            var searchCriteria = query != "{}" ? JsonConvert.DeserializeObject<UserSearchCriteriaDto>(query) :
                new UserSearchCriteriaDto
                {
                    PageIndex = 1,
                    PageSize = Int32.MaxValue
                };
            return Response(_UserManagementService.SearchUsers(searchCriteria));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("users/{userID}")]
        public IHttpActionResult GetUser(string userID)
        {
            return Response(_UserManagementService.GetUser(userID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPost, Route("users/{userID}/")]
        public IHttpActionResult UpdateUser(string userID, Dictionary<string, object> properties)
        {
            return Response(_UserManagementService.UpdateUser(userID, properties));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("users")]
        public IHttpActionResult SaveUser(UserDto user)
        {
            return Ok(_UserManagementService.SaveUser(user));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("hospitals")]
        public IHttpActionResult GetHospitals(bool isCenter)
        {
            return Ok(_UserManagementService.GetHospitals(isCenter));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("hospitals")]
        public IHttpActionResult SaveHospital(HospitalProfileDto hospital)
        {
            return Ok(_UserManagementService.SaveHospital(hospital));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("notification/configs")]
        public IHttpActionResult GetNotificationConfigs()
        {
            return Ok(_NotificationService.GetNotificationConfigs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("notification/configs")]
        public IHttpActionResult SaveNotificationConfig(List<NotificationConfigDto> configs)
        {
            return Ok(_NotificationService.SaveNotificationConfigs(configs));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("departments")]
        public IHttpActionResult GetDepartments()
        {
            return Ok(_ConfigurationService.GetDepartments());
        }

        [HttpGet, Route("userhospital")]
        public IHttpActionResult GetUserHospital()
        {
            var user = base.CurrentUser();
            return Ok(_ConfigurationService.GetHospital(user.UniqueID));
        }

        [HttpGet, Route("userdam")]
        public IHttpActionResult GetUserDam()
        {
            return Ok(_ConfigurationService.GetDam());
        }

        [HttpGet, Route("userdamidAsync")]
        public async Task<IHttpActionResult> GetUserDamIdAsync()
        {
            return Response(await _ConfigurationService.GetDamIdAsync());
        }


        [HttpGet, Route("userdambyid/{id}")]
        [AllowAnonymous]
        public IHttpActionResult GetUserDamByID(string id)
        {
            return Ok(_ConfigurationService.GetDamByID(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("dams")]
        public IHttpActionResult GetDams()
        {
            return Ok(_ConfigurationService.GetDams());
        }

        [HttpGet, Route("servicetype")]
        public IHttpActionResult GetServiceType()
        {
            var user = base.CurrentUser();
            var dic = (Dictionary<string, List<ServiceTypeDto>>)HttpContext.Current.Application["serviceType"];
            return Ok(dic[user.Language]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("recipientconfigs")]
        public IHttpActionResult GetRecipientConfigs()
        {
            return Response(_ConfigurationService.GetRecipientConfigs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("recipientconfigs")]
        public IHttpActionResult SaveRecipientConfigs(HospitalDefaultDto hospitalDefaultDto)
        {
            var user = base.CurrentUser();
            return Response(_ConfigurationService.SaveRecipientConfigs(hospitalDefaultDto,user.UniqueID));
        }

        [HttpGet, Route("hospitaldefaultforhospital")]
        public IHttpActionResult GetHospitalDefaultForHospital()
        {
            return Ok(_ConfigurationService.GetHospitalDefaultForHospital());
        }

        [HttpGet, Route("hospitaldefaultforexpert")]
        public IHttpActionResult GetHospitalDefaultForExpert()
        {
            return Ok(_ConfigurationService.GetHospitalDefaultForExpert());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("recipientconfigsreceiver")]
        public IHttpActionResult GetRecipientConfigsForReceiver()
        {
             var user = base.CurrentUser();
            return Response(_ConfigurationService.GetRecipientConfigsForReceiver(user.UniqueID));
        }

        [HttpGet, Route("generatepatientno")]
        public IHttpActionResult GeneratePatientNo()
        {
            var user = base.CurrentUser();
            return Ok(_ConfigurationService.GeneratePatientNo(user.UniqueID));
        }

        [HttpGet, Route("generatepatientnoasync")]
        public async Task<IHttpActionResult> GeneratePatientNoAsync()
        {
            var user = base.CurrentUser();
            return Response(await _ConfigurationService.GeneratePatientNoAsync(user.UniqueID));
        }
    }
}
