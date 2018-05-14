using System;
using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class RequestPatientHistoryMapper : Profile
    {
        public RequestPatientHistoryMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientCase, PatientBaseInfoDto>()
               .ForMember(dto => dto.PatientCaseID, opt =>
                   opt.MapFrom(src => src.UniqueID))
                    .ForMember(dto => dto.Age, opt =>
                   opt.MapFrom(src => src.Age))
                      .ForMember(dto => dto.Birthday, opt =>
                   opt.MapFrom(src => src.Birthday))
                    .ForMember(dto => dto.Gender, opt =>
                   opt.MapFrom(src => src.Gender))
                   .ForMember(dto => dto.IdentityCard, opt =>
                   opt.MapFrom(src => src.IdentityCard))
                   .ForMember(dto => dto.InsuranceNumber, opt =>
                   opt.MapFrom(src => src.InsuranceNumber))
                   .ForMember(dto => dto.PatientName, opt =>
                   opt.MapFrom(src => src.PatientName))
                   .ForMember(dto => dto.LastEditTime, opt =>
                   opt.MapFrom(src => src.LastEditTime))
                   .ForMember(dto => dto.LastEditUser, opt =>
                   opt.MapFrom(src => src.LastEditUser))
                   .ForMember(dto => dto.Telephone, opt =>
                   opt.MapFrom(src => src.Telephone));

            CreateMap<PatientBaseInfoDto, PatientCase>()
                .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.PatientCaseID))
                    .ForMember(src => src.Age, opt =>
                    opt.MapFrom(dto => dto.Age));
        }
    }
}
