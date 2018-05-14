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
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Role, RoleDto>()
                  .ForMember(dto => dto.RoleProfileList, opt => opt.Ignore())
                  .ForMember(dto => dto.IsCopy, opt => opt.Ignore())
                  .ForMember(dto => dto.CopyRoleName, opt => opt.Ignore())
                  .ForMember(dto => dto.Site, opt => opt.Ignore())
                   .ForMember(dto => dto.ParentID, opt => opt.Ignore())
                .ForMember(dto => dto.IsSystem, opt =>
                    opt.MapFrom(src => src.IsSystem.HasValue ? (bool?)(src.IsSystem.Value == 1) : null));

            CreateMap<RoleDto, Role>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.UniqueID.ToString()))
                .ForMember(src => src.IsSystem, opt =>
                    opt.MapFrom(dto => dto.IsSystem.HasValue ? (int?)(dto.IsSystem.Value ? 1 : 0) : null));
        }
    }
}
