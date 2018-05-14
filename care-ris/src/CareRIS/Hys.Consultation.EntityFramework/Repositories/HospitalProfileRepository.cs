using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Platform.Data.EntityFramework;

namespace Hys.Consultation.EntityFramework.Repositories
{
    public class HospitalProfileRepository : Repository<HospitalProfile>, IHospitalProfileRepository
    {
        public HospitalProfileRepository(IConsultationContext context)
            :base(context)
        {

        }
    }
}
