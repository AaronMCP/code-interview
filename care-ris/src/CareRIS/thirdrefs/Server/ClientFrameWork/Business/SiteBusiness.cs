using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class SiteBusiness : MarshalByRefObject
    {
        public SiteBusiness() { }

        public System.Data.DataSet GetFilterSite(string strUserGuid, string strRoleName, string strCurSite,string strMatchingName)
        {
            return DaoInstanceFactory.GetInstance().GetFilterSite(strUserGuid, strRoleName, strCurSite, strMatchingName);
        }

        public string GetSite(string settingSite)
        {
            return DaoInstanceFactory.GetInstance().GetSite(settingSite);
        }
      
    }
}
