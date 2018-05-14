using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class NotificationConfig : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string Template { get; set; }
        public string SiteID { get; set; }
        public int Event { get; set; }
        public string Variables { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDefault { get; set; }

        public string NotifyTypes { get; set; }
        public string SendingTimes { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
