using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RequestListRepository : Repository<RequestList>, IRequestListRepository
    {
        public RequestListRepository(IRisProContext context)
            : base(context) 
        { 

        }
    }
}
