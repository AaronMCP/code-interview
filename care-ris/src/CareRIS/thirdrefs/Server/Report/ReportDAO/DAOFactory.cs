using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DataAccessLayer;

namespace Server.ReportDAO
{
    public class DAOFactory<T> where T : class
    {
        static T _instance = null;

        DAOFactory()
        {

        }

        public static T getInstance()
        {
            if (_instance == null)
            {
                using (RisDAL oKodak = new RisDAL())
                {

                    string dbProviderType = string.Format("Server.ReportDAO.Impl.{0}DAO_{1}",
                        typeof(T).Name.Substring(1), oKodak.DriverClassName.ToUpper());

                    Type type = Type.GetType(dbProviderType);

                    _instance = Activator.CreateInstance(type) as T;
                }
            }

            return _instance;
        }
    }
}
