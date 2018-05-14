using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class PrintTemplateFieldsMapper : Profile
    {
        public PrintTemplateFieldsMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PrintTemplateFields, PrintTemplateFieldsDto>();

            CreateMap<PrintTemplateFieldsDto, PrintTemplateFields>();
        }
    }
}
