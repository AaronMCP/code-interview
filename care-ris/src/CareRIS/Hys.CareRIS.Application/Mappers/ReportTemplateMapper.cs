namespace Hys.CareRIS.Application.Mappers
{
    using AutoMapper;
    using Hys.CareRIS.Application.Dtos;
    using Hys.CareRIS.Domain.Entities;
    using System;

    public class RpeortTemplateMapper : Profile
    {
        public RpeortTemplateMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ReportTemplate, ReportTemplateDto>()
                    .ForMember(dto => dto.WYSText, opt =>
                    opt.MapFrom(src => ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(src.WYS))))
                    .ForMember(dto => dto.WYGText, opt =>
                    opt.MapFrom(src => ReportMapper.GetStrFromRTF(ReportMapper.GetStringFromBytes(src.WYG))))
                    .ForMember(dto => dto.ParentID, opt => opt.Ignore())
                    .ForMember(dto => dto.Type, opt => opt.Ignore());
            CreateMap<ReportTemplateDto, ReportTemplate>()
                    .ForMember(src => src.WYS, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(dto.WYSText == null? "" : dto.WYSText)))
               .ForMember(src => src.WYG, opt =>
                    opt.MapFrom(dto => ReportMapper.GetBytes(dto.WYGText == null ? "" : dto.WYGText)));
        }
    }
}
