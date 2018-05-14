using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportListRepository : Repository<ReportList>, IReportListRepository
    {
        public ReportListRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
