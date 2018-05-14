using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class LoadImageBusiness : MarshalByRefObject
    {
        public LoadImageBusiness() { }

        public System.Data.DataSet GetExamInfo( string strExamDomain,string strAccNo)
        {

            return DaoInstanceFactory.GetInstance().GetExamInfo(strExamDomain, strAccNo);
        }
    }
}
