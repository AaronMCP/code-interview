﻿using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Mappers
{
    public class ACRCodePathologicalMapper : Profile
    {
        public ACRCodePathologicalMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ACRCodePathological, ACRCodePathologicalDto>().
                 ForMember(dto => dto.AnId, opt =>
                 opt.MapFrom(src => src.AID)).
                  ForMember(dto => dto.PaId, opt =>
                 opt.MapFrom(src => src.PID));
     
            CreateMap<ACRCodePathologicalDto, ACRCodePathological>().
                 ForMember(src => src.AID, opt =>
               opt.MapFrom(dto => dto.AnId)).
                ForMember(src => src.PID, opt =>
               opt.MapFrom(dto => dto.PaId));
   
        }
    }
}
