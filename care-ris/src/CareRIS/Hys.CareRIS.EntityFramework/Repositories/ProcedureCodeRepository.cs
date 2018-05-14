using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class ProcedureCodeRepository: Repository<Procedurecode>, IProcedureCodeRepository
    {
        public ProcedureCodeRepository(IRisProContext context)
            : base(context)
        {

        }
    }
}
