using C1.C1Report;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.V2;
using Hys.CareRIS.Application.Dtos.Report;
using System.Net.Http;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.WebApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/v1/report")]
    public class ReportController : ApiControllerBase
    {
        private readonly IReportService _ReportService;
        private readonly IReportPrintService _ReportPrintService;
        private readonly IReportLockService _ReportLockService;
        private readonly IReportTemplateService _ReportTemplateService;
        private readonly IRegistrationService _RegistrationService;
        private readonly IConfigurationService _ConfigurationService;
        private readonly IUserManagementService _userManagementService;
        private readonly ICommonService _CommonService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportService"></param>
        public ReportController(IReportService reportService, IReportPrintService reportPrintService,
            IRegistrationService registrationService, IConfigurationService configurationService, IUserManagementService userManagementService,
            ICommonService commonService, IReportLockService reportLockService, IReportTemplateService reportTemplateService
            )
        {
            _ReportService = reportService;
            _ReportPrintService = reportPrintService;
            _ReportLockService = reportLockService;
            _ReportTemplateService = reportTemplateService;
            _RegistrationService = registrationService;
            _ConfigurationService = configurationService;
            _userManagementService = userManagementService;
            _CommonService = commonService;
        }

        /// <summary>
        /// Get report data by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("reports/{id}")]
        public IHttpActionResult GetReport(string id)
        {
            var result = _ReportService.GetReport(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get report data by procedure id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("reportbyprocedureid/{id}")]
        public IHttpActionResult GetReportByProcedureID(string id)
        {
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (procedureDto != null)
            {
                var result = _ReportService.GetReport(procedureDto.ReportID);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Get procudure data by orderid
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getproceduresbyorderid/{id}")]
        public IHttpActionResult GetProceduresByOrderID(string id)
        {
            return Ok(_ReportService.GetProcedureIDsByOrderID(id));
        }

        /// <summary>
        /// Get procudure data by orderid
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getexamedproceduresbyorderid/{id}")]
        public IHttpActionResult GetExamedProceduresByOrderID(string id)
        {
            return Ok(_ReportService.GetExamedProcedureIDsByOrderID(id));
        }

        /// <summary>
        /// Get procudure data by reportid
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getproceduresbyreportid/{id}")]
        public IHttpActionResult GetProceduresByReportID(string id)
        {
            return Ok(_ReportService.GetProcedureIDsByReportID(id));
        }
        #region base Info
        /// <summary>
        /// Get baseInfo data by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("baseinfobyorderid/{id}")]
        public IHttpActionResult GetBaseInfoByOrderID(string id)
        {
            var user = base.CurrentUser();
            string content = _ReportPrintService.GetBaseInfoByOrderID(id, user.Domain, user.Site);

            if (content != null)
            {
                return Ok(content);
            }

            return NotFound();
        }

        /// <summary>
        /// Get baseInfo data by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("baseinfo/{id}")]
        public IHttpActionResult GetBaseInfoByProcedureID(string id)
        {
            var user = base.CurrentUser();
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            string content = _ReportPrintService.GetBaseInfoByProcedureID(procedureDto, id, user.Domain, user.Site);

            if (content != null)
            {
                return Ok(content);
            }
            return NotFound();
        }

        /// <summary>
        /// Get baseInfo desc data by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("baseinfodesc/{id}")]
        public IHttpActionResult GetBaseInfoDescByProcedureID(string id)
        {
            string baseInfoDesc = "";
            baseInfoDesc = _ReportService.GetBaseInfoDescByProcedureID( id);

            return Ok(baseInfoDesc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("baseinfodescbyorderid/{id}")]
        public IHttpActionResult GetBaseInfoDescByOrderID(string id)
        {
            string baseInfoDesc = "";

            baseInfoDesc = _ReportService.GetBaseInfoDescByOrderID(id);

            return Ok(baseInfoDesc);
        }

        #endregion


        /// <summary>
        /// New report data.
        /// </summary>
        /// <param name="reportDto">patinet data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("reports")]
        public IHttpActionResult PostReport([FromBody]ReportDto reportDto)
        {
            var user = base.CurrentUser();

            var report = _ReportService.CreateReport(reportDto, user.LoginName, user.UniqueID, user.Domain, user.Site);

            //write broker
            //data
            ProcedureDto procedure = _ReportService.GetIntegerProcedureByReportID(reportDto.UniqueID);

            //get order
            OrderDto order = _RegistrationService.GetOrder(procedure.OrderID);
            //get patient
            PatientDto patient = _RegistrationService.GetPatient(order.PatientID);
            //end
            string guid = Guid.NewGuid().ToString();
            _CommonService.WriteBroker(guid, patient, order, procedure, report, 30, 16, 200);
            guid = Guid.NewGuid().ToString();
            if (reportDto.Status == (int)RPStatus.Submit)
            {
                _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 203);
            }
            //FirstApprove
            else if (reportDto.Status == (int)RPStatus.FirstApprove)
            {
                _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 206);
            }

            return Created("", report);
        }



        /// <summary>
        /// Update report data
        /// </summary>
        /// <param name="id">patinet id</param>
        /// <param name="reportDto">report data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reports/{id}")]
        public IHttpActionResult PutReport(string id, [FromBody]ReportDto reportDto)
        {
            var user = base.CurrentUser();
            var report = _ReportService.GetReport(reportDto.UniqueID);
            if (report == null)
            {
                return NotFound();
            }

            //write broker
            //data
            ProcedureDto procedure = _ReportService.GetIntegerProcedureByReportID(reportDto.UniqueID);

            //get order
            OrderDto order = _RegistrationService.GetOrder(procedure.OrderID);
            //get patient
            PatientDto patient = _RegistrationService.GetPatient(order.PatientID);
            string guid = Guid.NewGuid().ToString();

            //save data
            _ReportService.ModifyReport(reportDto, user.LoginName, user.UniqueID, user.Domain, user.Site);


            if (reportDto.DeleteMark.HasValue && reportDto.DeleteMark.Value)
            {
                report = _ReportService.GetReport(reportDto.UniqueID);
                _CommonService.WriteBroker(guid, patient, order, procedure, report, 33, 16, 204, false, user.UniqueID);

                return Ok();
            }
            else
            {
                if (report.Status != reportDto.Status)
                {
                    report = _ReportService.GetReport(reportDto.UniqueID);
                    if (reportDto.Status == (int)RPStatus.Submit)
                    {
                        _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 203);
                    }
                    //FirstApprove
                    else if (reportDto.Status == (int)RPStatus.FirstApprove)
                    {
                        _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 206);
                    }
                    else if (reportDto.Status == (int)RPStatus.SecondApprove)
                    {
                        _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 206);
                    }
                    else if (reportDto.Status == (int)RPStatus.Reject)
                    {
                        _CommonService.WriteBroker(guid, patient, order, procedure, report, 32, 16, 205);
                    }
                    else if (reportDto.Status == (int)RPStatus.Draft)
                    {
                        _CommonService.WriteBroker(guid, patient, order, procedure, report, 30, 16, 200);
                    }
                }

                report = _ReportService.GetReport(reportDto.UniqueID);
            }
            return Ok(report);
        }

        /// <summary>
        /// Delete report data
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("reports/{id}")]
        public IHttpActionResult DeleteReport(string id)
        {
            var report = _ReportService.GetReport(id);
            if (report == null)
            {
                return NotFound();
            }
            _ReportService.DeleteReport(id);
            return Ok();
        }

        /// <summary>
        /// Get report files data by report id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("reportfilesbyreportid/{id}")]
        public IHttpActionResult GetReportFilesByReportID(string id)
        {
            var result = _ReportService.GetReportFilesByReportID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get PrintTemplate data by id
        /// </summary>
        /// <param name="id">PrintTemplate id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("PrintTemplates/{id}")]
        public IHttpActionResult GetPrintTemplate(string id)
        {
            var result = _ReportPrintService.GetPrintTemplate(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get PrintTemplate data by id
        /// </summary>
        /// <param name="id">PrintTemplate id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("printtemplatebycriteria")]
        [BindJson(typeof(PrintTemplateDto), "json")]
        public IHttpActionResult GetPrintTemplateByCriteria(PrintTemplateDto json)
        {
            if (json == null)
            {
                json = new PrintTemplateDto();
            }
            var user = base.CurrentUser();
            var result = _ReportPrintService.GetPrintTemplateByCriteria(json, user.Domain, user.Site);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get PrintTemplate data by id
        /// </summary>
        /// <param name="id">PrintTemplate id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("PrintTemplateFields/{id}")]
        public IHttpActionResult GetPrintTemplateFields(string id)
        {
            var result = _ReportPrintService.GetPrintTemplateFields(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #region Report Template
        [HttpGet]
        [Route("ReportTemplate/{id}")]
        public IHttpActionResult GetReportTemplateDirecByID(string id)
        {
            var result = _ReportTemplateService.GetReportTemplateDirecByID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ReportTemplateNodes/{parentID}/{userID}")]
        public IHttpActionResult GetReportTemplateNodes(string parentID, string userID)
        {
            var result = _ReportTemplateService.GetReportTemplateNodes(parentID, userID, "");
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region lock
        /// <summary>
        /// get lock for report
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlock/{reportID}")]
        public IHttpActionResult GetLockByReportID(string reportID)
        {
            IEnumerable<ProcedureDto> procedures = _ReportService.GetProcedureByReportID(reportID);

            ProcedureDto procedureDto = procedures.ToList()[0];
            var result = _ReportLockService.GetLock(procedureDto.OrderID, LockType.Register);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// get lock for report
        /// 验证order是否锁死
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlockbyorderid/{orderID}")]
        public IHttpActionResult GetLockByorderID(string orderID)
        {
            var result = _ReportLockService.GetLock(orderID, LockType.Register);
            if (result != null)
            {
                return Ok(result);
            }

            return Ok("");
        }
        /// <summary>
        /// 病人的锁
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlockbypatientid/{patientId}")]
        public IHttpActionResult GetLockByPatientId(string patientId)
        {
            var result = _ReportLockService.GetLockByPatientId(patientId);
            return Ok(result);
        }
        /// <summary>
        /// get lock by orderId, any LockType
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlockbyorderidbyanylocktype/{orderID}")]
        public IHttpActionResult GetLockByorderIDByAnyLockType(string orderID)
        {
            var result = _ReportLockService.GetLock(orderID);
            if (result != null)
            {
                return Ok(result);
            }

            return Ok("");
        }
        /// <summary>
        /// add lock for report
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("addlockbyorderid/{orderID}")]
        public IHttpActionResult AddLockByOrderID(string orderID)
        {
            var user = base.CurrentUser();

            var result = _ReportLockService.AddLockByOrderID(orderID,
                user.LoginName, user.UniqueID, user.Domain,
                user.Site, Request.GetClientIp());

            return Ok(result);
        }

        /// <summary>
        /// add lock for report
        /// </summary>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("addlockbyprocedureid/{procedureID}")]
        public IHttpActionResult AddLockByProcedureID(string procedureID)
        {
            var user = base.CurrentUser();

            var result = _ReportLockService.AddLockByProcedureID(procedureID,
                user.LoginName, user.UniqueID, user.Domain,
                user.Site, Request.GetClientIp());

            return Ok(result);
        }

        /// <summary>
        /// add lock for report
        /// </summary>
        /// <param name="procedureID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addlockbyprocedureids")]
        public IHttpActionResult AddLockByProcedureIDs([FromBody]List<string> ids)
        {
            if (ids == null)
            {
                return Ok(true);
            }
            var user = base.CurrentUser();

            var result = true;
            foreach (string id in ids)
            {
                result = _ReportLockService.AddLockByProcedureID(id,
                     user.LoginName, user.UniqueID, user.Domain,
                     user.Site, Request.GetClientIp());
            }

            return Ok(result);
        }

        /// <summary>
        /// add lock for report
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("addlockbyreportid/{reportID}")]
        public IHttpActionResult AddLockByReportID(string reportID)
        {
            var user = base.CurrentUser();
            var result = _ReportLockService.AddLockByReportID(reportID,
                                user.LoginName, user.UniqueID, user.Domain,
                                user.Site, Request.GetClientIp());

            return Ok(result);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletelockbyorderid/{orderID}")]
        public IHttpActionResult DeleteLockByOrderID(string orderID)
        {
            var user = base.CurrentUser();
            _ReportLockService.DeleteLock(orderID, LockType.Register, user.UniqueID);

            return Ok(true);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletelockbyprocedureid/{procedureID}")]
        public IHttpActionResult DeleteLockByProcedureID(string procedureID)
        {
            var user = base.CurrentUser();
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(procedureID);
            List<string> procedureIDS = new List<string>();

            //not use ip when unlock
            //string ownerIP = ReportUtils.GetMyIP();
            procedureIDS.Add(procedureID);
            _ReportLockService.DeleteLock(procedureDto.OrderID, procedureIDS, LockType.Register, user.UniqueID);

            return Ok(true);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("deletelockbyprocedureids")]
        public IHttpActionResult DeleteLockByProcedureIDs([FromBody]List<string> ids)
        {
            if (ids == null)
            {
                return Ok(true);
            }

            var user = base.CurrentUser();
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(ids[0]);
            List<string> procedureIDS = new List<string>();

            _ReportLockService.DeleteLock(procedureDto.OrderID, ids, LockType.Register, user.UniqueID);

            return Ok(true);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletelockbyreportid/{reportID}")]
        public IHttpActionResult DeleteLockByReportID(string reportID)
        {
            var user = base.CurrentUser();
            _ReportLockService.DeleteLockByReportID(reportID, LockType.Register, user.UniqueID);

            return Ok(true);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletelockbyuserid")]
        public IHttpActionResult DeleteLockByUserID()
        {
            var user = base.CurrentUser();
            _ReportLockService.DeleteLockByUserID(LockType.Register, user.UniqueID);

            return Ok(true);
        }
        #endregion


        /// <summary>
        /// Get user for reject
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("userforreportbyparentid/{id}")]
        public IHttpActionResult GetAllUserByParentID(string id)
        {
            if (string.IsNullOrEmpty(id) || id == "undefined")
            {

                var result = _ReportService.GetAllRoleForReport();
                if (result != null)
                {
                    var userList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.RoleName,
                        HasChildren = true
                    }).ToList();
                    return Ok(userList);
                }
                return NotFound();

            }
            else
            {
                var result = _ReportService.GetUserForReportByRoleID(id);
                if (result != null)
                {
                    var userList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.LoginName + "(" + c.LocalName + ")",
                        HasChildren = false
                    }).ToList();
                    return Ok(userList);
                }
                return NotFound();
            }
        }

        /// <summary>
        /// 需要优化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gettemplatebyparentid/{id}")]
        public IHttpActionResult GetTemplateByParentID(string id)
        {
            var user = base.CurrentUser();
            if (string.IsNullOrEmpty(id) || id == "undefined")
            {
                //
                var result = _ReportTemplateService.GetReportTemplateNodes(id, user.UniqueID, user.Site);
                foreach (ReportTemplateDirecDto reportTemplateDirecDto in result)
                {
                    if (reportTemplateDirecDto.UniqueID == "GlobalTemplate" || reportTemplateDirecDto.UniqueID == user.Site || reportTemplateDirecDto.UniqueID == "UserTemplate")
                    {
                        reportTemplateDirecDto.Leaf = 0;
                    } else {

                        var children = _ReportTemplateService.GetReportTemplateNodes(reportTemplateDirecDto.UniqueID, user.UniqueID, user.Site);

                        if (children != null && children.Count() > 0)
                        {
                            reportTemplateDirecDto.Leaf = 0;
                        }
                        else
                        {
                            reportTemplateDirecDto.Leaf = 1;
                        }
                    }
                    
                }
                var templateList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.ItemName,
                        Type = c.Type,
                        HasChildren = true,
                        ParentID = c.ParentID,
                        enabled = c.Leaf.Value == 1 ? false : true
                    }).ToList();


                return Ok(templateList);

            }
            else
            {
                var result = _ReportTemplateService.GetReportTemplateNodes(id, user.UniqueID, user.Site);
                if (result != null)
                {
                    var templateList = result.Select(c =>
                    new
                    {
                        ID = c.UniqueID,
                        Name = c.ItemName,
                        Type = c.Type,
                        TemplateID = c.TemplateID,
                        ParentID = c.ParentID,
                        HasChildren = c.Leaf != null ? (c.Leaf.Value == 1 ? false : true) : true
                    }).ToList();
                    return Ok(templateList);
                }
                
                return Ok(result);
            }
        }

        /// <summary>
        /// Get report template data by id
        /// </summary>
        /// <param name="id">report template id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getreporttemplate/{id}")]
        public IHttpActionResult GetReportTemplate(string id)
        {
            var result = _ReportTemplateService.GetReportTemplateDirecByID(id);
            if (result != null)
            {
                return Ok(result.ReportTemplateDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Get procedure data by id
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getprocedurebyid/{id}")]
        public IHttpActionResult GetProcedureByID(string id)
        {
            var result = _RegistrationService.GetProcedure(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get baseInfo desc data by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getreportviewerbyprocedureid/{id}")]
        public IHttpActionResult GetReportViewerByProcedureID(string id)
        {
            string content = "";

            var user = base.CurrentUser();
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (!string.IsNullOrEmpty(procedureDto.ReportID))
            {

                content = _ReportPrintService.GetReportViewer(procedureDto.ReportID, user.Domain, user.Site);
            }

            return Ok(content);
        }

        /// <summary>
        /// Get baseInfo desc data by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getreportviewerbyprocedureid2")]
        [AllowAnonymous]
        public IHttpActionResult GetReportViewerByProcedureID(string id, string domain, string site, string printtemplateid)
        {
            ShowHtmlDataDto content = null;

            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (!string.IsNullOrEmpty(procedureDto.ReportID))
            {
                content = _ReportPrintService.GetReportViewer2(procedureDto.ReportID, domain, site, printtemplateid);
            }

            return Ok(content);
        }

        /// <summary>
        /// print report by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getreportprinturlbyprocedureid/{id}")]
        public IHttpActionResult GetReportPrintUrlByProcedureID(string id)
        {
            string url = "";
            var user = base.CurrentUser();
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (!string.IsNullOrEmpty(procedureDto.ReportID))
            {
                var pdfService = user.PdfService;
                url = _ReportPrintService.GetReportPrintUrl(pdfService, procedureDto.ReportID);
            }

            return Ok(url);
        }

        /// <summary>
        /// update print status by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <param name="printer">printer</param>
        /// <returns></returns>
        [HttpGet]
        [Route("updatereportprintstatusbyprocedureid")]
        public IHttpActionResult UpdateReportPrintStatusByProcedureID(string id, string printer)
        {
            var user = base.CurrentUser();
            bool result = _ReportPrintService.UpdateReportPrintStatusByProcedureID(id, printer, user.Site, user.Domain);

            return Ok(result);
        }

        /// <summary>
        /// update print status by procedureid
        /// </summary>
        /// <param name="id">procedure id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getservertime")]
        public IHttpActionResult ServerTime()
        {
            return Ok(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// New report data.
        /// </summary>
        /// <param name="reportDto">patinet data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatecomments")]
        public IHttpActionResult UpdateComments([FromBody]ReportDto reportDto)
        {
            _ReportService.UpdateComments(reportDto);
            return Ok(true);
        }

        /// <summary>
        /// New report data.
        /// </summary>
        /// <param name="reportDto">patinet data</param>
        /// <returns></returns>
        [HttpGet]
        [Route("updatereportprinttemplate")]
        public IHttpActionResult UpdateReportPrintTemplate(string reportID, string printTemplateID)
        {
            bool result = _ReportPrintService.UpdateReportPrintTemplate(reportID, printTemplateID);
            return Ok(result);
        }

        /// <summary>
        /// Get history report list data by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getreportlistbyid/{id}")]
        public IHttpActionResult GetReportListByReportID(string id)
        {
            var result = _ReportService.GetReportListByReportID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get other report list data by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getotherreportlistbyid/{id}")]
        public IHttpActionResult GetOtherReportListByReportID(string id)
        {
            var result = _ReportService.GetOtherReportsByReportID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get Other Report Print Data
        /// </summary>
        /// <param name="accno"></param>
        /// <param name="modalityType"></param>
        /// <param name="templateType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("other/printdata")]
        [AllowAnonymous]
        public IHttpActionResult GetOtherReportPrintData(string accno, string modalityType, string templateType, string site)
        {
            var result = _ReportPrintService.GetOtherReportPrintData(accno, modalityType, templateType, site);
            return Ok(result);
        }

        [HttpGet]
        [Route("other/printid")]
        [AllowAnonymous]
        public IHttpActionResult GetOtherReportPrintID(string accno, string modalityType, string templateType, string site)
        {
            var result = _ReportPrintService.GetOtherReportPrintID(accno, modalityType, templateType, site);
            return Ok(result);
        }

        /// <summary>
        /// Get Report by Procedure ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="site"></param>
        /// <param name="domain"></param>
        /// <returns>The html format for a report</returns>
        [HttpGet]
        [Route("html")]
        [AllowAnonymous]
        public IHttpActionResult GetReportByProID(string id, string site, string domain, string printtemplateid)
        {
            ShowHtmlDataDto content = null;
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (!string.IsNullOrEmpty(procedureDto.ReportID))
            {
                content = _ReportPrintService.GetReportViewer2(procedureDto.ReportID, domain, site, printtemplateid);
            }

            return Ok(content);
        }

        [HttpGet]
        [Route("reportprinttemplateid")]
        [AllowAnonymous]
        public IHttpActionResult GetReportPrintTemplateIDByProID(string id, string site, string domain)
        {
            string content = "";
            ProcedureDto procedureDto = _RegistrationService.GetProcedure(id);
            if (!string.IsNullOrEmpty(procedureDto.ReportID))
            {
                content = _ReportPrintService.GetReportPrintTemplateID(procedureDto.ReportID, domain, site);
            }

            return Ok(content);
        }

        /// <summary>
        /// Get other report list data by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getotherreportlistbyprocedureid/{id}")]
        public IHttpActionResult GetOtherReportListByProcedureID(string id)
        {
            var result = _ReportService.GetOtherReportsByProcedureID(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get pacs url by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpacsurl/{id}")]
        public IHttpActionResult GetPacsUrlByProcedureID(string id)
        {
            var user = base.CurrentUser();
            var result = _ReportService.GetPacsUrl(id, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Get pacs url by id
        /// </summary>
        /// <param name="id">report id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpacsurldx/{id}")]
        public IHttpActionResult GetPacsUrlDXByProcedureID(string id)
        {
            var user = base.CurrentUser();
            var result = _ReportService.GetPacsUrlDX(id, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// New report template data.
        /// </summary>
        /// <param name="reportDto">patinet data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("reporttemplate")]
        public IHttpActionResult CreateReportTemplate([FromBody]ReportTemplateDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            reportTemplateDto.Domain = user.Domain;
            var result = _ReportTemplateService.CreateReportTemplate(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// update report template data.
        /// </summary>
        /// <param name="reportDto">patinet data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reporttemplate")]
        public IHttpActionResult UpdateReportTemplate([FromBody]ReportTemplateDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            var result = _ReportTemplateService.UpdateReportTemplate(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        /// <summary>
        /// is duplicated
        /// </summary>
        /// <param name="ReportTemplateDto">reportTemplateDto</param>
        /// <returns></returns>
        [HttpPut]
        [Route("isduplicatedreporttemplate")]
        public IHttpActionResult IsDuplicatedTemplateName([FromBody]ReportTemplateDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            var result = _ReportTemplateService.IsDuplicatedTemplateName(reportTemplateDto, user.UniqueID);

            return Ok(result);
        }

        /// <summary>
        /// Delete lock data
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletetemplatebyid/{id}")]
        public IHttpActionResult DeleteTemplateByID(string id)
        {
            return Ok(_ReportTemplateService.DeleteTemplateByID(id));
        }


        [HttpGet]
        [Route("order/{id}")]
        public IHttpActionResult GetOrderByProcedureID(string id)
        {
            var result = _ReportService.GetOrderByProcedureID(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("imageStatus/{orderId}")]
        public IHttpActionResult GetImageStatusByOrderId(string orderId)
        {
            var result = _ReportService.GetImageStatusByOrderId(orderId);
            return Ok(result);
        }

        #region  模板配置 fxl
        /// <summary>
        /// 创建全局模板
        /// </summary>
        /// <param name="reportTemplateDto">Type:0 是全局模板 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("publicreporttemplate")]
        public IHttpActionResult CreatePublicReportTemplate([FromBody]ReportTemplateDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            reportTemplateDto.Domain = user.Domain;
            var result = _ReportTemplateService.CreatePublicReportTemplate(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// 修改全局模板
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("publicreporttemplate")]
        public IHttpActionResult UpdatePublicReportTemplate([FromBody]ReportTemplateDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            var result = _ReportTemplateService.UpdatePublicReportTemplate(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reporttemplatedirec")]
        public IHttpActionResult CreateReportTemplateDirec([FromBody]ReportTemplateDirecDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            reportTemplateDto.Domain = user.Domain;
            var result = _ReportTemplateService.CreateReportTemplateDirec(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return null;
        }


        /// <summary>
        /// 修改目录
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reporttemplatedirec")]
        public IHttpActionResult UpdateReportTemplateDirec([FromBody]ReportTemplateDirecDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            var result = _ReportTemplateService.UpdateReportTemplateDirec(reportTemplateDto, user.UniqueID);
            if (result != null)
            {
                return Ok(result);
            }
            return null;
        }
        /// <summary>
        /// 验证模板名
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reporttemplateexist")]
        public IHttpActionResult ReportTemplateExist([FromBody]ReportTemplateDirecDto reportTemplateDto)
        {
            var user = base.CurrentUser();
            var result = _ReportTemplateService.CreateReportTemplateExist(reportTemplateDto, user.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 目录上移一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("nodeup/{id}")]
        public IHttpActionResult NodeItemOrderUp(string id)
        {
            var result = _ReportTemplateService.NodeItemOrderUp(id);
            return Ok(result);
        }
        /// <summary>
        /// 目录上下移一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("nodedown/{id}")]
        public IHttpActionResult NodeItemOrderDown(string id)
        {
            var result = _ReportTemplateService.NodeItemOrderDown(id);
            return Ok(result);
        }
        #endregion
    }

}
