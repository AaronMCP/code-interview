namespace Hys.Consultation.Application.Mappers
{
    using AutoMapper;
    using Hys.Consultation.Application.Dtos;
    using Hys.Consultation.Domain.Entities;
    using Hys.CareRIS.Domain.Entities;
    using System;

    public class ConsultatReportTemplateDirecMapper : Profile
    {
        public ConsultatReportTemplateDirecMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultatReportTemplateDirec, ConsultatReportTemplateDirecDto>();

            CreateMap<ConsultatReportTemplateDirecDto, ConsultatReportTemplateDirec>();
        }
    }
}
