using AutoMapper;
using C1.C1Report;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Mappers;
using Hys.CareRIS.EnterpriseLib;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.Platform.Domain.Ris;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Security;
using System.Xml;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ReportService : DisposableServiceBase, IReportService
    {
        private IReportRepository _ReportRepository;
        private IReportFileRepository _ReportFileRepository;
        private IRisProContext _dbContext;
        private IReportPrintService _reportPrintService;
   

        public ReportService(
            IReportRepository reportRepository,
            IReportFileRepository reportFileRepository,
            IRisProContext dbContext,
            IReportPrintService reportPrintService
            )
        {
            _ReportRepository = reportRepository;
            _ReportFileRepository = reportFileRepository;
            _dbContext = dbContext;
            _reportPrintService = reportPrintService;
            AddDisposableObject(reportRepository);
            AddDisposableObject(reportFileRepository);
            AddDisposableObject(dbContext);
            AddDisposableObject(reportPrintService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public ReportDto GetReport(string reportID)
        {
            var report = _ReportRepository.Get(p => p.UniqueID == reportID.ToString()).FirstOrDefault();
            if (report != null)
            {
                return Mapper.Map<Report, ReportDto>(report);
            }
            return null;
        }

        public ReportDto GetReportByProcedureID(string procedureID)
        {
            var query = (from p in _dbContext.Set<Procedure>()
                         join o in _dbContext.Set<Report>() on p.ReportID equals o.UniqueID
                         where p.UniqueID == procedureID
                         select o);
            var report = query.FirstOrDefault();
            if (report != null)
            {
                return Mapper.Map<Report, ReportDto>(report);
            }
            return null;
        }

        public void AddReport(ReportDto reportDto)
        {
            Report report = Mapper.Map<ReportDto, Report>(reportDto);
            if (report.Comments != null)
                report.Comments = report.Comments.Replace("<BR>", "\r\n").Replace("<br>", "\r\n").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Trim("\t\r\n ".ToCharArray());

            _dbContext.Set<Report>().Add(report);

            ReportList reportList = Mapper.Map<ReportDto, ReportList>(reportDto);
            reportList.OperationTime = DateTime.Now;
            reportList.ReportID = reportDto.UniqueID;
            reportList.UniqueID = Guid.NewGuid().ToString();
            _dbContext.Set<ReportList>().Add(reportList);
            _dbContext.SaveChanges();
        }

        public void UpdateReport(ReportDto reportDto)
        {
            var report = _dbContext.Set<Report>().Where(p => p.UniqueID == reportDto.UniqueID).FirstOrDefault();
            var oldCommets = report.Comments;
            Mapper.Map(reportDto, report);
            report.Comments = oldCommets;
            //list
            ReportList reportList = Mapper.Map<ReportDto, ReportList>(reportDto);
            reportList.OperationTime = DateTime.Now;
            reportList.ReportID = reportDto.UniqueID;
            reportList.UniqueID = Guid.NewGuid().ToString();
            _dbContext.Set<ReportList>().Add(reportList);

            _dbContext.SaveChanges();
        }

        public void UpdateComments(ReportDto reportDto)
        {
            var report = _dbContext.Set<Report>().Where(p => p.UniqueID == reportDto.UniqueID).FirstOrDefault();
            //convert html to text
            if (reportDto.Comments != null)
                report.Comments = reportDto.Comments.Replace("<BR>", "\r\n").Replace("<br>", "\r\n").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Trim("\t\r\n ".ToCharArray());
            _dbContext.SaveChanges();
        }

        public ReportDto CreateReport(ReportDto reportDto, string userName, string userID, string domain, string site)
        {
            SetReportInfoForCreateReport(reportDto, userID, domain, site);

            AddReport(reportDto);
            var report = GetReport(reportDto.UniqueID);
            //update procedue
            if (report != null && reportDto.ProcedureIDs != null)
            {
                string orderID = "";
                foreach (string procedureID in reportDto.ProcedureIDs)
                {
                    Procedure procedure = _dbContext.Set<Procedure>().Where(p => p.UniqueID == procedureID).FirstOrDefault();
                    if (procedure != null)
                    {
                        procedure.Status = reportDto.Status.Value;
                        procedure.ReportID = reportDto.UniqueID;
                        orderID = procedure.OrderID;
                    }
                }

                if (IsFinishReferral(reportDto, site))
                {
                    Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == orderID).FirstOrDefault();
                    if (order != null)
                    {
                        order.CurrentSite = order.ExamSite;
                    }
                }

                _dbContext.SaveChanges();
            }

            return report;
        }

        private void SetReportInfoForCreateReport(ReportDto reportDto, string userID, string domain, string site)
        {
            if (string.IsNullOrEmpty(reportDto.UniqueID))
            {
                reportDto.UniqueID = Guid.NewGuid().ToString();
            }

            if (reportDto.IsPositive == -1)
            {
                reportDto.IsPositive = null;
            }

            if (string.IsNullOrEmpty(reportDto.UniqueID))
            {
                reportDto.UniqueID = Guid.NewGuid().ToString();
            }
            reportDto.CreateTime = DateTime.Now;
            reportDto.Domain = domain;
            reportDto.Creater = userID;
            reportDto.IsPrint = false;
            reportDto.IsLeaveWord = false;
            reportDto.IsDraw = false;
            reportDto.IsLeaveSound = false;

            //not null
            reportDto.ReportName = "";
            if (reportDto.ProcedureIDs != null && reportDto.ProcedureIDs.Count > 0)
            {
                reportDto.ReportName = MakeReportName(reportDto.ProcedureIDs.ToArray());
            }
            reportDto.ScoringVersion = "";
            reportDto.AccordRate = "";

            //
            if (reportDto.Status == null || reportDto.Status == -1)
            {
                reportDto.Status = (int)RPStatus.Draft;
            }

            if (reportDto.Status >= (int)RPStatus.Submit)
            {
                reportDto.SubmitDomain = domain;
                reportDto.SubmitSite = site;
                reportDto.Submitter = userID;
                reportDto.SubmitTime = DateTime.Now;
            }
            //FirstApprove
            if (reportDto.Status == (int)RPStatus.FirstApprove)
            {
                reportDto.FirstApproveDomain = domain;
                reportDto.FirstApprover = userID;
                reportDto.FirstApproveSite = site;
                reportDto.FirstApproveTime = DateTime.Now;
            }
            else
            {
                //not save PrintTemplateID when not Approve
                reportDto.PrintTemplateID = "";
            }
        }

        private string GetConfigValue(string name, string site)
        {
            SiteProfile siteProfile = _dbContext.Set<SiteProfile>().Where(s => s.Name == name && s.Site == site).FirstOrDefault();

            if (siteProfile != null)
            {
                return siteProfile.Value;
            }

            SystemProfile systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == name).FirstOrDefault();

            if (systemProfile != null)
            {
                return systemProfile.Value;
            }

            return "";
        }

        public void ModifyReport(ReportDto reportDto, string userName, string userID, string domain, string site)
        {
            if (reportDto.IsPositive == -1)
            {
                reportDto.IsPositive = null;
            }

            //delete
            if (reportDto.DeleteMark.HasValue && reportDto.DeleteMark.Value)
            {

                reportDto.Deleter = userID;
                reportDto.DeleteTime = DateTime.Now;
                DeleteReport(reportDto);
            }
            else
            {
                SetReportInfoByDiffStatus(reportDto, userID, domain, site);
                reportDto.Mender = userID;
                reportDto.ModifyTime = DateTime.Now;

                UpdateReport(reportDto);

                //update procedue
                if (reportDto.ProcedureIDs != null)
                {
                    string orderID = "";
                    foreach (string procedureID in reportDto.ProcedureIDs)
                    {
                        Procedure procedure = _dbContext.Set<Procedure>().Where(p => p.UniqueID == procedureID).FirstOrDefault();

                        if (procedure != null)
                        {
                            procedure.Status = reportDto.Status.Value;
                            procedure.ReportID = reportDto.UniqueID;
                            orderID = procedure.OrderID;
                        }
                    }

                    if (IsFinishReferral(reportDto, site))
                    {
                        Order order = _dbContext.Set<Order>().Where(p => p.UniqueID == orderID).FirstOrDefault();
                        if (order != null)
                        {
                            order.CurrentSite = order.ExamSite;
                        }
                    }

                    _dbContext.SaveChanges();
                }

            }
        }

        private bool IsFinishReferral(ReportDto reportDto, string site)
        {
            if (reportDto.Status == (int)RPStatus.FirstApprove)
            {
                //referal
                string delayFinishReferralAfterApprove = GetConfigValue("ReportEditor_DelayFinishReferralAfterApprove", site);
                if (delayFinishReferralAfterApprove == "0")
                {
                    string finishReferralAfterApprove = GetConfigValue("ReportEditor_FinishReferralAfterApprove", site);
                    if (finishReferralAfterApprove == "1")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void SetReportInfoByDiffStatus(ReportDto reportDto, string userID, string domain, string site)
        {
            if (reportDto.Status == (int)RPStatus.Submit)
            {
                reportDto.SubmitDomain = domain;
                reportDto.SubmitSite = site;
                reportDto.Submitter = userID;
                reportDto.SubmitTime = DateTime.Now;
            }
            //reject
            else if (reportDto.Status == (int)RPStatus.Reject)
            {
                reportDto.RejectDomain = domain;
                reportDto.Rejecter = userID;
                reportDto.RejectSite = site;
                reportDto.RejectTime = DateTime.Now;
                reportDto.SubmitDomain = "";
                reportDto.SubmitSite = "";
                reportDto.Submitter = "";
                reportDto.SubmitterName = "";
                reportDto.SubmitTime = null;
            }
            //FirstApprove
            else if (reportDto.Status == (int)RPStatus.FirstApprove)
            {
                reportDto.FirstApproveDomain = domain;
                reportDto.FirstApprover = userID;
                reportDto.FirstApproveSite = site;
                reportDto.FirstApproveTime = DateTime.Now;
            }
            //SecondApprove
            else if (reportDto.Status == (int)RPStatus.SecondApprove)
            {
                reportDto.SecondApproveDomain = domain;
                reportDto.SecondApprover = userID;
                reportDto.SecondApproveSite = site;
                reportDto.SecondApproveTime = DateTime.Now;
            }

            if (reportDto.Status < (int)RPStatus.FirstApprove)
            {
                reportDto.PrintTemplateID = "";
            }
        }

        private string MakeReportName(string[] rpGuids)
        {
            string newName = "Error Report " + Guid.NewGuid().ToString();

            if (rpGuids == null || rpGuids.Length == 0)
            {
                throw (new Exception("Miss Parameter"));
            }

            string accNo = "";
            string desc = "";
            GetInfoForReportName(rpGuids, out accNo, out desc);

            if (accNo != "")
            {
                newName = ReportUtils.StringRight(accNo, 4);
                newName += "_" + desc;
                newName += DateTime.Now.Second.ToString();
                newName = "_" + newName;
            }

            return newName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportID"></param>
        public void DeleteReport(string reportID)
        {
            var report = _ReportRepository.Get(p => p.UniqueID == reportID).FirstOrDefault();

            if (report != null)
            {
                _ReportRepository.Delete(report);

                _ReportRepository.SaveChanges();
            }
        }

        /// <summary>
        /// delete report
        /// </summary>
        /// <param name="reportDto"></param>
        public void DeleteReport(ReportDto reportDto)
        {
            var report = _dbContext.Set<Report>().Where(p => p.UniqueID == reportDto.UniqueID).FirstOrDefault();

            if (report != null)
            {
                //update procedure
                var procedureList = (from p in _dbContext.Set<Procedure>()
                                     where p.ReportID == reportDto.UniqueID
                                     select p).ToList();
                foreach (Procedure procedure in procedureList)
                {
                    procedure.Status = System.Convert.ToInt32(RPStatus.Examination);
                    procedure.ReportID = "";
                }

                //insert ReportDelPool
                _dbContext.Set<ReportDelPool>().Add(Mapper.Map<ReportDto, ReportDelPool>(reportDto));

                //remove ReportList
                var reportList = (from p in _dbContext.Set<ReportList>()
                                  where p.ReportID == reportDto.UniqueID
                                  select p).ToList();
                foreach (ReportList reportListItem in reportList)
                {
                    _dbContext.Set<ReportList>().Remove(reportListItem);
                }

                //remove report
                _dbContext.Set<Report>().Remove(report);

                _dbContext.SaveChanges();

            }
        }

        public IEnumerable<ReportFileDto> GetReportFilesByReportID(string reportID)
        {
            var query = _ReportFileRepository.Get(p => p.ReportID == reportID);
            var reportFiles = query.Select(p => Mapper.Map<ReportFile, ReportFileDto>(p)).ToList();
            return reportFiles;
        }

        /// <summary>
        /// get report history
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public IEnumerable<ReportListDto> GetReportListByReportID(string reportID)
        {
            List<ReportListDto> reportLists = new List<ReportListDto>();
            var query = (from r in _dbContext.Set<ReportList>()
                         join u in _dbContext.Set<User>() on r.Creater equals u.UniqueID
                         into ru
                         from ru1 in ru.DefaultIfEmpty()
                         join u2 in _dbContext.Set<User>() on r.Mender equals u2.UniqueID
                         into ru2
                         from ru3 in ru2.DefaultIfEmpty()
                         where r.ReportID == reportID
                         select new
                         {
                             UniqueID = r.UniqueID,
                             WYS = r.WYS,
                             WYG = r.WYG,
                             IsPositive = r.IsPositive,
                             Creater = ru1.LocalName,
                             Mender = ru3.LocalName,
                             OperationTime = r.OperationTime,
                             Status = r.Status
                         }).Distinct().OrderByDescending(c => c.OperationTime).ToList();

            foreach (var r in query)
            {
                ReportListDto reportListDto = new ReportListDto
                {
                    UniqueID = r.UniqueID,
                    WYSText = ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(r.WYS)),
                    WYGText = ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(r.WYG)),
                    IsPositive = r.IsPositive,
                    Creater = r.Creater,
                    Mender = r.Mender,
                    Status = r.Status,
                    OperationTime = r.OperationTime
                };
                reportLists.Add(reportListDto);
            }

            return reportLists;
        }

        /// <summary>
        /// create repor tname
        /// </summary>
        /// <param name="rpIDs"></param>
        /// <param name="accNo"></param>
        /// <param name="description"></param>
        private void GetInfoForReportName(string[] rpIDs, out string accNo, out string description)
        {
            accNo = "";
            description = "";
            string procedureID = rpIDs[0];
            var query = (from o in _dbContext.Set<Order>()
                         join p in _dbContext.Set<Procedure>() on o.UniqueID equals p.OrderID
                         where procedureID == p.UniqueID
                         select new
                         {
                             AccNo = o.AccNo,
                         });

            foreach (var res in query)
            {
                accNo = res.AccNo;
                break;
            }

            List<string> descList = new List<string>();
            foreach (string procedureid in rpIDs)
            {
                var query2 = (from p in _dbContext.Set<Procedure>()
                              join c in _dbContext.Set<Procedurecode>() on p.ProcedureCode equals c.ProcedureCode
                              where procedureid == p.UniqueID
                              select new
                              {
                                  Description = c.Description
                              });


                foreach (var res in query2)
                {
                    descList.Add(res.Description);
                }
            }
            description = string.Join("_", descList.ToArray());
        }

        public IEnumerable<ProcedureDto> GetProcedureByReportID(string reportID)
        {
            var query = _dbContext.Set<Procedure>().Where(p => p.ReportID == reportID).ToList();
            List<ProcedureDto> procedureDtos = new List<ProcedureDto>();
            foreach (Procedure procedure in query)
            {
                procedureDtos.Add(Mapper.Map<Procedure, ProcedureDto>(procedure));
            }

            return procedureDtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IEnumerable<ProcedureDto> GetProcedureByOrderID(string orderID)
        {
            var query = _dbContext.Set<Procedure>().Where(p => p.OrderID == orderID).ToList();
            List<ProcedureDto> procedureDtos = new List<ProcedureDto>();
            foreach (Procedure procedure in query)
            {
                if (procedure.Status == (int)RPStatus.Examination)
                {
                    procedureDtos.Add(Mapper.Map<Procedure, ProcedureDto>(procedure));
                }
            }

            return procedureDtos;
        }

        #region reject
        /// <summary>
        /// get role for reject
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> GetAllRoleForReport()
        {
            var query = (from u in _dbContext.Set<User>()
                         join ru in _dbContext.Set<RoleToUser>() on u.UniqueID equals ru.UserID
                         into uru
                         from uru1 in uru.DefaultIfEmpty()
                         join r in _dbContext.Set<Role>() on uru1.RoleName equals r.RoleName
                         into ur2
                         from ur21 in ur2.DefaultIfEmpty()
                         where ur21.UniqueID != null && ur21.RoleName.Contains("Radiologist")
                         select new RoleDto
                         {
                             RoleName = ur21.Description,
                             UniqueID = ur21.UniqueID,
                         }).Distinct().OrderBy(c => c.RoleName);
            return query.ToList();
        }

        /// <summary>
        /// get user for reject by roleid
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<UserDto> GetUserForReportByRoleID(string roleID)
        {
            //reference GetAllRoleForReport
            var query = (from u in _dbContext.Set<User>()
                         join ru in _dbContext.Set<RoleToUser>() on u.UniqueID equals ru.UserID
                         into uru
                         from uru1 in uru.DefaultIfEmpty()
                         join r in _dbContext.Set<Role>() on uru1.RoleName equals r.RoleName
                         into ur2
                         from ur21 in ur2.DefaultIfEmpty()
                         where ur21.UniqueID == roleID
                         select new UserDto
                         {
                             LoginName = u.LoginName,
                             LocalName = u.LocalName,
                             UniqueID = u.UniqueID
                         }).Distinct().OrderBy(c => c.LoginName);
            return query.ToList();
        }
        #endregion

        private class OrderPatient
        {
            public string PatientNo { get; set; }
            public string LocalName { get; set; }
            public string Gender { get; set; }
            public string AccNo { get; set; }
            public string CurrentAge { get; set; }
        }
        #region baseInfo
        /// <summary>
        /// 需要优化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetBaseInfoDescByProcedure(ProcedureDto procedure)
        {
            string baseInfoDesc = "";
            if (procedure != null)
            {
                var orderPatient = (from o in _dbContext.Set<Order>()
                                    join p in _dbContext.Set<Patient>() on o.PatientID equals p.UniqueID
                                    where o.UniqueID == procedure.OrderID
                                    select new OrderPatient
                                    {
                                        PatientNo = p.PatientNo,
                                        LocalName = p.LocalName,
                                        Gender = p.Gender,
                                        AccNo = o.AccNo,
                                        CurrentAge = o.CurrentAge
                                    }).FirstOrDefault(); ;

                if (orderPatient != null)
                {
                    //当前年龄
                    var groupedDVs = _dbContext.Set<DictionaryValue>().Where(d => d.Tag == (int)Enums.DictionaryTag.AgeCompany).ToList();
                    string[] split = orderPatient.CurrentAge.Split(new Char[] { ' ' });
                    var age = "";
                    if (split.Count() >= 1)
                    {
                        var ageDictionaryValue = groupedDVs.Where(d => d.Value.Equals(split[1], StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (ageDictionaryValue != null)
                        {
                            age = split[0] + ageDictionaryValue.Text;
                        }
                    }
                    baseInfoDesc += orderPatient.PatientNo;
                    baseInfoDesc += "," + orderPatient.LocalName;
                    baseInfoDesc += "," + orderPatient.Gender;
                    baseInfoDesc += "," + age;
                    baseInfoDesc += "," + orderPatient.AccNo;
                    baseInfoDesc += "," + procedure.CheckingItem;
                }
            }

            return baseInfoDesc;
        }
        #endregion

        public List<ReportDto> GetOtherReportsByReportID(string id)
        {
            var order = (from r in _dbContext.Set<Report>()
                         join p in _dbContext.Set<Procedure>() on r.UniqueID equals p.ReportID
                         join o in _dbContext.Set<Order>() on p.OrderID equals o.UniqueID
                         where r.UniqueID == id
                         select o).FirstOrDefault();
            if (order != null)
            {
                var reports = (from r in _dbContext.Set<Report>()
                               join p in _dbContext.Set<Procedure>() on r.UniqueID equals p.ReportID
                               join o in _dbContext.Set<Order>() on p.OrderID equals o.UniqueID
                               join u in _dbContext.Set<User>() on r.Creater equals u.UniqueID
                               into rcreater
                               from rcreaterdata in rcreater.DefaultIfEmpty()
                               join u2 in _dbContext.Set<User>() on r.Submitter equals u2.UniqueID
                                into ru
                               from ru1 in ru.DefaultIfEmpty()
                               join u3 in _dbContext.Set<User>() on r.FirstApprover equals u3.UniqueID
                               into ru3
                               from ru3u in ru.DefaultIfEmpty()
                               where o.PatientID == order.PatientID && r.UniqueID != id
                               select new ReportDto
                               {
                                   UniqueID = r.UniqueID,
                                   WYGText = r.WYGText,
                                   WYSText = r.WYSText,
                                   IsPositive = r.IsPositive,
                                   CreateTime = r.CreateTime,
                                   CreaterName = rcreaterdata.LocalName,
                                   SubmitterName = ru1.LocalName,
                                   FirstApproverName = ru3u.LocalName,
                                   ModifyTime = r.ModifyTime,
                                   ReportName = p.CheckingItem,
                                   //temp
                                   Creater = p.UniqueID,
                                   Status = p.Status
                               }
                               ).ToList();

                return GetOtherReportProcess(reports);
            }

            return null;
        }

        public List<ReportDto> GetOtherReportsByProcedureID(string id)
        {
            var order = (from p in _dbContext.Set<Procedure>()
                         join o in _dbContext.Set<Order>() on p.OrderID equals o.UniqueID
                         where p.UniqueID == id
                         select o).FirstOrDefault();
            if (order != null)
            {
                var reports = (from r in _dbContext.Set<Report>()
                               join p in _dbContext.Set<Procedure>() on r.UniqueID equals p.ReportID
                               join o in _dbContext.Set<Order>() on p.OrderID equals o.UniqueID
                               where o.PatientID == order.PatientID && r.UniqueID != id
                               select new
                               {
                                   r.UniqueID,
                                   r.WYGText,
                                   r.WYSText,
                                   r.IsPositive,
                                   r.CreateTime,
                                   r.ModifyTime,
                                   p.CheckingItem,
                                   Creater = p.UniqueID,
                                   p.Status,
                                   tmpCreaterID = r.Creater,
                                   tmpMenderID = r.Mender,
                                   tmpSubmitterID = r.Submitter,
                                   tmpApproverID = r.FirstApprover
                               }).ToList();

                var userList = new List<string>();
                reports.ForEach(r =>
                {
                    if (!string.IsNullOrEmpty(r.tmpCreaterID))
                    {
                        userList.Add(r.tmpCreaterID);
                    }
                    if (!string.IsNullOrEmpty(r.tmpMenderID))
                    {
                        userList.Add(r.tmpMenderID);
                    }
                    if (!string.IsNullOrEmpty(r.tmpSubmitterID))
                    {
                        userList.Add(r.tmpSubmitterID);
                    }
                    if (!string.IsNullOrEmpty(r.tmpApproverID))
                    {
                        userList.Add(r.tmpApproverID);
                    }
                });
                userList = userList.Distinct().ToList();

                var dbUserList = from u in _dbContext.Set<User>()
                                 where userList.Contains(u.UniqueID)
                                 select new { u.UniqueID, u.LocalName };

                var reportDtos =
                    (from r in reports
                     select new ReportDto
                     {
                         UniqueID = r.UniqueID,
                         WYGText = r.WYGText,
                         WYSText = r.WYSText,
                         IsPositive = r.IsPositive,
                         CreateTime = r.CreateTime,
                         CreaterName = dbUserList.Where(u => u.UniqueID == r.tmpCreaterID).Select(u => u.LocalName).FirstOrDefault(),
                         MenderName = dbUserList.Where(u => u.UniqueID == r.tmpMenderID).Select(u => u.LocalName).FirstOrDefault(),
                         ModifyTime = r.ModifyTime,
                         ReportName = r.CheckingItem,
                         SubmitterName = dbUserList.Where(u => u.UniqueID == r.tmpSubmitterID).Select(u => u.LocalName).FirstOrDefault(),
                         FirstApproverName = dbUserList.Where(u => u.UniqueID == r.tmpApproverID).Select(u => u.LocalName).FirstOrDefault(),
                         Creater = r.Creater,
                         Status = r.Status
                     }).ToList();
                return GetOtherReportProcess(reportDtos);
            }

            return null;
        }

        private List<ReportDto> GetOtherReportProcess(List<ReportDto> reports)
        {

            List<string> combineList = new List<string>();
            foreach (ReportDto report in reports)
            {
                if (report.ModifyTime != null && report.ModifyTime > report.CreateTime)
                {
                    report.CreateTime = report.ModifyTime;
                }

                if (!combineList.Contains(report.Creater))
                {
                    List<ReportDto> reportList = reports.Where(r => r.UniqueID == report.UniqueID).ToList();
                    if (reportList.Count > 1)
                    {
                        List<string> checkNames = new List<string>();
                        foreach (ReportDto reportForName in reportList)
                        {
                            checkNames.Add(reportForName.ReportName);
                            if (reportForName.Creater != report.Creater)
                            {
                                combineList.Add(reportForName.Creater);
                            }
                        }
                        report.ReportName = string.Join(",", checkNames.ToArray());
                    }
                }
            }

            foreach (string pid in combineList)
            {
                var report = reports.Where(r => r.Creater == pid).FirstOrDefault();
                if (report != null)
                {
                    reports.Remove(report);
                }
            }

            reports = reports.OrderByDescending(r => r.CreateTime).ToList();

            return reports;
        }

        public string GetPacsUrl(string procedureID, string loginUserID)
        {
            var procedure = _dbContext.Set<Procedure>().FirstOrDefault(p => p.UniqueID == procedureID);
            var procedureDto = Mapper.Map<Procedure, ProcedureDto>(procedure);
            var dt = _reportPrintService.GetBaseInfoByProcedure(procedureDto);
            var siteName = dt.Rows[0]["tRegOrder__ExamSite"].ToString(); ;
            if (siteName != "")
            {
                var site = _dbContext.Set<Site>().FirstOrDefault(p => p.SiteName == siteName);
                if (site == null)
                {
                    return "";
                }
                var ret = site.PacsWebServer;
                ret = ret.ToLower();
                var user = _dbContext.Set<User>().FirstOrDefault(p => p.UniqueID == loginUserID);
                if (ret.Contains("{user_name}"))
                {
                    ret = ret.Replace("{user_name}", user.LoginName);
                }
                if (ret.Contains("{password}"))
                {
                    string password = "";
                    try
                    {
                        var c = new Cryptography("GCRIS2-20061025");
                        var decryptedPassword = c.DeEncrypt(user.Password);
                        if (decryptedPassword != "")
                        {
                            password = decryptedPassword;
                        }
                    }
                    catch
                    {
                    }

                    ret = ret.Replace("{password}", password);
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ret = ret.Replace("{" + dt.Columns[i].ColumnName.ToLower() + "}", ReportUtils.GetFieldValue(dt, 0, i));
                    if (ret.IndexOf("{") < 0) break;
                }
                return ret;
            }
            return "";
        }

        public string GetPacsUrlDX(string procedureID, string loginUserID)
        {
            Procedure procedure = _dbContext.Set<Procedure>().Where(p => p.UniqueID == procedureID).FirstOrDefault();
            ProcedureDto procedureDto = Mapper.Map<Procedure, ProcedureDto>(procedure);
            DataTable dt = _reportPrintService.GetBaseInfoByProcedure(procedureDto);
            string siteName = dt.Rows[0]["tbRegOrder__ExamSite"].ToString();
            if (siteName != "")
            {
                Site site = _dbContext.Set<Site>().Where(p => p.SiteName == siteName).FirstOrDefault();
                string ret = site.PacsServer;
                ret = ret.ToLower();
                User user = _dbContext.Set<User>().Where(p => p.UniqueID == loginUserID).FirstOrDefault();
                if (ret.Contains("{user_name}"))
                {
                    ret = ret.Replace("{user_name}", user.LoginName);
                }
                if (ret.Contains("{password}"))
                {
                    string password = "";
                    try
                    {
                        var c = new Cryptography("GCRIS2-20061025");
                        var decryptedPassword = c.DeEncrypt(user.Password);
                        if (decryptedPassword != "")
                        {
                            password = decryptedPassword;
                        }
                    }
                    catch
                    {
                    }

                    ret = ret.Replace("{password}", password);
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ret = ret.Replace("{" + dt.Columns[i].ColumnName.ToLower() + "}", ReportUtils.GetFieldValue(dt, 0, i));
                    if (ret.IndexOf("{") < 0) break;
                }

                return ret;

            }
            return "";
        }

        public List<string> GetProcedureIDsByReportID(string id)
        {
            List<string> procedureIDs = new List<string>();
            IEnumerable<ProcedureDto> procedureDtos = GetProcedureByReportID(id);
            foreach (ProcedureDto procedureDto in procedureDtos)
            {
                procedureIDs.Add(procedureDto.UniqueID);
            }
            return procedureIDs;
        }

        public List<string> GetProcedureIDsByOrderID(string id)
        {
            IEnumerable<ProcedureDto> procedureDtos = GetProcedureByOrderID(id);
            List<string> procedureIDs = new List<string>();
            foreach (ProcedureDto procedureDto in procedureDtos)
            {
                procedureIDs.Add(procedureDto.UniqueID);
            }

            return procedureIDs;
        }

        public List<string> GetExamedProcedureIDsByOrderID(string id)
        {
            IEnumerable<ProcedureDto> procedureDtos = GetProcedureByOrderID(id);
            List<string> procedureIDs = new List<string>();
            foreach (ProcedureDto procedureDto in procedureDtos)
            {
                if (procedureDto.Status == (int)RPStatus.Examination)
                {
                    procedureIDs.Add(procedureDto.UniqueID);
                }
            }

            return procedureIDs;
        }

        public string GetBaseInfoDescByProcedureID(string id)
        {
            string baseInfoDesc = "";
            var pro = _dbContext.Set<Procedure>().Where(r => r.UniqueID== id).FirstOrDefault();
            ProcedureDto procedure = null;
            if (pro != null)
            {
                if (string.IsNullOrEmpty(pro.ReportID))
                {
                    procedure = Mapper.Map<Procedure, ProcedureDto>(pro);
                }
                else
                {
                    List<ProcedureDto> procedureDtos = GetProcedureByReportID(pro.ReportID).ToList();
                    procedure = procedureDtos[0];

                    foreach (ProcedureDto procedureItem in procedureDtos)
                    {
                        if (procedureItem.UniqueID != procedure.UniqueID)
                        {
                            //if (procedureItem.BodyPart != "")
                            //{
                            //    procedure.BodyPart += "," + procedureItem.BodyPart;
                            //}

                            //if (procedureItem.ProcedureCode != "")
                            //{
                            //    procedure.ProcedureCode += "," + procedureItem.ProcedureCode;
                            //}

                            //if (procedureItem.RPDesc != "")
                            //{
                            //    procedure.RPDesc += "," + procedureItem.RPDesc;
                            //}

                            if (procedureItem.CheckingItem != "")
                            {
                                procedure.CheckingItem += "," + procedureItem.CheckingItem;
                            }
                        }
                    }
                }
            }
            baseInfoDesc = GetBaseInfoDescByProcedure(procedure);
            return baseInfoDesc;
        }

        public string GetBaseInfoDescByOrderID(string id)
        {
            string baseInfoDesc = "";
            List<ProcedureDto> procedureDtos = GetProcedureByOrderID(id).ToList();

            // assume procedures count should not be 0 because it is locked by client code, but return null
            // in case the api is not called from client
            if (procedureDtos.Count == 0)
            {
                return baseInfoDesc;
            }

            ProcedureDto procedure = procedureDtos[0];

            foreach (ProcedureDto procedureItem in procedureDtos)
            {
                if (procedureItem.UniqueID != procedure.UniqueID)
                {
                    //if (procedureItem.BodyPart != "")
                    //{
                    //    procedure.BodyPart += "," + procedureItem.BodyPart;
                    //}

                    //if (procedureItem.ProcedureCode != "")
                    //{
                    //    procedure.ProcedureCode += "," + procedureItem.ProcedureCode;
                    //}

                    //if (procedureItem.RPDesc != "")
                    //{
                    //    procedure.RPDesc += "," + procedureItem.RPDesc;
                    //}
                    if (procedureItem.CheckingItem != "")
                    {
                        procedure.CheckingItem += "," + procedureItem.CheckingItem;
                    }
                }
            }
            baseInfoDesc = GetBaseInfoDescByProcedure(procedure);

            return baseInfoDesc;
        }

        public ProcedureDto GetIntegerProcedureByReportID(string id)
        {
            var queryProcedure = GetProcedureByReportID(id).ToList();
            ProcedureDto procedure = queryProcedure[0];
            foreach (ProcedureDto procedureItem in queryProcedure)
            {
                if (procedureItem.UniqueID != procedure.UniqueID)
                {
                    if (procedureItem.BodyPart != "")
                    {
                        procedure.BodyPart += "," + procedureItem.BodyPart;
                    }

                    if (procedureItem.ProcedureCode != "")
                    {
                        procedure.ProcedureCode += "," + procedureItem.ProcedureCode;
                    }

                    if (procedureItem.RPDesc != "")
                    {
                        procedure.RPDesc += "," + procedureItem.RPDesc;
                    }
                }
            }

            return procedure;
        }

        public OrderLiteDto GetOrderByProcedureID(string id)
        {
            var order = (from o in _dbContext.Set<Order>()
                         join p in _dbContext.Set<Procedure>() on o.UniqueID equals p.OrderID
                         where p.UniqueID.Equals(id)
                         select new OrderLiteDto
                         {
                             IsScan = o.IsScan.HasValue ? (o.IsScan == 1) : false,
                             AccNo = o.AccNo,
                             UniqueID = o.UniqueID,
                             ReferralID = o.ReferralID
                         }
                         ).FirstOrDefault();
            return order;
        }

        /// <summary>
        /// Get  image status by orderId
        /// </summary>
        /// <returns></returns>
        public bool GetImageStatusByOrderId(string orderId)
        {
            var existImage = 1;
            var result = _dbContext.Set<Procedure>().Any(p => p.OrderID.Equals(orderId)
                && p.IsExistImage.HasValue && p.IsExistImage.Value.Equals(existImage));
            return result;

        }

    }
}
