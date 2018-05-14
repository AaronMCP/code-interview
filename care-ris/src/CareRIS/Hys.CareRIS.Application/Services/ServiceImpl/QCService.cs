using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.EntityFramework;
using Hys.Platform.Application;
using System;
using System.Collections.Generic;
using Hys.CareRIS.Domain.Entities;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.SqlClient;
using Hys.CrossCutting.Common.Interfaces;
using Kendo.DynamicLinq;
using System.Data.Entity.SqlServer;
using Hys.CareRIS.Domain.Interface;
using System.Transactions;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class QCService : DisposableServiceBase, IQCService
    {

        private IProcedureRepository _ProcedureRepository;

        private IRisProContext _dbContext;
        public QCService(IRisProContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region 查询
        /// <summary>
        /// 查询病人
        /// </summary>
        public async Task<IEnumerable<PatientDto>> QueryPatient(string patientName, string patientId, DateTime? beginDate, DateTime? endDate, bool? isVip)
        {
            var fixedPatientName = (patientName ?? "").Trim();
            var fixedPatientId = (patientId ?? "").Trim();

            if (patientName == "" && fixedPatientId == "" && !beginDate.HasValue && !endDate.HasValue && !isVip.HasValue)
            {
                return new List<PatientDto>();
            }

            var query = _dbContext.Set<Patient>().AsQueryable();
            if (patientName != "")
            {
                query = query.Where(p => p.LocalName == fixedPatientName);
            }
            if (fixedPatientId != "")
            {
                query = query.Where(p => p.PatientNo == fixedPatientId);
            }
            if (beginDate.HasValue)
            {
                var fixedBeginDate = beginDate.Value.Date;
                query = query.Where(p => p.CreateTime >= fixedBeginDate);
            }
            if (endDate.HasValue)
            {
                var fixedEndDate = new DateTime(
                    endDate.Value.Year,
                    endDate.Value.Month,
                    endDate.Value.Day,
                    23, 59, 59);
                query = query.Where(p => p.CreateTime <= fixedEndDate);
            }
            if (isVip.HasValue)
            {
                var fixedIsVip = isVip.Value ? 1 : 0;
                query = query.Where(p => p.IsVIP == fixedIsVip);
            }

            var result = await query.Select(t => t).ToListAsync();
            var data = Mapper.Map<List<PatientDto>>(result);
            return data;
        }
        /// <summary>
        /// 质控修改病人分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataSourceResult GetPageablePatients(DataSourceRequest request)
        {
            var syncSet = _dbContext.Set<Sync>();
            var query = from p in _dbContext.Set<Patient>()
                        join ot in _dbContext.Set<Order>() on p.UniqueID equals ot.PatientID into og
                        from o in og.DefaultIfEmpty()
                        select new
                        {
                            UniqueID = p.UniqueID,
                            PatientNo = p.PatientNo,
                            LocalName = p.LocalName,
                            EnglishName = p.EnglishName,
                            ReferenceNo = p.ReferenceNo,
                            Birthday = p.Birthday,
                            Gender = p.Gender,
                            Address = p.Address,
                            Telephone = p.Telephone,
                            IsVIP = p.IsVIP,
                            CreateTime = p.CreateTime,
                            Comments = p.Comments,
                            RemotePID = p.RemotePID,
                            Optional1 = p.Optional1,
                            Optional2 = p.Optional2,
                            Optional3 = p.Optional3,
                            Alias = p.Alias,
                            Marriage = p.Marriage,
                            Domain = p.Domain,
                            GlobalID = p.GlobalID,
                            MedicareNo = p.MedicareNo,
                            ParentName = p.ParentName,
                            RelatedID = p.RemotePID,
                            Site = p.Site,
                            SocialSecurityNo = p.SocialSecurityNo,
                            UpdateTime = p.UpdateTime,
                            IsUploaded = p.IsUploaded
                        };
            query = query.Distinct();
            return query.ToDataSourceResult(request);
        }

        /// <summary>
        /// 获取病人检查信息
        /// </summary>
        /// <param name="patientGuid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderDto>> QueryOrder(string patientGuid)
        {
            if (patientGuid == null || patientGuid.Trim().Length == 0)
            {
                return new List<OrderDto>();
            }
            var pid = patientGuid.Trim();
            var query = await (from o in _dbContext.Set<Order>()
                               join rpt in _dbContext.Set<Procedure>() on o.UniqueID equals rpt.OrderID into rps
                               from rp in rps.DefaultIfEmpty()
                               where o.PatientID == pid && (rp == null || rp.Status >= 20||rp.Status==0)//=0已撤销 显示用来恢复
                               orderby o.CreateTime descending
                               select o).ToListAsync();
            query = query.Distinct().ToList();
            var data = Mapper.Map<List<OrderDto>>(query);
            return data;
        }

        /// <summary>
        /// 获取病人检查项目
        /// GetProcedures(string patientID, string orderID = null)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProcedureDto>> QueryProcedure(string orderId)
        {
            var query = await _dbContext.Set<Procedure>().
                                Where(p => p.OrderID == orderId).
                                OrderBy(p => p.ModalityType).ToListAsync();
            var result = Mapper.Map<List<ProcedureDto>>(query);
            return result;
        }

        //修改病人 public PatientDto UpdatePatient(PatientEditDto patientEdit)

        //修改检查申请单
        //public void UpdateOrder(OrderDto orderDto, bool IsRegistrationView = false)

        /// <summary>
        /// 质控修改检查部位
        /// </summary>
        /// <param name="procedureUpdate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ProcedureDto UpdateProcedure(ProcedureDto procedureUpdate, string userId)
        {
            var oldProcedure = _dbContext.Set<Procedure>().Where(r => r.UniqueID == procedureUpdate.UniqueID).FirstOrDefault();
            if (procedureUpdate != null && !string.IsNullOrEmpty(procedureUpdate.OrderID) && oldProcedure != null)
            {
                oldProcedure.FilmSpec = procedureUpdate.FilmSpec;
                oldProcedure.FilmCount = procedureUpdate.FilmCount;
                oldProcedure.ContrastName = procedureUpdate.ContrastName;
                oldProcedure.ContrastDose = procedureUpdate.ContrastDose;
                oldProcedure.ExposalCount = procedureUpdate.ExposalCount;
                // oldProcedure.Deposit = procedureUpdate.Deposit;
                oldProcedure.Charge = procedureUpdate.Charge;
                oldProcedure.Technician = procedureUpdate.Technician;
                oldProcedure.TechDoctor = procedureUpdate.TechDoctor;
                oldProcedure.TechNurse = procedureUpdate.TechNurse;
                oldProcedure.Posture = procedureUpdate.Posture;//体位
                oldProcedure.MedicineUsage = procedureUpdate.MedicineUsage;//用药方式
                oldProcedure.UpdateTime = DateTime.Now;
                oldProcedure.Mender = userId;
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                //    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                //{
                _dbContext.SaveChanges();
                //    ts.Complete();
                //}
                var newp = _dbContext.Set<Procedure>().Where(p => p.UniqueID.Equals(oldProcedure.UniqueID)).FirstOrDefault();
                return Mapper.Map<Procedure, ProcedureDto>(newp);
            }
            else
            {
                return null;
            }
        }

        //验证order是否被锁定 Sync ReportLockService
        //public SyncDto GetLock(string orderID, LockType lockType)
        //合并病人
        public bool MergePatients(MergeObjectDto mergePatientInfo)
        {
            if (mergePatientInfo == null)
            {
                return false;
            }
            try
            {
                //验证病人
                var queryPatient = _dbContext.Set<Patient>();
                var srcp = queryPatient.Where(p => p.UniqueID == mergePatientInfo.SrcPatientID).FirstOrDefault();
                var targetp = queryPatient.Where(p => p.UniqueID == mergePatientInfo.TargetPatientID).FirstOrDefault();
                if (srcp == null || targetp == null)
                {
                    return false;
                }
                //更新order 的 PatientGuid
                var queryOrder = _dbContext.Set<Order>();
                var orders = queryOrder.Where(o => o.PatientID == mergePatientInfo.SrcPatientID);
                foreach (var order in orders)
                {
                    order.PatientID = mergePatientInfo.TargetPatientID;
                }
                if (mergePatientInfo.AfterDelPatient)
                {
                    queryPatient.Remove(srcp);
                }
                //是否删除被合并掉的病人

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    _dbContext.SaveChanges();
                    ts.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 移动检查
        /// </summary>
        /// <param name="mergePatientInfo"></param>
        /// <returns></returns>
        public bool MoveOrder(MergeObjectDto moveInfo)
        {
            if (moveInfo == null)
            {
                return false;
            }
            try
            {
                //验证病人 和检查
                var queryPatient = _dbContext.Set<Patient>();
                var queryOrder = _dbContext.Set<Order>();
                var targetp = queryPatient.Where(p => p.UniqueID == moveInfo.TargetPatientID).FirstOrDefault();
                var srcOrder = queryOrder.Where(o => o.UniqueID == moveInfo.SrcOrderID).FirstOrDefault();
                if (srcOrder == null || targetp == null)
                {
                    return false;
                }
                //order>procedure>patient
                //更新order 的 PatientGuid
                srcOrder.PatientID = moveInfo.TargetPatientID;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    _dbContext.SaveChanges();
                    ts.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //合并检查申请单
        public bool MergeOrder(MergeObjectDto mergeInfo)
        {
            if (mergeInfo == null)
            {
                return false;
            }
            try
            {
                //验证检查
                var queryPatient = _dbContext.Set<Patient>();
                var queryOrder = _dbContext.Set<Order>();
                var queryProcedure = _dbContext.Set<Procedure>();

                var srcOrder = queryOrder.Where(o => o.UniqueID == mergeInfo.SrcOrderID).FirstOrDefault();
                var targeOrder = queryOrder.Where(o => o.UniqueID == mergeInfo.TargetOrderID).FirstOrDefault();
                if (srcOrder == null || targeOrder == null)
                {
                    return false;
                }
                //合并申请单
                if (mergeInfo.IsMergeRequisition)
                {
                    MergeRequisition(srcOrder.AccNo, targeOrder.AccNo);
                }
                //合并费用
                if (mergeInfo.IsMergeOrderCharge)
                {
                    MergeOrderCharge(mergeInfo.SrcOrderID, mergeInfo.TargetOrderID);
                }
                var srcp = queryPatient.Where(p => p.UniqueID == srcOrder.PatientID).FirstOrDefault();
                var targetp = queryPatient.Where(p => p.UniqueID == targeOrder.PatientID).FirstOrDefault();
                if (srcp == null || targetp == null)
                {
                    return false;
                }

                //更新 检查的 orderid
                var procedures = queryProcedure.Where(p => p.OrderID == mergeInfo.SrcOrderID);
                foreach (var pro in procedures)
                {
                    pro.OrderID = mergeInfo.TargetOrderID;
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    //删除被合检查
                    queryOrder.Remove(srcOrder);
                    _dbContext.SaveChanges();
                    ts.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 合并申请单
        /// </summary>
        /// <param name="mergeInfo"></param>
        /// <returns></returns>
        public void MergeRequisition(string srcAccNo, string targetAccNo)
        {

            var queryRequisition = _dbContext.Set<Requisition>();
            //更新 Requisition  AccNo
            var requisitions = queryRequisition.Where(p => p.AccNo == srcAccNo);
            foreach (var pro in requisitions)
            {
                pro.AccNo = targetAccNo;
            }

        }
        /// <summary>
        /// 合并费用
        /// </summary>
        /// <param name="mergeInfo"></param>
        /// <returns></returns>
        public void MergeOrderCharge(string srcOrderID, string targetOrderID)
        {


            string sql = "UPDATE tbOrderCharge SET OrderGuid=@TargetOrderID Where OrderGuid=@SrcOrderID";
            //更新OrderCharge  OrderID
            var param = new List<Object>();
            param.Add(new SqlParameter("@TargetOrderID", targetOrderID));
            param.Add(new SqlParameter("@SrcOrderID", srcOrderID));
            var result = _dbContext.ExecuteSqlCommand(sql, param.ToArray());


        }
        //移动检查项目
        public bool MoveCheckingItem(MergeObjectDto moveInfo)
        {

            if (moveInfo == null)
            {
                return false;
            }
            try
            {
                //验证检查
                var queryProcedure = _dbContext.Set<Procedure>();
                var queryOrder = _dbContext.Set<Order>();
                var queryPatient = _dbContext.Set<Patient>();
                var srcProcedure = queryProcedure.Where(o => o.UniqueID == moveInfo.SrcProcedureID).FirstOrDefault();
                var targeProcedure = queryProcedure.Where(o => o.UniqueID == moveInfo.TargetProcedureID).FirstOrDefault();
                if (srcProcedure == null || targeProcedure == null)
                {
                    return false;
                }
                //同一检查下面的检查项目不用移动
                if (srcProcedure.OrderID == targeProcedure.OrderID)
                {
                    return false;
                }

                var srcOrder = queryOrder.Where(o => o.UniqueID == srcProcedure.OrderID).FirstOrDefault();
                var targeOrder = queryOrder.Where(o => o.UniqueID == targeProcedure.OrderID).FirstOrDefault();
                if (srcOrder == null || targeOrder == null)
                {
                    return false;
                }

                var srcp = queryPatient.Where(p => p.UniqueID == srcOrder.PatientID).FirstOrDefault();
                var targetp = queryPatient.Where(p => p.UniqueID == targeOrder.PatientID).FirstOrDefault();
                if (srcp == null || targetp == null)
                {
                    return false;
                }

                //更新 检查项目的 orderid
                srcProcedure.OrderID = targeProcedure.OrderID;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //撤销检查申请
        public bool RevokeProcedure(string orderID)
        {

            if (orderID == null)
            {
                return false;
            }
            try
            {
                //验证检查
                var queryProcedure = _dbContext.Set<Procedure>();
                var queryOrder = _dbContext.Set<Order>();
                var srcOrder = queryOrder.Where(o => o.UniqueID == orderID).FirstOrDefault();
                if (srcOrder == null)
                {
                    return false;
                }

                var procedures = queryProcedure.Where(p => p.OrderID == orderID).ToList() ;
                foreach (var pro in procedures)
                {
                    pro.PreStatus = pro.Status;
                    pro.Status = 0;
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //恢复检查申请
        public bool RecoveryProcedure(string orderID)
        {

            if (orderID == null)
            {
                return false;
            }
            try
            {
                //验证检查
                var queryProcedure = _dbContext.Set<Procedure>();
                var queryOrder = _dbContext.Set<Order>();
                var srcOrder = queryOrder.Where(o => o.UniqueID == orderID).FirstOrDefault();
                if (srcOrder == null)
                {
                    return false;
                }

                var procedures = queryProcedure.Where(p => p.OrderID == orderID).ToList();
                foreach (var pro in procedures)
                {
                    pro.Status = pro.PreStatus.HasValue ? (int)pro.PreStatus : 0;
                    pro.PreStatus = null;
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region  质控评分

        #region  查询
        public async Task<PaginationResult> SearchQualityScoringList(WorklistSearchCriteriaDto criteria, string site)
        {

            if (criteria.Pagination == null)
            {
                criteria.Pagination = new PaginationDto
                {
                    PageIndex = 0,
                    PageSize = 20
                };
            }
            else
            {
                if (criteria.Pagination.PageIndex > 0)
                {
                    criteria.Pagination.PageIndex = criteria.Pagination.PageIndex - 1;
                }
            }

            string sql = "EXEC SP_QualityScoringListWeb @PageIndex=@PageIndex,@PageSize = @PageSize,@Where=@SqlWhere,@TotalCount = @TotalCount output,@RandomNum = 0 ";

            StringBuilder sqlwhere = new StringBuilder(" and 1 = 1 ");
            sqlwhere.Append(string.Format(" and (tbregorder.examsite='{0}')", site));

            if (!string.IsNullOrEmpty(criteria.PatientNo))
            {
                sqlwhere.Append(string.Format(" and tbRegPatient.PatientID like '{0}'", "%" + criteria.PatientNo + "%"));
            }
            if (!string.IsNullOrEmpty(criteria.PatientName))
            {
                sqlwhere.Append(string.Format(" and tbRegPatient.LocalName like '{0}'", "%" + criteria.PatientName + "%"));
            }
            //放射编号
            if (!string.IsNullOrEmpty(criteria.AccNo))
            {
                sqlwhere.Append(string.Format(" and tbRegPatient.AccNo like '{0}'", "%" + criteria.AccNo + "%"));
            }

            // 高级查询
            //(tbProcedureCode.CheckingItem in (''CT踝关节平扫''))
            //CreateTimeRanges = new List<TimeRangeDto>();
            //ExamineTimeRanges = new List<TimeRangeDto>();

            //病人类型
            if (criteria.PatientTypes.Count > 0)
            {
                var patientTypes = string.Join("','", criteria.PatientTypes);
                sqlwhere.Append(string.Format(" tbRegOrder.PatientType in {0}", "('" + patientTypes + "')"));
            }
            //设备类型
            if (criteria.ModalityTypes.Count > 0)
            {
                var modalityTypes = string.Join("','", criteria.PatientTypes);
                sqlwhere.Append(string.Format(" tbProcedureCode.ModalityType in {0}", "('" + modalityTypes + "')"));
            }
            //评分
            if (criteria.Results.Count > 0)
            {
                var results = string.Join("','", criteria.Results);
                sqlwhere.Append(string.Format(" tbQualityScoring.Result in {0}", "('" + results + "')"));
            }
            //状态
            if (criteria.Statuses.Count > 0)
            {
                var statuses = string.Join(",", criteria.Results);
                sqlwhere.Append(string.Format(" tbProcedureCode.Status in {0}", "(" + statuses + ")"));
            }

            criteria.Pagination.TotalCount = 0;
            var param = new List<Object>();
            param.Add(new SqlParameter("@PageIndex", criteria.Pagination.PageIndex));
            param.Add(new SqlParameter("@PageSize", criteria.Pagination.PageSize));
            param.Add(new SqlParameter("@SqlWhere", sqlwhere.ToString()));
            var parcount = new SqlParameter { ParameterName = "@TotalCount", Value = criteria.Pagination.TotalCount, Direction = ParameterDirection.Output };
            param.Add(parcount);

            var query = _dbContext.SqlQuery<QualityScoreDto>(sql, param.ToArray());
            var data = await query.ToListAsync();

            var result = new PaginationResult
            {
                Data = data,
                Total = (int)parcount.Value
            };
            return result;
        }

        public DataSourceResult GetPageableQualityCtrl(DataSourceRequest request, string site)
        {
            var query = from p in _dbContext.Set<Patient>()
                        join o in _dbContext.Set<Order>() on p.UniqueID equals o.PatientID
                        join rp in _dbContext.Set<Procedure>() on o.UniqueID equals rp.OrderID
                        join qst in _dbContext.Set<QualityScore>() on rp.UniqueID equals qst.AppraiseObject into qsl
                        from qs in qsl.DefaultIfEmpty()
                        join rt in _dbContext.Set<Report>() on rp.ReportID equals rt.UniqueID into rl
                        from r in rl.DefaultIfEmpty()
                        join u in _dbContext.Set<User>() on rp.Technician equals u.UniqueID into tul
                        from tu in tul.DefaultIfEmpty()
                        where rp.IsExistImage == 1 && rp.Status != 25 && o.ExamSite == site
                        #region construct select
                        select new QualityScoreDto
                        {
                            OrderID = o.UniqueID,
                            ProcedureID = rp.UniqueID,
                            Status = rp.Status,
                            AccNo = o.AccNo,
                            StudyInstanceUID = o.StudyInstanceUID,
                            PatientID = p.PatientNo,
                            LocalName = p.LocalName,
                            Birthday = p.Birthday,
                            ExamSystem = rp.ExamSystem,
                            Modality = rp.Modality,
                            Technician = tu.LocalName,
                            Gender = p.Gender,
                            ApplyDept = o.ApplyDept,
                            PatientType = o.PatientType,
                            RPDesc = rp.RPDesc,
                            BodyPart = rp.BodyPart,
                            ExamineTime = rp.ExamineTime,
                            CreateTime = rp.CreateTime,
                            SubmitTime = r.SubmitTime,
                            FirstApproveTime = r.FirstApproveTime,
                            AssignTime = o.AssignTime,
                            ModalityType = rp.ModalityType,
                            CheckingItem = rp.CheckingItem,
                            TechnicianID = rp.Technician,
                            ReportID = r.UniqueID,
                            SubmitSite = r.SubmitSite,
                            ScoringVersion = r.ScoringVersion,
                            Result = qs.Result,
                            Result2 = qs.Result2,
                            Result3 = qs.Result3,
                            AppraiseObject = qs.AppraiseObject,
                            Comment = qs.Comment,
                            ReportQuality = r.ReportQuality,
                            ReportQuality2 = r.ReportQuality2,
                            ReportQualityComments = r.ReportQualityComments,
                            AccordRate = r.AccordRate,
                            PrintTemplateID = r.PrintTemplateID
                        };
                        #endregion
            return query.ToDataSourceResult(request);
        }

        /// <summary>
        /// 历史评分
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<ScoringResultDto> QueryScoringHistoryList(string objectId, int type)
        {
            if (string.IsNullOrEmpty(objectId))
            {
                return null;
            }
            var users = _dbContext.Set<User>();
            var query = _dbContext.Set<ScoringResult>().Where(s => s.ObjectGuid == objectId && s.Type == type).OrderBy(s => s.CreateDate).ToList();

            var result = Mapper.Map<List<ScoringResultDto>>(query);
            result = result.Select(r =>
            {
                var user = users.Where(u => u.UniqueID == r.Appraiser).FirstOrDefault();
                if (user != null)
                {
                    r.UserName = user.LocalName;
                }
                return r;
            }).ToList();
            return result;

        }

        #endregion

        #region  修改
        /// <summary>
        /// 保存评分
        /// </summary>
        /// <param name="scroe"></param>
        /// <returns></returns>
        public bool SaveScoring(QualityScoreDto score)
        {
            if (score == null)
            {
                return false;
            }
            string sql = "";
            var param = new List<Object>();
            //报告质量评分
            if (score.ScoringType == 2)
            {
                //更新tbReport表,ReportQuality, ReportQualityComments,ScoringVersion,ReportQuality2
                var reprotIds = string.Join("','", score.ReportIDs);
                sql = string.Format("Update tbReport set ReportQuality = @ReqportQuality,ReportQuality2 = @ReqportQuality2,ReportQualityComments=@ReportQualityComments,ScoringVersion=@ScoringVersion,AccordRate=@AccordRate where ReportGuid in ('{0}') \r\n", reprotIds.ToString());
                param.Add(new SqlParameter("@ReqportQuality", score.ReportQuality));
                param.Add(new SqlParameter("@ReqportQuality2", score.ReportQuality2));
                param.Add(new SqlParameter("@ReportQualityComments", score.ReportQualityComments));
                param.Add(new SqlParameter("@ScoringVersion", score.ScoringVersion));
                param.Add(new SqlParameter("@AccordRate", score.AccordRate));
                //更新tbScoringResult  
                sql += string.Format(" Update tbScoringResult set IsFinalVersion =0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type={1} \r\n ", reprotIds, score.ScoringType);

                foreach (string guid in score.ReportIDs)
                {
                    if (!string.IsNullOrWhiteSpace(guid))
                    {
                        sql += string.Format("Insert into tbScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment,AccordRate) values(NEWID(),'{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}')",
                               guid, score.ScoringType, score.ResultItem, score.Domain, score.ReportQuality, score.Appraiser, score.ReportQualityComments, score.AccordRate) + "\r\n";
                    }
                }

            }
            else//图像质量评分
            {
                //获取tbProcedureCode的信息
                var procedIds = string.Join("','", score.ProcedIDs);

                string sqlGeneral = string.Format("Select ProcedureGuid as UniqueID,ExamineDt as ExamineTime,Technician from tbProcedureCode where OrderGuid ='{0}' and IsExistImage = 1 and ProcedureGuid in ('{1}')  \r\n ", score.OrderID, procedIds);
                var pros = _dbContext.Set<Procedure>().Where(p => p.OrderID == score.OrderID && score.ProcedIDs.Contains(p.UniqueID)).ToList();

                //tbQualityScoring
                if (pros != null && pros.Count > 0)
                {
                    //删除tbQualityScoring的评价
                    sql = string.Format(" Delete from tbQualityScoring where AppraiseObject in ('{0}') \r\n ", procedIds);
                    //在新增
                    foreach (var pr in pros)
                    {
                        sql += string.Format(" Insert into tbQualityScoring(Guid,AppraiseObject,OrderGuid,ExaminateDt,Type,Result,Appraisee,Appraiser,AppraiseDate,Comment,Domain,Result2,Result3) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',@Comment,'{9}','{10}','{11}')",
                                            Guid.NewGuid().ToString(),
                                            pr.UniqueID.ToString(),
                                            score.OrderID,
                                            pr.ExamineTime.ToString(),
                                            score.ScoringType,
                                            score.Result,
                                            pr.Technician,
                                            score.Appraiser,
                                            DateTime.Now,
                                            score.Domain,
                                           score.Result2,
                                           score.Result3
                                            ) + "\r\n";
                    }
                    param.Add(new SqlParameter("@Comment", score.Comment));
                    //tbScoringResult
                    sql += string.Format(" Update tbScoringResult set IsFinalVersion = 0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type={1} \r\n ", procedIds, score.ScoringType);

                    foreach (string guid in score.ProcedIDs)
                    {
                        if (!string.IsNullOrWhiteSpace(guid))
                        {
                            sql += string.Format("Insert into tbScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment) values(NEWID(),'{0}',{1},'{2}','{3}','{4}','{5}','{6}')",
                                  guid, score.ScoringType, score.ResultItem, score.Domain, score.Result, score.Appraiser, score.Comment) + "\r\n";
                        }
                    }
                }
            }
            if (sql.Trim().Length > 0)
            {
                var result = _dbContext.ExecuteSqlCommand(sql, param.ToArray());

                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetSettings(string type, ref string version)
        {

            return null;
        }
        #endregion

        #endregion
    }
}
