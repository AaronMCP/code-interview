using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Mappers
{
    public class ACRCodeAnatomicalMapper : Profile
    {
        public ACRCodeAnatomicalMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ACRCodeAnatomical, ACRCodeAnatomicalDto>().
                ForMember(dto => dto.AnId, opt =>
                 opt.MapFrom(src=>src.AID));
     
            CreateMap<ACRCodeAnatomicalDto, ACRCodeAnatomical>().
                ForMember(src => src.AID, opt =>
               opt.MapFrom(dto => dto.AnId));
   
        }
    }
}
