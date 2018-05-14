using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class DictionaryMapper : Profile
    {
        public DictionaryMapper()
        {
            Configure();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Dictionary, DictionaryDto>()
              .ForMember(dto => dto.value, opt => opt.Ignore())
              .ForMember(dto => dto.IsHidden, opt =>
                  opt.MapFrom(src => src.IsHidden.HasValue ? (bool?)(src.IsHidden.Value == 1) : null));

            CreateMap<DictionaryDto, Dictionary>()
             .ForMember(src => src.IsHidden, opt =>
                 opt.MapFrom(dto => dto.IsHidden.HasValue ? (int?)(dto.IsHidden.Value ? 1 : 0) : null));
        }
    }
}
