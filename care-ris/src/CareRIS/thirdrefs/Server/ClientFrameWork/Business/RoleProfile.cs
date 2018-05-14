using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class RoleProfile : MarshalByRefObject
    {
        public RoleProfile() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID">ID of module for load</param>
        /// <param name="RoleID">GUID of role for load</param>
        /// <returns></returns>
        public DsProfile Load(string ModuleID, string RoleID)
        {
            //Server.DAO.ClientFramework.RoleProfile objDAO;

            ////Need a factory here:
            //objDAO = new Server.DAO.ClientFramework.RoleProfileSql();

            //return objDAO.Load(ModuleID, RoleID);
            return DaoInstanceFactory.GetInstance().LoadRoleProfile(ModuleID, RoleID);
        }

        /// <summary>
        /// Get roleProfile accoding to the specified profile name and roleID.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string GetRoleProfileValue(string name, string roleId)
        {
            return DaoInstanceFactory.GetInstance().GetRoleProfileValue(name, roleId);
        }
    }


}
