using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO.ClientFramework;
using System.Data;

namespace Server.Business.ClientFramework
{
    public class BillBoard : MarshalByRefObject
    {
        public DataSet GetAllNotesInDB(string userguid, string roleName)
        {
            return DaoInstanceFactory.GetInstance().GetAllNotesInDB(userguid, roleName);
        }
        public DataSet GetBillBoardDictionaryData()
        {
            return DaoInstanceFactory.GetInstance().GetBillBoardDictionaryData();
        }
    }
}
