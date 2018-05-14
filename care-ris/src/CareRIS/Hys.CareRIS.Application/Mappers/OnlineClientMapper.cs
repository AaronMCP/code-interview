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
    public class OnlineClientMapper : Profile
    {
        public OnlineClientMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<OnlineClient, OnlineClientDto>()
                .ForMember(dto => dto.IsOnline, opt =>
                    opt.MapFrom(src => src.IsOnline.HasValue ? (bool?)(src.IsOnline.Value == 1) : null));

            CreateMap<OnlineClientDto, OnlineClient>()
                .ForMember(src => src.IsOnline, opt =>
                    opt.MapFrom(dto => dto.IsOnline.HasValue ? (int?)(dto.IsOnline.Value ? 1 : 0) : null));
        }
    }
}
