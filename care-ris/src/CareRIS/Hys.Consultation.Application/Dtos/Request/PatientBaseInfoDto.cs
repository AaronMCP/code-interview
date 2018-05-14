using System;

namespace Hys.Consultation.Application.Dtos
{
    public class PatientBaseInfoDto : AuditDto
    {
        public string PatientCaseID { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string PatientNo { get; set; }
        public string InsuranceNumber { get; set; }
        public string IdentityCard { get; set; }
        public string Telephone { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
