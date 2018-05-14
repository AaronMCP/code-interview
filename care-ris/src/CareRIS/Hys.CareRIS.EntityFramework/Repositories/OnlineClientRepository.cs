using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using System;
using System.Linq;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class OnlineClientRepository : Repository<OnlineClient>, IOnlineClientRepository
    {
        public OnlineClientRepository(IRisProContext context)
            : base(context)
        {
        }

    }
}
