using AutoMapper;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Application.Dtos;

namespace Hys.Consultation.Application.Mappers
{
    public class ConsultationDictionaryMapper : Profile
    {
        public ConsultationDictionaryMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ConsultationDictionary, ConsultationDictionaryDto>();
            CreateMap<ConsultationDictionaryDto, ConsultationDictionary>()
                .ForMember(c => c.LastEditTime, dto => dto.Ignore())
                .ForMember(c => c.LastEditUser, dto => dto.Ignore())
                .ForMember(c => c.Description, dto => dto.Ignore());
        }
    }

}