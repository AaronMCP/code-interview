using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Platform.Data.EntityFramework;

namespace Hys.Consultation.EntityFramework.Repositories
{
    public class EMRItemDetailRepository : Repository<EMRItemDetail>, IEMRItemDetailRepository
    {
        public EMRItemDetailRepository(IConsultationContext context)
            :base(context)
        {

        }
    }
}
