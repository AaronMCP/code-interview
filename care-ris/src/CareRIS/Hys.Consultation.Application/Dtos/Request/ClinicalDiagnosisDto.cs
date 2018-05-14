namespace Hys.Consultation.Application.Dtos
{
    public class ClinicalDiagnosisDto : AuditDto
    {
        public string PatientCaseID { get; set; }
        public string ClinicalDiagnosis { get; set; }
    }
}
