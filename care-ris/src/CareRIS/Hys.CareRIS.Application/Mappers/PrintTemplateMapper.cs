using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class PrintTemplateMapper : Profile
    {
        public PrintTemplateMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PrintTemplate, PrintTemplateDto>()
                .ForMember(dto => dto.IsDefaultByType, opt =>
                    opt.MapFrom(src => src.IsDefaultByType.HasValue ? (bool?)(src.IsDefaultByType.Value == 1) : null))
                .ForMember(dto => dto.IsDefaultByModality, opt =>
                    opt.MapFrom(src => src.IsDefaultByModality.HasValue ? (bool?)(src.IsDefaultByModality.Value == 1) : null));

            CreateMap<PrintTemplateDto, PrintTemplate>()
               .ForMember(src => src.IsDefaultByType, opt =>
                    opt.MapFrom(dto => dto.IsDefaultByType.HasValue ? (int?)(dto.IsDefaultByType.Value ? 1 : 0) : null))
               .ForMember(src => src.IsDefaultByModality, opt =>
                    opt.MapFrom(dto => dto.IsDefaultByModality.HasValue ? (int?)(dto.IsDefaultByModality.Value ? 1 : 0) : null));
        }
    }
}
