using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class PatientCaseSearchDto : ConsultationSearchBaseDto
    {
        public List<PatientCaseDto> Cases { get; set; }
    }
}
