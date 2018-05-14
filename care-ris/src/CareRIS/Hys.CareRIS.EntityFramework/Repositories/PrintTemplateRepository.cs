using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class PrintTemplateRepository : Repository<PrintTemplate>, IPrintTemplateRepository
    {
        public PrintTemplateRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
