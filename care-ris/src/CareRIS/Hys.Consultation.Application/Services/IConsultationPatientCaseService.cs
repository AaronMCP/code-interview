using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.PatientCase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Services
{
    public interface IConsultationPatientCaseService : IDisposable
    {
        PatientCaseInfoDto CreatePatientCase(PatientCaseInfoDto patientCaseDto, IEnumerable<ExamModuleDto> defaultModules, string userID, string localName);
        List<PatientCaseInfoDto> GetCombinePatientCaseList(string patientId, string identityCard);
        Task<List<PatientCaseInfoDto>> GetCombinePatientCaseListAsync(CombinePatientCaseDto combinePatientCase);
        
        bool CombinePatientCase(PatientCaseCombineDto patientCaseCombineDto);
        PatientCaseInfoDto GetPatientCaseNoItems(string id);
        bool EditPatientCase(PatientCaseEditInfoDto patientCaseDto, string userID);

        bool ExamInfoDeleteFile(string patientCaseID, string fileID);
        bool ExamInfoDeleteItem(string patientCaseID, string itemID);
        bool ExamInfoFileNameChanged(string patientCaseID, string fileID, string fileName, string userID);
        PatientCaseInfoDto ExamInfoItemAdded(PatientCaseInfoDto patientCaseInfoDto, string userID);
        PatientCaseInfoDto ExamInfoItemEdited(PatientCaseInfoDto patientCaseInfoDto, string userID);

        bool ReUploadPatientCase(string id);
        bool ReUploadExamItem(string id);
        bool ReUploadFileItem(string id);

        Dictionary<string, bool> DICOMPatientCaseRelations(List<string> dicoms, string userID);
        dynamic GetCaseInfoFromRis(string id);
        List<ConsultationResultDto> GetConsultationResult(string orderId);
    }
}
