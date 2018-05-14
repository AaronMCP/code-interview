using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class GWDataIndexMapper : Profile
    {
        public GWDataIndexMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<GWDataIndex, GWDataIndexDto>();

            CreateMap<GWDataIndexDto, GWDataIndex>();
        }
    }
}
