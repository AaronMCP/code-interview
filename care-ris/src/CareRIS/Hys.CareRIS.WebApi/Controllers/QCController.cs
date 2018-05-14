using Hys.CareRIS.Application.Services;
using Hys.CareRIS.Application.Dtos;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Kendo.DynamicLinq;

namespace Hys.CareRIS.WebApi.Controllers
{
    [RoutePrefix("api/v1/qc")]
    public class QCController : ApiControllerBase
    {
        private IQCService _QCService;
        public QCController(IQCService QCService)
        {
            _QCService = QCService;
        }

        [Route("patient")]
        public async Task<IHttpActionResult> GetPatient(string patientName = null, string patientId = null, DateTime? beginDate = null, DateTime? endDate = null, bool? isVip = null)
        {
            var result = await _QCService.QueryPatient(patientName, patientId, beginDate, endDate, isVip);
            return Ok(result);
        }

        [Route("pageablepatient"), HttpPost]
        public IHttpActionResult GetPageablePatients(DataSourceRequest request)
        {
            var result = _QCService.GetPageablePatients(request);
            return Ok(result);
        }

        [Route("order/{patientGuid}")]
        public async Task<IHttpActionResult> GetOrder(string patientGuid)
        {
            var result = await _QCService.QueryOrder(patientGuid);
            return Ok(result);
        }

        [Route("procedure/{orderId}")]
        public async Task<IHttpActionResult> GetProcedure(string orderId)
        {
            var result = await _QCService.QueryProcedure(orderId);
            return Ok(result);
        }

        /// <summary>
        /// 质控评分列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("scoringlist"), HttpPost]
        public async Task<IHttpActionResult> GetQualityScoringList([FromBody]DataSourceRequest request)
        {
            var user = CurrentUser();
            var result = await Task.Run<DataSourceResult>(() => _QCService.GetPageableQualityCtrl(request, user.Site));
            return Ok(result);
        }
        /// <summary>
        /// 保存评分
        /// </summary>
        /// <param name="scoreInfo">
        /// scoreInfo.ScoringType:1 图片质量评分
        /// scoreInfo.ResultItem 报告质量 xml 例子：<Items><Item>描述合适;</Item><Item>无错别字或符号;</Item><Item>报告完整;</Item><Item>结论准确;</Item></Items>
        /// scoreInfo.Result 评分结果 合格 
        /// scoreInfo.ProcedIDs   ProcedureID的集合
        /// scoreInfo.Result2 :Image1
        /// scoreInfo.Result3 选中的报告质量indexs 例:0,1,2
        /// OrderID
        /// Comment备注
        /// 
        ///   scoreInfo.ScoringType:2报告质量评分
        ///   scoreInfo.ResultItem 报告质量 xml 例子：<Items><Item>描述合适;</Item><Item>无错别字或符号;</Item><Item>报告完整;</Item><Item>结论准确;</Item></Items>
        ///   scoreInfo.ReportQuality  评分结果
        ///   scoreInfo. ReportQuality2 选中的报告质量indexs 例 :0,1,2
        ///   scoreInfo.ReportQualityComments 备注
        ///   scoreInfo.ScoringVersion :Report1
        ///   scoreInfo. ReportIDs  ReportID 的集合
        ///   scoreInfo.AccordRate 诊断符合 例：符合 
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("savescoring")]
        public IHttpActionResult SaveScoring([FromBody]QualityScoreDto scoreInfo)
        {
            var user = base.CurrentUser();
            scoreInfo.Domain = user.Domain;
            scoreInfo.Appraiser = user.UniqueID;
            return Ok(_QCService.SaveScoring(scoreInfo));

        }
        /// <summary>
        /// 历史评分
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("scoringhistorylist")]
        public IHttpActionResult GetScoringHistoryList(string objectId, int type)
        {
            return Ok(_QCService.QueryScoringHistoryList(objectId, type));
        }

        /// <summary>
        /// 修改检查项目
        /// </summary>
        /// <param name="procedureUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateprocedure")]
        public IHttpActionResult UpdateProcedure([FromBody]ProcedureDto procedureUpdate)
        {
            var user = base.CurrentUser();
            var result = _QCService.UpdateProcedure(procedureUpdate, user.UniqueID);
            return Ok(result);
        }
        /// <summary>
        /// 合并病人
        /// </summary>
        /// <param name="mergePatientInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mergepatient")]
        public IHttpActionResult MergePatient([FromBody]MergeObjectDto mergePatientInfo)
        {
            var user = base.CurrentUser();
            var result = _QCService.MergePatients(mergePatientInfo);
            return Ok(result);
        }

        /// <summary>
        /// 移动检查
        /// </summary>
        /// <param name="moveInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("moveorder")]
        public IHttpActionResult MoveOrder([FromBody]MergeObjectDto moveInfo)
        {
            var user = base.CurrentUser();
            var result = _QCService.MoveOrder(moveInfo);
            return Ok(result);
        }

        /// <summary>
        /// 合并检查
        /// </summary>
        /// <param name="mergInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mergeorder")]
        public IHttpActionResult MergeOrder([FromBody]MergeObjectDto mergInfo)
        {
            var user = base.CurrentUser();
            var result = _QCService.MergeOrder(mergInfo);
            return Ok(result);
        }

        /// <summary>
        /// 移动检查项目
        /// </summary>
        /// <param name="moveInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("movecheckingitem")]
        public IHttpActionResult MoveCheckingItem([FromBody]MergeObjectDto moveInfo)
        {
            var result = _QCService.MoveCheckingItem(moveInfo);
            return Ok(result);
        }

        /// <summary>
        /// 撤销检查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("revokeprocedure/{id}")]
        public IHttpActionResult RevokeProcedure(string id)
        {
            var result = _QCService.RevokeProcedure(id);
            return Ok(result);
        }

        /// <summary>
        /// 恢复检查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("recoveryprocedure/{id}")]
        public IHttpActionResult RecoveryProcedure(string id)
        {
            var result = _QCService.RecoveryProcedure(id);
            return Ok(result);
        }
    }
}