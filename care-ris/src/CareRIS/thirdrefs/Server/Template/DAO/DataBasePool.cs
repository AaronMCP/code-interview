using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataAccessLayer;

namespace Server.DAO.Templates
{
    
        public sealed class DataBasePool
        {
            private static DataBasePool instance = new DataBasePool();
            private IDBProvider dbProvider = null;
            private string dataBaseType = "";

            private DataBasePool()
            {
                using (RisDAL dataAccess = new RisDAL())
                {
                    dataBaseType = dataAccess.DriverClassName;
                    string dbProviderType = "Server.DAO.Templates.Impl." + dataAccess.DriverClassName + "Provider";
                    Type type = Type.GetType(dbProviderType);
                    dbProvider = Activator.CreateInstance(type) as IDBProvider;
                }
            }

            public static DataBasePool Instance
            {
                get
                {
                    return instance;
                }
            }

            public IDBProvider GetDBProvider()
            {
                if (dbProvider != null)
                {
                    return dbProvider;
                }
                else
                {
                    //Configuration Error,return null
                    //Or we can return a default DataBaseProvider
                    return null;
                }
            }
        }
    
}
