using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.Consultation.Application.Services;
using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hys.CrossCutting.Common.Utils;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Consultation Web Api 
    /// </summary>
    [RoutePrefix("api/v1/patientcase")]
    public class ConsultationPatientCaseController : ApiControllerBase
    {
        private readonly IConsultationPatientCaseService _consultationPatientCaseService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultationService"></param>
        public ConsultationPatientCaseController(IConsultationPatientCaseService consultationPatientCaseService)
        {
            _consultationPatientCaseService = consultationPatientCaseService;
        }

        [HttpPost]
        [Route("newpatientcase")]
        public IHttpActionResult CreatePatientCase([FromBody]PatientCaseInfoDto patientCaseDto)
        {
            var user = base.CurrentUser();
            var defaultModules = (Dictionary<string, IEnumerable<ExamModuleDto>>)HttpContext.Current.Application["examModule"];
            var result = _consultationPatientCaseService.CreatePatientCase(patientCaseDto, defaultModules[user.Language],user.UniqueID,user.LocalName);
            return Ok(result);
        }

        [HttpGet]
        [Route("samepatientcases")]
        public IHttpActionResult GetCombinePatientCaseList(string patientId, string identityCard)
        {
            var result = _consultationPatientCaseService.GetCombinePatientCaseList(patientId, identityCard);
            return Ok(result);
        }

        [HttpGet]
        [Route("samepatientcasesasync")]
        public async Task<IHttpActionResult> GetCombinePatientCaseListAsync([FromUri]string query)
        {
            var combinePatientCase = JsonConvert.DeserializeObject<CombinePatientCaseDto>(query);
            return Response(await _consultationPatientCaseService.GetCombinePatientCaseListAsync(combinePatientCase));
        }

        [HttpPost]
        [Route("combinepatientcase")]
        public IHttpActionResult CombinePatientCase([FromBody]PatientCaseCombineDto patientCaseCombineDto)
        {
            var result = _consultationPatientCaseService.CombinePatientCase(patientCaseCombineDto);
            return Ok(result);
        }

        [HttpGet]
        [Route("patientcasenoitems/{id}")]
        public IHttpActionResult GetPatientCaseNoItems(string id)
        {
            var result = _consultationPatientCaseService.GetPatientCaseNoItems(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("editpatientcase")]
        public IHttpActionResult EditPatientCase([FromBody]PatientCaseEditInfoDto patientCaseDto)
        {
            var user = base.CurrentUser();
            var result = _consultationPatientCaseService.EditPatientCase(patientCaseDto,user.UniqueID);
            return Ok(result);
        }

        [HttpGet]
        [Route("examinfodeletefile")]
        public IHttpActionResult ExamInfoDeleteFile(string pid, string fid)
        {
            var result = _consultationPatientCaseService.ExamInfoDeleteFile(pid, fid);
            return Ok(result);
        }

        [HttpGet]
        [Route("examinfodeleteitem")]
        public IHttpActionResult ExamInfoDeleteItem(string pid, string itemid)
        {
            var result = _consultationPatientCaseService.ExamInfoDeleteItem(pid, itemid);
            return Ok(result);
        }

        [HttpGet]
        [Route("examinfofilenamechanged")]
        public IHttpActionResult ExamInfoFileNameChanged(string pid, string fid, string fname)
        {
            var user = base.CurrentUser();
            var result = _consultationPatientCaseService.ExamInfoFileNameChanged(pid, fid, fname,user.UniqueID);
            return Ok(result);
        }

        [HttpPost]
        [Route("examinfoitemadded")]
        public IHttpActionResult ExamInfoItemAdded([FromBody]PatientCaseInfoDto patientCaseInfoDto)
        {
            var user = base.CurrentUser();
            var result = _consultationPatientCaseService.ExamInfoItemAdded(patientCaseInfoDto,user.UniqueID);
            return Ok(result);
        }

        [HttpPost]
        [Route("examinfoitemedited")]
        public IHttpActionResult ExamInfoItemEdited([FromBody]PatientCaseInfoDto patientCaseInfoDto)
        {
            var user = base.CurrentUser();
            var result = _consultationPatientCaseService.ExamInfoItemEdited(patientCaseInfoDto, user.UniqueID);
            return Ok(result);
        }

        [HttpGet]
        [Route("reuploadpatientcase/{id}")]
        public IHttpActionResult ReUploadPatientCase(string id)
        {
            var result = _consultationPatientCaseService.ReUploadPatientCase(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("reuploadexamitem/{id}")]
        public IHttpActionResult ReUploadExamItem(string id)
        {
            var result = _consultationPatientCaseService.ReUploadExamItem(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("reuploadfileitem/{id}")]
        public IHttpActionResult ReUploadFileItem(string id)
        {
            var result = _consultationPatientCaseService.ReUploadFileItem(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("dicomrelations/{userid}")]
        [AllowAnonymous]
        public IHttpActionResult DICOMPatientCaseRelations([FromBody]List<string> dicoms, string userid)
        {
            var result = _consultationPatientCaseService.DICOMPatientCaseRelations(dicoms, userid);
            return Ok(result);
        }

        /// <summary>
        /// Get case info by procedureId or orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("caseInfo/{id}")]
        public IHttpActionResult GetCaseInfoFromRis(string id)
        {
            var result = _consultationPatientCaseService.GetCaseInfoFromRis(id);
            return Ok(result);
        }

        /// <summary>
        /// Get consultation result by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet, Route("consultationResult/{orderId}")]
        public IHttpActionResult GetConsultationResult(string orderId)
        {
            var result = _consultationPatientCaseService.GetConsultationResult(orderId);
            return Ok(result);
        }
    }
}
