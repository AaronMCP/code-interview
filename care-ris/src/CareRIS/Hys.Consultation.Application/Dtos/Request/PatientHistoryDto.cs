namespace Hys.Consultation.Application.Dtos
{
    public class PatientHistoryDto : AuditDto
    {
        public string PatientCaseID { get; set; }
        public string History { get; set; }
    }
}
