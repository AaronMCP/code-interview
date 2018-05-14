using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class ChangeReasonMapper : Profile
    {
        public ChangeReasonMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationRequest, ChangeReasonDto>()
                .ForMember(dto => dto.RequestID, opt =>
                    opt.MapFrom(src => src.UniqueID))
                .ForMember(dto => dto.ChangeReasonType, opt =>
                    opt.MapFrom(src => src.ChangeReasonType))
                .ForMember(dto => dto.OtherReason, opt =>
                    opt.MapFrom(src => src.OtherReason));

            CreateMap<ChangeReasonDto, ConsultationRequest>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.RequestID))
                    .ForMember(src => src.ChangeReasonType, opt =>
                    opt.MapFrom(dto => dto.ChangeReasonType));
        }
    }
}
