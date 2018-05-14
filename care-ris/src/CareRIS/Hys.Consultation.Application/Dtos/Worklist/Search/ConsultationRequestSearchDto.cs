using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestSearchDto : ConsultationSearchBaseDto
    {
        public List<ConsultationRequestDto> Requests { get; set; }
    }
}
