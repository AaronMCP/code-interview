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
/*   Author : Paul Li                                                       */
/****************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using DataAccessLayer;

namespace Server.DAO.QualityControl
{
    public sealed class DataBasePool
    {
        private static DataBasePool instance = new DataBasePool();
        private IDBProvider dbProvider = null;
        private string dataBaseType = new RisDAL().DriverClassName;

        private DataBasePool()
        {
            //KodakDAL dataAccess = new KodakDAL();
            using (var dataAccess = new RisDAL())
            {
                string dbProviderType = "Server.DAO.QualityControl.Impl." + dataAccess.DriverClassName + "Provider";
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
