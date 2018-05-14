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
    public class ModalityShareMapper : Profile
    {
        public ModalityShareMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ModalityShareDto, ModalityShare>()
                .ForMember(dto => dto.TimeSlice, opt => opt.MapFrom(src => new ModalityTimeSlice
                {
                    ModalityType = src.ModalityType,
                    Modality = src.Modality,
                    StartDt = src.StartDt,
                    EndDt = src.EndDt,
                    Description = src.Description,
                    MaxNumber = src.MaxNumber,
                    DateType = src.DateType,
                    AvailableDate = src.AvailableDate
                }))
                .ForMember(dto => dto.Guid, opt => opt.MapFrom(src => src.UniqueID));

            CreateMap<ModalityShare, ModalityShareDto>()
                .ForMember(dto => dto.UniqueID, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dto => dto.ModalityType, opt => opt.MapFrom(src => src.TimeSlice.ModalityType))
                .ForMember(dto => dto.Modality, opt => opt.MapFrom(src => src.TimeSlice.Modality))
                .ForMember(dto => dto.StartDt, opt => opt.MapFrom(src => src.TimeSlice.StartDt))
                .ForMember(dto => dto.EndDt, opt => opt.MapFrom(src => src.TimeSlice.EndDt))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.TimeSlice.Description))
                .ForMember(dto => dto.MaxNumber, opt => opt.MapFrom(src => src.TimeSlice.MaxNumber))
                .ForMember(dto => dto.Domain, opt => opt.MapFrom(src => src.TimeSlice.Domain))
                .ForMember(dto => dto.DateType, opt => opt.MapFrom(src => src.TimeSlice.DateType))
                .ForMember(dto => dto.AvailableDate, opt => opt.MapFrom(src => src.TimeSlice.AvailableDate));
        }
    }
}
