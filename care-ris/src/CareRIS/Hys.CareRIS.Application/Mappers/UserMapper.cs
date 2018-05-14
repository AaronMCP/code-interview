using AutoMapper;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
namespace Hys.CareRIS.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.RoleName, opt => opt.Ignore())
                .ForMember(dto => dto.Site, opt => opt.Ignore())
                .ForMember(dto => dto.Language, opt => opt.Ignore())
                .ForMember(dto => dto.HasValidPeriod, opt => opt.Ignore())
                .ForMember(dto => dto.ValidStartDate, opt => opt.Ignore())
                .ForMember(dto => dto.ValidEndDate, opt => opt.Ignore())
                .ForMember(dto => dto.PdfService, opt => opt.Ignore())
                .ForMember(dto => dto.Department, opt => opt.Ignore())
                .ForMember(dto => dto.Telephone, opt => opt.Ignore())
                .ForMember(dto => dto.Mobile, opt => opt.Ignore())
                .ForMember(dto => dto.Email, opt => opt.Ignore())
                .ForMember(dto => dto.IsSetExpireDate, opt => opt.Ignore())
                .ForMember(dto => dto.StartDate, opt => opt.Ignore())
                .ForMember(dto => dto.EndDate, opt => opt.Ignore())
                .ForMember(dto => dto.RolesName, opt => opt.Ignore())
                .ForMember(dto => dto.UserProfileList, opt => opt.Ignore())
                .ForMember(dto => dto.AccessSites, opt => opt.Ignore())
                .ForMember(dto => dto.IsLocked, opt =>
                    opt.MapFrom(src => src.IsLocked.HasValue ? (bool?)(src.IsLocked.Value == 1) : null));

            CreateMap<UserDto, User>()
                .ForMember(src => src.IsLocked, opt =>
                    opt.MapFrom(dto => dto.IsLocked.HasValue ? (int?)(dto.IsLocked.Value ? 1 : 0) : null));
        }
    }
}
