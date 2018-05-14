using System;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestDto : ConsultationRequestBaseDto
    {
        public string Receiver { get; set; }
        public int Status { get; set; }

        public List<String> ReceiverIDs { get; set; }
        public List<Selection> Selections { get; set; }
        public DateTime? StatusUpdateTime { get; set; }
        public int ConsultantType { get; set; }
        public string RequestUserName { get; set; }
    }
    public class Selection
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string HospitalID { get; set; }
        public string HospitalName { get; set; }

    }
}
