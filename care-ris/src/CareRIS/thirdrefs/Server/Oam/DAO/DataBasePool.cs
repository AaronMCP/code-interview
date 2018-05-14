#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataAccessLayer;

namespace Server.DAO.Oam
{
    public sealed class DataBasePool
    {
        private static DataBasePool instance = new DataBasePool();
        private IDBProvider dbProvider = null;
        private string dataBaseType = "";

        private DataBasePool()
        {
            using (RisDAL dal = new RisDAL())
            {
                dataBaseType = dal.DriverClassName;
            }
            //string filePath = Environment.SystemDirectory + "/Oam-provider.xml";
            //if (!File.Exists(filePath))
            //{
            //    throw new OamException("Can't find the configuration file Oam-provider.xml in system directory.");
            //}

            //FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            ////Load the MemoryStream to XmlDocument
            //XmlDocument xmlProvider = new XmlDocument();
            //xmlProvider.Load(stream);

            //XmlNode node = xmlProvider.SelectSingleNode("/Oam-provider/DBProvider");
            //foreach(XmlAttribute x in node.Attributes)
            //{
            //    if(x.Name.Equals("driverClassName"))
            //    {
            //        string dbProviderType = x.Value;
            //        Type type = Type.GetType(dbProviderType);
            //        dbProvider = Activator.CreateInstance(type) as IDBProvider;
            //        break;
            //    }
            //}

            //stream.Close();
            string dbProviderType = "Server.DAO.Oam.Impl." + dataBaseType + "Provider";
            Type type = Type.GetType(dbProviderType);
            dbProvider = Activator.CreateInstance(type) as IDBProvider;
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
            if(dbProvider != null)
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
