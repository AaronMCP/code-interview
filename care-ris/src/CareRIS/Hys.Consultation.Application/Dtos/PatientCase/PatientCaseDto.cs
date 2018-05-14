using System;

namespace Hys.Consultation.Application.Dtos
{
    public class PatientCaseDto : PatientBaseDto
    {
        public int Status { get; set; }

        public string ExamIDs { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
