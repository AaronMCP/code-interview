using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestSpecialistSearchDto : ConsultationSearchBaseDto
    {
        public IEnumerable<ConsultationRequestSpecialistDto> Requests { get; set; }
    }
}
