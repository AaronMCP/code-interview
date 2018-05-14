using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class DictionaryValueRepository : Repository<DictionaryValue>, IDictionaryValueRepository
    {
        public DictionaryValueRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
