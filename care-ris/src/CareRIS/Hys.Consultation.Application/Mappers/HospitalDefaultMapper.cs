using AutoMapper;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class HospitalDefaultMapper : Profile
    {
        public HospitalDefaultMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<HospitalDefaultDto, HospitalDefault>();
            CreateMap<HospitalDefault, HospitalDefaultDto>();
        }
    }
}
