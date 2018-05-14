using Hys.CareRIS.Application.Dtos;
using Hys.CrossCutting.Common.Interfaces;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services
{
    public interface IQCService : IDisposable
    {
        /// <summary>
        /// 查询病人
        /// </summary>
        /// <param name="patientName"></param>
        /// <param name="patientId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isVip"></param>
        /// <returns></returns>
        Task<IEnumerable<PatientDto>> QueryPatient(string patientName, string patientId, DateTime? beginDate, DateTime? endDate, bool? isVip);

        DataSourceResult GetPageablePatients(DataSourceRequest request);

        Task<IEnumerable<OrderDto>> QueryOrder(string patientGuid);

        Task<IEnumerable<ProcedureDto>> QueryProcedure(string orderId);

        Task<PaginationResult> SearchQualityScoringList(WorklistSearchCriteriaDto criteria, string site);
        /// <summary>
        /// 保存质控评分
        /// </summary>
        /// <param name="score"></param>
        /// <param name="site"></param>
        bool SaveScoring(QualityScoreDto score);

        IEnumerable<ScoringResultDto> QueryScoringHistoryList(string objectId, int type);

        /// <summary>
        /// kendui Grid result
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DataSourceResult GetPageableQualityCtrl(DataSourceRequest request, string site);
        /// <summary>
        /// 合并病人
        /// </summary>
        /// <param name="mergePatientInfo"></param>
        /// <returns></returns>
        bool MergePatients(MergeObjectDto mergePatientInfo);

        /// <summary>
        /// 移动检查
        /// </summary>
        /// <param name="moveInfo"></param>
        /// <returns></returns>
        bool MoveOrder(MergeObjectDto moveInfo);

        /// <summary>
        /// 合并检查
        /// </summary>
        /// <param name="mergeInfo"></param>
        /// <returns></returns>
        bool MergeOrder(MergeObjectDto mergeInfo);
        /// <summary>
        /// 移动检查项目
        /// </summary>
        /// <param name="moveInfo"></param>
        /// <returns></returns>
        bool MoveCheckingItem(MergeObjectDto moveInfo);

        bool RevokeProcedure(string orderID);
        bool RecoveryProcedure(string orderID);
        ProcedureDto UpdateProcedure(ProcedureDto procedureUpdate, string userId);
    }
}
