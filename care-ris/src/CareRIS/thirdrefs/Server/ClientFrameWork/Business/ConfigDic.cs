
using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class ConfigDic : MarshalByRefObject
    {
        public ConfigDic() { }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <returns></returns>
        public DsConfigDic Load(int Type)
        {
            return DaoInstanceFactory.GetInstance().LoadConfigDic(Type);
        }
        /// <summary>
        /// Write Config Diction value to database
        /// </summary>
        /// <param name="row">Contains updated datay</param>
        /// <param name="iType">Type of the data</param>
        /// <returns></returns>
        public int WriteConfigDicRow(DsConfigDic.ConfigDicRow row, int iType)
        {
            return DaoInstanceFactory.GetInstance().WriteConfigDicRow(row, iType);
        }

    }
}
