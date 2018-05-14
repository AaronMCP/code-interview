using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Mappers
{
    public class RoleDirMapper : Profile
    {
        public RoleDirMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<RoleDir, RoleDirDto>()
                 .ForMember(dto => dto.RoleName, opt => opt.Ignore())
                  .ForMember(dto => dto.Description, opt => opt.Ignore())
                   .ForMember(dto => dto.IsSystem, opt => opt.Ignore());

            CreateMap<RoleDirDto, RoleDir>();
        }
    }
}
