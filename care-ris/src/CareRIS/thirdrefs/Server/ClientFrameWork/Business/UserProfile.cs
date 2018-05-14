using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO;
using System.Data;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class UserProfile : MarshalByRefObject
    {
        public UserProfile() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DsProfile Load(string ModuleID, string RoleID, String UserID)
        {

            return DaoInstanceFactory.GetInstance().LoadUserProfile(ModuleID, RoleID, UserID);
        }
        /// <summary>
        /// Save data into storage/database
        /// </summary>
        /// <param name="Ds">Detail infomation of updated data</param>
        /// <param name="RoleName"></param>
        /// <param name="UserGUID"></param>
        /// <returns></returns>
        public int Save(DataSet Ds, string RoleName, string UserGUID)
        {
            return DaoInstanceFactory.GetInstance().SaveUserProfile(Ds, RoleName, UserGUID);
        }

    }


}
