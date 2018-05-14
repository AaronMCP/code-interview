using System;

// ReSharper disable once CheckNamespace
namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationRequestFlatDto
    {
        public string UniqueID { get; set; }
        public string RequestUserID { get; set; }
        public string RequestHospitalID { get; set; }
        public string PatientCaseID { get; set; }
        public string ServiceTypeID { get; set; }
        public int ConsultantType { get; set; }
        public string ReceiveHospitalID { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string ExpectedTimeRange { get; set; }

        public string RequestDescription { get; set; }
        public string RequestRequirement { get; set; }

        public int Status { get; set; }
        public DateTime StatusUpdateTime { get; set; }
        public string AssignedBy { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public string ConsultationStartTime { get; set; }

        public string ConsultationEndTime { get; set; }
        public string MeetingRoom { get; set; }
        public string AssignedDescription { get; set; }
        public string SpecialComment { get; set; }

        public DateTime RequestCreateDate { get; set; }
        public DateTime? RequestCompleteDate { get; set; }

        public string OtherReason { get; set; }
        public string ChangeReasonType { get; set; }
        public string ConsultationReportID { get; set; }
        public string RequestUserName { get; set; }
        public string LastEditUser { get; set; }
        public string LastEditUserName { get; set; }
        public DateTime LastEditTime { get; set; }
        public int IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteReason { get; set; }
        public string DeleteUser { get; set; }
    }
}
