using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportDelPoolRepository : Repository<ReportDelPool>, IReportDelPoolRepository
    {
        public ReportDelPoolRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
