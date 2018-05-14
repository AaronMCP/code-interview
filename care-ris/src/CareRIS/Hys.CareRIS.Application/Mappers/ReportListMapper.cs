using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Drawing;

namespace Hys.CareRIS.Application.Mappers
{
    public class ReportListMapper : Profile
    {
        public ReportListMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ReportList, ReportListDto>()
                .ForMember(dto => dto.WYS, opt =>
                    opt.MapFrom(src => ReportMapper.GetStringFromBytes(src.WYS)))
                .ForMember(dto => dto.WYG, opt =>
                    opt.MapFrom(src => ReportMapper.GetStringFromBytes(src.WYG)))
                .ForMember(dto => dto.IsDiagnosisRight, opt =>
                    opt.MapFrom(src => src.IsDiagnosisRight.HasValue ? (bool?)(src.IsDiagnosisRight.Value == 1) : null))
                .ForMember(dto => dto.IsPrint, opt =>
                    opt.MapFrom(src => src.IsPrint.HasValue ? (bool?)(src.IsPrint.Value == 1) : null))
                    .ForMember(dto => dto.WYSText, opt =>
                    opt.MapFrom(src => ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(src.WYS))))
                    .ForMember(dto => dto.WYGText, opt =>
                    opt.MapFrom(src => ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(src.WYG))));

            CreateMap<ReportListDto, ReportList>()
               .ForMember(src => src.WYS, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(ReportMapper.GetRTF(dto.WYS))))
               .ForMember(src => src.WYG, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(ReportMapper.GetRTF(dto.WYG))))
               .ForMember(src => src.WYSText, opt =>
                    opt.MapFrom(dto => dto.WYS))
               .ForMember(src => src.WYGText, opt =>
                    opt.MapFrom(dto => dto.WYG))
               .ForMember(src => src.ReportText, opt =>
                    opt.MapFrom(dto => dto.WYS + dto.WYG))
               .ForMember(src => src.IsDiagnosisRight, opt =>
                    opt.MapFrom(dto => dto.IsDiagnosisRight.HasValue ? (int?)(dto.IsDiagnosisRight.Value ? 1 : 0) : null))
               .ForMember(src => src.IsPrint, opt =>
                    opt.MapFrom(dto => dto.IsPrint.HasValue ? (int?)(dto.IsPrint.Value ? 1 : 0) : null));

            CreateMap<ReportDto, ReportList>()
               .ForMember(src => src.WYS, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(ReportMapper.GetRTF(dto.WYS))))
               .ForMember(src => src.WYG, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(ReportMapper.GetRTF(dto.WYG))))
               .ForMember(src => src.WYSText, opt =>
                    opt.MapFrom(dto => dto.WYS))
               .ForMember(src => src.WYGText, opt =>
                    opt.MapFrom(dto => dto.WYG))
               .ForMember(src => src.ReportText, opt =>
                    opt.MapFrom(dto => dto.WYS + dto.WYG))
               .ForMember(src => src.IsDiagnosisRight, opt =>
                    opt.MapFrom(dto => dto.IsDiagnosisRight.HasValue ? (int?)(dto.IsDiagnosisRight.Value ? 1 : 0) : null))
               .ForMember(src => src.IsPrint, opt =>
                    opt.MapFrom(dto => dto.IsPrint.HasValue ? (int?)(dto.IsPrint.Value ? 1 : 0) : null));
        }

      
    }
}
