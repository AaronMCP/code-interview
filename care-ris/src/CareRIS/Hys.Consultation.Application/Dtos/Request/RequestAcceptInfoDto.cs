using System;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class RequestAcceptInfoDto 
    {
        public string RequestID { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public string consultationStartTime { get; set; }
        public string Description { get; set; }
        public string MeetingRoom { get; set; }
        public string DefaultID { get; set; }
        public List<string> ExpertList { get; set; }
    }
}
