using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IRisProContext context)
            : base(context)
        {

        }

        public Role GetRoles(string roleName)
        {
            return this.Get(r => r.RoleName == roleName).FirstOrDefault();
        }
    }
}
