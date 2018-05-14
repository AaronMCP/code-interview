using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class PatientListMapper : Profile
    {
        public PatientListMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientList, PatientListDto>()
                .ForMember(s => s.MatchKey, d => d.Ignore())
                .ForMember(s => s.HisID, d => d.Ignore())
                .ForMember(dto => dto.IsVip, opt =>
                    opt.MapFrom(src => src.IsVIP.HasValue ? (bool?)(src.IsVIP.Value == 1) : null))
                .ForMember(dto => dto.IsUploaded, opt =>
                    opt.MapFrom(src => src.IsUploaded.HasValue ? (bool?)(src.IsUploaded.Value == 1) : null));

            CreateMap<PatientListDto, PatientList>()
                .ForMember(s => s.RelatedID, d => d.Ignore())
                .ForMember(src => src.IsVIP, opt =>
                    opt.MapFrom(dto => dto.IsVip.HasValue ? (int?)(dto.IsVip.Value ? 1 : 0) : null))
                .ForMember(src => src.IsUploaded, opt =>
                    opt.MapFrom(dto => dto.IsUploaded.HasValue ? (int?)(dto.IsUploaded.Value ? 1 : 0) : null));
        }
    }
}
