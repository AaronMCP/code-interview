using System;

namespace Hys.Consultation.Application.Dtos.Configuration
{
    public class NotificationData
    {
        public NotificationData()
        {
            SendingTime = DateTime.Now;
        }

        public NotifyEvent NotifyEvent { get; set; }
        public String OwnerID { get; set; }
        public String Recipients { get; set; }
        public DateTime SendingTime { get; set; }
        public NotificationContext Context { get; set; }
    }
}