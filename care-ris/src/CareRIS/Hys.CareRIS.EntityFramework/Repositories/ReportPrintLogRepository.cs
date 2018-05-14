using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportPrintLogRepository: Repository<ReportPrintLog>, IReportPrintLogRepository
    {
        public ReportPrintLogRepository(IRisProContext context)
            :base(context)
        {

        }
    }
}
