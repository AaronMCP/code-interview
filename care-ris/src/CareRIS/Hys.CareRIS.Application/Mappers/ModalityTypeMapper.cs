using System;
using System.Linq;
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class ModalityTypeMapper : Profile
    {
        public ModalityTypeMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ModalityType, ModalityTypeDto>()
                  .ForMember(dto => dto.HasChildren, opt => opt.Ignore())
                       .ForMember(dto => dto.Childrens, opt => opt.Ignore());
            

            CreateMap<ModalityTypeDto, ModalityType>()
             .ForMember(src => src.UniqueID, opt =>
                 opt.MapFrom(dto => dto.UniqueId.ToString()));
        }
    }
}
