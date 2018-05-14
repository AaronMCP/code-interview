using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class ConsultationAssign : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ConsultationRequestID { get; set; }
        public string AssignedUserID { get; set; }
        public DateTime AssignedTime { get; set; }
        public int IsHost { get; set; }
        public string Comments { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
