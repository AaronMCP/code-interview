namespace Hys.CareRIS.Application.Mappers
{
    using AutoMapper;
    using Hys.CareRIS.Application.Dtos;
    using Hys.CareRIS.Domain.Entities;
    using System;

    public class ReportTemplateDirecMapper : Profile
    {
        public ReportTemplateDirecMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ReportTemplateDirec, ReportTemplateDirecDto>();

            CreateMap<ReportTemplateDirecDto, ReportTemplateDirec>();
        }
    }
}
