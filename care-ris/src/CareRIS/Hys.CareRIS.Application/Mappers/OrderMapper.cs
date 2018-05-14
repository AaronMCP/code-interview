using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.IsScan, opt =>
                    opt.MapFrom(src => src.IsScan.HasValue ? (bool?)(src.IsScan.Value == 1) : null))
                .ForMember(dto => dto.IsCharge, opt =>
                    opt.MapFrom(src => src.IsCharge.HasValue ? (bool?)(src.IsCharge.Value == 1) : null))
                .ForMember(dto => dto.IsBedside, opt =>
                    opt.MapFrom(src => src.IsBedside.HasValue ? (bool?)(src.IsBedside.Value == 1) : null))
                .ForMember(dto => dto.IsThreeDRebuild, opt =>
                    opt.MapFrom(src => src.IsThreeDRebuild.HasValue ? (bool?)(src.IsThreeDRebuild.Value == 1) : null))
                .ForMember(dto => dto.IsEmergency, opt =>
                    opt.MapFrom(src => src.IsEmergency.HasValue ? (bool?)(src.IsEmergency.Value == 1) : null))
                .ForMember(dto => dto.IsFilmSent, opt =>
                    opt.MapFrom(src => src.IsFilmSent.HasValue ? (bool?)(src.IsFilmSent.Value == 1) : null))
                     .ForMember(dto => dto.IsReferral, opt =>
                    opt.MapFrom(src => src.IsReferral.HasValue ? (bool?)(src.IsReferral.Value == 1) : null));

            CreateMap<OrderDto, Order>()
                .ForMember(src => src.IsScan, opt =>
                    opt.MapFrom(dto => dto.IsScan.HasValue ? (int?)(dto.IsScan.Value ? 1 : 0) : null))
                .ForMember(src => src.IsCharge, opt =>
                    opt.MapFrom(dto => dto.IsCharge.HasValue ? (int?)(dto.IsCharge.Value ? 1 : 0) : null))
                .ForMember(src => src.IsBedside, opt =>
                    opt.MapFrom(dto => dto.IsBedside.HasValue ? (int?)(dto.IsBedside.Value ? 1 : 0) : null))
                 .ForMember(src => src.IsThreeDRebuild, opt =>
                    opt.MapFrom(dto => dto.IsThreeDRebuild.HasValue ? (int?)(dto.IsThreeDRebuild.Value ? 1 : 0) : null))
                .ForMember(src => src.IsEmergency, opt =>
                    opt.MapFrom(dto => dto.IsEmergency.HasValue ? (int?)(dto.IsEmergency.Value ? 1 : 0) : null))
                .ForMember(src => src.IsFilmSent, opt =>
                    opt.MapFrom(dto => dto.IsFilmSent.HasValue ? (int?)(dto.IsFilmSent.Value ? 1 : 0) : null))
                .ForMember(src => src.IsReferral, opt =>
                    opt.MapFrom(dto => dto.IsReferral.HasValue ? (int?)(dto.IsReferral.Value ? 1 : 0) : null));
        }
    }
}
