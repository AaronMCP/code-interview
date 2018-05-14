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
    public class ConsultationRequestMapper : Profile
    {
        public ConsultationRequestMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationRequest, ConsultationRequestFlatDto>();
            CreateMap<ConsultationRequestFlatDto, ConsultationRequest>();
        }
    }

}