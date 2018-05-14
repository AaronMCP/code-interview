using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class RequestChargeMapper : Profile 
    {
        public RequestChargeMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<RequestCharge, RequestChargeDto>()
                .ForMember(dto => dto.IsItemCharged, opt =>
                    opt.MapFrom(src => src.IsItemCharged.HasValue ? (bool?)(src.IsItemCharged.Value == 1) : null));


            CreateMap<RequestChargeDto, RequestCharge>()
                .ForMember(src => src.IsItemCharged, opt =>
                    opt.MapFrom(dto => dto.IsItemCharged.HasValue ? (int?)(dto.IsItemCharged.Value ? 1 : 0) : null));
        }
    }
}
