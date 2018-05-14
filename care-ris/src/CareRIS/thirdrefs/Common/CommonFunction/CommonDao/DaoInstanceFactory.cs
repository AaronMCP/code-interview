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
/*                        Author : Bruce Deng
/****************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;

namespace Server.DAO.Common
{
    public class DaoInstanceFactory
    {
        private static ICommonFunctionDao CommonFunctionDao = null;

        public static ICommonFunctionDao GetInstance()
        {
            if (CommonFunctionDao == null)
            {
                //KodakDAL oKodak = new KodakDAL();
                using (var oKodak = new RisDAL())
                {
                    string dbProviderType = string.Format("Server.DAO.Common.Impl.{0}CommonFunctionImpl", oKodak.DriverClassName);
                    Type type = Type.GetType(dbProviderType);
                    CommonFunctionDao = Activator.CreateInstance(type) as ICommonFunctionDao;
                }
            }
            return CommonFunctionDao;
        }
    }
}
