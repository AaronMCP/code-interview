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
    public class ACRCodeSubAnatomicalMapper : Profile
    {
        public ACRCodeSubAnatomicalMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ACRCodeSubAnatomical, ACRCodeSubAnatomicalDto>()
                .ForMember(dto => dto.AnId, opt =>
                 opt.MapFrom(src => src.AID))
                 .ForMember(dto => dto.SuId, opt =>
                 opt.MapFrom(src => src.SID));

            CreateMap<ACRCodeSubAnatomicalDto, ACRCodeSubAnatomical>()
                .ForMember(src => src.AID, opt =>
              opt.MapFrom(dto => dto.AnId))
              .ForMember(src => src.SID, opt =>
              opt.MapFrom(dto => dto.SuId));

        }
    }
}
