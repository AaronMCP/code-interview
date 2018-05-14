using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class BodySystemMapRepository : Repository<BodySystemMap>, IBodySystemMapRepository
    {
        public BodySystemMapRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
