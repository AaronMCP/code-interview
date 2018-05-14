using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class GWOrderRepository : Repository<GWOrder>, IGWOrderRepository
    {
        public GWOrderRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
