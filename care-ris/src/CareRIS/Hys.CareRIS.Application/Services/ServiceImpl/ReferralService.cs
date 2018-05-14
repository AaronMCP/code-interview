using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Hys.CrossCutting.Common.Interfaces;
using Hys.CareRIS.Application.Dtos.Referral;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities.Referral;
using Hys.CareRIS.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Data;
using System.IO;
using System.Text;
using Hys.CrossCutting.Common.Utils;
using System.Transactions;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ReferralService : DisposableServiceBase, IReferralService
    {
        private readonly IRisProContext _dbContext;
        private IReportLockService _reportLockService;

        public ReferralService(IRisProContext dbContext,
            IReportLockService reportLockService)
        {
            _dbContext = dbContext;
            _reportLockService = reportLockService;
            AddDisposableObject(dbContext);
            AddDisposableObject(reportLockService);
        }

        public ReferralListSearchDto GetReferralList(ReferralListSearchCriteriaDto searchCriteria, string userSite)
        {
            var query = from r in _dbContext.Set<ReferralList>()
                        join s in _dbContext.Set<Site>() on r.TargetSite equals s.SiteName into sties
                        from site in sties.DefaultIfEmpty()
                        where r.SourceSite.Equals(userSite)
                        select new ReferralListDto
                        {
                            AccNo = r.AccNo,
                            ReferralID = r.UniqueID,
                            Gender = r.Gender,
                            LocalName = r.LocalName,
                            TargetSite = r.TargetSite,
                            RefStatus = r.RefStatus ?? default(int),
                            ProcedureCode = r.ProcedureCode,
                            ModalityType = r.ModalityType,
                            CreateDt = r.CreateDt,
                            Refpurpose = r.Refpurpose ?? default(int),
                            SiteName = site.Alias,
                            RPStatus = r.RPStatus ?? default(int),
                        };

            if (searchCriteria.StatusList != null && searchCriteria.StatusList.Length > 0)
            {
                var statusList = searchCriteria.StatusList.Select(s => (int)s);
                query = query.Where(p => statusList.Contains(p.RefStatus));
            }
            
            var count = query.Count();
            var referrals = query.OrderByDescending(o => o.CreateDt).Skip((searchCriteria.PageIndex - 1) * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize)
                .ToList();

            return new ReferralListSearchDto { Count = count, Referrals = referrals };
        }

        public bool ReSend(string id, string userId)
        {
            var referralList = _dbContext.Set<ReferralList>().Where(r => r.UniqueID == id).FirstOrDefault();
            var order = _dbContext.Set<Order>().Where(o => o.AccNo == referralList.AccNo).FirstOrDefault();

            if (referralList != null)
            {
                ReferralEvent referralEvent = new ReferralEvent();
                ReferralLog referralLog = new ReferralLog();

                CreateReSendReferralEvent(userId, order, ref referralList, ref referralEvent, ref referralLog);
                _dbContext.Set<ReferralEvent>().Add(referralEvent);
                _dbContext.Set<ReferralLog>().Add(referralLog);

                //procedure
                var procedures = _dbContext.Set<Procedure>().Where(o => o.OrderID == order.UniqueID).ToList();
                foreach (Procedure procedure in procedures)
                {
                    procedure.Status = -1;
                }

                _dbContext.SaveChanges();
                return true;

            }

            return false;
        }

        public string GetProcedureID(string accNo, string procedureCode)
        {
            var procedure = (from o in _dbContext.Set<Order>()
                             join p in _dbContext.Set<Procedure>()
                             on o.UniqueID equals p.OrderID
                             where o.AccNo == accNo && procedureCode.Contains(p.ProcedureCode)
                             select p).FirstOrDefault();
            if (procedure != null)
            {
                return procedure.UniqueID;
            }

            return "";
        }

        private ReferralEvent CreateReSendReferralEvent(Order order, ReferralList referralList)
        {
            ReferralEvent referralEvent = new ReferralEvent
            {
                ReferralID = "",
                OperatorGuid = "",
                OperateDt = DateTime.Now,
                SourceDomain = order.Domain,
                TargetDomain = "",
                Event = (int)Enums.Ref_EventType.AutoReferral,
                Status = referralList.RefStatus,
                Tag = 0,
                Content = "ORDERID=" + order.UniqueID + "&MODALITYTYPE=" + referralList.ModalityType,
                ExamDomain = "",
                ExamAccNo = "",
                UniqueID = Guid.NewGuid().ToString(),
                OperatorName = ""

            };

            return referralEvent;
        }

        private void CreateReSendReferralEvent(string userId, Order order, ref ReferralList referralList, ref ReferralEvent referralEvent, ref ReferralLog referralLog)
        {
            User user = _dbContext.Set<User>().Where(u => u.UniqueID == userId).FirstOrDefault();
            if (referralList.Scope == (int)Enums.Ref_Scope.MultiSite)
            {
                referralEvent = new ReferralEvent
                {
                    ReferralID = referralList.UniqueID,
                    OperatorGuid = userId,
                    OperateDt = DateTime.Now,
                    SourceDomain = order.Domain,
                    Event = (int)Enums.Ref_EventType.SendReferral,
                    Status = referralList.Refpurpose,
                    ExamDomain = referralList.ExamDomain,
                    ExamAccNo = referralList.ExamAccNo,
                    UniqueID = Guid.NewGuid().ToString(),
                    OperatorName = user.LocalName,
                    SourceSite = referralList.SourceSite,
                    TargetSite = referralList.TargetSite,
                    TargetDomain = referralList.TargetDomain,
                    Scope = referralList.Scope,
                };

                referralLog = new ReferralLog
                {
                    ReferralID = referralList.UniqueID,
                    OperatorGuid = userId,
                    OperateDt = DateTime.Now,
                    SourceDomain = order.Domain,
                    OperatorName = user.LocalName,
                    SourceSite = referralList.SourceSite,
                    TargetSite = referralList.TargetSite,
                    TargetDomain = referralList.TargetDomain,
                    Memo = "",
                    EventDesc = Enum.GetName(typeof(Enums.Ref_EventType), Enums.Ref_EventType.SendReferral),
                    RefPurpose = referralList.Refpurpose,
                    CreateDt = DateTime.Now
                };

                referralList.RefStatus = (int)Enums.ReferralStatus.Sent;
            }
        }

        public bool SendReferral(ManualReferralDto manualReferralDto, string domain, string site, string userId)
        {
            //get new referralid
            var order = _dbContext.Set<Order>().Where(o => o.UniqueID == manualReferralDto.OrderID).FirstOrDefault();

            //judge whether send the target, reject and cancel
            var specialReferralList = _dbContext.Set<ReferralList>().Where(o => o.AccNo == order.AccNo &&
                o.RefStatus != (int)Enums.ReferralStatus.Rejected &&
                o.RefStatus != (int)Enums.ReferralStatus.Canceled &&
                o.RefStatus != (int)Enums.ReferralStatus.Finished).ToList();
            if (specialReferralList.Count > 0)
            {
                return false;
            }

            var patient = _dbContext.Set<Patient>().Where(o => o.UniqueID == order.PatientID).FirstOrDefault();
            ReferralList referralList = new ReferralList();
            ReferralEvent referralEvent = new ReferralEvent();
            ReferralLog referralLog = new ReferralLog();

            List<Procedure> procedures = _dbContext.Set<Procedure>().Where(o => o.OrderID == manualReferralDto.OrderID).ToList();
            string strprocedureCode = "";
            string strcheckingItem = "";
            string strModalityType = "";
            int status = 0;
            List<string> strprocedureCodes = new List<string>();
            List<string> strcheckingItems = new List<string>();
            if (procedures.Count > 0)
            {
                strModalityType = procedures[procedures.Count - 1].ModalityType;
                foreach (Procedure procedure in procedures)
                {
                    strprocedureCodes.Add(procedure.ProcedureCode);
                    strcheckingItems.Add(procedure.CheckingItem);
                    if (procedure.Status > status)
                    {
                        status = procedure.Status;
                    }
                }
                strprocedureCode = string.Join(",", strprocedureCodes);
                strcheckingItem = string.Join(",", strcheckingItems);
            }

            //set referralList
            referralList.UniqueID = GetNewReferralID(domain);
            referralList.Scope = (int)Enums.Ref_Scope.MultiSite;
            referralList.ProcedureCode = strprocedureCode;
            referralList.CheckingItem = strcheckingItem;
            referralList.ModalityType = strModalityType;
            referralList.RPStatus = status;
            referralList.InitialDomain = domain;
            referralList.SourceDomain = domain;
            referralList.SourceSite = site;
            //patient info
            referralList.PatientID = patient.PatientNo;
            referralList.LocalName = patient.LocalName;
            referralList.EnglishName = patient.EnglishName;
            referralList.Gender = patient.Gender;
            referralList.Birthday = patient.Birthday;
            referralList.TelePhone = patient.Telephone == null ? "" : patient.Telephone;
            referralList.Address = patient.Address == null ? "" : patient.Address;
            //order info
            referralList.AccNo = order.AccNo;
            referralList.ApplyDoctor = order.ApplyDoctor == null ? "" : order.ApplyDoctor;
            referralList.HealthHistory = order.HealthHistory == null ? "" : order.HealthHistory;
            referralList.Observation = order.Observation == null ? "" : order.Observation;
            referralList.IsExistSnapshot = 0;
            referralList.GetReportDomain = "";

            referralList.TargetSite = manualReferralDto.TargetSite;
            Site tarSite = _dbContext.Set<Site>().Where(s => s.SiteName == manualReferralDto.TargetSite).FirstOrDefault();
            if (tarSite != null)
            {
                referralList.TargetDomain = tarSite.Domain;
            }
            if (referralList.RPStatus == (int)RPStatus.Examination)
            {
                referralList.Refpurpose = (int)Hys.CareRIS.Application.Services.ServiceImpl.Enums.ReferralPurpose.WriteReport;
            }
            else if (referralList.RPStatus == (int)RPStatus.Submit)
            {
                referralList.Refpurpose = (int)Hys.CareRIS.Application.Services.ServiceImpl.Enums.ReferralPurpose.ApproveReport;
            }
            referralList.Direction = (int)Hys.CareRIS.Application.Services.ServiceImpl.Enums.Direction.In;
            referralList.CreateDt = DateTime.Now;
            referralList.ExamAccNo = "";
            referralList.ExamDomain = "";
            referralList.OriginalBizData = "";
            referralList.PackagedBizData = "";
            referralList.RefApplication = "";
            referralList.RefReport = "";

            CreateReSendReferralEvent(userId, order, ref referralList, ref referralEvent, ref referralLog);
            //set memo
            referralEvent.Memo = manualReferralDto.Memo;
            referralEvent.Tag = 0;
            referralEvent.Content = "";
            referralLog.Memo = manualReferralDto.Memo;
            _dbContext.Set<ReferralList>().Add(referralList);
            _dbContext.Set<ReferralEvent>().Add(referralEvent);
            _dbContext.Set<ReferralLog>().Add(referralLog);
            //update order
            order.ReferralID = referralList.UniqueID;
            order.IsReferral = 1;

            //update procedure
            foreach (Procedure procedure in procedures)
            {
                procedure.Status = -1;
            }

            _dbContext.SaveChanges();

            return true;
        }

        private string GetNewReferralID(string domain)
        {
            string referralIDLengthName = "ReferralIDLength";
            int length = 5;
            //get length
            SystemProfile systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == referralIDLengthName).FirstOrDefault();
            if (systemProfile != null)
            {
                Int32.TryParse(systemProfile.Value, out length);
                if (length < 1)
                {
                    length = 5;
                }
            }
            //get prefix
            DomainList domainList = _dbContext.Set<DomainList>().Where(s => s.DomainName == domain).FirstOrDefault();
            string preFix = string.Empty;
            if (domainList != null && !string.IsNullOrEmpty(domainList.DomainPrefix))
            {
                preFix = domainList.DomainPrefix;
            }
            int maxID = -1;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                IDMaxValue idMaxValue = _dbContext.Set<IDMaxValue>().Where(s => s.Tag == 4).FirstOrDefault();
                if (idMaxValue == null || idMaxValue.Value == null)
                {
                    return "";
                }

                idMaxValue.Value = idMaxValue.Value + 1;
                maxID = idMaxValue.Value;
                _dbContext.SaveChanges();
                // SAVE DATA
                ts.Complete();
            }

            if (maxID != -1)
            {
                return preFix + maxID.ToString().PadLeft(length, '0');
            }

            return "";
        }

        public List<SiteDto> GetTargetSites(string site)
        {
            string referralSiteOptions = "";
            SiteProfile siteProfile = _dbContext.Set<SiteProfile>().Where(s => s.Site == site && s.Name == Contants.Profile.ReferralSiteOptions).FirstOrDefault();
            if (siteProfile == null)
            {
                SystemProfile systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == Contants.Profile.ReferralSiteOptions).FirstOrDefault();
                referralSiteOptions = systemProfile.Value;
            }
            else
            {
                referralSiteOptions = siteProfile.Value;
            }
            //split
            List<string> referralSiteOptionList = referralSiteOptions.Split(new string[] { Contants.Profile.ReferralSiteOptionsSplit }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (referralSiteOptionList.Count > 0)
            {
                List<SiteDto> sites = _dbContext.Set<Site>().Where(s => referralSiteOptionList.Contains(s.SiteName)).Select(Mapper.Map<Site, SiteDto>).ToList();
                return sites;
            }

            return null;
        }

        public bool GetCanReferral(string role)
        {
            RoleProfile roleProfile = _dbContext.Set<RoleProfile>().Where(s => s.RoleName == role && s.Name == "CanReferral").FirstOrDefault();
            if (roleProfile != null)
            {
                if (!string.IsNullOrEmpty(roleProfile.Value) && roleProfile.Value == "1")
                {
                    return true;
                }
                else if (!string.IsNullOrEmpty(roleProfile.Value) && roleProfile.Value == "0")
                {
                    return false;
                }
            }
            else
            {
                SystemProfile systemProfile = _dbContext.Set<SystemProfile>().Where(s => s.Name == "CanReferral").FirstOrDefault();
                if (systemProfile != null)
                {
                    if (!string.IsNullOrEmpty(systemProfile.Value) && systemProfile.Value == "1")
                    {
                        return true;
                    }
                    else if (!string.IsNullOrEmpty(systemProfile.Value) && systemProfile.Value == "0")
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool CancelReferral(string id, string userId, string localName)
        {
            var referralList = _dbContext.Set<ReferralList>().Where(r => r.UniqueID == id).FirstOrDefault();
            var order = _dbContext.Set<Order>().Where(o => o.AccNo == referralList.AccNo).FirstOrDefault();

            if (referralList != null)
            {
                //validate status
                if (referralList.RefStatus == (int)Enums.ReferralStatus.Accept ||
                    referralList.RefStatus == (int)Enums.ReferralStatus.Arrived ||
                    referralList.RefStatus == (int)Enums.ReferralStatus.Sent ||
                    referralList.RefStatus == (int)Enums.ReferralStatus.SentFailed ||
                    referralList.RefStatus == (int)Enums.ReferralStatus.CancelFailed)
                {
                    bool canCancel = (referralList.Refpurpose == (int)Enums.ReferralPurpose.WriteReport &&
                    referralList.RPStatus <= (int)Hys.CareRIS.Application.Dtos.Report.RPStatus.Examination) ||
                    (referralList.Refpurpose == (int)Enums.ReferralPurpose.ApproveReport &&
                    referralList.RPStatus < (int)Hys.CareRIS.Application.Dtos.Report.RPStatus.FirstApprove);
                    if (!canCancel)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                //valid lock
                bool hasLock = _reportLockService.HasLockForUser(order.UniqueID, userId);
                if (hasLock)
                {
                    return false;
                }
                //log
                ReferralLog referralLog = new ReferralLog();
                referralLog = new ReferralLog
                {
                    ReferralID = referralList.UniqueID,
                    OperatorGuid = userId,
                    OperateDt = DateTime.Now,
                    SourceDomain = order.Domain,
                    OperatorName = localName,
                    SourceSite = referralList.SourceSite,
                    TargetSite = referralList.TargetSite,
                    TargetDomain = referralList.TargetDomain,
                    Memo = "",
                    EventDesc = Enum.GetName(typeof(Enums.Ref_EventType), Enums.Ref_EventType.CancelReferral),
                    RefPurpose = referralList.Refpurpose,
                    CreateDt = DateTime.Now
                };
                _dbContext.Set<ReferralLog>().Add(referralLog);

                //referralList
                referralList.RefStatus = (int)Enums.ReferralStatus.Canceled;

                //procedure
                var procedures = _dbContext.Set<Procedure>().Where(o => o.OrderID == order.UniqueID).ToList();
                foreach (Procedure procedure in procedures)
                {
                    procedure.Status = referralList.RPStatus.Value;
                }

                //order
                order.ReferralID = "";
                order.IsReferral = 0;
                order.Assign2Site = referralList.TargetSite;
                order.CurrentSite = referralList.SourceSite;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
