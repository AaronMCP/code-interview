using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class GWReportRepository : Repository<GWReport>, IGWReportRepository
    {
        public GWReportRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
