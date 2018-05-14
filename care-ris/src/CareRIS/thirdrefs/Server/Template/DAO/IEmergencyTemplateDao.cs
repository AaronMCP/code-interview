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

namespace Server.DAO.Templates
{
    public interface IEmergencyTemplateDao
    {
        bool QueryEYTemplate(DataSet ds, string strTemplateType, ref string strError);
        bool SaveEYTemplate(DataSet ds, ref string strError);
        bool DelEYTemplate(string strTemplateGuid, ref string strError);
        bool UpdateEYTemplate(DataSet ds, ref string strError);
        bool LockEYTemplate(string strTemplateGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError);
        bool UnLockEYTemplate(string strTemplateGuid, string strOwner, ref string strError);
    }
}
