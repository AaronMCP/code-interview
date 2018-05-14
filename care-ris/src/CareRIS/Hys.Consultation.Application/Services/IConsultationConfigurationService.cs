using Hys.Consultation.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Services
{
    public interface IConsultationConfigurationService
    {
        IEnumerable<ConsultationDictionaryDto> GetAllDictionaries(string lang);
        IEnumerable<ConsultationDictionaryDto> GetDictionaryByType(int type, string language);
        IEnumerable<ConsultationDictionaryDtos> GetDictionaryByTypes(int[] types, string language);
        IEnumerable<ExamModuleDto> GetDefaultExamModule(string lang);
        IEnumerable<ExamModuleDto> GetUserExamModule(string userID, string language);
        IEnumerable<ExamModuleDto> GetExamModule(string owner);
        bool AddExamModules(IEnumerable<ExamModuleDto> modules, string userID);
        bool UpdateExamModule(ExamModuleDto module, string userID);
        bool UpdateExamModules(IEnumerable<ExamModuleDto> modules, string userID);
        HospitalProfileDto GetHospital(string userID);
        DAMInfoDto GetDam();
        Task<string> GetDamIdAsync();
        DAMInfoDto GetDamByID(string id);
        IEnumerable<RoleDto> GetRoles();
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        bool SaveRole(RoleDto role);
        IEnumerable<DepartmentDto> GetDepartments();
        List<ServiceTypeDto> GetServiceType(string lang);
        IEnumerable<HospitalDefaultDto> GetRecipientConfigs();
        bool SaveRecipientConfigs(HospitalDefaultDto hospitalDefaultDto, string userID);
        List<HospitalDefaultDto> GetHospitalDefaultForHospital();
        List<HospitalDefaultDto> GetHospitalDefaultForExpert();
        bool ValidateRoleName(string roleID, string roleName);
        List<DAMInfoDto> GetDams();
        IEnumerable<HospitalDefaultDto> GetRecipientConfigsForReceiver(string userID);
        string GeneratePatientNo(string userID);
        Task<string> GeneratePatientNoAsync(string userID);
    }
}
