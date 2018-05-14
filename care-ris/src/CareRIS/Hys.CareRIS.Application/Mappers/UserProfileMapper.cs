
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
namespace Hys.CareRIS.Application.Mappers
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            Configure();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(dto => dto.IsExportable, opt =>
                    opt.MapFrom(src => src.IsExportable == 1))
                .ForMember(dto => dto.CanBeInherited, opt =>
                    opt.MapFrom(src => src.CanBeInherited == 1))
                .ForMember(dto => dto.PropertyType, opt =>
                     opt.MapFrom(src => src.PropertyType == 1))
                .ForMember(dto => dto.IsHidden, opt =>
                    opt.MapFrom(src => src.IsHidden == 1));

            CreateMap<UserProfileDto, UserProfile>()
                .ForMember(src => src.IsExportable, opt =>
                    opt.MapFrom(dto => dto.IsExportable ? 1 : 0))
                .ForMember(src => src.CanBeInherited, opt =>
                    opt.MapFrom(dto => dto.CanBeInherited ? 1 : 0))
                .ForMember(src => src.PropertyType, opt =>
                    opt.MapFrom(dto => dto.PropertyType ? 1 : 0))
                .ForMember(src => src.IsHidden, opt =>
                    opt.MapFrom(dto => dto.IsHidden ? 1 : 0));
        }
    }
}
