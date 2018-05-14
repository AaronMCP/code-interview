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
using System.Data;
using CommonGlobalSettings;

namespace Server.Business.Oam
{
    public interface IProcedureCodeService
    {
        DataSet GetProcedureCodeList(string strDomain, string strSite);
        bool AddProcedureCode(ProcedureCodeModel model);
        int DeleteProcedureCode(string procedureCode, string site);
        bool ModifyProcedureCode(ProcedureCodeModel model);
        DataSet QueryExamSystem(string modalityType, string bodyPart);
        DataSet QueryAllExamSystem();
        bool QueryBodyCategory(string categoryName, string description, string shortcutCode);
        bool AddBodyCategory(string tag, string categoryName, string description, string shortcutCode);
        bool IsBodyPartExist(string modalityType, string bodyPart, string examSystem);
        bool AddBodyPart(string modalityType, string bodyPart, string examSystem, string domain);
        DataSet QueryBodyPart(string modalityType, string site);
        DataSet GetProceTimeSliceDuration(string timeSliceDur);
        DataSet QueryChargeTypeFee(string procedureCode);
        bool ModifyProcedureCodeFrequency(ProcedureCodeModel model);
        bool Copy2Site(string site, string domain);
        bool Delall4Site(string site, string domain);
        DataSet QueryCheckingItem(string modalityType);
        DataSet GetSiteProcedureCode(string site);
    }
}
