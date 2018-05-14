using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RequestItemRepository : Repository<RequestItem>, IRequestItemRepository
    {
        public RequestItemRepository(IRisProContext context)
            : base(context) 
        { 

        }
    }
}
