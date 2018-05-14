using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
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
               .ForMember(dto => dto.IsLocked, opt =>
                   opt.MapFrom(src => src.IsLocked.HasValue ? (bool?)(src.IsLocked.Value == 1) : null));

            CreateMap<UserDto, User>()
                .ForMember(src => src.IsLocked, opt =>
                    opt.MapFrom(dto => dto.IsLocked.HasValue ? (int?)(dto.IsLocked.Value ? 1 : 0) : null));
        }
    }
}
