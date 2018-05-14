using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class RoleToUserRepository : Repository<RoleToUser>, IRoleToUserRepository
    {
        public RoleToUserRepository(IRisProContext context)
            : base(context)
        {

        }

        public IEnumerable<RoleToUser> GetUserRoles(string userId, string domain)
        {
            return this.Get(u => u.UserID == userId && u.Domain == domain).ToList();
        }
    }
}
