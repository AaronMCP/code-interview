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
    public class ApplyDoctorMapper : Profile
    {
        public ApplyDoctorMapper()
        {
            Configure();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ApplyDoctor, ApplyDoctorDto>()
               .ForMember(dto => dto.DeptName, opt => opt.Ignore());
            CreateMap<ApplyDoctorDto, ApplyDoctor>()
             .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.UniqueID.ToString()));
        }
    }
}
