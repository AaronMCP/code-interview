using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class SyncRepository : Repository<Sync>, ISyncRepository
    {
        public SyncRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
