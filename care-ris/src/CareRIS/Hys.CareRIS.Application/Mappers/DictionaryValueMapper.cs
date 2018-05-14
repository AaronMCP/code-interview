using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class DictionaryValueMapper : Profile
    {
        public DictionaryValueMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<DictionaryValue, DictionaryValueDto>()
                .ForMember(dto => dto.IsDefault, opt =>
                    opt.MapFrom(src => src.IsDefault.HasValue ? (bool?)(src.IsDefault.Value == 1) : null));

            CreateMap<DictionaryValueDto, DictionaryValue>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.UniqueID.ToString()))
                .ForMember(src => src.IsDefault, opt =>
                    opt.MapFrom(dto => dto.IsDefault.HasValue ? (int?)(dto.IsDefault.Value ? 1 : 0) : null));
        }
    }
}
