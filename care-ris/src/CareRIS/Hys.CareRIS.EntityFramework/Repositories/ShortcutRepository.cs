using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ShortcutRepository : Repository<Shortcut>, IShortcutRepository
    {
        public ShortcutRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
