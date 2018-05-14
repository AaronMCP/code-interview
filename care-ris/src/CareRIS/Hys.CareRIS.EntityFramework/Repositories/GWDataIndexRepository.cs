using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class GWDataIndexRepository : Repository<GWDataIndex>, IGWDataIndexRepository
    {
        public GWDataIndexRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
