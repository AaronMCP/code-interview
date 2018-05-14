using AutoMapper;
using Hys.CareRIS.Application.Dtos.Referral;
using Hys.CareRIS.Domain.Entities.Referral;

namespace Hys.CareRIS.Application.Mappers.Referral
{
    public class ReferralListMapper : Profile
    {
        public ReferralListMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<ReferralList, ReferralListDto>()
                .ForMember(s => s.ReferralID, d => d.Ignore())
                .ForMember(s => s.CurrentAge, d => d.Ignore())
                .ForMember(s => s.ProcedureID, d => d.Ignore())
                .ForMember(s => s.SiteName, d => d.Ignore());

            CreateMap<ReferralListDto, ReferralList>();
        }
    }
}
