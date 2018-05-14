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
/*   Author : Terrence Jiang                                                                       */
/*   Create Date: July.2006
/****************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.Business.Oam
{
    public interface IPathologyService
    {
        //The follow functions have same comments as commented in AbstractDBProvider
        DataTable LoadMainPathology(int aid);
        DataTable LoadSubPathology(int aid, int pid);
        bool AddNewPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en, string strDomain);
        string AddNewPathologyStorProc(string strAid, string strPid, string strDesc, string strDesc_en, string strDomain);
        bool UpdateMainPathology(string strAid, string strPid, string strDesc, string strDesc_en);
        bool UpdateSubPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en);
        bool DeletePathology(string strAid, string strPid, string strSid,out string strReturnMessage);
    }
}
