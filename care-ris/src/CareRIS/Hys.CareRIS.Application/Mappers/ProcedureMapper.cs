using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class ProcedureMapper : Profile
    {
        public ProcedureMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Procedure, ProcedureDto>()
                  .ForMember(dto => dto.IsCharge, opt =>
                  opt.MapFrom(src => src.IsCharge.HasValue ? (bool?)(src.IsCharge.Value == 1) : null))
                  .ForMember(dto => dto.IsExistImage, opt =>
                  opt.MapFrom(src => src.IsExistImage.HasValue ? (bool?)(src.IsExistImage.Value == 1) : null))
                  .ForMember(dto => dto.BookingNotice, opt =>
                  opt.MapFrom(src =>src.BookingNotice!=null ?System.Text.Encoding.UTF8.GetString(src.BookingNotice):null));

            CreateMap<ProcedureDto, Procedure>()
              .ForMember(src => src.IsExistImage, opt =>
               opt.MapFrom(dto => dto.IsExistImage.HasValue ? (int?)(dto.IsExistImage.Value ? 1 : 0) : null))
              .ForMember(src => src.IsCharge, opt =>
               opt.MapFrom(dto => dto.IsCharge.HasValue ? (int?)(dto.IsCharge.Value ? 1 : 0) : null))
               .ForMember(src => src.BookingNotice, opt =>
               opt.MapFrom(dto => !string.IsNullOrEmpty(dto.BookingNotice) ? System.Text.Encoding.UTF8.GetBytes(dto.BookingNotice):null));
        }
    }
}
