using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RequisitionRepository : Repository<Requisition>, IRequisitionRepository
    {
        public RequisitionRepository(IRisProContext context)
            : base(context) 
        { 

        }
    }
}
