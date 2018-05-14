using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO.ClientFramework;
using System.Data;

namespace Server.Business.ClientFramework
{
    public class SiteProfile : MarshalByRefObject
    {
        public SiteProfile() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID">ID of module for load</param>
        /// <returns></returns>
        public DsProfile Load(string ModuleID, string SiteName)
        {
            return DaoInstanceFactory.GetInstance().LoadSiteProfile(ModuleID, SiteName);
        }

        public DataSet Load(string ModuleID)
        {
            return DaoInstanceFactory.GetInstance().LoadAllSiteProfile(ModuleID);
        }
    }
}
