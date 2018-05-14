using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Services;

using Hys.CareRIS.WebApi.Utils;
using Newtonsoft.Json;
using IUserManagementService = Hys.CareRIS.Application.Services.IUserManagementService;
using Hys.CareRIS.Application.Dtos;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Consultation Web Api 
    /// </summary>
    public class ConsultationController : ApiControllerBase
    {
        private readonly IConsultationService _consultationService;
        private readonly IShortcutService _shortcutService;
        private readonly IUserManagementService _userManagementService;

        private const string ContentMessage = "Requset id cannot be empty.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultationService"></param>
        /// <param name="shortcutService"></param>
        /// <param name="userManagementService"></param>
        public ConsultationController(IConsultationService consultationService, IShortcutService shortcutService, IUserManagementService userManagementService)
        {
            _consultationService = consultationService;
            _shortcutService = shortcutService;
            _userManagementService = userManagementService;
        }

        /// <summary>
        /// Search consultation requests
        /// </summary>
        /// <param name="query">Search Criteria</param>
        /// <returns>ConsultationRequestSearchDto</returns>
        [HttpGet]
        [Route("api/v1/consultationrequests")]
        public async Task<IHttpActionResult> GetConsultationRequests([FromUri]string query)
        {
            var user = base.CurrentUser();
            var searchCriteria = JsonConvert.DeserializeObject<ConsultationRequestSearchCriteriaDto>(query);
            return Response(await _consultationService.SearchDoctorRequests(searchCriteria, user.Language));
        }

        /// <summary>
        /// Search consultation requests
        /// </summary>
        /// <param name="query"></param>
        /// <returns>ConsultationRequestSearchDto</returns>
        [HttpGet]
        [Route("api/v1/consultationpatientcases")]
        public async Task<IHttpActionResult> GetPatientCases([FromUri]string query)
        {
            var searchCriteria = JsonConvert.DeserializeObject<PatientCaseSearchCriteriaDto>(query);
            return Response(await _consultationService.SearchPatientCases(searchCriteria));
        }

        #region Shortcut

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortcut"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/shortcuts")]
        public IHttpActionResult AddShortcut(ShortcutDto shortcut)
        {
            var result = _shortcutService.AddShortcut(shortcut);
            return Created<ShortcutDto>("api/v1/shortcut/" + result.UniqueId, result);
        }

        /// <summary>
        /// GetShortcuts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/shortcuts")]
        public IHttpActionResult GetShortcuts(string query)
        {
            IEnumerable<ShortcutDto> result = null;

            var param = JsonConvert.DeserializeObject<dynamic>(query);
            var userId = param.userId.Value;
            var category = (ShortcutCategory)param.category.Value;
            result = _shortcutService.GetShortcuts(userId, category);

            return Ok(result);
        }

        [HttpDelete]
        [Route("api/v1/shortcut/{shortcutId}")]
        public IHttpActionResult DeleteShortcut(string shortcutId)
        {
            _shortcutService.DeleteShortcut(shortcutId);
            return Ok();
        }

        [HttpPut]
        [Route("api/v1/shortcut/{shortcutId}")]
        public IHttpActionResult UpdateShortcut(string shortcutId, ShortcutDto shortcut)
        {
            if (string.IsNullOrEmpty(shortcutId))
            {
                throw new ArgumentNullException("shortcutId");
            }

            var updated = _shortcutService.UpdateShortcut(shortcut);
            return Ok(updated);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCaseId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/getEMRItems")]
        public IHttpActionResult GetEMRItems(string patientCaseId, string type)
        {
            var result = _consultationService.GetEMRItemSuper(patientCaseId, type);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/specialistrequests")]
        public async Task<IHttpActionResult> GetSpecialistRequests([FromUri]string query)
        {
            var user = base.CurrentUser();
            var searchCriteria = JsonConvert.DeserializeObject<ConsultationRequestSearchCriteriaDto>(query);
            var result = await _consultationService.SearchSpecialistRequests(searchCriteria, user.Language);
            return Ok(result);
        }


        [HttpGet, Route("api/v1/consultationdetail/{requestId}")]
        public async Task<IHttpActionResult> GetConsultationDetialAsync(string requestId)
        {
            var result = await _consultationService.GetConsultationDetailAsync(requestId);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/newrequest")]
        public IHttpActionResult CreateRequest([FromBody]NewConsultationRequestDto newRequest)
        {
            var user = base.CurrentUser();
            var result = _consultationService.CreateRequest(newRequest, user.Language, user.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportAdvice"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/reoprtadvice")]
        public HttpResponseMessage PutReportAdvice([FromBody]ReportAdviceDto reportAdvice)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(reportAdvice.ConsultationReportID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                _consultationService.UpdateReportAdvice(reportAdvice);
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientHistory"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/patienthistory")]
        public HttpResponseMessage PutPatientHistory([FromBody]PatientHistoryDto patientHistory)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(patientHistory.PatientCaseID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                _consultationService.UpdateCaseHistory(patientHistory);
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clinicalDiagnosis"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/clinicaldiagnosis")]
        public HttpResponseMessage PutClinicalDiagnosis([FromBody]ClinicalDiagnosisDto clinicalDiagnosis)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(clinicalDiagnosis.PatientCaseID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                _consultationService.UpdateClinicalDiagnosis(clinicalDiagnosis);
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/requestdescription")]
        public HttpResponseMessage PutRequestDescription([FromBody]RequestInfomationDto request)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(request.RequestID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                _consultationService.UpdateRequestBaseInfo(request);
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCase"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/patientcasebaseinfo")]
        public HttpResponseMessage PutPatientBaseInfo([FromBody]PatientBaseInfoDto patientCase)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(patientCase.PatientCaseID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                _consultationService.UpdatePatientCaseBaseInfo(patientCase);
                response = new HttpResponseMessage(HttpStatusCode.OK);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/requestchangereason")]
        public HttpResponseMessage PutRequestChangeReason([FromBody]ChangeReasonDto reason)
        {
            HttpResponseMessage response = null;
            var content = "Id cannot be empty.";
            if (string.IsNullOrEmpty(reason.RequestID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                if (_consultationService.UpdateChangeReason(reason))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    response = ResponseUtils.BadRequest(string.Format("The request ID[{0}] doesn't exist.",
                        reason.RequestID));
                }
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/reporthistory/{reportId}")]
        public IHttpActionResult GetReportHistories(string reportId)
        {
            var result = _consultationService.GetReportHistoryByReportID(reportId);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/changereason/{requestId}")]
        public IHttpActionResult GetChangeReason(string requestId)
        {
            if (!string.IsNullOrEmpty(requestId))
            {
                BadRequest();
            }

            var result = _consultationService.GetChangeReason(requestId);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/selectconsulthospitals")]
        public IHttpActionResult GetConsultHospitals(string query)
        {
            var searchCriteria = JsonConvert.DeserializeObject<ConsultHospitalSearchCriteriaDto>(query);
            return Ok(_consultationService.GetConsultHospitals(searchCriteria));
        }

        [HttpGet, Route("api/v1/selectconsultarea")]
        public IHttpActionResult GetConsultArea()
        {
            return Ok(_consultationService.GetConsultArea());
        }

        [HttpPost]
        [Route("api/v1/consultation/acceptrequest")]
        public IHttpActionResult AcceptRequest([FromBody]RequestAcceptInfoDto requestAcceptInfoDto)
        {
            var user = base.CurrentUser();
            return Ok(_consultationService.AcceptRequest(requestAcceptInfoDto, user.Language));
        }

        [HttpPut]
        [Route("api/v1/consultation/acceptrequestinfo")]
        public IHttpActionResult PutAcceptRequest([FromBody]RequestAcceptInfoDto requestAcceptInfoDto)
        {
            var user = base.CurrentUser();
            return Ok(_consultationService.UpdateAcceptRequest(requestAcceptInfoDto, user.Language));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportAdvice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/consultation/createreoprtadvice")]
        public IHttpActionResult CreateReportAdvice([FromBody]ReportAdviceDto reportAdvice)
        {
            string consultationReportID = Guid.NewGuid().ToString();
            reportAdvice.ConsultationReportID = consultationReportID;
            _consultationService.UpdateReportAdvice(reportAdvice);
            return Ok(consultationReportID);
        }

        [HttpGet]
        [Route("api/v1/consultation/gettemplatebyparentid/{id}")]
        public IHttpActionResult GetTemplateByParentID(string id)
        {
            var user = base.CurrentUser();
            if (string.IsNullOrEmpty(id) || id == "undefined")
            {
                //
                var result = _consultationService.GetReportTemplateNodes(id, user.UniqueID, user.Site);
                foreach (ConsultatReportTemplateDirecDto reportTemplateDirecDto in result)
                {
                    var children = _consultationService.GetReportTemplateNodes(reportTemplateDirecDto.UniqueID, user.UniqueID, user.Site);
                    if (children != null && children.Count() > 0)
                    {
                        reportTemplateDirecDto.Leaf = 0;
                    }
                    else
                    {
                        reportTemplateDirecDto.Leaf = 1;
                    }
                }
                var templateList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.ItemName,
                        HasChildren = true,
                        enabled = c.Leaf.Value == 1 ? false : true
                    }).ToList();


                return Ok(templateList);

            }
            else
            {
                var result = _consultationService.GetReportTemplateNodes(id, user.UniqueID, user.Site);
                if (result != null)
                {
                    var templateList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.ItemName,
                        HasChildren = c.Leaf != null ? (c.Leaf.Value == 1 ? false : true) : true
                    }).ToList();
                    return Ok(templateList);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// Get report template data by id
        /// </summary>
        /// <param name="id">report template id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/consultation/getreporttemplate/{id}")]
        public IHttpActionResult GetReportTemplate(string id)
        {
            var result = _consultationService.GetReportTemplateDirecByID(id);
            if (result != null)
            {
                return Ok(result.ReportTemplateDto);
            }
            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receive"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/consultation/requestreceive")]
        public HttpResponseMessage UpdateRequestReceive([FromBody]RequestReceiverDto receive)
        {
            HttpResponseMessage response = null;
            var content = "ID cannot be empty.";
            var user = base.CurrentUser();
            if (string.IsNullOrEmpty(receive.RequestID))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                if (_consultationService.UpdateRequestReveive(receive, user.Language))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    response = ResponseUtils.BadRequest(string.Format("The request ID[{0}] doesn't exist.",
                        receive.RequestID));
                }
            }

            return response;
        }

        [HttpGet]
        [Route("api/v1/consultation/completerequest/{id}")]
        public IHttpActionResult CompleteRequest(string id)
        {
            var result = _consultationService.CompleteRequest(id);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/infoforacceptrequest/{requestId}")]
        public IHttpActionResult GetInfoForAcceptRequest(string requestId)
        {
            var result = _consultationService.GetInfoForAcceptRequest(requestId);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/assigns/{requestId}")]
        public IHttpActionResult GetConsultationAssigns(string requestId)
        {
            var result = _consultationService.GetConsultationAssigns(requestId);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/meetings")]
        public IHttpActionResult GetMeetings()
        {
            var user = base.CurrentUser();
            var result = _consultationService.GetMeetings(user.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/users/{userID}/roles")]
        public async Task<IHttpActionResult> GetRisUserRolesAsync(string userID)
        {
            return Response(await _userManagementService.GetUserRolesAsync(userID));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/v1/consultation/users/{userID}/domain/{domain}/defaultRole")]
        public async Task<IHttpActionResult> UpdateRisUserDefaultRoleAsync(string userID, string domain, Dictionary<string, string> data)
        {
            return Response(await _userManagementService.UpdateRisUserDefaultRoleAsync(userID, domain, data));
        }

        [HttpGet, Route("api/v1/consultation/expertadvices/{requestid}")]
        public IHttpActionResult GetExpertAdvices(string requestid)
        {
            var result = _consultationService.GetExpertAdvices(requestid);
            return Ok(result);
        }

        [HttpPost, Route("api/v1/consultation/expertadvices")]
        public IHttpActionResult SaveExpertAdvices([FromBody]ConsultationAssignDto consultationAssignDto)
        {
            var result = _consultationService.SaveExpertAdvices(consultationAssignDto);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/advicereport/{requestid}")]
        public IHttpActionResult GetAdviceReport(string requestid)
        {
            var result = _consultationService.GetAdviceReport(requestid);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/expertadvicereport/{requestid}")]
        public IHttpActionResult GetExpertAdviceReport(string requestid)
        {
            var result = _consultationService.GetExpertAdviceReport(requestid, "");
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/hostadvicereport/{requestid}")]
        public IHttpActionResult GetHostAdviceReport(string requestid)
        {
            var result = _consultationService.GetHostAdviceReport(requestid);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/ishost/{requestid}")]
        public IHttpActionResult IsHost(string requestid)
        {
            var result = _consultationService.IsHost(requestid);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/isexpert/{requestid}")]
        public IHttpActionResult IsExpert(string requestid)
        {
            var result = _consultationService.IsExpert(requestid);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/editReportPermission/{requestid}")]
        public IHttpActionResult EditReportPermission(string requestid)
        {
            var result = _consultationService.EditReportPermission(requestid);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/vnc")]
        public IHttpActionResult GetVNC()
        {
            var user = base.CurrentUser();
            var result = _consultationService.GetVNCUrl(user.UniqueID);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/updatemeetingstatus")]
        public IHttpActionResult UpdateMeetingStatus(string requestId, string confKey, string hostName, string meetingPassword)
        {
            var result = _consultationService.UpdateMeetingStatus(requestId, confKey, hostName, meetingPassword);
            return Ok(result);
        }

        /// <summary>
        /// delete request
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPut, Route("api/v1/consultation/requestdelete")]
        public IHttpActionResult DeleteRquest([FromBody]RequestDeleteReason reason)
        {
            var result = _consultationService.DeleteRequest(reason);
            return Ok(result);
        }

        /// <summary>
        /// recover request
        /// </summary>
        /// <param name="requestid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/requestrecover/{requestid}")]
        public HttpResponseMessage RecoverRequest(string requestid)
        {
            HttpResponseMessage response = null;
            if (string.IsNullOrEmpty(requestid))
            {
                response = ResponseUtils.BadRequest(ContentMessage);
            }
            else
            {
                response = _consultationService.RecoverRequest(requestid) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    ResponseUtils.BadRequest(string.Format("The request ID[{0}] doesn't exist.", requestid));
            }

            return response;
        }

        /// <summary>
        /// delete patient case
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPut, Route("api/v1/consultation/patientcasedelete")]
        public IHttpActionResult DeletePatientCase([FromBody]PatientCaseDeleteDto reason)
        {
            var result = _consultationService.DeletePatientCase(reason);
            return Ok(result);
        }

        /// <summary>
        /// Recover patient case
        /// </summary>
        /// <param name="patientCaseId"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/patientcaserecover/{patientCaseId}")]
        public HttpResponseMessage RecoverPatientCase(string patientCaseId)
        {
            HttpResponseMessage response = null;
            const string content = "patientCase id cannot be empty.";
            if (string.IsNullOrEmpty(patientCaseId))
            {
                response = ResponseUtils.BadRequest(content);
            }
            else
            {
                response = _consultationService.RecoverPatientCase(patientCaseId) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    ResponseUtils.BadRequest(string.Format("The patientCase ID[{0}] doesn't exist.", patientCaseId));
            }

            return response;
        }

        [HttpGet, Route("api/v1/consultation/usersetting")]
        public IHttpActionResult GetUserSetting(string roleid, string userid, int type)
        {
            var result = _consultationService.GetUserSetting(roleid, userid, type);
            return Ok(result);
        }

        [HttpGet, Route("api/v1/consultation/usersettingasync")]
        public async Task<IHttpActionResult> GetUserSettingAsync(string roleid, string userid, int type)
        {
            return Ok(await _consultationService.GetUserSettingAsync(roleid, userid, type));
        }

        [HttpPost, Route("api/v1/consultation/usersetting")]
        public IHttpActionResult SaveUserSetting([FromBody]UserSettingDto userSettingDto)
        {
            var result = _consultationService.SaveUserSetting(userSettingDto);
            return Ok(result);
        }

        /// <summary>
        /// Update request status 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/request/{requestid}/{status}")]
        public HttpResponseMessage UpdateRequestStatus(string requestid, int status)
        {
            HttpResponseMessage response = null;
            if (string.IsNullOrEmpty(requestid))
            {
                response = ResponseUtils.BadRequest(ContentMessage);
            }
            else
            {
                response = _consultationService.UpdateRequestStatus(requestid, status) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    ResponseUtils.BadRequest(string.Format("The request ID[{0}] doesn't exist.", requestid));
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        /// <returns></returns>
        [HttpGet, Route("api/v1/consultation/requeststatistics"), AllowAnonymous]
        public async Task<IHttpActionResult> ConsultationRequestStatistics(DateTime fromDateTime, DateTime toDateTime)
        {
            var requests = await _consultationService.ConsultationRequestStatistics(fromDateTime, toDateTime);
            return Ok(requests);
        }
    

    }
}
