using System;
using System.Linq;
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Mappers
{
    public class ProcedureCodeMapper : Profile
    {
        public ProcedureCodeMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Procedurecode, ProcedureCodeDto>()
                 .ForMember(dto => dto.ExamSystem, opt => opt.Ignore());

            CreateMap<ProcedureCodeDto, Procedurecode>()
              .ForMember(src => src.ApproveWarningTime, opt => opt.Ignore());
        }
    }
}
