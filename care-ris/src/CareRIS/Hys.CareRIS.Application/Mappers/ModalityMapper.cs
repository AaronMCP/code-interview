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
    public class ModalityMapper : Profile
    {
        public ModalityMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Modality, ModalityDto>()
            .ForMember(dto => dto.ApplyHaltPeriod, opt =>
                    opt.MapFrom(src => src.ApplyHaltPeriod == 1 ? true : false));
            CreateMap<ModalityDto, Modality>()
             .ForMember(src => src.UniqueID, opt => opt.MapFrom(dto => dto.UniqueID.ToString()))
             .ForMember(src => src.ApplyHaltPeriod, 
                        opt => opt.MapFrom(dto => dto.ApplyHaltPeriod.HasValue ? (int)(dto.ApplyHaltPeriod.Value ? 1 : 0) : 0));
        }
    }
}
