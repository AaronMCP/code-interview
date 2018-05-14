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
    public class PersonMapper : Profile
    {
        public PersonMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dto => dto.UniqueID, opt =>
                  opt.MapFrom(src => src.UniqueID));
            CreateMap<PersonDto, Person>()
             .ForMember(src => src.UniqueID, opt =>
                 opt.MapFrom(dto => dto.UniqueID));
        }
    }

}