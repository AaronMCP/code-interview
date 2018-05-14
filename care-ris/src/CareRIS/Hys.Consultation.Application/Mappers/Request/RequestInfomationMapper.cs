using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class RequestInfomationMapper : Profile
    {
        public RequestInfomationMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationRequest, RequestInfomationDto>()
               .ForMember(dto => dto.RequestID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                .ForMember(dto => dto.RequestPurpose, opt =>
                   opt.MapFrom(src => src.RequestDescription));

            CreateMap<RequestInfomationDto, ConsultationRequest>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.RequestID))
                    .ForMember(src => src.RequestDescription, opt =>
                    opt.MapFrom(dto => dto.RequestPurpose));
        }
    }
}
