using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class NotifyConfigMapper : Profile
    {
        public NotifyConfigMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<NotificationConfig, NotificationConfigDto>();
            CreateMap<NotificationConfigDto, NotificationConfig>();
        }
    }
}
