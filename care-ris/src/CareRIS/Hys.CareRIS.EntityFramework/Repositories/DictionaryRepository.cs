using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
