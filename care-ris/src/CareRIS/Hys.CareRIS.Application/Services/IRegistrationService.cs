using Hys.CareRIS.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services
{
    public interface IRegistrationService : IDisposable
    {
        IEnumerable<PatientDto> GetPatients();
        PatientDto GetPatient(string patientId);
        PatientDto GetPatientByNo(string patientNo);
        IEnumerable<PatientDto> GetPatientsByName(string patientName);
        void AddPatient(PatientDto patinetDto);
        PatientDto AddPatient(PatientEditDto patientEdit);
        void UpdatePatient(PatientDto patinetDto);
        PatientDto UpdatePatient(PatientEditDto patientEdit);
        void DeletePatient(string patientId);
        IQueryable GetProcedures(string patientID, string orderID = null);

        // Orders
        IEnumerable<OrderDto> GetAllOrders();
        OrderDto GetOrder(string orderId);
        void AddOrder(OrderDto orderDto);
        void UpdateOrder(OrderDto orderDto, bool IsRegistrationView = false);
        void DeleteOrder(string orderId);
        RegistrationDto AddNewRegistration(RegistrationDto registration, string domain, string userName, string site);
        RegistrationDto TransferRegistration(List<RegistrationDto> registrations, string domain, string userName, string site);
        RegistrationViewDto GetRegistrationInfo(string orderId);
        string GetPatientNo(string site);
        void TransferBooking2Registration(string orderId, string userId, string userName);

        // Procedures
        IEnumerable<ProcedureDto> GetAllProcedures();
        ProcedureDto GetProcedure(string procedureId);
        IQueryable GetProceduresByOrderID(string orderID);
        void AddProcedure(ProcedureDto procedureDto);
        List<ProcedureDto> AddProcedure(ProcedureInsertDto procedureInsert);
        void UpdateProcedure(ProcedureDto ProcedureDtoDto);
        ProcedureDto UpdateProcedure(string id, ProcedureDto procedureUpdate, string userId);
        int DeleteProcedure(string procedureId, string loginUserID, bool isFromRegistrationView = false);
        void UpdateProcedure(string procedureID, int status, string reportID);
        void FinishExam(string orderID, string examSite, string examDomain, string examAccNo, string userId, string userName);
        IEnumerable<ProcedureCodeDto> GetProcedureCodes(string site);
        ProcedureDto GetProcedureByCode(string code, string modality, string domain);
        void UpdateProcedureSlice(string orderId, SliceDto slice);

        // AccesssionNumberList
        AccessionNumberListDto GetAccessionNumberList(string accNo);

        // Print
        string GetRequisitionUrl(string accNo, string modalityType, string genPDFServiceURL);
        string GetBarCodeUrl(string accNo, string modalityType, string genPDFServiceURL);

        //BodySystemMap
        IEnumerable<BodySystemMapDto> GetBodySystemMaps(string site);

        string SimplifiedToEnglish(SimplifyEnglishDto simplify);

        // intergration service
        Task<RequestInfoDto> GetRequestInfo(string cardNumber, string cardType, string hisconnURL, string domain, string site);
        List<PatientDto> GetSimilarPatient(string globalID, string risPatientID, string hisID, string patientName, string site);
        PatientDto GetIntergrationPatientInfo(string cardNumber, string cardType, string hisconnURL);
        bool ProcessReuqestInfo(List<RequestDto> requests, string domain, string site, string patientID = null, Dictionary<string, string> dics = null);

        // requisition related
        ImageData SaveTempImage(ImageData imgData, string userName);
        List<ImageData> DownLoadRequisitionFiles(string accNo, string domain, string userName);
        void ClearTempFile(string accNo, string userName);
        //ImageData GetImageFromTempFile(string fileName);
        int UploadRequisitionFile(List<string> accNos, string erNo, string relativePath, string imageQualityLevel, string userName, string domain);
        bool DeleteImage(string fileName, string relativePath, string requisitionID, string domain, string userName);
        string GenerateERNo(string site);
        int ProcessRequisitionInOrder(string accNo, string erNo, string relativePath, string imageQualityLevel, string userName, string domain);
        bool ValidateFTP(string domain);

        Task<IEnumerable<ModalityDto>> GetBookingModalities(string modalityType, string site);
        Task<IEnumerable<ModalityTimeSliceDto>> GetModalitySchedule(string modality, DateTime date, string site, string userId, string role);
        Task<string> LockModalityQuota(ModalityTimeSliceDto timeSlice, string site);
        Task UnlockModalityQuota(string unlockGuid, string modality, DateTime? start, DateTime? end, string site);
    }
}
