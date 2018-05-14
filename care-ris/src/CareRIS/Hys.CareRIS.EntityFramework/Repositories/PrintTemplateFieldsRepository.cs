using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class PrintTemplateFieldsRepository : Repository<PrintTemplateFields>, IPrintTemplateFieldsRepository
    {
        public PrintTemplateFieldsRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
