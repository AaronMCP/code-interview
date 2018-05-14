using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Login;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class Role : MarshalByRefObject
    {
        public Role() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DsRole Load()
        {
            return DaoInstanceFactory.GetInstance().LoadAllRole();
        }

        public DsRole Load4Login()
        {
            return DaoInstanceFactory.GetInstance().LoadAllRole4Login();
        }

    }
}
