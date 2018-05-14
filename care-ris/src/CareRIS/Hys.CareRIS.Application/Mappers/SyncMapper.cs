using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class SyncMapper : Profile
    {
        public SyncMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Sync, SyncDto>()
            .ForMember(dto => dto.OwnerName, opt => opt.Ignore())
            .ForMember(dto => dto.LoginName, opt => opt.Ignore())
            .ForMember(dto => dto.ModuleTitle, opt => opt.Ignore());

            CreateMap<SyncDto, Sync>();
        }
    }
}
