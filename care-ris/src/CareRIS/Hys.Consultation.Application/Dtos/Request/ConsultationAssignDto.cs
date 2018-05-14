using System;
namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationAssignDto
    {
        public string UniqueID { get; set; }
        public string DisplayName { get; set; }
        public string ConsultationRequestID { get; set; }
        public string AssignedUserID { get; set; }
        public DateTime AssignedTime { get; set; }
        public int IsHost { get; set; }
        public string Comments { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
        public string Avatar { get; set; }
        public string HospitalID { get; set; }

    }
}
