using System;

namespace Hys.Consultation.Application.Dtos
{
    public class NotificationConfigDto
    {
        public string UniqueID { get; set; }
        public string Template { get; set; }
        public string SiteID { get; set; }
        public NotifyEvent Event { get; set; }
        public string Variables { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDefault { get; set; }

        public string NotifyTypes { get; set; }
        public string SendingTimes { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
