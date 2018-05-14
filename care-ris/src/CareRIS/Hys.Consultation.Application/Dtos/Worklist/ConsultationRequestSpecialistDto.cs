using System;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestSpecialistDto : ConsultationRequestBaseDto
    {
        public string Requester { get; set; }
        public int Status { get; set; }
        public string RequesterHospital { get; set; }
        public string Receiver { get; set; }

        public DateTime StatusUpdateTime { get; set; }
        public string RequestUserName { get; set; }
    }
}
