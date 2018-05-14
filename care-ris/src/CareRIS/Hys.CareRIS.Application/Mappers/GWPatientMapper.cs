using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class GWPatientMapper : Profile
    {
        public GWPatientMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<GWPatient, GWPatientDto>();

            CreateMap<GWPatientDto, GWPatient>();
        }
    }
}
