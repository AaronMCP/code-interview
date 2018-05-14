using System;

namespace Hys.Consultation.Application.Dtos
{
    public class ChangeReasonDto : AuditDto
    {
        public string RequestID { get; set; }
        public string ChangeReasonType { get; set; }
        public string OtherReason { get; set; }
        public int Status { get; set; }
        public DateTime StatusUpdateTime { get; set; }
    }
}
