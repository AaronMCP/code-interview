using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class Login : MarshalByRefObject
    {
        public Login() { }

        public string GetDbServerTime()
        {
            return DaoInstanceFactory.GetInstance().GetDbServerTime();
        }
        public string GetLocalName(string UserGuid)
        {
            return DaoInstanceFactory.GetInstance().GetLocalName(UserGuid);
        }

        /// <summary>
        /// Get no. of all on-line user
        /// </summary>
        /// <param name="bWebUser">
        /// If true, gets all user from web clinic client
        /// If false, gets all user from smart client
        /// </param>
        /// <returns></returns>
        public int GetOnlineUserNo(bool bWebUser, bool bSelfUser, string ipaddress)
        {
            return DaoInstanceFactory.GetInstance().GetOnlineUserNo(bWebUser, bSelfUser, ipaddress);
        }

        public void OnlineStatusInit()
        {
            DaoInstanceFactory.GetInstance().OnlineStatusInit();
        }

        public void LogOutBySessionID(string SessionID)
        {
            DaoInstanceFactory.GetInstance().LogOutBySessionID(SessionID);
        }

        public void LogOut(string UserGuid, bool bWebUser)
        {
            DaoInstanceFactory.GetInstance().LogOut(UserGuid, bWebUser);
        }

        public int IsOnLine(string szUserGuid, string szRoleName, string szIpAddress, string szUrl, string szSessionID, bool IsLogined, bool bWebUser, bool IsHijackLogin, bool IsSelfService)
        {

            return DaoInstanceFactory.GetInstance().IsOnLine(szUserGuid, szRoleName, szIpAddress, szUrl, szSessionID, IsLogined, bWebUser, IsHijackLogin, IsSelfService);
        }

        public string GetUserGuidDbAuthentication(string LoginName, string Password, string RoleName)
        {

            return DaoInstanceFactory.GetInstance().GetUserGuid(LoginName, Password, RoleName);
        }

        public string GetUserGuidLadpAuthentication(string LoginName, string Password, string RoleName)
        {
            string rt = string.Empty;

            string adPath = System.Configuration.ConfigurationManager.AppSettings["DomainPath"];
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            string DommainName, UserName;
            DommainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"];
            UserName = LoginName;
            if (adAuth.IsAuthenticated(DommainName, UserName, Password))
            {
                rt = DaoInstanceFactory.GetInstance().GetUserGuidByDmnLgnName(LoginName, RoleName);
            }
            return rt;
        }
        public KeyValuePair<int, int> ExpireDayCheck(string LoginName)
        {
            return DaoInstanceFactory.GetInstance().ExpireDayCheck(LoginName);
        }
        public KeyValuePair<int, int> GetExpireDays(string LoginName)
        {
            return DaoInstanceFactory.GetInstance().GetExpireDays(LoginName);
        }

        public string GetUeserGuidByLgnName(string loginName, string roleName)
        {
            return DaoInstanceFactory.GetInstance().GetUserGuidByLoginName(loginName, roleName);
        }

        public string GetRoleName(string UserGuid)
        {
            return DaoInstanceFactory.GetInstance().GetRoleName(UserGuid);
        }


        public System.Data.DataSet GetOnlineClients()
        {
            return DaoInstanceFactory.GetInstance().GetOnlineClients();
        }

        public void UpdatePasswordNewEncry(string userguid, string password)
        {
            DaoInstanceFactory.GetInstance().UpdatePasswordNewEncry(userguid, password);
        }
    }
}
