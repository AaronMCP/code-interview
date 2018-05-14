using System.Collections.Generic;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.Configuration;

namespace Hys.Consultation.Application.Services
{
    public interface INotificationService
    {
        void CallbackNotification(string ownerID,params NotifyEvent[] notifyEvent);
        bool SendNotification(NotificationData notificationData);
        IEnumerable<NotificationConfigDto> GetNotificationConfigs();
        bool SaveNotificationConfigs(List<NotificationConfigDto> configs);
    }
}
