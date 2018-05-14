using AutoMapper;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Mappers
{
    public class EMRItemSuperMapper : Profile
    {
        public EMRItemSuperMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            //CreateMap<EMRItemSuperDto, EMRItem>().ForMember("ItemDetails", sd => sd.Ignore());
            CreateMap<EMRItemSuperDto, EMRItem>().ForSourceMember(s => s.ItemDetails, sd => sd.Ignore());
            CreateMap<EMRItem, EMRItemSuperDto>();
        }
    }
}
