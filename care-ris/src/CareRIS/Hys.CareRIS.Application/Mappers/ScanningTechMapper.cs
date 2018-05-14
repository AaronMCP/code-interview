﻿using System;
using System.Linq;
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class ScanningTechMapper : Profile
    {
        public ScanningTechMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ScanningTech, ScanningTechDto>();
            CreateMap<ScanningTechDto, ScanningTech>();
        }
    }
}
