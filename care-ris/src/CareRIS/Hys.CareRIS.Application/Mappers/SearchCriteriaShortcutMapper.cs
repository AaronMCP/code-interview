using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class SearchCriteriaShortcutMapper : Profile
    {
        public SearchCriteriaShortcutMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Shortcut, SearchCriteriaShortcutDto>()
              .ForMember(dto => dto.IsDefault, opt =>
                  opt.MapFrom(src => src.Type == 1));

            CreateMap<SearchCriteriaShortcutDto, Shortcut>()
             .ForMember(src => src.Type, opt =>
                 opt.MapFrom(dto => dto.IsDefault ? 1 : 0))
             .ForMember(src => src.Category, opt =>
                 opt.MapFrom(dto => WorklistService.Category)); 
        }
    }
}
