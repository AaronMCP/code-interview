using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.Configuration;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class NotificationService : INotificationService
    {
        private readonly ILoginUserService _LoginUserService;
        private readonly IConsultationContext _DBContext;

        public NotificationService(ILoginUserService loginUserService, IConsultationContext dbContext)
        {
            _LoginUserService = loginUserService;
            _DBContext = dbContext;
        }

        public bool SendNotification(NotificationData notificationData)
        {
            var config = GetNotificationConfigs().FirstOrDefault(c => c.Event == notificationData.NotifyEvent);
            if (config != null && config.IsEnable && !String.IsNullOrWhiteSpace(config.NotifyTypes))
            {
                var template = config.Template;
                var text = notificationData.Context.Translate(template);
                foreach (var type in config.NotifyTypes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _DBContext.Set<Notification>().Add(new Notification
                    {
                        UniqueID = Guid.NewGuid().ToString(),
                        IsSended = false,
                        Event = (int)notificationData.NotifyEvent,
                        LastEditTime = DateTime.Now,
                        LastEditUser = _LoginUserService.CurrentUserID,
                        NotifyType = int.Parse(type),
                        Recipients = notificationData.Recipients,
                        PendingTime = DateTime.Now,
                        Sender = _LoginUserService.CurrentUserID,
                        OwnerID = notificationData.OwnerID,
                        Text = text,
                    });
                    if (!String.IsNullOrWhiteSpace(config.SendingTimes))
                    {
                        //config.AdjustTime
                        foreach (var adjustment in config.SendingTimes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            int offset;
                            if (int.TryParse(adjustment, out offset))
                            {
                                var sendingTime = notificationData.SendingTime.AddMinutes(-offset);
                                if (sendingTime > DateTime.Now)
                                {
                                    _DBContext.Set<Notification>().Add(new Notification
                                    {
                                        UniqueID = Guid.NewGuid().ToString(),
                                        IsSended = false,
                                        Event = (int)notificationData.NotifyEvent,
                                        LastEditTime = DateTime.Now,
                                        LastEditUser = _LoginUserService.CurrentUserID,
                                        NotifyType = int.Parse(type),
                                        Recipients = notificationData.Recipients,
                                        PendingTime = sendingTime,
                                        Sender = _LoginUserService.CurrentUserID,
                                        OwnerID = notificationData.OwnerID,
                                        Text = text,
                                    });
                                }
                            }
                        }
                    }
                }

                _DBContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void CallbackNotification(string ownerID, params NotifyEvent[] notifyEvent)
        {
            var events = notifyEvent.Select(n => (int)n);
            _DBContext.Set<Notification>().RemoveRange(_DBContext.Set<Notification>().Where(c => c.OwnerID == ownerID && !c.IsSended && events.Contains(c.Event)));
            _DBContext.SaveChanges();
        }

        public IEnumerable<NotificationConfigDto> GetNotificationConfigs()
        {
            return _DBContext.Set<NotificationConfig>().Where(c => c.IsDefault)
                .Select(Mapper.Map<NotificationConfig, NotificationConfigDto>);
        }

        public bool SaveNotificationConfigs(List<NotificationConfigDto> configs)
        {
            var dataSet = _DBContext.Set<NotificationConfig>();
            foreach (var item in configs)
            {
                var config = item;
                var existing = dataSet.FirstOrDefault(c => c.UniqueID == config.UniqueID);
                if (existing != null)
                {
                    Mapper.Map(config, existing);
                }
            }
            _DBContext.SaveChanges();
            return true;
        }
    }
}