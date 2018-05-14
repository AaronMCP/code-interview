using System;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestTransfer
    {
        public string RequestId { get; set; }
        public string PatientNo { get; set; }
        public string IdentityCard { get; set; }
        public string PatientCaseID { get; set; }
        public string PatientName { get; set; }
        public string CurrentAge { get; set; }
        public string Gender { get; set; }

        public string ReceiveHospitalID { get; set; }

        public DateTime? ConsultationDate { get; set; }
        public DateTime? RequestCreateDate { get; set; }
        public string ConsultationStartTime { get; set; }
        public string ConsultationEndTime { get; set; }
        public string ExpectedTimeRange { get; set; }
        public DateTime? ExpectedDate { get; set; }

        public int ConsultantType { get; set; }

        public string RequestUserID { get; set; }
        public string RequestUserName { get; set; }
        public string RequestHospitalID { get; set; }
        public int Status { get; set; }
        public int IsDeleted { get; set; }

        public DateTime LastEditTime { get; set; }
        public DateTime StatusUpdateTime { get; set; }
    }
}
