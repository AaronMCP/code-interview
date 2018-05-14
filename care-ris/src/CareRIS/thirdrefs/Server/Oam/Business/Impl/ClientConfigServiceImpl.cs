using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class ClientConfigServiceImpl : IClientConfigService
    {
        private IClientConfigDAO clientConfigDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet GetClientConfigDataSet()
        {
            try
            {
                return clientConfigDAO.GetClientConfigDataSet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
