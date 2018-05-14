using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class RequestReceiveMapper : Profile
    {
        public RequestReceiveMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationRequest, RequestReceiverDto>()
               .ForMember(dto => dto.RequestID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                .ForMember(dto => dto.ReceiveUserID, opt =>
                   opt.MapFrom(src => src.RequestUserID))
                    .ForMember(dto => dto.ServiceTypeID, opt =>
                   opt.MapFrom(src => src.ServiceTypeID))
                    .ForMember(dto => dto.ExpectedDate, opt =>
                   opt.MapFrom(src => src.ExpectedDate))
                    .ForMember(dto => dto.ExpectedTimeRange, opt =>
                   opt.MapFrom(src => src.ExpectedTimeRange));

            CreateMap<RequestReceiverDto, ConsultationRequest>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.RequestID))
                    .ForMember(src => src.RequestUserID, opt =>
                    opt.MapFrom(dto => dto.ReceiveUserID))
                    .ForMember(src => src.ServiceTypeID, opt =>
                    opt.MapFrom(dto => dto.ServiceTypeID))
                    .ForMember(src => src.ExpectedDate, opt =>
                    opt.MapFrom(dto => dto.ExpectedDate))
                    .ForMember(src => src.ExpectedTimeRange, opt =>
                    opt.MapFrom(dto => dto.ExpectedTimeRange));
        }
    }
}
