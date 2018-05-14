using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.V2;
using Hys.CareRIS.WebApi.Utils;
using System.Collections.Generic;
using Hys.CareRIS.Domain.Entities;
using WebApi.OutputCache.V2.TimeAttributes;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Registration Web Api
    /// </summary>
    [RoutePrefix("api/v1/registration")]
    [AutoInvalidateCacheOutput]
    public class RegistrationController : ApiControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IWorklistService _workListService;
        private readonly ICommonService _ProcedurecodeService;

        /// <summary>
        /// Registration controller constructor
        /// </summary>
        /// <param name="registrationService"></param>
        /// <param name="workListService"></param>
        /// <param name="procedurecodeService"></param>
        public RegistrationController(IRegistrationService registrationService, IWorklistService workListService,
            ICommonService procedurecodeService)
        {
            _registrationService = registrationService;
            _workListService = workListService;
            _ProcedurecodeService = procedurecodeService;
        }

        private string HISConServiceURL
        {
            get
            {
                return ConfigurationManager.AppSettings["HISConServiceURL"];
            }
        }

        /// <summary>
        /// Get patient data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("patients")]
        public IHttpActionResult GetPatients()
        {
            var result = _registrationService.GetPatients();
            return Ok(result);
        }

        /// <summary>
        /// Get patient data by patientName.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("patients/search")]
        public IHttpActionResult GetPatientsByName(string patientName)
        {
            var result = _registrationService.GetPatientsByName(patientName);
            return Ok(result);
        }

        /// <summary>
        /// Get patient data by id
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("patients/{id}")]
        public IHttpActionResult GetPatient(string id)
        {
            var result = _registrationService.GetPatient(id);
            return Ok(result);
        }

        /// <summary>
        /// New patient data.
        /// </summary>
        /// <param name="patientDto">patinet data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("patients")]
        public IHttpActionResult PostPatient([FromBody]PatientEditDto patientEdit)
        {
            var patient = _registrationService.AddPatient(patientEdit);
            return Created("", patient);
        }

        /// <summary>
        /// Update patient data
        /// </summary>
        /// <param name="id">patinet id</param>
        /// <param name="patientDto">patient data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("patients/{id}")]
        public IHttpActionResult PutPatient(string id, [FromBody]PatientEditDto patientEdit)
        {
            var patient = _registrationService.UpdatePatient(patientEdit);
            return Ok(patient);
        }

        /// <summary>
        /// Delete patient data
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("patients/{id}")]
        public IHttpActionResult DeletePatient(string id)
        {
            var patient = _registrationService.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            _registrationService.DeletePatient(id);
            return Ok();
        }

        /// <summary>
        /// Get patient's procedures with report info.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("patients/procedures/{id}")]
        public IHttpActionResult GetProcedures(string id, string orderID = null)
        {
            var result = _registrationService.GetProcedures(id, orderID);
            return Ok(result);
        }

        #region Orders API
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("orders")]
        public IHttpActionResult GetAllOrders()
        {
            var result = _registrationService.GetAllOrders();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/{id}")]
        public IHttpActionResult GetOrder(string id)
        {
            var result = _registrationService.GetOrder(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        /// <summary>
        /// Insert new order data.
        /// </summary>
        /// <param name="orderDto">order data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders")]
        public IHttpActionResult PostOrder([FromBody]OrderDto orderDto)
        {
            if (orderDto.UniqueID.Trim() == string.Empty)
            {
                orderDto.UniqueID = Guid.NewGuid().ToString();
            }
            _registrationService.AddOrder(orderDto);

            var order = _registrationService.GetOrder(orderDto.UniqueID);
            return Created("", order);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/generate/patientNo/{site}")]
        public IHttpActionResult GetPatientNo(string site)
        {
            string accNo = _registrationService.GetPatientNo(site);
            return Ok(accNo);
        }

        /// <summary>
        /// Insert new order data.
        /// </summary>
        /// <param name="registration">new registration</param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders/newRegistration")]
        public IHttpActionResult AddNewRegistration([FromBody]RegistrationDto registration)
        {
            var user = base.CurrentUser();
            var result = _registrationService.AddNewRegistration(registration, user.Domain, user.LoginName, user.Site);
            return Created("", result);
        }

        /// <summary>
        /// Transfer Registration.
        /// </summary>
        /// <param name="registration">transfer registration</param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders/transferRegistration")]
        public IHttpActionResult TransferRegistration([FromBody]List<RegistrationDto> registrations)
        {
            var user = base.CurrentUser();
            var result = _registrationService.TransferRegistration(registrations, user.Domain, user.LoginName, user.Site);
            return Created("", result);
        }

        /// <summary>
        /// Get Registration Info
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{orderId}")]
        public IHttpActionResult GetRegistrationInfo(string orderId)
        {
            var result = _registrationService.GetRegistrationInfo(orderId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// 中文转拼音
        /// 业务背景：
        /// 就是在界面上输入病人姓名，那么系统
        /// 会自动把用户输入的姓名由中文转为拼音。
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("orders/simplifiedToEnglish")]
        public IHttpActionResult SimplifiedToEnglish([FromBody]SimplifyEnglishDto simplify)
        {
            try
            {
                return Ok(_registrationService.SimplifiedToEnglish(simplify));
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }

        /// <summary>
        /// Update order data
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="orderDto">order data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("orders/{id}")]
        public IHttpActionResult PutOrder(string id, [FromBody]OrderDto orderDto)
        {
            _registrationService.UpdateOrder(orderDto, true);
            var order = _registrationService.GetOrder(orderDto.UniqueID);
            return Ok(order);
        }

        /// <summary>
        /// Delete order data
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("orders/{id}")]
        public IHttpActionResult DeleteOrder(string id)
        {
            var order = _registrationService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            _registrationService.DeleteOrder(id);
            return Ok();
        }

        /// <summary>
        /// Get Requisition Url
        /// </summary>
        /// <param name="accNo"></param>
        /// <param name="modalityType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/requisition/url")]
        public IHttpActionResult GetRequisitionUrl(string accNo, string modalityType)
        {
            try
            {
                var user = base.CurrentUser();
                var pdfService = user.PdfService;
                var result = _registrationService.GetRequisitionUrl(accNo, modalityType, pdfService);
                if (!String.IsNullOrEmpty(result))
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Bar Code Url
        /// </summary>
        /// <param name="accNo"></param>
        /// <param name="modalityType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/barcode/url")]
        public IHttpActionResult GetBarCodeUrl(string accNo, string modalityType)
        {
            var user = base.CurrentUser();
            var pdfService = user.PdfService;
            var result = _registrationService.GetBarCodeUrl(accNo, modalityType, pdfService);
            if (!String.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get procedures via order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("orders/procedures/{orderId}")]
        public IHttpActionResult GetProceduresByOrderID(string orderId)
        {
            var result = _registrationService.GetProceduresByOrderID(orderId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region Procedures API

        /// <summary>
        /// Get all Procedures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("procedures")]
        public IHttpActionResult GetAllProcedures()
        {
            var result = _registrationService.GetAllProcedures();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        /// <summary>
        /// Get procedure by id
        /// </summary>
        /// <param name="id">patientId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("procedures/{id}")]
        public IHttpActionResult GetProcedure(string id)
        {
            var result = _registrationService.GetProcedure(id);
            return Ok(result);
        }

        /// <summary>
        /// Insert new procedure data.
        /// </summary>
        /// <param name="procedureDto">procedure data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("procedures")]
        public IHttpActionResult PostProcedure([FromBody]ProcedureInsertDto procedureInsert)
        {
            if (procedureInsert != null)
            {
                var result = _registrationService.AddProcedure(procedureInsert);
                return Created("", result);
            }
            else
            {
                return InternalServerError(new ArgumentNullException());
            }
        }

        /// <summary>
        /// Update procedure data
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <param name="procedureDto">procedure data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("procedures/{id}")]
        public IHttpActionResult PutProcedure(string id, [FromBody]ProcedureDto procedureUpdate)
        {
            var user = base.CurrentUser();
            var procedure = _registrationService.UpdateProcedure(id, procedureUpdate, user.UniqueID);
            if (procedure == null)
            {
                return InternalServerError(new NullReferenceException());
            }
            return Ok(procedure);
        }

        /// <summary>
        /// finish exam
        /// </summary>
        /// <param name="id">order id</param>
        /// <param name="orderDto">order data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("finishexam/{id}")]
        public IHttpActionResult FinishExam(string id, [FromBody]OrderDto orderDto)
        {
            var user = base.CurrentUser();
            _registrationService.FinishExam(id, orderDto.ExamSite, orderDto.ExamDomain, orderDto.ExamAccNo, user.UniqueID, user.LoginName);

            return Ok(true);
        }

        /// <summary>
        /// Delete procedure data
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("procedures/{id}")]
        public IHttpActionResult DeleteProcedure(string id)
        {
            var user = base.CurrentUser();
            var result = _registrationService.DeleteProcedure(id, user.UniqueID, true);
            return Ok(result);
        }

        /// <summary>
        /// Update procedure BookingBeginTime and BookingEndTime
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("procedures/slice/{orderId}")]
        public IHttpActionResult UpdateProcedureSlice(string orderId, SliceDto slice)
        {
            _registrationService.UpdateProcedureSlice(orderId, slice);
            return Ok(true);
        }
        #endregion

        #region ProcedureCode
        /// <summary>
        /// Get procedurecodes data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("procedureCodes/{site}")]
        //[CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetProcedureCodes(string site)
        {
            var result = _registrationService.GetProcedureCodes(site);
            return Ok(result);
        }

        /// <summary>
        /// Get procedurecode by code.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("procedureCodes/code/")]
        public IHttpActionResult GetProcedureCodeByCode(string code, string modality)
        {
            var user = base.CurrentUser();
            var result = _registrationService.GetProcedureByCode(code, modality, user.Domain);
            return Ok(result);
        }
        #endregion

        #region BodySystemMap
        /// <summary>
        /// Get BodySystemMaps data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("bodySystemMaps/{site}")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetBodySystemMaps(string site)
        {
            var result = _registrationService.GetBodySystemMaps(site);
            return Ok(result);
        }

        /// <summary>
        /// Get BodySystemMaps text.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("bodySystemMapsText/{site}")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetBodySystemMapsText(string site)
        {
            var result = _registrationService.GetBodySystemMaps(site);
            if (result != null)
            {
                var checkingItemList = result.Select(c =>
                   new
                   {
                       Text = c.ExamSystem,

                   }).Distinct().ToList();
                return Ok(checkingItemList);
            }
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("bodySystemMapText")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetBodySystemMapText()
        {
            var user = base.CurrentUser();
            var result = _registrationService.GetBodySystemMaps(user.Site);

            if (result != null)
            {

                var procedureCodeList = result.Select(c =>
                    new
                    {
                        Text = c.BodyPart,

                    }).Distinct().ToList();
                return Ok(result);
            }
            return Ok(result);
        }

        #endregion

        #region Intergration
        [HttpGet]
        [Route("intergration/requestInfo")]
        public async Task<IHttpActionResult> GetRequestInfo(string cardNumber, string cardType)
        {
            var user = base.CurrentUser();
            var result = await _registrationService.GetRequestInfo(cardNumber, cardType, HISConServiceURL, user.Domain, user.Site);
            return Ok(result);
        }

        [HttpGet]
        [Route("intergration/patient")]
        public IHttpActionResult GetIntergrationPatientInfo(string cardNumber, string cardType = "")
        {
            var result = _registrationService.GetIntergrationPatientInfo(cardNumber, cardType, HISConServiceURL);
            return Ok(result);
        }

        [HttpGet]
        [Route("intergration/similarPatient")]
        public IHttpActionResult GetSimilarPatient(string globalID, string risPatientID, string hisID, string patientName,
            string matchKey = null, string matchValue = null)
        {
            var user = base.CurrentUser();
            var result = _registrationService.GetSimilarPatient(globalID, risPatientID, hisID, patientName, user.Site);
            return Ok(result);
        }

        [HttpPost]
        [Route("intergration/processRequests")]
        public IHttpActionResult ProcessReuqestInfo(List<RequestDto> requests)
        {
            var user = base.CurrentUser();
            var result = _registrationService.ProcessReuqestInfo(requests, user.Domain, user.Site);
            return Ok(result);
        }
        #endregion

        #region Requisition

        [HttpPost]
        [Route("requisition/image")]
        public IHttpActionResult SaveTempImage([FromBody]ImageData imgData)
        {
            var user = base.CurrentUser();
            var result = _registrationService.SaveTempImage(imgData, user.LoginName);
            return Ok(result);
        }

        [HttpDelete]
        [Route("requisition/image")]
        public IHttpActionResult DeleteImage(string fileName, string relativePath = null, string requisitionID = null)
        {
            var user = base.CurrentUser();
            var result = _registrationService.DeleteImage(fileName, relativePath, requisitionID, user.Domain, user.LoginName);
            return Ok(result);
        }

        [HttpGet]
        [Route("requisition/image/erNo")]
        public IHttpActionResult GenerateErNo()
        {
            var user = base.CurrentUser();
            var result = _registrationService.GenerateERNo(user.Site);
            return Ok(result);
        }

        [HttpGet]
        [Route("requisition/image/{accNo}")]
        public IHttpActionResult DownLoadRequisitionFiles(string accNo)
        {
            var user = base.CurrentUser();
            var result = _registrationService.DownLoadRequisitionFiles(accNo, user.Domain, user.LoginName);
            return Ok(result);
        }



        [HttpPost]
        [Route("requisition/order/image")]
        public IHttpActionResult ProcessRequisitionInOrder([FromBody]RequisitionLiteDto resuitionLite)
        {
            var user = base.CurrentUser();
            var result = _registrationService.ProcessRequisitionInOrder(resuitionLite.AccNo, resuitionLite.ErNo, resuitionLite.RelativePath, resuitionLite.ImageQualityLevel, user.LoginName, user.Domain);
            return Ok(result);
        }

        [HttpDelete]
        [Route("requisition/order/image/{erNo}")]
        public IHttpActionResult ClearTempFile(string erNo)
        {
            var user = base.CurrentUser();
            _registrationService.ClearTempFile(erNo, user.LoginName);
            return Ok(true);
        }

        [HttpGet]
        [Route("requisition/ftp")]
        public IHttpActionResult ValidateFTP()
        {
            var user = base.CurrentUser();
            var result = _registrationService.ValidateFTP(user.Domain);
            return Ok(result);

        }
        #endregion

        #region booking
        [HttpGet]
        [Route("booking/modalities")]
        public async Task<IHttpActionResult> GetModalities(string modalityType, string site)
        {
            var result = await _registrationService.GetBookingModalities(modalityType, site);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modality"></param>
        /// <param name="date"></param>
        /// <param name="site"></param>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <param name="viewType">it's value is "day" or "week"</param>
        /// <returns></returns>
        [HttpGet]
        [Route("booking/modalitytimeslice")]
        public async Task<IHttpActionResult> GetModalityTimeSlice(string modality, DateTime startDate, DateTime endDate, string site, string userId, string role)
        {
            IEnumerable<ModalityTimeSliceDto> result = new List<ModalityTimeSliceDto>();

            for (var day = startDate; day <= endDate; day = day.AddDays(1))
            {
                var tmpRes = await _registrationService.GetModalitySchedule(modality, day, site, userId, role);
                result = result.Concat(tmpRes);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("booking/locktimeslice")]
        public async Task<IHttpActionResult> LockModalityQuota([FromBody]ModalityTimeSliceDto timeSlice)
        {
            var user = base.CurrentUser();
            var lockId = await _registrationService.LockModalityQuota(timeSlice, user.Site);
            return Ok(lockId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("booking/unlocktimeslice")]
        public async Task<IHttpActionResult> UnlockModalityQuota([FromBody]JObject parameters)
        {
            var unlockGuid = parameters["unlockGuid"].ToObject<string>();
            var modality = parameters["modality"].ToObject<string>();
            var start = parameters["start"].ToObject<DateTime?>();
            var end = parameters["end"].ToObject<DateTime?>();
            var site = parameters["site"].ToObject<string>();
            await _registrationService.UnlockModalityQuota(unlockGuid, modality, start, end, site);
            return Ok();
        }

        /// <summary>
        /// Transfer booking to registration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("booking/bookingToRegistration/{id}")]
        public IHttpActionResult TransferBooking2Registration(string id)
        {
            var user = base.CurrentUser();
            _registrationService.TransferBooking2Registration(id, user.UniqueID, user.LoginName);
            return Ok(true);
        }
        #endregion

        [AllowAnonymous]
        [HttpDelete]
        [Route("cache/elimination")]
        public void ClearCache()
        {
            // do nothing here, if someone called this method, all cache will be cleared in this controller
        }
    }
}
