using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO;
using Server.DAO.ClientFramework;
using System.Data;

namespace Server.Business.ClientFramework
{
    public class SystemProfile : MarshalByRefObject
    {
        public SystemProfile() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID">ID of module for load</param>
        /// <returns></returns>
        public DsProfile Load(string ModuleID)
        {
            //Server.DAO.ClientFramework.SystemProfile objDAO;

            //Need a factory here:
            //objDAO = new Server.DAO.ClientFramework.SystemProfileSql();

            //return objDAO.Load(ModuleID);
            return DaoInstanceFactory.GetInstance().LoadSystemProfile(ModuleID);
        }
    }

    public class AllProfile : MarshalByRefObject
    {
        public AllProfile() { }

        public DataSet LoadAllProfile()
        {
            return DaoInstanceFactory.GetInstance().LoadAllProfile();
        }
    }

}
