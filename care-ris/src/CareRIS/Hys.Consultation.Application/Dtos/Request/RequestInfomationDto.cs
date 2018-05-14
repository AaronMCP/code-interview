namespace Hys.Consultation.Application.Dtos
{
    public class RequestInfomationDto : AuditDto
    {
        public string RequestID { get; set; }

        public string RequestPurpose { get; set; }

        public string RequestRequirement { get; set; }
    }
}
