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
namespace Server.DAO.Oam
{
    public interface  IAnatomyDAO
    {
        //The follow functions have same comments as commented in AbstractDBProvider
        DataTable GetMainAnatomy();
        DataTable GetSubAnatomy(int aid);
        bool AddNewAnatomy(string strAid, string strSid, string strDesc, string strDesc_en, string strDomain);
        string AddNewAnatomyStorProc(string strAid, string strDesc, string strDesc_en, string strDomain);
        bool UpdateMainAnatomy(string strAid, string strDesc, string strDesc_en);
        bool UpdateSubAnatomy(string strAid, string strSid, string strDesc, string strDesc_en);
        bool DeleteAnatomy(string strAid, string strSid);
        bool IsExistAnatomay(string strAid, string strSid,out string returnMessage);
    }
}
