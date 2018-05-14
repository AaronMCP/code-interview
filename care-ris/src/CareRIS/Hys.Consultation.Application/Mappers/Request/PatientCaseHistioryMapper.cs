using AutoMapper;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Mappers
{
    public class PatientCaseHistioryMapper : Profile
    {
        public PatientCaseHistioryMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<PatientCase, PatientCaseHistory>()
                 .ForMember(dto => dto.PatientCaseID, opt =>
                    opt.MapFrom(src => src.UniqueID));
            CreateMap<PatientCaseHistory, PatientCase>()
                        .ForMember(src => src.UniqueID, opt =>
                    opt.MapFrom(dto => dto.PatientCaseID));
        }
    }
}
