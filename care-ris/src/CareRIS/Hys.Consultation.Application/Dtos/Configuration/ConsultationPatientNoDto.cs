using System;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationPatientNoDto
    {
        public string UniqueID { get; set; }

        public string HospitalID { get; set; }
        public string Prefix { get; set; }
        public int MaxLength { get; set; }
        public int CurrentValue { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}