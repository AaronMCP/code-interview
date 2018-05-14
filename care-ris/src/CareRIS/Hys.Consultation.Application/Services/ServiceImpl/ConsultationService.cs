using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.Configuration;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.CareRIS.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using Hys.CrossCutting.Common.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;
using Hys.Consultation.Domain.Interface;
using Hys.CareRIS.Domain.Entities;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class ConsultationService : DisposableServiceBase, IConsultationService
    {
        private readonly IConsultationContext _dbContext;
        private readonly IRisProContext _risProContext;
        private readonly IEMRItemRepository _EMRItemRepository;
        private readonly IEMRItemDetailRepository _EMRItemDetailRepository;
        private readonly IConsultationConfigurationService _ConsultationConfigurationService;
        private readonly INotificationService _NotificationService;
        private readonly IUserManagementService _UserManagementService;
        private readonly ILoginUserService _LoginUserService;

        public ConsultationService(IEMRItemRepository EMRItemInstanse, IEMRItemDetailRepository EMRItemDetailInstanse,
            IConsultationContext consultationContext, IRisProContext risProContext,
            IConsultationConfigurationService consultationConfigurationService, INotificationService notificationService,
            IUserManagementService userManagementService, ILoginUserService loginUserService)
        {
            _dbContext = consultationContext;
            _risProContext = risProContext;
            _EMRItemRepository = EMRItemInstanse;
            _EMRItemDetailRepository = EMRItemDetailInstanse;
            _ConsultationConfigurationService = consultationConfigurationService;
            _NotificationService = notificationService;
            _UserManagementService = userManagementService;
            _LoginUserService = loginUserService;

            AddDisposableObject(consultationConfigurationService);
        }

        private bool GetOverdue(string range, List<ConsultationDictionaryDto> timeRanges, DateTime? consultationDate, int status)
        {
            bool isOverdue = false;
            DateTime endtime = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(range))
            {
                // time range dic : eg. 9:00|12:00
                var timeRange = timeRanges.Where(t => t.Value.Equals(range)).FirstOrDefault().Description.Split('|');
                if (timeRange.Length > 1 && consultationDate.HasValue && status.Equals((int)ConsultationRequestStatus.Accepted))
                {
                    string hour = timeRange[1];

                    if (!DateTime.TryParse(consultationDate.Value.ToShortDateString() + " " + hour, out endtime))
                    {
                        endtime = DateTime.MaxValue;
                    }
                }
            }
            isOverdue = endtime < DateTime.Now;
            return isOverdue;
        }

        public async Task<ConsultationRequestSearchDto> SearchDoctorRequests(ConsultationRequestSearchCriteriaDto criteria, string language)
        {
            var query = SearchRequest(criteria);
            var totalCount = await query.CountAsync();
            var list = await query.OrderByDescending(o => o.RequestCreateDate).Skip((criteria.PageIndex - 1) * criteria.PageSize)
                .Take(criteria.PageSize).ToListAsync();
            var requestIds = list.Select(r => r.RequestId).ToList();

            var refs = await _dbContext.Set<ConsultationReceiverXRef>().Where(p => requestIds.Contains(p.ConsultationRequestID)).ToListAsync();
            var responseIds = refs.Select(p => p.ReceiverID).Distinct();
            var hospitals = await _dbContext.Set<HospitalProfile>()
                .Where(u => responseIds.Contains(u.UniqueID))
                .Select(s => new { s.UniqueID, s.HospitalName }).ToListAsync();

            var assignments = await _dbContext.Set<ConsultationAssign>().Where(a => requestIds.Contains(a.ConsultationRequestID)).ToListAsync();
            var userIDs = responseIds.Concat(assignments.Select(a => a.AssignedUserID));
            var users = await _risProContext.Set<User>().Where(u => userIDs.Contains(u.UniqueID)).Select(u => new { u.UniqueID, u.LocalName }).ToListAsync();

            var ConsultationTimeRangeType = 2;

            var dic = (Dictionary<string, IEnumerable<ConsultationDictionaryDto>>)HttpContext.Current.Application["dic"];
            var timeRanges = dic[language.ToLower()].Where(d => d.Type == ConsultationTimeRangeType).ToList();
            var result = new List<ConsultationRequestDto>();
            list.ForEach(r =>
            {
                bool isOverdue = GetOverdue(r.ConsultationStartTime, timeRanges, r.ConsultationDate, r.Status);
                result.Add(new ConsultationRequestDto
                {
                    PatientCaseID = r.PatientCaseID,
                    PatientName = r.PatientName,
                    Gender = r.Gender,
                    CurrentAge = r.CurrentAge,
                    PatientNo = r.PatientNo,
                    RequestCreateDate = r.RequestCreateDate,
                    ConsultationDate = r.ConsultationDate,
                    StatusUpdateTime = r.StatusUpdateTime,
                    Status = r.Status,
                    ConsultationStartTime = r.ConsultationStartTime,
                    ConsultationEndTime = r.ConsultationEndTime,
                    ConsultantType = r.ConsultantType,
                    ExpectedTimeRange = r.ExpectedTimeRange,
                    RequestId = r.RequestId,
                    RequestUserName = r.RequestUserName,
                    ReceiveHospitalID = r.ReceiveHospitalID,
                    IsDeleted = r.IsDeleted,
                    IdentityCard = r.IdentityCard,
                    IsOverdue = isOverdue,
                });
            });
            result.ForEach(p =>
            {
                var receiverIDs = refs.Where(e => e.ConsultationRequestID.Equals(p.RequestId))
                    .Select(c => c.ReceiverID);
                if (receiverIDs.Any())
                {
                    switch (p.ConsultantType)
                    {
                        case (int)ReceiveType.Expert:
                            var names = users.Where(u => receiverIDs.Contains(u.UniqueID)).Select(u => u.LocalName);
                            if (names != null)
                            {
                                p.Receiver = String.Join(",", names);
                            }
                            break;
                        case (int)ReceiveType.Center:
                            var hospital = hospitals.FirstOrDefault(u => u.UniqueID.Equals(receiverIDs.FirstOrDefault()));
                            if (hospital != null)
                            {
                                p.Receiver = hospital.HospitalName;
                            }
                            break;
                    }
                }

                var assignment = assignments.Where(a => a.ConsultationRequestID == p.RequestId).OrderByDescending(a => a.IsHost).ToList();
                if (assignment.Count > 0)
                {
                    var experts = assignment.Select(a =>
                    {
                        var user = users.FirstOrDefault(u => u.UniqueID == a.AssignedUserID);
                        if (user != null)
                        {
                            return a.IsHost == 1 ? user.LocalName + "(主持人)" : user.LocalName;
                        }
                        return String.Empty;
                    });
                    p.Experts = String.Join(", ", experts);
                }
            });

            return new ConsultationRequestSearchDto
            {
                Count = totalCount,
                Requests = result,
            };
        }

        public async Task<ConsultationRequestSpecialistSearchDto> SearchSpecialistRequests(ConsultationRequestSearchCriteriaDto criteria, string language)
        {
            var query = SearchRequest(criteria);
            var count = await query.CountAsync();
            var list = await query.OrderByDescending(q => q.RequestCreateDate).Skip((criteria.PageIndex - 1) * criteria.PageSize)
                          .Take(criteria.PageSize).ToListAsync();
            var requestIds = list.Select(r => r.RequestId).ToList();
            var responseIds = _dbContext.Set<ConsultationReceiverXRef>().Where(p => requestIds.Contains(p.ConsultationRequestID))
                .Select(p => p.ReceiverID).Distinct().ToList();
            // get hospital names
            var requestHospitalIds = list.Select(l => l.RequestHospitalID).Distinct().ToArray();
            var hospitalIds = requestHospitalIds.Union(responseIds).Distinct();
            var hospitals = await _dbContext.Set<HospitalProfile>()
                .Where(u => hospitalIds.Contains(u.UniqueID))
                .Select(s => new { s.UniqueID, s.HospitalName }).ToListAsync();
            var assignments = await _dbContext.Set<ConsultationAssign>().Where(a => requestIds.Contains(a.ConsultationRequestID)).ToListAsync();

            // get user names
            var requestUserIds = list.Select(l => l.RequestUserID).Distinct().ToArray();
            var userIds = requestUserIds.Union(responseIds).Union(assignments.Select(a => a.AssignedUserID)).Distinct();

            var users = await _risProContext.Set<User>()
                .Where(u => userIds.Contains(u.UniqueID))
                .Select(u => new { u.UniqueID, u.LocalName }).ToListAsync();

            var results = new List<ConsultationRequestSpecialistDto>();
            var ConsultationTimeRangeType = 2;
            var dic = (Dictionary<string, IEnumerable<ConsultationDictionaryDto>>)HttpContext.Current.Application["dic"];
            var timeRanges = dic[language.ToLower()].Where(d => d.Type == ConsultationTimeRangeType).ToList();

            list.ForEach(p =>
            {
                bool isOverdue = GetOverdue(p.ConsultationStartTime, timeRanges, p.ConsultationDate, p.Status);
                var request = new ConsultationRequestSpecialistDto
                {
                    PatientCaseID = p.PatientCaseID,
                    PatientName = p.PatientName,
                    Gender = p.Gender,
                    CurrentAge = p.CurrentAge,
                    PatientNo = p.PatientNo,
                    ConsultationDate = p.ConsultationDate,
                    StatusUpdateTime = p.StatusUpdateTime,
                    Status = p.Status,
                    ConsultationStartTime = p.ConsultationStartTime,
                    ConsultationEndTime = p.ConsultationEndTime,
                    IsOverdue = isOverdue,
                    RequestCreateDate = p.RequestCreateDate,
                    ExpectedTimeRange = p.ExpectedTimeRange,
                    RequestId = p.RequestId,
                    RequestUserName = p.RequestUserName,
                    ReceiveHospitalID = p.ReceiveHospitalID,
                    IsDeleted = p.IsDeleted,
                    IdentityCard = p.IdentityCard
                };

                var user = users.FirstOrDefault(u => u.UniqueID.Equals(p.RequestUserID));
                if (null != user)
                {
                    request.Requester = request.RequestUserName ?? user.LocalName;
                }

                var hospital = hospitals.FirstOrDefault(h => h.UniqueID.Equals(p.RequestHospitalID));
                if (null != hospital)
                {
                    request.RequesterHospital = hospital.HospitalName;
                }

                var receiverIDS = _dbContext.Set<ConsultationReceiverXRef>().Where(e => e.ConsultationRequestID.Equals(p.RequestId))
                  .Select(c => c.ReceiverID);
                if (receiverIDS != null)
                {
                    if (p.ConsultantType == 1)
                    {
                        var names = users.Where(u => receiverIDS.Contains(u.UniqueID)).Select(u => u.LocalName);
                        if (names != null)
                        {
                            request.Receiver = String.Join(",", names);
                        }
                    }
                    else if (p.ConsultantType == 0)
                    {
                        var receiveHospital = hospitals.FirstOrDefault(u => u.UniqueID.Equals(receiverIDS.FirstOrDefault()));
                        if (receiveHospital != null)
                        {
                            request.Receiver = receiveHospital.HospitalName;
                        }
                    }
                }

                var assignment = assignments.Where(a => a.ConsultationRequestID == p.RequestId).OrderByDescending(a => a.IsHost).ToList();
                if (assignment.Count > 0)
                {
                    var experts = assignment.Select(a =>
                    {
                        var expert = users.FirstOrDefault(u => u.UniqueID == a.AssignedUserID);
                        if (expert != null)
                        {
                            return a.IsHost == 1 ? expert.LocalName + "(主持人)" : expert.LocalName;
                        }
                        return String.Empty;
                    });
                    request.Experts = String.Join(", ", experts);
                }
                results.Add(request);
            });

            return new ConsultationRequestSpecialistSearchDto
            {
                Count = count,
                Requests = results
            };
        }

        public IEnumerable<EMRItemSuperDto> GetEMRItemSuper(string patientCaseId, string type)
        {
            var EMRItems = _EMRItemRepository.Get(item => item.PatientCaseID == patientCaseId && item.EMRItemType == type)
                .Select(s => Mapper.Map<EMRItem, EMRItemSuperDto>(s)).ToList();
            foreach (var item in EMRItems)
            {
                var detail = _EMRItemDetailRepository.Get(g => g.EMRItemID == item.UniqueID).Select(s => Mapper.Map<EMRItemDetail, EMRItemDetailDto>(s));
                item.ItemDetails = detail.ToList();
            }

            return EMRItems.OrderByDescending(e => e.ExamDate);
        }

        public async Task<PatientCaseSearchDto> SearchPatientCases(PatientCaseSearchCriteriaDto criteria)
        {
            var query = (from p in _dbContext.Set<PatientCase>()
                         join pp in _dbContext.Set<PersonPatientCase>() on p.UniqueID equals pp.PatientCaseID
                         join per in _dbContext.Set<Person>() on pp.PersonID equals per.UniqueID
                         orderby p.LastEditTime descending
                         select new
                         {
                             p.CreateTime,
                             CurrentAge = p.Age,
                             p.Gender,
                             LastUpdateTime = p.LastEditTime,
                             PatientCaseID = p.UniqueID,
                             p.PatientName,
                             per.PatientNo,
                             p.InsuranceNumber,
                             p.IdentityCard,
                             p.Status,
                             p.IsDeleted,
                             p.Creator
                         });

            if (!_LoginUserService.IsSystemAdmin)
            {
                query = query.Where(q => q.Creator.Equals(_LoginUserService.CurrentUserID));
            }

            if (!string.IsNullOrEmpty(criteria.PatientNo))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientNo, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.PatientNo.Equals(criteria.PatientNo)) :
                    query.Where(p => p.PatientNo.Contains(actualSearchValue));
            }
            if (!string.IsNullOrEmpty(criteria.PatientName))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientName, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.PatientName.Equals(criteria.PatientName)) :
                    query.Where(p => p.PatientName.Contains(actualSearchValue));

            }
            if (!string.IsNullOrEmpty(criteria.InsuranceNumber))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.InsuranceNumber, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.InsuranceNumber.Equals(criteria.InsuranceNumber)) :
                    query.Where(p => p.InsuranceNumber.Contains(actualSearchValue));
            }
            if (!string.IsNullOrEmpty(criteria.IdentityCard))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.IdentityCard, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.IdentityCard.Equals(criteria.IdentityCard)) :
                    query.Where(p => p.IdentityCard.Contains(actualSearchValue));
            }

            if (criteria.StatusList != null && criteria.StatusList.Length > 0)
            {
                if (criteria.StatusList.Length == 1)
                {
                    var status = criteria.StatusList[0];
                    switch (status)
                    {
                        case PatientCaseStatus.All:
                            break;
                        case PatientCaseStatus.Deleted:
                            query = query.Where(p => p.IsDeleted == 1);
                            break;
                        default:
                            query = query.Where(p => p.IsDeleted == 0 && p.Status == (int)status);
                            break;
                    }
                }
                else
                {
                    var statusList = criteria.StatusList.Select(s => (int)s);
                    query = query.Where(p => statusList.Contains(p.Status));

                    if (!criteria.IncludeDeleted)
                    {
                        query = query.Where(q => q.IsDeleted == 0);
                    }
                }
            }
            else if (!criteria.IncludeDeleted)
            {
                query = query.Where(q => q.IsDeleted == 0);
            }


            var totalCount = await query.CountAsync();
            var result = await query.OrderByDescending(o => o.CreateTime).Skip((criteria.PageIndex - 1) * criteria.PageSize).Take(criteria.PageSize)
                .Select(p => new PatientCaseDto
                {
                    CreateTime = p.CreateTime,
                    CurrentAge = p.CurrentAge,
                    Gender = p.Gender,
                    LastUpdateTime = p.LastUpdateTime,
                    PatientCaseID = p.PatientCaseID,
                    PatientName = p.PatientName,
                    PatientNo = p.PatientNo,
                    IdentityCard = p.IdentityCard,
                    Status = p.Status,
                    IsDeleted = p.IsDeleted
                }).ToListAsync();

            #region Get patient case exam ids

            var patientCaseIDs = result.Select(r => r.PatientCaseID);
            var exams = await (from e in _dbContext.Set<EMRItem>()
                               where patientCaseIDs.Contains(e.PatientCaseID)
                               select new { e.PatientCaseID, e.AccessionNo, e.EMRItemType }).ToListAsync();
            var modules = await (from m in _dbContext.Set<ExamModule>()
                                 where patientCaseIDs.Contains(m.Owner)
                                 select new { m.Type, m.Title }).ToListAsync();
            foreach (var patientCaseDto in result)
            {
                var dto = patientCaseDto;
                patientCaseDto.ExamIDs = String.Join(",", exams.Where(e => e.PatientCaseID == dto.PatientCaseID).Select(i =>
                {
                    var module = modules.FirstOrDefault(m => m.Type == i.EMRItemType);
                    return module == null ? i.AccessionNo : i.AccessionNo + "-" + module.Title;
                }));
            }
            #endregion
            return new PatientCaseSearchDto
            {
                Count = totalCount,
                Cases = result
            };
        }

        public async Task<ConsultationDetailDto> GetConsultationDetailAsync(string requestId)
        {
            var query = await (from p in _dbContext.Set<PatientCase>()
                               join pp in _dbContext.Set<PersonPatientCase>()
                                   on p.UniqueID equals pp.PatientCaseID
                               join per in _dbContext.Set<Person>()
                                   on pp.PersonID equals per.UniqueID
                               join d in _dbContext.Set<ConsultationRequest>()
                               on p.UniqueID equals d.PatientCaseID
                               join r in _dbContext.Set<ConsultationReport>()
                               on d.UniqueID equals r.RequestID into his
                               from s in his.DefaultIfEmpty()
                               join h in _dbContext.Set<HospitalProfile>()
                               on d.RequestHospitalID equals h.UniqueID into hospital
                               from hos in hospital.DefaultIfEmpty()
                               join st in _dbContext.Set<ServiceType>()
                               on d.ServiceTypeID equals st.UniqueID into serviceTypes
                               from sts in serviceTypes.DefaultIfEmpty()
                               where d.UniqueID.Equals(requestId, StringComparison.OrdinalIgnoreCase)
                               select new
                               {
                                   ServiceTypeID = sts.UniqueID,
                                   ServiceTypeName = sts.Name,
                                   Birthday = p.Birthday,
                                   Telephone = p.Telephone,
                                   PatientCaseID = p.UniqueID,
                                   PatientName = p.PatientName,
                                   Gender = p.Gender,
                                   CurrentAge = p.Age,
                                   PatientNo = per.PatientNo,
                                   InsuranceNumber = p.InsuranceNumber,
                                   IdentityCard = p.IdentityCard,
                                   History = p.History,
                                   ClinicalDiagnosis = p.ClinicalDiagnosis,
                                   ConsultationDate = d.ConsultationDate,
                                   Status = d.Status,
                                   ConsultationStartTime = d.ConsultationStartTime,
                                   ConsultationEndTime = d.ConsultationEndTime,
                                   RequestCreateDate = d.RequestCreateDate,
                                   RequestCompleteDate = d.RequestCompleteDate,
                                   ExpectedTimeRange = d.ExpectedTimeRange,
                                   ConsultantType = d.ConsultantType,
                                   EditorID = s.EditorID,
                                   ConsultationAdvice = s.Advice,
                                   ConsultationRemark = s.Description,
                                   RequestHospitalName = hos.HospitalName,
                                   RequestUserID = d.RequestUserID,
                                   RequestUserName = d.RequestUserName,
                                   RequestHospitalID = d.RequestHospitalID,
                                   RequestPurpose = d.RequestDescription,
                                   RequestRequirement = d.RequestRequirement,
                                   RequestReportID = s.UniqueID,
                                   ExpectedDate = d.ExpectedDate,
                                   RequestId = d.UniqueID,
                                   OtherReason = d.OtherReason,
                                   ChangeReasonType = d.ChangeReasonType,
                                   AssignedDescription = d.AssignedDescription,
                                   IsDelete = d.IsDeleted,
                                   d.DeleteReason,
                                   d.DeleteTime,
                                   d.DeleteUser
                               }).FirstAsync();

            var result = new ConsultationDetailDto
            {
                Birthday = query.Birthday,
                PatientCaseID = query.PatientCaseID,
                PatientName = query.PatientName,
                Gender = query.Gender,
                CurrentAge = query.CurrentAge,
                PatientNo = query.PatientNo,
                InsuranceNumber = query.InsuranceNumber,
                IdentityCard = query.IdentityCard,
                ConsultationDate = query.ConsultationDate,
                Status = query.Status,
                ConsultationStartTime = query.ConsultationStartTime,
                ConsultationEndTime = query.ConsultationEndTime,
                RequestCreateDate = query.RequestCreateDate,
                RequestCompleteDate = query.RequestCompleteDate,
                ExpectedTimeRange = query.ExpectedTimeRange,
                ConsultantType = query.ConsultantType,
                Writer = query.EditorID,
                ConsultationAdvice = query.ConsultationAdvice,
                ConsultationRemark = query.ConsultationRemark,
                RequestHospitalName = query.RequestHospitalName,
                Telephone = query.Telephone,
                RequestRequirement = query.RequestRequirement,
                RequestPurpose = query.RequestPurpose,
                History = query.History,
                ClinicalDiagnosis = query.ClinicalDiagnosis,
                ServiceTypeName = query.ServiceTypeName,
                ConsultationReportID = query.RequestReportID,
                ServiceTypeID = query.ServiceTypeID,
                ExpectedDate = query.ExpectedDate,
                RequestId = query.RequestId,
                RequestUserName = query.RequestUserName,
                OtherReason = query.OtherReason,
                ChangeReasonType = query.ChangeReasonType,
                AssignedDescription = query.AssignedDescription,
                IsDeleted = query.IsDelete,
                DeleteReason = query.DeleteReason,
                DeleteTime = query.DeleteTime,
                DeleteUser = query.DeleteUser
            };

            var receiverIDs = await _dbContext.Set<ConsultationReceiverXRef>().Where(p => p.ConsultationRequestID.Equals(requestId)).Select(p => p.ReceiverID).ToListAsync();
            if (receiverIDs.Count > 0)
            {
                result.ReceiverIDs = receiverIDs;
                if (query.ConsultantType.Equals((int)ConsultantType.Hospital))
                {

                    var hospitalName = await _dbContext.Set<HospitalProfile>().Where(h => h.UniqueID == receiverIDs.FirstOrDefault()).Select(s => s.HospitalName).FirstOrDefaultAsync();
                    if (!string.IsNullOrEmpty(hospitalName))
                    {
                        result.Receiver = hospitalName;
                    }
                    var selection = new Selection() { Type = query.ConsultantType, Text = hospitalName, Value = receiverIDs.FirstOrDefault() };
                    result.Selections = new List<Selection>() { selection };

                }
                else
                {
                    var expertHospital = await (from h in _dbContext.Set<HospitalProfile>()
                                                join u in _dbContext.Set<UserExtention>() on h.UniqueID equals u.HospitalID
                                                where u.UniqueID.Equals(receiverIDs.FirstOrDefault())
                                                select h).FirstOrDefaultAsync();

                    result.Selections = new List<Selection>();
                    receiverIDs.ForEach(r =>
                    {
                        var user = _risProContext.Set<User>().FirstOrDefault(u => r.Equals(u.UniqueID));
                        if (user != null)
                        {
                            var selection = new Selection()
                            {
                                Type = query.ConsultantType,
                                Text = user.LocalName,
                                Value = r,
                                HospitalID = expertHospital.UniqueID,
                                HospitalName = expertHospital.HospitalName
                            };
                            result.Selections.Add(selection);
                        }
                    });
                    var names = await _risProContext.Set<User>().Where(u => receiverIDs.Contains(u.UniqueID)).Select(s => s.LocalName).ToListAsync();
                    if (names != null)
                    {
                        result.Receiver = string.Join(",", names);
                    }
                }
            }

            var requestUser = await _risProContext.Set<User>().FirstOrDefaultAsync(u => u.UniqueID == query.RequestUserID);
            if (requestUser != null)
            {
                result.RequestUser = result.RequestUserName ?? requestUser.LocalName;
            }

            var writer = await _risProContext.Set<User>().Where(u => u.UniqueID == query.EditorID).Select(s => s.LocalName).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(writer))
            {
                result.Writer = writer;
            }

            var deleteUser = await _risProContext.Set<User>().Where(u => u.UniqueID == query.DeleteUser).Select(s => s.LocalName).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(deleteUser))
            {
                result.DeleteUserName = deleteUser;
            }

            // report histories
            result.ReportHistories = await _dbContext.Set<ConsultationReportHistory>()
                .Where(w => w.ConsultationReportID.Equals(query.RequestReportID))
                .OrderByDescending(o => o.LastEditTime)
                .Select(s => new ConsultationReportHistoryDto
                {
                    ConsultationAdvice = s.Advice,
                    LastEditTime = s.LastEditTime
                }).ToListAsync();

            //requestId
            List<ConsultationAssign> assignList = await _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID == requestId).ToListAsync();
            List<string> expertIDs = new List<string>();
            List<string> expertNames = new List<string>();
            foreach (ConsultationAssign consultationAssign in assignList)
            {
                var userName = await _risProContext.Set<Hys.CareRIS.Domain.Entities.User>().Where(u => u.UniqueID == consultationAssign.AssignedUserID).Select(s => s.LocalName).FirstOrDefaultAsync();
                if (!string.IsNullOrEmpty(userName))
                {
                    expertNames.Add(userName);
                    expertIDs.Add(consultationAssign.AssignedUserID);
                }
                if (consultationAssign.IsHost == 1)
                {
                    result.HostName = userName;
                    result.HostID = consultationAssign.AssignedUserID;
                }
            }
            result.AssignExpertIDs = string.Join(",", expertIDs);
            result.AssignExpertNames = string.Join(",", expertNames);
            return result;
        }



        public void UpdateReportAdvice(ReportAdviceDto advice)
        {
            var reports = _dbContext.Set<ConsultationReport>();

            var report = reports.FirstOrDefault(r => r.UniqueID.Equals(advice.ConsultationReportID));
            ConsultationRequest request = _dbContext.Set<ConsultationRequest>().Where(c => c.UniqueID == advice.RequestID && c.Status == (int)ConsultationRequestStatus.Completed).FirstOrDefault();
            if (null != report)
            {
                //complete to save history
                if (request != null)
                {
                    _dbContext.Set<ConsultationReportHistory>().Add(new ConsultationReportHistory
                    {
                        UniqueID = Guid.NewGuid().ToString(),
                        ConsultationReportID = report.UniqueID,
                        Advice = report.Advice,
                        Description = report.Description,
                        EditorID = report.EditorID,
                        LastEditTime = DateTime.Now,
                        LastEditUser = _LoginUserService.CurrentUserID,
                        RequestID = report.RequestID
                    });
                }

                string requestID = report.RequestID;
                Mapper.Map(advice, report);
                report.LastEditTime = DateTime.Now;
                report.LastEditUser = _LoginUserService.CurrentUserID;
                report.RequestID = requestID;
            }
            else
            {
                advice.LastEditTime = DateTime.Now;
                advice.LastEditUser = _LoginUserService.CurrentUserID;
                reports.Add(Mapper.Map<ReportAdviceDto, ConsultationReport>(advice));
            }

            _dbContext.SaveChanges();

            //send notification

            if (request != null)
            {
                var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.ReceiveHospitalID);
                var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == request.PatientCaseID);
                if (hospital != null && patientCase != null)
                {
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.ConsolutionReportUpdated,
                        OwnerID = advice.ConsultationReportID,
                        Recipients = request.RequestUserID,
                        Context = new NotificationContext
                        {
                            ConsolutionHospitalName = hospital.HospitalName,
                            PatientName = patientCase.PatientName,
                        }
                    });
                }
            }
        }

        public void UpdateReceiver(RequestReceiverDto receiver)
        {
            var requests = _dbContext.Set<ConsultationRequest>();

            var request = requests.FirstOrDefault(r => r.UniqueID.Equals(receiver.RequestID));
            if (null != request)
            {
                Mapper.Map(receiver, request);

                request.LastEditTime = DateTime.Now;
            }
            else
            {
                requests.Add(Mapper.Map<RequestReceiverDto, ConsultationRequest>(receiver));
            }

            _dbContext.SaveChanges();
        }

        public void UpdatePatientCaseBaseInfo(PatientBaseInfoDto patient)
        {
            var patients = _dbContext.Set<PatientCase>();

            var patientCase = patients.FirstOrDefault(r => r.UniqueID.Equals(patient.PatientCaseID));
            if (null != patientCase)
            {
                patient.LastEditTime = DateTime.Now;
                Mapper.Map(patient, patientCase);
            }
            else
            {
                patients.Add(Mapper.Map<PatientBaseInfoDto, PatientCase>(patient));
            }

            _dbContext.SaveChanges();
        }

        public void UpdateRequestBaseInfo(RequestInfomationDto requestDto)
        {
            var requests = _dbContext.Set<ConsultationRequest>();

            var request = requests.FirstOrDefault(r => r.UniqueID.Equals(requestDto.RequestID));
            if (null != request)
            {
                requestDto.LastEditTime = DateTime.Now;
                Mapper.Map(requestDto, request);
            }
            else
            {
                requests.Add(Mapper.Map<RequestInfomationDto, ConsultationRequest>(requestDto));
            }

            _dbContext.SaveChanges();
        }

        public void UpdateCaseHistory(PatientHistoryDto patientHistory)
        {
            var results = _dbContext.Set<PatientCase>();

            var result = results.FirstOrDefault(r => r.UniqueID.Equals(patientHistory.PatientCaseID));
            if (null != result)
            {
                patientHistory.LastEditTime = DateTime.Now;
                Mapper.Map(patientHistory, result);
            }
            else
            {
                results.Add(Mapper.Map<PatientHistoryDto, PatientCase>(patientHistory));
            }

            _dbContext.SaveChanges();
        }

        public void UpdateClinicalDiagnosis(ClinicalDiagnosisDto clinicalDiagnosis)
        {
            var results = _dbContext.Set<PatientCase>();

            var result = results.FirstOrDefault(r => r.UniqueID.Equals(clinicalDiagnosis.PatientCaseID));
            if (null != result)
            {
                clinicalDiagnosis.LastEditTime = DateTime.Now;
                Mapper.Map(clinicalDiagnosis, result);
            }
            else
            {
                results.Add(Mapper.Map<ClinicalDiagnosisDto, PatientCase>(clinicalDiagnosis));
            }

            _dbContext.SaveChanges();
        }

        public string CreateRequest(NewConsultationRequestDto newRequest, string language, string userID)
        {
            PatientCase patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(u => u.UniqueID == newRequest.PatientCaseID);
            if (patientCase == null)
            {
                return "";
            }
            patientCase.Status = (int)PatientCaseStatus.Applied;

            ConsultationRequest consultationRequest = new ConsultationRequest();
            consultationRequest.UniqueID = Guid.NewGuid().ToString();
            consultationRequest.RequestHospitalID = _ConsultationConfigurationService.GetHospital(userID).UniqueID;
            consultationRequest.PatientCaseID = newRequest.PatientCaseID;
            consultationRequest.ServiceTypeID = newRequest.ConsultationType;
            consultationRequest.RequestUserID = _LoginUserService.CurrentUserID;
            if (_LoginUserService.ServiceContext.IsPublicAccount)
            {
                consultationRequest.RequestUserName = _LoginUserService.ServiceContext.LocalName;
                consultationRequest.LastEditUserName = _LoginUserService.ServiceContext.LocalName;
            }
            if (!string.IsNullOrEmpty(newRequest.SelectHospital))
            {
                consultationRequest.ConsultantType = (int)ConsultantType.Hospital;
                var xRef = new ConsultationReceiverXRef() { UniqueID = Guid.NewGuid().ToString(), ConsultationRequestID = consultationRequest.UniqueID, ReceiverID = newRequest.SelectHospital };
                _dbContext.Set<ConsultationReceiverXRef>().Add(xRef);
                consultationRequest.ReceiveHospitalID = newRequest.SelectHospital;
            }
            else
            {
                //expert changed to multiple,and these expert must be the same hospital
                consultationRequest.ConsultantType = (int)ConsultantType.Expert;
                if (newRequest.SelectExperts != null && newRequest.SelectExperts.Length > 0)
                {
                    newRequest.SelectExperts.ToList().ForEach(e =>
                    {
                        var xRef = new ConsultationReceiverXRef() { UniqueID = Guid.NewGuid().ToString(), ConsultationRequestID = consultationRequest.UniqueID, ReceiverID = e };
                        _dbContext.Set<ConsultationReceiverXRef>().Add(xRef);
                    });

                    UserExtention user = _dbContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID.Equals(newRequest.SelectExperts.FirstOrDefault()));
                    if (user != null)
                    {
                        consultationRequest.ReceiveHospitalID = user.HospitalID;
                    }
                }

            }

            consultationRequest.ExpectedDate = newRequest.ExpectedDate;
            consultationRequest.ExpectedTimeRange = newRequest.ExpectedTimeRange;
            consultationRequest.RequestDescription = newRequest.RequestPurpose;
            consultationRequest.RequestRequirement = newRequest.RequestRequirement;
            consultationRequest.Status = (int)ConsultationRequestStatus.Applied;
            consultationRequest.StatusUpdateTime = DateTime.Now;
            consultationRequest.RequestCreateDate = DateTime.Now;
            consultationRequest.LastEditUser = _LoginUserService.CurrentUserID;
            consultationRequest.LastEditTime = DateTime.Now;

            _dbContext.Set<ConsultationRequest>().Add(consultationRequest);

            //history
            var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(consultationRequest);
            history.UniqueID = Guid.NewGuid().ToString();
            _dbContext.Set<ConsultationRequestHistory>().Add(history);
            _dbContext.SaveChanges();

            SendNotificationWhenCreateRequest(consultationRequest, language);

            return consultationRequest.UniqueID;
        }

        public IEnumerable<ConsultationReportHistoryDto> GetReportHistoryByReportID(string reportID)
        {
            if (string.IsNullOrEmpty(reportID))
            {
                return null;
            }

            return _dbContext.Set<ConsultationReportHistory>().
                Where(h => h.ConsultationReportID.Equals(reportID)).
                OrderByDescending(o => o.LastEditTime).ToList().
                Select(Mapper.Map<ConsultationReportHistory, ConsultationReportHistoryDto>);
        }

        public bool UpdateChangeReason(ChangeReasonDto reason)
        {
            var results = _dbContext.Set<ConsultationRequest>();

            var result = results.FirstOrDefault(r => r.UniqueID.Equals(reason.RequestID));
            if (null == result) return false;
            int oldStatus = result.Status;
            var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(result);
            history.UniqueID = Guid.NewGuid().ToString();
            _dbContext.Set<ConsultationRequestHistory>().Add(history);

            reason.LastEditTime = DateTime.Now;
            reason.StatusUpdateTime = DateTime.Now;
            result.LastEditUser = _LoginUserService.CurrentUserID;
            if (_LoginUserService.ServiceContext.IsPublicAccount)
            {
                result.LastEditUserName = _LoginUserService.ServiceContext.LocalName;
            }

            Mapper.Map(reason, result);
            _dbContext.SaveChanges();

            //send notification
            if (oldStatus != reason.Status)
            {
                if (reason.Status == (int)ConsultationRequestStatus.Rejected)
                {
                    var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == result.ReceiveHospitalID);
                    var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == result.PatientCaseID);
                    if (hospital != null && patientCase != null)
                    {
                        _NotificationService.CallbackNotification(result.UniqueID, NotifyEvent.ConsolutionAdminAssignToExpert);
                        _NotificationService.SendNotification(new NotificationData
                        {
                            NotifyEvent = NotifyEvent.ConsolutionAdminDeclinedRequest,
                            OwnerID = result.UniqueID,
                            Recipients = result.RequestUserID,
                            Context = new NotificationContext
                            {
                                ConsolutionHospitalName = hospital.HospitalName,
                                PatientName = patientCase.PatientName,
                            }
                        });
                    }
                }
                else if (reason.Status == (int)ConsultationRequestStatus.Cancelled)
                {
                    SendNotificationWhenCancelRequest(result);
                }
            }

            return true;
        }

        private string GetConsAdminIds(string hospitalID)
        {
            //send notification
            var paginationResult = _UserManagementService.SearchUsers(new UserSearchCriteriaDto
            {
                PageIndex = 0,
                PageSize = 10000,
                ShowAllUser = true,
                IncludeMobile = true,
                HospitalID = hospitalID,
                RoleID = LoginUserService.ConsAdminRoleID
            });
            var userList = paginationResult.Data as IEnumerable<UserDto>;
            return userList == null ? String.Empty : String.Join(",", userList.Select(u => u.UniqueID));
        }

        private IQueryable<ConsultationRequestTransfer> SearchRequest(ConsultationRequestSearchCriteriaDto criteria)
        {
            var query = (from d in _dbContext.Set<ConsultationRequest>()
                         join p in _dbContext.Set<PatientCase>() on d.PatientCaseID equals p.UniqueID
                         join pp in _dbContext.Set<PersonPatientCase>() on p.UniqueID equals pp.PatientCaseID
                         join per in _dbContext.Set<Person>() on pp.PersonID equals per.UniqueID
                         select new
                         {
                             p.UniqueID,
                             p.PatientName,
                             p.Gender,
                             p.Age,
                             per.PatientNo,
                             p.InsuranceNumber,
                             p.IdentityCard,
                             RequestId = d.UniqueID,
                             d.AssignedDate,
                             d.ConsultationDate,
                             d.LastEditTime,
                             d.Status,
                             d.ConsultationStartTime,
                             d.ConsultationEndTime,
                             d.RequestCreateDate,
                             d.ExpectedTimeRange,
                             d.ConsultantType,
                             d.StatusUpdateTime,
                             d.RequestHospitalID,
                             d.ExpectedDate,
                             d.RequestUserID,
                             d.RequestUserName,
                             d.ReceiveHospitalID,
                             IsDelete = d.IsDeleted
                         });

            switch (criteria.SearchType)
            {
                case SearchType.RequestSearchDoctor:
                    query = query.Where(q => q.RequestUserID.Equals(_LoginUserService.CurrentUserID));
                    break;
                case SearchType.RequestSearchCenter:
                    query =
                        query.Join(_dbContext.Set<UserExtention>().Where(u => u.UniqueID.Equals(_LoginUserService.CurrentUserID)),
                        q => q.ReceiveHospitalID, h => h.HospitalID,
                            (q, h) => new { q, h })
                            .Select(@t => @t.q);
                    break;
                case SearchType.Expert:
                    query = from q in query
                            join ass in _dbContext.Set<ConsultationAssign>()
                            on q.RequestId equals ass.ConsultationRequestID
                            where ass.AssignedUserID.Equals(_LoginUserService.CurrentUserID)
                            select q;
                    break;
            }

            if (!string.IsNullOrEmpty(criteria.PatientNo))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientNo, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.PatientNo.Equals(criteria.PatientNo)) :
                    query.Where(p => p.PatientNo.Contains(actualSearchValue));
            }
            if (!string.IsNullOrEmpty(criteria.PatientName))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.PatientName, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.PatientName.Equals(criteria.PatientName)) :
                    query.Where(p => p.PatientName.Contains(actualSearchValue));

            }
            if (!string.IsNullOrEmpty(criteria.InsuranceNumber))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.InsuranceNumber, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.InsuranceNumber.Equals(criteria.InsuranceNumber)) :
                    query.Where(p => p.InsuranceNumber.Contains(actualSearchValue));
            }
            if (!string.IsNullOrEmpty(criteria.IdentityCard))
            {
                string actualSearchValue;
                var searchingType = SearchingUtil.ProcessSearchValue(criteria.IdentityCard, out actualSearchValue);
                query = searchingType == SearchingType.Exact ?
                    query.Where(p => p.IdentityCard.Equals(criteria.IdentityCard)) :
                    query.Where(p => p.IdentityCard.Contains(actualSearchValue));
            }

            if (criteria.StatusList != null && criteria.StatusList.Length > 0)
            {
                if (criteria.StatusList.Length == 1)
                {
                    var status = criteria.StatusList[0];
                    switch (status)
                    {
                        case ConsultationRequestStatus.All:
                            break;
                        case ConsultationRequestStatus.Deleted:
                            query = query.Where(p => p.IsDelete == 1);
                            break;
                        default:
                            if (criteria.IncludeDeleted)
                            {
                                query = query.Where(p => p.Status == (int)status);
                            }
                            else
                            {
                                query = query.Where(p => p.IsDelete == 0 && p.Status == (int)status);
                            }
                            break;
                    }
                }
                else
                {
                    var statusList = criteria.StatusList.Select(s => (int)s);
                    query = query.Where(p => statusList.Contains(p.Status));

                    if (!criteria.IncludeDeleted)
                    {
                        query = query.Where(q => q.IsDelete == 0);
                    }
                }
            }
            else if (!criteria.IncludeDeleted)
            {
                query = query.Where(q => q.IsDelete == 0);
            }

            if (criteria.ConsultationStartDate.HasValue)
            {
                query = query.Where(p => p.ConsultationDate >= criteria.ConsultationStartDate.Value);
            }
            if (criteria.ConsultationEndDate.HasValue)
            {
                var consultationEndDate = criteria.ConsultationEndDate.Value.AddDays(1);
                query = query.Where(p => p.ConsultationDate < consultationEndDate);
            }
            if (criteria.RequestStartDate.HasValue)
            {
                query = query.Where(p => p.RequestCreateDate >= criteria.RequestStartDate.Value);
            }
            if (criteria.RequestEndDate.HasValue)
            {
                var requestEndDate = criteria.RequestEndDate.Value.AddDays(1);
                query = query.Where(p => p.RequestCreateDate < requestEndDate);
            }

            var results = query.Select(r => new ConsultationRequestTransfer
            {
                PatientCaseID = r.UniqueID,
                ConsultantType = r.ConsultantType,
                ConsultationDate = r.ConsultationDate,
                ConsultationEndTime = r.ConsultationEndTime,
                ConsultationStartTime = r.ConsultationStartTime,
                CurrentAge = r.Age,
                ExpectedTimeRange = r.ExpectedTimeRange,
                ExpectedDate = r.ExpectedDate,
                PatientName = r.PatientName,
                Gender = r.Gender,
                PatientNo = r.PatientNo,
                IdentityCard = r.IdentityCard,
                RequestCreateDate = r.RequestCreateDate,
                Status = r.Status,
                RequestId = r.RequestId,
                LastEditTime = r.LastEditTime,
                StatusUpdateTime = r.StatusUpdateTime,
                RequestUserID = r.RequestUserID,
                RequestHospitalID = r.RequestHospitalID,
                RequestUserName = r.RequestUserName,
                ReceiveHospitalID = r.ReceiveHospitalID,
                IsDeleted = r.IsDelete
            });

            return results;
        }

        public PaginationResult GetConsultHospitals(ConsultHospitalSearchCriteriaDto searchCriteria)
        {
            if (searchCriteria.PageIndex == 0)
            {
                searchCriteria.PageIndex = 1;
            }

            var searchResult = _dbContext.Set<HospitalProfile>().Where(h => h.IsConsultation == true);

            if (!String.IsNullOrEmpty(searchCriteria.Name))
            {
                searchResult = searchResult.Where(u => u.HospitalName.Contains(searchCriteria.Name));
            }

            if (!String.IsNullOrEmpty(searchCriteria.ProvinceName))
            {
                searchResult = searchResult.Where(u => u.Province == searchCriteria.ProvinceName);
            }

            if (!String.IsNullOrEmpty(searchCriteria.CityName))
            {
                searchResult = searchResult.Where(u => u.City == searchCriteria.CityName);
            }

            var hospitals = searchResult.OrderBy(u => u.HospitalName).Skip((searchCriteria.PageIndex - 1) * searchCriteria.PageSize).Take(searchCriteria.PageSize)
                .ToList();
            return new PaginationResult { Data = hospitals, Total = searchResult.Count() };
        }

        public List<SelectAreaDto> GetConsultArea()
        {
            List<SelectAreaDto> selectAreaList = new List<SelectAreaDto>();
            List<HospitalProfile> hospitalProfileList = (from h in _dbContext.Set<HospitalProfile>()
                                                         where h.IsConsultation == true && h.Province != null && h.Province != ""
                                                         select h).ToList();

            selectAreaList = hospitalProfileList.Select(c => new SelectAreaDto
            {
                ProvinceName = c.Province
            }
                ).Where(c => c.ProvinceName != null).GroupBy(i => i.ProvinceName).Select(x => x.First()).ToList();

            foreach (SelectAreaDto selectAreaDto in selectAreaList)
            {
                selectAreaDto.CityNames = (from h in hospitalProfileList
                                           where h.Province == selectAreaDto.ProvinceName
                                           && h.City != null && h.City != ""
                                           select h.City).Distinct().ToList();
            }
            return selectAreaList;
        }


        public ChangeReasonDto GetChangeReason(string requestId)
        {
            if (!string.IsNullOrEmpty(requestId))
            {
                var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(requestId));

                if (null != request)
                {
                    return new ChangeReasonDto
                    {
                        RequestID = request.UniqueID,
                        ChangeReasonType = request.ChangeReasonType,
                        OtherReason = request.OtherReason
                    };
                }
            }

            return null;
        }

        public bool AcceptRequest(RequestAcceptInfoDto requestAcceptInfoDto, string language)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(requestAcceptInfoDto.RequestID));

            if (null != request)
            {
                if (request.Status == (int)ConsultationRequestStatus.Reconsider)
                {
                    var expertList = _dbContext.Set<ConsultationAssign>().Where(u => u.ConsultationRequestID.Equals(requestAcceptInfoDto.RequestID));
                    _dbContext.Set<ConsultationAssign>().RemoveRange(expertList);
                }

                request.Status = (int)ConsultationRequestStatus.Accepted;
                request.StatusUpdateTime = DateTime.Now;
                request.AssignedBy = _LoginUserService.CurrentUserID;
                request.AssignedDate = DateTime.Now;
                request.ConsultationDate = requestAcceptInfoDto.ConsultationDate;
                request.ConsultationStartTime = requestAcceptInfoDto.consultationStartTime;
                request.MeetingRoom = requestAcceptInfoDto.MeetingRoom;
                request.AssignedDescription = requestAcceptInfoDto.Description;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                request.LastEditTime = DateTime.Now;

                foreach (var userID in requestAcceptInfoDto.ExpertList)
                {
                    _dbContext.Set<ConsultationAssign>().Add(new ConsultationAssign
                    {
                        UniqueID = Guid.NewGuid().ToString(),
                        ConsultationRequestID = requestAcceptInfoDto.RequestID,
                        AssignedUserID = userID,
                        AssignedTime = DateTime.Now,
                        IsHost = userID == requestAcceptInfoDto.DefaultID ? 1 : 0,
                        LastEditUser = _LoginUserService.CurrentUserID,
                        LastEditTime = DateTime.Now
                    });
                }

                //history
                var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(request);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<ConsultationRequestHistory>().Add(history);
                _dbContext.SaveChanges();

                SendNotificationWhenAcceptRequest(request, requestAcceptInfoDto, language);

                return true;
            }
            return false;
        }

        public bool UpdateAcceptRequest(RequestAcceptInfoDto requestAcceptInfoDto, string language)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(requestAcceptInfoDto.RequestID));

            if (null != request)
            {
                request.ConsultationDate = requestAcceptInfoDto.ConsultationDate;
                request.ConsultationStartTime = requestAcceptInfoDto.consultationStartTime;
                request.AssignedDescription = requestAcceptInfoDto.Description;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                request.LastEditTime = DateTime.Now;

                var expertList = _dbContext.Set<ConsultationAssign>().Where(u => u.ConsultationRequestID.Equals(requestAcceptInfoDto.RequestID));
                var addList = requestAcceptInfoDto.ExpertList.Select(id => new ConsultationAssign
                {
                    UniqueID = Guid.NewGuid().ToString(),
                    ConsultationRequestID = requestAcceptInfoDto.RequestID,
                    AssignedUserID = id,
                    AssignedTime = DateTime.Now,
                    IsHost = id == requestAcceptInfoDto.DefaultID ? 1 : 0,
                    LastEditUser = _LoginUserService.CurrentUserID,
                    LastEditTime = DateTime.Now
                });

                _dbContext.Set<ConsultationAssign>().RemoveRange(expertList);
                _dbContext.Set<ConsultationAssign>().AddRange(addList);

                SendNotificationWhenAcceptRequest(request, requestAcceptInfoDto, language, false);

                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        private void SendNotificationWhenCreateRequest(ConsultationRequest request, string language)
        {
            //send notification
            var recipients = GetConsAdminIds(request.ReceiveHospitalID);
            if (!String.IsNullOrWhiteSpace(recipients))
            {
                var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.RequestHospitalID);

                var serviceType = (Dictionary<string, List<ServiceTypeDto>>)HttpContext.Current.Application["serviceType"];
                var serviceTypeList = serviceType[language];
                var serviceTypeDto = serviceTypeList.FirstOrDefault(s => s.UniqueID == request.ServiceTypeID);
                var requestUser = _UserManagementService.GetUser(request.RequestUserID);
                if (hospital != null && serviceTypeDto != null && requestUser != null)
                {
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.DoctorSendRequest,
                        OwnerID = request.UniqueID,
                        Recipients = recipients,
                        Context = new NotificationContext
                        {
                            DoctorName = requestUser.LocalName,
                            DoctorHospitalName = hospital.HospitalName,
                            ConsolutionType = serviceTypeDto.Name,
                            ConsolutionDate = request.ExpectedDate.Value.ToString("yyyy-MM-dd"),
                        }
                    });
                }
            }
        }

        private void SendNotificationWhenCancelRequest(ConsultationRequest request)
        {
            //send notification -> recipient -> Consolution Admin
            var recipients = GetConsAdminIds(request.ReceiveHospitalID);
            if (!String.IsNullOrWhiteSpace(recipients))
            {
                var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.RequestHospitalID);
                var requestUser = _UserManagementService.GetUser(_LoginUserService.CurrentUserID);
                var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == request.PatientCaseID);
                if (hospital != null && requestUser != null && patientCase != null)
                {
                    _NotificationService.CallbackNotification(request.UniqueID, NotifyEvent.ConsolutionAdminAssignToExpert);
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.DoctorCancelRequest,
                        OwnerID = request.UniqueID,
                        Recipients = recipients,
                        Context = new NotificationContext
                        {
                            DoctorName = requestUser.LocalName,
                            DoctorHospitalName = hospital.HospitalName,
                            PatientName = patientCase.PatientName
                        }
                    });
                }
            }
        }

        private void SendNotificationWhenAcceptRequest(ConsultationRequest request, RequestAcceptInfoDto requestAcceptInfoDto, string language, bool sendToDoctor = true)
        {
            //send notification -> to expert
            var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.RequestHospitalID);
            if (requestAcceptInfoDto.ExpertList.Count > 0)
            {
                var serviceType = (Dictionary<string, List<ServiceTypeDto>>)HttpContext.Current.Application["serviceType"];
                var serviceTypeList = serviceType[language];
                var serviceTypeDto = serviceTypeList.FirstOrDefault(s => s.UniqueID == request.ServiceTypeID);
                var consultUser = _UserManagementService.GetUser(_LoginUserService.CurrentUserID);
                if (hospital != null && serviceTypeDto != null && consultUser != null)
                {
                    var sendingTime = DateTime.Now;
                    var config = _NotificationService.GetNotificationConfigs().FirstOrDefault(dto => dto.Event == NotifyEvent.ConsolutionAdminAssignToExpert);
                    if (config != null && request.ConsultationDate.HasValue && !String.IsNullOrWhiteSpace(config.SendingTimes))
                    {
                        var date = request.ConsultationDate.Value.Date;
                        var timeRange = new Dictionary<string, TimeSpan> { { "1", new TimeSpan(0, 9, 0, 0) }, { "2", new TimeSpan(0, 13, 0, 0) }, { "3", new TimeSpan(0, 17, 0, 0) }, };
                        sendingTime = date.Add(timeRange[request.ConsultationStartTime]);
                        // callback notifications
                        _NotificationService.CallbackNotification(request.UniqueID, NotifyEvent.ConsolutionAdminAssignToExpert);
                    }
                    //recipient -> Consolution Admin
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.ConsolutionAdminAssignToExpert,
                        OwnerID = request.UniqueID,
                        Recipients = String.Join(",", requestAcceptInfoDto.ExpertList),
                        SendingTime = sendingTime,
                        Context = new NotificationContext
                        {
                            ConsolutionAdminName = consultUser.LocalName,
                            DoctorHospitalName = hospital.HospitalName,
                            ConsolutionType = serviceTypeDto.Name,
                            ConsolutionDate = request.ConsultationDate.Value.ToString("yyyy-MM-dd"),
                        }
                    });
                }
            }

            if (sendToDoctor)
            {
                //to doctor
                var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == request.PatientCaseID);
                var consolutionHospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.ReceiveHospitalID);

                if (consolutionHospital != null && patientCase != null)
                {
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.ConsolutionAdminAcceptedNotifyDoctor,
                        OwnerID = request.UniqueID,
                        Recipients = request.RequestUserID,
                        Context = new NotificationContext
                        {
                            ConsolutionHospitalName = consolutionHospital.HospitalName,
                            PatientName = patientCase.PatientName,
                            ConsolutionDate = request.ConsultationDate.Value.ToString("yyyy-MM-dd"),
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Get report template tree nodes by parentID and userID
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<ConsultatReportTemplateDirecDto> GetReportTemplateNodes(string parentID, string userID, string site)
        {
            if (string.IsNullOrEmpty(parentID) || parentID == "undefined")
            {

                var templateDirecList = new List<ConsultatReportTemplateDirecDto>();
                templateDirecList.Add(new ConsultatReportTemplateDirecDto
                {
                    UniqueID = "GlobalTemplate",
                    ItemName = "公有模版"
                });

                return templateDirecList;
            }
            else
            {

                //string sql = string.Format("select * from tbReportTemplateDirec where ParentID = '{0}' and (TYPE=0 OR TYPE=2 or (TYPE=1 AND UserGuid = '{1}'))  and [DirectoryType]='report' order by ItemOrder", strItemGuid, strUserGuid);
                var reportTemplateDirecs = _dbContext.Set<ConsultatReportTemplateDirec>().Where(
                    p => p.ParentID == parentID &&
                    (p.Type == 0 || p.Type == 2 || (p.Type == 1 && p.UserID == userID))
                    && p.DirectoryType == "report"
                    ).OrderBy(p => p.ItemOrder);
                if (reportTemplateDirecs.Count() != 0)
                {
                    var reportTemplateDirecDtos = new List<ConsultatReportTemplateDirecDto>();
                    foreach (var r in reportTemplateDirecs)
                    {
                        var reportTemplateDirecDto = Mapper.Map<ConsultatReportTemplateDirec, ConsultatReportTemplateDirecDto>(r);

                        reportTemplateDirecDtos.Add(reportTemplateDirecDto);
                    }
                    return reportTemplateDirecDtos;
                }

                return null;
            }
        }

        /// <summary>
        /// Get report template detail info by uniqueID
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public ConsultatReportTemplateDirecDto GetReportTemplateDirecByID(string uniqueID)
        {
            var reportTemplateDirec = _dbContext.Set<ConsultatReportTemplateDirec>().Where(
                p => p.UniqueID.Equals(uniqueID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (reportTemplateDirec != null)
            {
                var reportTemplateDirecDto = Mapper.Map<ConsultatReportTemplateDirec, ConsultatReportTemplateDirecDto>(reportTemplateDirec);
                var reportTemplate = _dbContext.Set<ConsultatReportTemplate>().Where(p => p.UniqueID == reportTemplateDirec.TemplateID).FirstOrDefault();
                if (reportTemplate != null)
                {
                    var reportTemplateDto = Mapper.Map<ConsultatReportTemplate, ConsultatReportTemplateDto>(reportTemplate);
                    reportTemplateDirecDto.ReportTemplateDto = reportTemplateDto;
                    reportTemplateDirecDto.ReportTemplateDto.UserID = reportTemplateDirec.UserID;
                }
                return reportTemplateDirecDto;
            }
            return null;
        }

        public bool CompleteRequest(string requestID)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(requestID));

            if (null != request)
            {
                request.Status = (int)ConsultationRequestStatus.Completed;
                request.StatusUpdateTime = DateTime.Now;
                request.RequestCompleteDate = DateTime.Now;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                request.LastEditTime = DateTime.Now;

                //history
                var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(request);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<ConsultationRequestHistory>().Add(history);

                _dbContext.SaveChanges();

                //send notification
                var hospital = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == request.ReceiveHospitalID);
                var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == request.PatientCaseID);
                if (hospital != null && patientCase != null)
                {
                    _NotificationService.SendNotification(new NotificationData
                    {
                        NotifyEvent = NotifyEvent.ConsolutionReportUpdated,
                        OwnerID = request.UniqueID,
                        Recipients = request.RequestUserID,
                        Context = new NotificationContext
                        {
                            ConsolutionHospitalName = hospital.HospitalName,
                            PatientName = patientCase.PatientName,
                        }
                    });
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Update receive info , until now only applied status can upadte this infomation
        /// </summary>
        /// <param name="receive"></param>
        public bool UpdateRequestReveive(RequestReceiverDto receive, string language)
        {
            if (string.IsNullOrEmpty(receive.RequestID))
            {
                return false;
            }

            var consultationRequest = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(receive.RequestID));

            if (null != consultationRequest)
            {

                consultationRequest.ServiceTypeID = receive.ServiceTypeID;
                var receiveHospitalID = String.Empty;
                if (receive.ConsultantType.Equals((int)(int)ConsultantType.Hospital))
                {
                    receiveHospitalID = receive.ReceiverIDs.FirstOrDefault();
                }
                else
                {
                    var user = _dbContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID == receive.ReceiverIDs.FirstOrDefault());
                    if (user != null)
                    {
                        receiveHospitalID = user.HospitalID;
                    }
                }
                var receiverChanged = consultationRequest.ReceiveHospitalID != receiveHospitalID;
                if (receiverChanged)
                {
                    SendNotificationWhenCancelRequest(consultationRequest);
                    consultationRequest.ReceiveHospitalID = receiveHospitalID;
                }

                if (receive.IsExpected)
                {
                    consultationRequest.ExpectedDate = receive.ExpectedDate;
                    consultationRequest.ExpectedTimeRange = receive.ExpectedTimeRange;
                }
                else
                {
                    consultationRequest.ConsultationDate = receive.ConsolutionDate;
                    consultationRequest.ConsultationStartTime = receive.ConsolutionTimeRange;
                }

                // update ConsultationReceiversXRef table
                var xRefs =
                _dbContext.Set<ConsultationReceiverXRef>()
                        .Where(p => p.ConsultationRequestID.Equals(receive.RequestID));
                _dbContext.Set<ConsultationReceiverXRef>().RemoveRange(xRefs);
                receive.ReceiverIDs.ToList().ForEach(r =>
                {
                    var xRefNew = new ConsultationReceiverXRef() { UniqueID = Guid.NewGuid().ToString(), ConsultationRequestID = receive.RequestID, ReceiverID = r };
                    _dbContext.Set<ConsultationReceiverXRef>().Add(xRefNew);
                });
                consultationRequest.ConsultantType = receive.ConsultantType;
                consultationRequest.LastEditUser = _LoginUserService.CurrentUserID;
                consultationRequest.LastEditTime = DateTime.Now;

                if (receiverChanged)
                {
                    SendNotificationWhenCreateRequest(consultationRequest, language);
                }
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public object GetInfoForAcceptRequest(string requestID)
        {
            var receiverIDs =
          _dbContext.Set<ConsultationReceiverXRef>()
              .Where(p => p.ConsultationRequestID.Equals(requestID))
              .Select(p => p.ReceiverID).ToList();

            var query = (from r in _dbContext.Set<ConsultationRequest>()
                         join st in _dbContext.Set<ServiceType>()
                  on r.ServiceTypeID equals st.UniqueID into serviceTypes
                         from sts in serviceTypes.DefaultIfEmpty()
                         where r.UniqueID == requestID
                         select new ConsultationDetailDto()
                         {
                             ServiceTypeID = r.ServiceTypeID,
                             ExpectedDate = r.ExpectedDate,
                             ExpectedTimeRange = r.ExpectedTimeRange,
                             ConsultantType = r.ConsultantType,
                             ServiceTypeName = sts.Name
                         }
            ).FirstOrDefault();
            //get hos info by userid


            if (query != null && receiverIDs.Count > 0)
            {
                if (query.ConsultantType == 0)
                {
                    var hospitalName = _dbContext.Set<HospitalProfile>().Where(h => h.UniqueID == receiverIDs.FirstOrDefault()).Select(s => s.HospitalName).FirstOrDefault();
                    if (!string.IsNullOrEmpty(hospitalName))
                    {
                        query.Receiver = hospitalName;
                    }
                }
                else
                {
                    var names = _risProContext.Set<Hys.CareRIS.Domain.Entities.User>().Where(u => receiverIDs.Contains(u.UniqueID)).Select(s => s.LocalName);
                    if (names != null)
                    {
                        query.Receiver = string.Join(", ", names);
                    }
                    query.Selections = new List<Selection>();
                    receiverIDs.ForEach(r =>
                    {
                        var user = _risProContext.Set<User>().FirstOrDefault(u => r.Equals(u.UniqueID));
                        if (user != null)
                        {
                            var selection = new Selection() { Type = query.ConsultantType, Text = user.LocalName, Value = r };
                            query.Selections.Add(selection);
                        }
                    });
                }
            }
            return query;
        }

        public IEnumerable<ConsultationAssignDto> GetConsultationAssigns(string requestID)
        {
            var consultationAssigns =
                _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID.Equals(requestID))
                .Select(s => new ConsultationAssignDto { UniqueID = s.AssignedUserID, IsHost = s.IsHost })
                .ToList();

            var userIds = consultationAssigns.Select(s => s.UniqueID);

            var users = _risProContext.Set<CareRIS.Domain.Entities.User>().Where(u => userIds.Contains(u.UniqueID)).Select(u => new
            {
                u.UniqueID,
                u.LocalName
            });

            consultationAssigns.ForEach(p =>
            {
                var user = users.FirstOrDefault(u => u.UniqueID.Equals(p.UniqueID));
                var hospital = _dbContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID.Equals(p.UniqueID));
                if (hospital != null)
                {
                    p.HospitalID = hospital.UniqueID;
                }
                if (user != null)
                {
                    p.DisplayName = user.LocalName;
                }
            });

            return consultationAssigns;
        }

        public MeetingInfoDto GetMeetings(string userID)
        {

            MeetingInfoDto meetingInfoDto = GetMeetingInfo(userID);
            return meetingInfoDto;


        }

        public string GetVNCUrl(string userID)
        {

            MeetingInfoDto meetingInfoDto = new Dtos.MeetingInfoDto();
            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            List<SysConfig> sysConfigHospitals = _dbContext.Set<SysConfig>().Where(s => s.Module == (int)SysConfigModule.VNC && s.ConfigOwner != null && s.ConfigOwner == hospitalProfileDto.UniqueID).ToList();
            if (sysConfigHospitals.Count == 0)
            {
                List<SysConfig> sysConfigDefaults = _dbContext.Set<SysConfig>().Where(s => s.Module == (int)SysConfigModule.VNC && s.ConfigOwner == null).ToList();
                if (sysConfigDefaults != null)
                {

                    SysConfig sysConfig = sysConfigDefaults.Where(s => s.ConfigKey == "Url").FirstOrDefault();
                    if (sysConfig != null)
                    {
                        return sysConfig.ConfigValue;
                    }
                }
            }
            else
            {
                SysConfig sysConfig = sysConfigHospitals.Where(s => s.ConfigKey == "Url").FirstOrDefault();
                if (sysConfig != null)
                {
                    return sysConfig.ConfigValue;
                }

            }
            return "";
        }

        public List<ConsultationAssignDto> GetExpertAdvices(string requestID)
        {
            //not include self
            List<ConsultationAssignDto> list = _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID == requestID &&
                c.Comments != null && c.Comments != "" && c.AssignedUserID != _LoginUserService.CurrentUserID)
                .Select(Mapper.Map<ConsultationAssign, ConsultationAssignDto>).OrderByDescending(c => c.LastEditTime).ToList();
            if (list.Count > 0)
            {
                List<string> userIDs = list.Select(c => c.AssignedUserID).ToList();
                List<User> users = _risProContext.Set<User>().Where(u => userIDs.Contains(u.UniqueID)).ToList();
                List<UserExtention> userExtenstions = _dbContext.Set<UserExtention>().Where(u => userIDs.Contains(u.UniqueID)).ToList();

                foreach (ConsultationAssignDto consultationAssignDto in list)
                {
                    consultationAssignDto.DisplayName = "";
                    consultationAssignDto.Avatar = "";
                    User user = users.Where(u => u.UniqueID == consultationAssignDto.AssignedUserID).FirstOrDefault();
                    if (user != null)
                    {
                        consultationAssignDto.DisplayName = user.LocalName;
                    }

                    UserExtention userExtention = userExtenstions.Where(u => u.UniqueID == consultationAssignDto.AssignedUserID).FirstOrDefault();
                    if (userExtention != null)
                    {
                        consultationAssignDto.Avatar = userExtention.Avatar;
                    }
                }
            }

            return list;
        }

        public bool SaveExpertAdvices(ConsultationAssignDto consultationAssignDto)
        {
            ConsultationAssign oldAssign = _dbContext.Set<ConsultationAssign>().FirstOrDefault(c => c.ConsultationRequestID == consultationAssignDto.ConsultationRequestID && c.AssignedUserID == _LoginUserService.CurrentUserID);
            if (oldAssign != null)
            {
                oldAssign.Comments = consultationAssignDto.Comments;
                oldAssign.LastEditTime = DateTime.Now;
                oldAssign.LastEditUser = _LoginUserService.CurrentUserID;
                _dbContext.SaveChanges();
                return true;

            }
            return false;
        }

        public ReportAdviceDto GetAdviceReport(string requestID)
        {
            return _dbContext.Set<ConsultationReport>().Where(c => c.RequestID == requestID).Select(c => Mapper.Map<ConsultationReport, ReportAdviceDto>(c)).FirstOrDefault();
        }

        public ConsultationAssignDto GetExpertAdviceReport(string requestID, string userID)
        {
            //not include self
            ConsultationAssignDto consultationAssignDto = null;

            if (string.IsNullOrEmpty(userID))
            {
                consultationAssignDto = _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID == requestID &&
                 c.AssignedUserID == _LoginUserService.CurrentUserID)
                 .Select(Mapper.Map<ConsultationAssign, ConsultationAssignDto>).FirstOrDefault();
            }
            else
            {
                consultationAssignDto = _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID == requestID &&
                 c.AssignedUserID == userID)
                 .Select(Mapper.Map<ConsultationAssign, ConsultationAssignDto>).FirstOrDefault();
            }

            if (consultationAssignDto != null)
            {
                consultationAssignDto.DisplayName = "";
                consultationAssignDto.Avatar = "";
                User user = _risProContext.Set<User>().Where(u => u.UniqueID == consultationAssignDto.AssignedUserID).FirstOrDefault();
                UserExtention userExtenstion = _dbContext.Set<UserExtention>().Where(u => u.UniqueID == consultationAssignDto.AssignedUserID).FirstOrDefault();

                if (user != null)
                {
                    consultationAssignDto.DisplayName = user.LocalName;
                }

                if (userExtenstion != null)
                {
                    consultationAssignDto.Avatar = userExtenstion.Avatar;
                }
            }

            return consultationAssignDto;
        }

        public ConsultationAssignDto GetHostAdviceReport(string requestID)
        {
            ConsultationAssign consultationAssign = _dbContext.Set<ConsultationAssign>().Where(c => c.ConsultationRequestID == requestID && c.IsHost == 1).FirstOrDefault();
            ConsultationAssignDto consultationAssignDto = GetExpertAdviceReport(requestID, consultationAssign.AssignedUserID);
            ConsultationReport report = _dbContext.Set<ConsultationReport>().Where(c => c.RequestID == requestID).FirstOrDefault();
            if (report != null)
            {
                consultationAssignDto.LastEditTime = report.LastEditTime;
                consultationAssignDto.Comments = report.Advice;
            }
            return consultationAssignDto;
        }

        public bool IsHost(string requestID)
        {
            var count = _dbContext.Set<ConsultationAssign>().Count(c => c.ConsultationRequestID == requestID && c.AssignedUserID == _LoginUserService.CurrentUserID && c.IsHost == 1);

            return count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        public bool IsExpert(string requestID)
        {
            var count = _dbContext.Set<ConsultationAssign>().Count(c => c.ConsultationRequestID == requestID && c.AssignedUserID == _LoginUserService.CurrentUserID && c.IsHost != 1);

            return count > 0;
        }

        public bool EditReportPermission(string requestID)
        {
            return IsHost(requestID) || IsExpert(requestID);
        }

        /// <summary>
        /// update status to consulting
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool UpdateMeetingStatus(string requestId, string confKey, string hostName, string meetingPassword)
        {
            if (meetingPassword == null)
            {
                meetingPassword = "";
            }

            if (hostName == null)
            {
                hostName = "";
            }

            if (ValidateMeeting(confKey, hostName, meetingPassword))
            {
                var count = _dbContext.Set<ConsultationAssign>().Count(c => c.ConsultationRequestID == requestId && c.AssignedUserID == _LoginUserService.CurrentUserID && c.IsHost == 1);
                if (count > 0)
                {
                    var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(c => c.UniqueID.Equals(requestId));
                    if (null != request)
                    {
                        request.Status = (int)ConsultationRequestStatus.Consulting;
                        request.LastEditUser = _LoginUserService.CurrentUserID;
                        request.LastEditTime = DateTime.Now;

                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteRequest(RequestDeleteReason requestDeleteReason)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(r => r.UniqueID.Equals(requestDeleteReason.Id));

            if (request != null)
            {
                var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(request);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<ConsultationRequestHistory>().Add(history);

                request.IsDeleted = 1;
                request.DeleteTime = DateTime.Now;
                request.DeleteReason = requestDeleteReason.DeleteReason;
                request.DeleteUser = _LoginUserService.CurrentUserID;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                request.LastEditTime = DateTime.Now;
                _dbContext.SaveChanges();

                _NotificationService.CallbackNotification(request.UniqueID, NotifyEvent.ConsolutionAdminAssignToExpert);
                return true;
            }

            return false;
        }

        public bool RecoverRequest(string requestId)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(r => r.UniqueID.Equals(requestId));

            if (request != null)
            {
                var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(request);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<ConsultationRequestHistory>().Add(history);

                request.IsDeleted = 0;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                request.LastEditTime = DateTime.Now;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }


        public bool DeletePatientCase(PatientCaseDeleteDto patientCaseDelete)
        {
            var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(r => r.UniqueID.Equals(patientCaseDelete.Id));

            if (patientCase != null)
            {
                var history = Mapper.Map<PatientCase, PatientCaseHistory>(patientCase);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<PatientCaseHistory>().Add(history);

                patientCase.IsDeleted = 1;
                patientCase.DeleteTime = DateTime.Now;
                patientCase.DeleteReason = patientCaseDelete.DeleteReason;

                if (_LoginUserService.ServiceContext.IsPublicAccount)
                {
                    patientCase.DeletePublicAccountName = _LoginUserService.ServiceContext.LocalName;
                }

                patientCase.DeleteUser = _LoginUserService.CurrentUserID;
                patientCase.LastEditUser = _LoginUserService.CurrentUserID;
                patientCase.LastEditTime = DateTime.Now;
                patientCase.OrderID = string.Empty;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool RecoverPatientCase(string patientCaseId)
        {
            var patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(r => r.UniqueID.Equals(patientCaseId));

            if (patientCase != null)
            {
                var history = Mapper.Map<PatientCase, PatientCaseHistory>(patientCase);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<PatientCaseHistory>().Add(history);

                patientCase.IsDeleted = 0;
                patientCase.LastEditUser = _LoginUserService.CurrentUserID;
                patientCase.LastEditTime = DateTime.Now;

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        private bool ValidateMeeting(string confKey, string hostName, string meetingPassword)
        {
            return false;
        }

        private MeetingInfoDto GetMeetingInfo(string userID)
        {
            MeetingInfoDto meetingInfoDto = new Dtos.MeetingInfoDto();
            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            List<SysConfig> sysConfigHospitals = _dbContext.Set<SysConfig>().Where(s => s.Module == (int)SysConfigModule.Meeting && s.ConfigOwner != null && s.ConfigOwner == hospitalProfileDto.UniqueID).ToList();
            if (sysConfigHospitals.Count == 0)
            {
                List<SysConfig> sysConfigDefaults = _dbContext.Set<SysConfig>().Where(s => s.Module == (int)SysConfigModule.Meeting && s.ConfigOwner == null).ToList();
                if (sysConfigDefaults != null)
                {

                    GetMeetingInfoProcess(sysConfigDefaults, ref meetingInfoDto);
                    return meetingInfoDto;
                }
            }
            else
            {
                GetMeetingInfoProcess(sysConfigHospitals, ref meetingInfoDto);
                return meetingInfoDto;
            }

            return null;
        }

        private void GetMeetingInfoProcess(List<SysConfig> sysConfigs, ref MeetingInfoDto meetingInfoDto)
        {
            SysConfig sysConfig = sysConfigs.Where(s => s.ConfigKey == "IPAddress").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.IPAddress = sysConfig.ConfigValue;
            }

            sysConfig = sysConfigs.Where(s => s.ConfigKey == "User").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.User = sysConfig.ConfigValue;
            }

            sysConfig = sysConfigs.Where(s => s.ConfigKey == "Password").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.Password = sysConfig.ConfigValue;
            }

            sysConfig = sysConfigs.Where(s => s.ConfigKey == "Version").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.Version = sysConfig.ConfigValue;
            }

            sysConfig = sysConfigs.Where(s => s.ConfigKey == "MeetingPassword").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.MeetingPassword = sysConfig.ConfigValue;
            }

            sysConfig = sysConfigs.Where(s => s.ConfigKey == "Site").FirstOrDefault();
            if (sysConfig != null)
            {
                meetingInfoDto.Site = sysConfig.ConfigValue;
            }

        }

        public UserSettingDto GetUserSetting(string roleId, string userId, int type)
        {
            var userSetting = _dbContext.Set<UserSetting>().FirstOrDefault(s => s.UserID == userId && s.Type == type && (roleId == null || s.RoleID == roleId));
            return userSetting != null ? Mapper.Map<UserSetting, UserSettingDto>(userSetting) : null;
        }

        public async Task<UserSettingDto> GetUserSettingAsync(string roleId, string userId, int type)
        {
            var userSetting = await _dbContext.Set<UserSetting>().FirstOrDefaultAsync(s => s.UserID == userId && s.Type == type && (roleId == null || s.RoleID == roleId));
            return userSetting != null ? Mapper.Map<UserSetting, UserSettingDto>(userSetting) : null;
        }

        public bool SaveUserSetting(UserSettingDto userSettingDto)
        {
            if (string.IsNullOrEmpty(userSettingDto.UserSettingID))
            {
                var userSettingFind = _dbContext
                .Set<UserSetting>()
                .FirstOrDefault(s => s.RoleID == userSettingDto.RoleID && s.UserID == userSettingDto.UserID && s.Type == userSettingDto.Type);
                if (userSettingFind != null)
                {
                    userSettingFind.SettingValue = userSettingDto.SettingValue;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    userSettingDto.UserSettingID = Guid.NewGuid().ToString();
                    UserSetting userSetting = Mapper.Map<UserSettingDto, UserSetting>(userSettingDto);
                    _dbContext.Set<UserSetting>().Add(userSetting);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                var userSetting = _dbContext
                .Set<UserSetting>()
                .FirstOrDefault(s => s.UserSettingID == userSettingDto.UserSettingID);
                if (userSetting != null)
                {
                    userSetting.SettingValue = userSettingDto.SettingValue;
                    _dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool UpdateRequestStatus(string requestId, int status)
        {
            var request = _dbContext.Set<ConsultationRequest>().FirstOrDefault(d => d.UniqueID.Equals(requestId));
            if (null != request)
            {
                var history = Mapper.Map<ConsultationRequest, ConsultationRequestHistory>(request);
                history.UniqueID = Guid.NewGuid().ToString();
                _dbContext.Set<ConsultationRequestHistory>().Add(history);

                request.Status = status;
                request.LastEditTime = DateTime.Now;
                request.LastEditUser = _LoginUserService.CurrentUserID;
                _dbContext.SaveChanges();

                if (status == (int)ConsultationRequestStatus.Cancelled)
                {
                    _NotificationService.CallbackNotification(requestId, NotifyEvent.ConsolutionAdminAssignToExpert);
                }
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ConsultationRequestFlatDto>> ConsultationRequestStatistics(DateTime beginDateTime, DateTime endDateTime)
        {
            var requests = await (from r in _dbContext.Set<ConsultationRequest>()
                                  where r.IsDeleted == 0 && (
                                  (r.RequestCreateDate <= endDateTime && r.RequestCreateDate >= beginDateTime) ||
                                  (r.AssignedDate <= endDateTime && r.AssignedDate >= beginDateTime) ||
                                  (r.RequestCompleteDate <= endDateTime && r.RequestCompleteDate >= beginDateTime))
                                  select r).ToListAsync();
            var results = Mapper.Map<IEnumerable<ConsultationRequest>, IEnumerable<ConsultationRequestFlatDto>>(requests);
            return results;
        }

   


    }
}
