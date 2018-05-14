using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class Notification : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string Text { get; set; }
        public string Recipients { get; set; }
        public bool IsSended { get; set; }
        public int NotifyType { get; set; }

        public int Event { get; set; }
        public string OwnerID { get; set; }
        public string Sender { get; set; }
        public DateTime? PendingTime { get; set; }
        public int Result { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
