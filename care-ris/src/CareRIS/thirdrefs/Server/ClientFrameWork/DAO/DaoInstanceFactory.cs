using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Server.DAO.ClientFramework
{
    public class DaoInstanceFactory
    {
        private static IFrameWorkDAO objFrameWorkDAO = null;

        public static IFrameWorkDAO GetInstance()
        {
            if (objFrameWorkDAO == null)
            {
                string dbProviderType = "Server.DAO.ClientFramework."
                    + System.Configuration.ConfigurationManager.AppSettings["DriverClassName"]
                    + "Implement";
                Type type = Type.GetType(dbProviderType);
                objFrameWorkDAO = Activator.CreateInstance(type) as IFrameWorkDAO;
            }
            return objFrameWorkDAO;
        }
    }
}
