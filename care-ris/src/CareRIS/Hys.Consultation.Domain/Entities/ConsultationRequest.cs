using Hys.Platform.Domain;
using System;


namespace Hys.Consultation.Domain.Entities
{
    public partial class ConsultationRequest : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        /// <summary>
        /// Consultation RequestId
        /// </summary>
        public string UniqueID { get; set; }

        /// <summary>
        ///  Ris users table
        /// </summary>
        public string RequestUserID { get; set; }

        /// <summary>
        /// hospital file table
        /// </summary>
        public string RequestHospitalID { get; set; }
        public string PatientCaseID { get; set; }

        /// <summary>
        /// service type table
        /// </summary>
        public string ServiceTypeID { get; set; }

        /// <summary>
        /// 0: center 1: expert
        /// </summary>
        public int ConsultantType { get; set; }


        /// <summary>
        /// Request belong to which hospital
        /// </summary>
        public string ReceiveHospitalID { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string ExpectedTimeRange { get; set; }

        /// <summary>
        /// Request Purpose
        /// </summary>
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

        /// <summary>
        /// Report table
        /// </summary>
        public string ConsultationReportID { get; set; }
        public string RequestUserName { get; set; }
        public string LastEditUser { get; set; }
        public string LastEditUserName { get; set; }
        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// Delete info
        /// </summary>
        public int IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteReason { get; set; }
        public string DeleteUser { get; set; }
    }
}
