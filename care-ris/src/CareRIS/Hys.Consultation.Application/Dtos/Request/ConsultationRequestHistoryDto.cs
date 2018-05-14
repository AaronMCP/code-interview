using System;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestHistoryDto : ConsultationRequestBaseDto
    {
        public string ReuqestHistoryID { get; set; }

        public string Receiver { get; set; }
        public int Status { get; set; }

        public DateTime? StatusUpdateTime { get; set; }
        public int ConsultantType { get; set; }
    }

}
