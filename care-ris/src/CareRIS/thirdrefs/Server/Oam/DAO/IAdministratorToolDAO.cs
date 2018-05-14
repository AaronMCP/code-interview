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
using System.Data;
using Common.ActionResult;

namespace Server.DAO.Oam
{
    public interface IAdministratorToolDAO
    {
        bool QueryLock(string stOwner, string stBeginTime, string stEndTime, DataSet ds, ref string strError);
        bool UnLock(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, ref string strError);
        bool QueryOnline(DataSet ds, ref string strError);
        bool SetOffline(string strUserGuid, ref string strError);

    }
}
