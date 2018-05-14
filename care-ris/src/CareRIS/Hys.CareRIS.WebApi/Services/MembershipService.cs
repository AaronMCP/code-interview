using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Hys.CareRIS.WebApi.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(string username, string password);
    }

    /// <summary>
    /// Provide the functions for service authentication
    /// </summary>
    public class MembershipService : IMembershipService
    {
        private MembershipProvider _MembershipProvider;

        public MembershipService(MembershipProvider membershipProvider)
        {
            _MembershipProvider = membershipProvider;
        }

        public bool ValidateUser(string username, string password)
        {
            return _MembershipProvider.ValidateUser(username, password);
        }
    }
}