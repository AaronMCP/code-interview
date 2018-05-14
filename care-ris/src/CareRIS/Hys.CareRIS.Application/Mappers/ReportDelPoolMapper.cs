using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Drawing;

namespace Hys.CareRIS.Application.Mappers
{
    public class ReportDelPoolMapper : Profile
    {
        public ReportDelPoolMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ReportDelPool, ReportDto>()
                .ForMember(dto => dto.WYS, opt =>
                    opt.MapFrom(src => ReportMapper.GetStringFromBytes(src.WYS)))
                .ForMember(dto => dto.WYG, opt =>
                    opt.MapFrom(src => ReportMapper.GetStringFromBytes(src.WYG)))
                .ForMember(dto => dto.IsDiagnosisRight, opt =>
                    opt.MapFrom(src => src.IsDiagnosisRight.HasValue ? (bool?)(src.IsDiagnosisRight.Value == 1) : null))
                .ForMember(dto => dto.IsPrint, opt =>
                    opt.MapFrom(src => src.IsPrint.HasValue ? (bool?)(src.IsPrint.Value == 1) : null))
                .ForMember(dto => dto.IsLeaveWord, opt =>
                    opt.MapFrom(src => src.IsLeaveWord.HasValue ? (bool?)(src.IsLeaveWord.Value == 1) : null))
                .ForMember(dto => dto.IsDraw, opt =>
                    opt.MapFrom(src => src.IsDraw.HasValue ? (bool?)(src.IsDraw.Value == 1) : null))
                .ForMember(dto => dto.IsLeaveSound, opt =>
                    opt.MapFrom(src => src.IsLeaveSound.HasValue ? (bool?)(src.IsLeaveSound.Value == 1) : null))
                .ForMember(dto => dto.RebuildMark, opt =>
                    opt.MapFrom(src => src.RebuildMark.HasValue ? (bool?)(src.RebuildMark.Value == 1) : null))
                .ForMember(dto => dto.Uploaded, opt =>
                    opt.MapFrom(src => src.Uploaded.HasValue ? (bool?)(src.Uploaded.Value == 1) : null))
                .ForMember(dto => dto.IsModified, opt =>
                    opt.MapFrom(src => src.IsModified.HasValue ? (bool?)(src.IsModified.Value == 1) : null))
                .ForMember(dto => dto.DeleteMark, opt =>
                    opt.MapFrom(src => src.DeleteMark.HasValue ? (bool?)(src.DeleteMark.Value == 1) : null));

            CreateMap<ReportDto, ReportDelPool>()
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
                    opt.MapFrom(dto => dto.IsPrint.HasValue ? (int?)(dto.IsPrint.Value ? 1 : 0) : null))
               .ForMember(src => src.IsLeaveWord, opt =>
                    opt.MapFrom(dto => dto.IsLeaveWord.HasValue ? (int?)(dto.IsLeaveWord.Value ? 1 : 0) : null))
               .ForMember(src => src.IsDraw, opt =>
                    opt.MapFrom(dto => dto.IsDraw.HasValue ? (int?)(dto.IsDraw.Value ? 1 : 0) : null))
               .ForMember(src => src.IsLeaveSound, opt =>
                    opt.MapFrom(dto => dto.IsLeaveSound.HasValue ? (int?)(dto.IsLeaveSound.Value ? 1 : 0) : null))
               .ForMember(src => src.RebuildMark, opt =>
                    opt.MapFrom(dto => dto.RebuildMark.HasValue ? (int?)(dto.RebuildMark.Value ? 1 : 0) : null))
               .ForMember(src => src.Uploaded, opt =>
                    opt.MapFrom(dto => dto.Uploaded.HasValue ? (int?)(dto.Uploaded.Value ? 1 : 0) : null))
               .ForMember(src => src.IsModified, opt =>
                    opt.MapFrom(dto => dto.IsModified.HasValue ? (int?)(dto.IsModified.Value ? 1 : 0) : null))
               .ForMember(src => src.DeleteMark, opt =>
                    opt.MapFrom(dto => dto.DeleteMark.HasValue ? (int?)(dto.DeleteMark.Value ? 1 : 0) : null));
        }

     
    }
}
