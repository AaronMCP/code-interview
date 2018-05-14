using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Platform.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Repositories
{
    public class ShortcutRepository : Repository<Shortcut>, IShortcutRepository
    {
        public ShortcutRepository(IConsultationContext context)
            : base(context)
        {

        }
    }
}
