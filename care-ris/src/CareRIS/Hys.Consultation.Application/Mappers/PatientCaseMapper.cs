using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Mappers
{
    public class PatientCaseMapper : Profile
    {
        public PatientCaseMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientCaseDto, PatientCase>()
                .ForMember(p => p.Telephone, dto => dto.Ignore())
                .ForMember(p => p.Progress, dto => dto.Ignore())
                .ForMember(p => p.PatientNamePy, dto => dto.Ignore())
                .ForMember(p => p.LastEditUser, dto => dto.Ignore())
                .ForMember(p => p.InsuranceNumber, dto => dto.Ignore())
                .ForMember(p => p.IdentityCard, dto => dto.Ignore())
                .ForMember(p => p.HospitalId, dto => dto.Ignore())
                .ForMember(p => p.History, dto => dto.Ignore())
                .ForMember(p => p.ClinicalDiagnosis, dto => dto.Ignore())
                .ForMember(p => p.Birthday, dto => dto.Ignore());

            CreateMap<PatientCase, PatientCaseDto>()
                .ForMember(dto => dto.PatientBaseInfo, p => p.Ignore())
                .ForMember(dto => dto.LastUpdateTime, p => p.Ignore());
        }
    }

}