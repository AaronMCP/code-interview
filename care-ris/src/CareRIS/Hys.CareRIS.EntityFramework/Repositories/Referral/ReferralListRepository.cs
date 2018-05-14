using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities.Referral;
using Hys.CareRIS.Domain.Interface.Referral;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReferralListRepository : Repository<ReferralList>, IReferralListRepository
    {
        public ReferralListRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
