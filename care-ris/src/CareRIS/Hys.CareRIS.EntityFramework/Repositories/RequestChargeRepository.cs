using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RequestChargeRepository : Repository<RequestCharge>, IRequestChargeRepository
    {
        public RequestChargeRepository(IRisProContext context)
            : base(context) 
        { 

        }
    }
}
