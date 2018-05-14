using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ProcedureRepository : Repository<Procedure>, IProcedureRepository
    {
        public ProcedureRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
