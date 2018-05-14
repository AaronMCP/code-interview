using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ReportTemplateDirecRepository : Repository<ReportTemplateDirec>, IReportTemplateDirecRepository
    {
        public ReportTemplateDirecRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
