using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Mappers
{
    public class ApplyDeptMapper : Profile
    {
        public ApplyDeptMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ApplyDept, ApplyDeptDto>()
               .ForMember(dto => dto.FirstPingYinName, opt => opt.Ignore());
            CreateMap<ApplyDeptDto, ApplyDept>()
             .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.UniqueID.ToString()));
        }
    }
}
