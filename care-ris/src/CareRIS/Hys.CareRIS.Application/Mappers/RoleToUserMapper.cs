using AutoMapper;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Mappers
{
    public class RoleToUserMapper : Profile
    {
        public RoleToUserMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<RoleToUser, RoleToUserDto>();

            CreateMap<RoleToUserDto, RoleToUser>()
                   .ForMember(src => src.UniqueId, opt => opt.Ignore());
        }
    }
}
