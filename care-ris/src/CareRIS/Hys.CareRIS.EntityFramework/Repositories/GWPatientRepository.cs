using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class GWPatientRepository : Repository<GWPatient>, IGWPatientRepository
    {
        public GWPatientRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
