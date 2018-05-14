using AutoMapper;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class ConsultationRequestHistioryMapper : Profile
    {
        public ConsultationRequestHistioryMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationRequest, ConsultationRequestHistory>()
                 .ForMember(dto => dto.RequestID, opt =>
                    opt.MapFrom(src => src.UniqueID));
            CreateMap<ConsultationRequestHistory, ConsultationRequest>()
                        .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.RequestID));
        }
    }
}
