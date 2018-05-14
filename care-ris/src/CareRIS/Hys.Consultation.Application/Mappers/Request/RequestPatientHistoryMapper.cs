using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class RequestPatientMapper : Profile
    {
        public RequestPatientMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientCase, PatientHistoryDto>()
               .ForMember(dto => dto.PatientCaseID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                   .ForMember(dto => dto.LastEditTime, opt =>
                   opt.MapFrom(src => src.LastEditTime))
                   .ForMember(dto => dto.LastEditUser, opt =>
                   opt.MapFrom(src => src.LastEditUser));

            CreateMap<PatientHistoryDto, PatientCase>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.PatientCaseID));
        }
    }
}
