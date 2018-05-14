using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Utils;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;
using Hys.CareRIS.Application.Services.ServiceImpl;
using WebApi.OutputCache.V2;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [RoutePrefix("api/v1/structuredreport")]
    [AllowAnonymous]
    [AutoInvalidateCacheOutput]
    public class StructuredReportController : ApiControllerBase
    {
        private readonly IStructuredReportService _structuredReportService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="structuredReportService"></param>
        /// <param name="configurationService"></param>
        public StructuredReportController(IStructuredReportService structuredReportService)
        {
            _structuredReportService = structuredReportService;
        }

        /// <summary>
        /// Get procedurecodes data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("procedurecodes")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetProcedureCodes()
        {
            var result = _structuredReportService.GetProcedureCodes();
            return Ok(result);
        }

        /// <summary>
        /// Get ModalityTypes Data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("modalitytypes")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetModalityTypes()
        {
            var modalityTypes = _structuredReportService.GetModalityTypes();
            return Ok(modalityTypes);
        }

        /// <summary>
        /// Get BodyParts Data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("bodyparts")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetBodyParts()
        {
            var bodyparts = _structuredReportService.GetBodyParts();
            return Ok(bodyparts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addtoristemplate")]
        public IHttpActionResult AddtoRisTemplate(string template)
        {
            JavaScriptSerializer sa = new JavaScriptSerializer();
            var tml = sa.Deserialize<ReportTemplateDto>(template);
            ReportTemplateDto newReportTemplateDto = new ReportTemplateDto
            {
                TemplateName = tml.TemplateName,
                ModalityType = tml.ModalityType,
                BodyPart = tml.BodyPart
            };
            _structuredReportService.AddToRisTemplate(newReportTemplateDto);
            return Ok(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatetoristemplate")]
        public IHttpActionResult UpdatetoRisTemplate(string template)
        {
            JavaScriptSerializer sa = new JavaScriptSerializer();
            var tml = sa.Deserialize<ReportTemplateDto>(template);
            ReportTemplateDto newReportTemplateDto = new ReportTemplateDto
            {
                UniqueID = tml.UniqueID,
                TemplateName = tml.TemplateName,
                ModalityType = tml.ModalityType,
                BodyPart = tml.BodyPart
            };
            _structuredReportService.UpdateToRisTemplate(newReportTemplateDto);
            return Ok(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="templatename"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletetoristemplate")]
        public IHttpActionResult DeletetoRisTemplate(string templatename)
        {
            _structuredReportService.DeleteToRisTemplate(templatename);
            return Ok(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="templatename"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getristemplateid")]
        public IHttpActionResult GetRisTemplateId(string templatename)
        {
            var tid = _structuredReportService.GetRisReportTemplateId(templatename);
            return Ok(tid);
        }
    }
}