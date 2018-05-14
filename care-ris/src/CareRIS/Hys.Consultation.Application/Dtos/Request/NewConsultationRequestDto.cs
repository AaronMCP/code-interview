using System;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class NewConsultationRequestDto
    {
        public string RequestPurpose { get; set; }

        public string RequestRequirement { get; set; }
        public string PatientCaseID { get; set; }
        public string ConsultationType { get; set; }
        public string SelectHospital { get; set; }
        public string[] SelectExperts { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string ExpectedTimeRange { get; set; }
    }

}
