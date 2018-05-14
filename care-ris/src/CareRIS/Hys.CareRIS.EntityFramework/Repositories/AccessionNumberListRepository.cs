using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class AccessionNumberListRepository : Repository<AccessionNumberList>, IAccessionNumberListRepository
    {
        public AccessionNumberListRepository(IRisProContext context)
            :base(context)
        {

        }
    }
}
