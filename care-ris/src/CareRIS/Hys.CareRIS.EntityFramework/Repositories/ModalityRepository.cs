using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ModalityRepository : Repository<Modality>, IModalityRepository
    {
        public ModalityRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
