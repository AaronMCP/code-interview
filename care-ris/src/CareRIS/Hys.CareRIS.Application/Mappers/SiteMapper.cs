
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
namespace Hys.CareRIS.Application.Mappers
{
    public class SiteMapper : Profile
    {
        public SiteMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<Site, SiteDto>();

            CreateMap<SiteDto, Site>();
        }
    }
}
