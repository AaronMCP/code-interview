using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;
using System.Collections.Generic;

namespace Hys.CareRIS.Domain.Interface
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetRoles(string roleName);
    }
}
