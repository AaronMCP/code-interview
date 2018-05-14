using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class ScoringResultMapper : Profile
    {
        public ScoringResultMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ScoringResult, ScoringResultDto>()
                   .ForMember(dto => dto.UserName, opt => opt.Ignore());


            CreateMap<ScoringResultDto, ScoringResult>();
               
        }
    }
}
