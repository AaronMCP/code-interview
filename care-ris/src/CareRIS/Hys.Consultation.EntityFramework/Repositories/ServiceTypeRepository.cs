using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Platform.Data.EntityFramework;

namespace Hys.Consultation.EntityFramework.Repositories
{
    class ServiceTypeRepository: Repository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(IConsultationContext context)
            :base(context)
        {

        }
    }
}
