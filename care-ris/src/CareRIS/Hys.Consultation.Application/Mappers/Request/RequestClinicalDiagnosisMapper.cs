using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class RequestClinicalDiagnosisMapper : Profile
    {
        public RequestClinicalDiagnosisMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientCase, ClinicalDiagnosisDto>()
               .ForMember(dto => dto.PatientCaseID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                   .ForMember(dto => dto.LastEditTime, opt =>
                   opt.MapFrom(src => src.LastEditTime))
                   .ForMember(dto => dto.LastEditUser, opt =>
                   opt.MapFrom(src => src.LastEditUser));

            CreateMap<ClinicalDiagnosisDto, PatientCase>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.PatientCaseID));
        }
    }
}
