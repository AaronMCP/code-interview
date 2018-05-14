using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class ReportHistoryMapper : Profile
    {
        public ReportHistoryMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationReportHistory, ConsultationReportHistoryDto>()
                .ForMember(dto => dto.ReportID, opt =>
                    opt.MapFrom(src => src.UniqueID))
                .ForMember(dto => dto.Writer, opt =>
                    opt.MapFrom(src => src.EditorID))
                .ForMember(dto => dto.LastEditTime, opt =>
                    opt.MapFrom(src => src.LastEditTime))
                .ForMember(dto => dto.LastEditUser, opt =>
                    opt.MapFrom(src => src.LastEditUser))
                .ForMember(dto => dto.ConsultationAdvice, opt =>
                    opt.MapFrom(src => src.Advice));

            CreateMap<ConsultationReportHistoryDto, ConsultationReportHistory>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.ReportID))
                .ForMember(src => src.EditorID, opt =>
                    opt.MapFrom(dto => dto.Writer))
                .ForMember(src => src.Advice, opt =>
                    opt.MapFrom(dto => dto.ConsultationAdvice));
        }
    }
}
