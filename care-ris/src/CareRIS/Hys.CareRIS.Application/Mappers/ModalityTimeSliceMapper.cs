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
    public class ModalityTimeSliceMapper : Profile
    {
        public ModalityTimeSliceMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ModalityTimeSlice, ModalityTimeSliceDto>()
                .ForMember(s => s.UniqueID, d => d.MapFrom(s => s.TimeSliceGuid))
                .ForMember(s => s.TotalPrivateQuota, d => d.Ignore())
                .ForMember(s => s.TotalSharedQuota, d => d.Ignore())
                .ForMember(s => s.TotalUsedQuota, d => d.Ignore())
                .ForMember(s => s.TotalAvailableQuota, d => d.Ignore())
                .ForMember(s => s.IsShared, d => d.Ignore());
            CreateMap<ModalityTimeSliceDto, ModalityTimeSlice>()
                .ForMember(s => s.TimeSliceGuid, d => d.MapFrom(s => s.UniqueID));
        }
    }
}
