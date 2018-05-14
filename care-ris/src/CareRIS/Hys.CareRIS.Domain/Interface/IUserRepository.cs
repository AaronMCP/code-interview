using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        void UpdatePassword(string userId, string password);
        User2Domain GetUserExpiration(string userId);
    }
}
