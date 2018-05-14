using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class ReportAdviceMapper : Profile
    {
        public ReportAdviceMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationReport, ReportAdviceDto>()
               .ForMember(dto => dto.ConsultationReportID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                .ForMember(dto => dto.ConsultationAdvice, opt =>
                   opt.MapFrom(src => src.Advice))
                    .ForMember(dto => dto.ConsultationRemark, opt =>
                   opt.MapFrom(src => src.Description))
                    .ForMember(dto => dto.Writer, opt =>
                   opt.MapFrom(src => src.EditorID));

            CreateMap<ReportAdviceDto, ConsultationReport>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.ConsultationReportID))
                    .ForMember(src => src.Advice, opt =>
                    opt.MapFrom(dto => dto.ConsultationAdvice))
                    .ForMember(src => src.Description, opt =>
                    opt.MapFrom(dto => dto.ConsultationRemark))
                    .ForMember(src => src.EditorID, opt =>
                    opt.MapFrom(dto => dto.Writer));
        }
    }
}
