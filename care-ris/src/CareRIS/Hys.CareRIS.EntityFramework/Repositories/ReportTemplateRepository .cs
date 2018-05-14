using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportTemplateRepository : Repository<ReportTemplate>, IReportTemplateRepository
    {
        public ReportTemplateRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
