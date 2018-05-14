
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
namespace Hys.CareRIS.Application.Mappers
{
    public class DomainListMapper : Profile
    {
        public DomainListMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<DomainList, DomainListDto>();

            CreateMap<DomainListDto, DomainList>();
        }
    }
}
