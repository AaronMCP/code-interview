using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;
using System.Collections.Generic;

namespace Hys.CareRIS.Domain.Interface
{
    public interface IRoleToUserRepository : IRepository<RoleToUser>
    {
        IEnumerable<RoleToUser> GetUserRoles(string userId, string domain);
    }
}
