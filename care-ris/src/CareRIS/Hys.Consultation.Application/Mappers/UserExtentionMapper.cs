using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class UserExtentionMapper : Profile
    {
        public UserExtentionMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<UserExtention, UserDto>();
            CreateMap<UserDto, UserExtention>()
             .ForMember(dto => dto.Roles, e => e.Ignore());
        }
    }
}
