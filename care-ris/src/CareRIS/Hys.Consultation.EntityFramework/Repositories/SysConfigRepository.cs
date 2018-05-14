using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Platform.Data.EntityFramework;

namespace Hys.Consultation.EntityFramework.Repositories
{
    public class SysConfigRepository : Repository<SysConfig>, ISysConfigRepository
    {
        public SysConfigRepository(IConsultationContext context)
            :base(context)
        {

        }
    }
}
