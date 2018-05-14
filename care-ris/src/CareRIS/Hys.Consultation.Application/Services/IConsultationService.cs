using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.CrossCutting.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Services
{
    public interface IConsultationService : IDisposable
    {
        Task<ConsultationRequestSearchDto> SearchDoctorRequests(ConsultationRequestSearchCriteriaDto criteria, string language);

        Task<PatientCaseSearchDto> SearchPatientCases(PatientCaseSearchCriteriaDto criteria);

        IEnumerable<EMRItemSuperDto> GetEMRItemSuper(string patientCaseId, string type);

        Task<ConsultationRequestSpecialistSearchDto> SearchSpecialistRequests(ConsultationRequestSearchCriteriaDto criteria, string language);

        Task<ConsultationDetailDto> GetConsultationDetailAsync(string requestId);
        
        void UpdateReportAdvice(ReportAdviceDto advice);

        void UpdateReceiver(RequestReceiverDto receiver);

        void UpdatePatientCaseBaseInfo(PatientBaseInfoDto patient);

        void UpdateRequestBaseInfo(RequestInfomationDto request);

        void UpdateCaseHistory(PatientHistoryDto patientHistory);

        void UpdateClinicalDiagnosis(ClinicalDiagnosisDto clinicalDiagnosis);

        string CreateRequest(NewConsultationRequestDto newRequest, string language, string userID);

        IEnumerable<ConsultationReportHistoryDto> GetReportHistoryByReportID(string reportID);

        PaginationResult GetConsultHospitals(ConsultHospitalSearchCriteriaDto searchCriteria);

        bool UpdateChangeReason(ChangeReasonDto reason);

        ChangeReasonDto GetChangeReason(string requestId);

        List<SelectAreaDto> GetConsultArea();
        bool AcceptRequest(RequestAcceptInfoDto requestAcceptInfoDto,string language);
        IEnumerable<ConsultatReportTemplateDirecDto> GetReportTemplateNodes(string parentID, string userID, string site);
        ConsultatReportTemplateDirecDto GetReportTemplateDirecByID(string uniqueID);

        bool CompleteRequest(string requestID);

        bool UpdateRequestReveive(RequestReceiverDto receive, string language);
        object GetInfoForAcceptRequest(string requestID);

        IEnumerable<ConsultationAssignDto> GetConsultationAssigns(string requestID);

        MeetingInfoDto GetMeetings(string userID);
        string GetVNCUrl(string userID);

        bool UpdateAcceptRequest(RequestAcceptInfoDto requestAcceptInfoDto,string language);

        List<ConsultationAssignDto> GetExpertAdvices(string requestID);
        bool SaveExpertAdvices(ConsultationAssignDto consultationAssignDto);
        ReportAdviceDto GetAdviceReport(string requestID);
        ConsultationAssignDto GetExpertAdviceReport(string requestID, string userID);
        ConsultationAssignDto GetHostAdviceReport(string requestID);
        bool IsHost(string requestID);
        bool IsExpert(string requestID);
        bool EditReportPermission(string requestID);
        bool UpdateMeetingStatus(string requestId, string confKey, string hostName, string meetingPassword);

        bool DeleteRequest(RequestDeleteReason requestDeleteReason);
        bool RecoverRequest(string requestId);

        bool DeletePatientCase(PatientCaseDeleteDto patientCaseDelete);
        bool RecoverPatientCase(string patientCaseId);

        UserSettingDto GetUserSetting(string roleID, string userID, int type);
        Task<UserSettingDto> GetUserSettingAsync(string roleID, string userID, int type);
        bool SaveUserSetting(UserSettingDto userSettingDto);

        bool UpdateRequestStatus(string requestId, int status);

        Task<IEnumerable<ConsultationRequestFlatDto>> ConsultationRequestStatistics(DateTime beginDateTime, DateTime endDateTime);
     
    }
}
