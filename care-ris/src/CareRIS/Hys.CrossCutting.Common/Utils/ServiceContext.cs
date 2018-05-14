using System;
using System.Web;

namespace Hys.CrossCutting.Common.Utils
{
    public class ServiceContext
    {
        public ServiceContext(string userName, string userID, string domain, string site, string role, string localName, bool isPublicAccount, string language)
        {
            UserName = userName;
            UserID = userID;
            Domain = domain;
            Site = site;
            Role = role;
            LocalName = localName;
            IsPublicAccount = isPublicAccount;
            Language = language;
        }

        public string UserName { get; private set; }
        public string UserID { get; private set; }
        public string Domain { get; private set; }
        public string Site { get; private set; }
        public string Role { get; private set; }
        public string LocalName { get; private set; }
        public bool IsPublicAccount { get; private set; }
        public string Language { get; set; }

    }
}
