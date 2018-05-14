using Hys.Platform.Data.EntityFramework;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using System;
using System.Linq;

namespace Hys.CareRIS.EntityFramework.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IRisProContext context)
            : base(context)
        {
        }

        public void UpdatePassword(string userId, string password)
        {
            var user = Get(p => p.UniqueID.Equals(userId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                user.Password = password;
                SaveChanges();
            }
        }

        public User2Domain GetUserExpiration(string userId)
        {
            var result = DbContext.Set<User2Domain>().Where(x => x.UniqueID.Equals(userId, StringComparison.OrdinalIgnoreCase)).Take(1).FirstOrDefault();
            return result;
        }
    }
}
