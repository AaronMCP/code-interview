using System;

namespace Hys.Consultation.Application.Dtos
{
    public class NotificationDto
    {
        public string UniqueID { get; set; }
        public string Text { get; set; }
        public string Recipients { get; set; }
        public bool IsSended { get; set; }
        public NotifyType NotifyType { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
