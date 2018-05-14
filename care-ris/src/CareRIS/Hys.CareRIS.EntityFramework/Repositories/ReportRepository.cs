using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
