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

namespace Server.DAO.Oam
{
    public interface IProcedureCodeDAO
    {
        /// <summary>
        /// Get the time slice duration of porcedure which can sync to check RP in reg module
        /// </summary>
        /// <param name="timeSliceDur">the value of time slice duation to RP in systemProfile</param>
        /// <returns>
        /// DataSet contains a table including some duration of procedure in table "tDictionaryValue"
        /// </returns>
        DataSet GetProceTimeSliceDuration(string timeSliceDur);
        DataSet GetProcedureCodeList(string strDomain,string strSite);
        bool AddProcedureCode(ProcedureCodeModel model);
        /// <summary>
        /// Delete the Procedure Code From DB
        /// </summary>
        /// <param name="procedureCode">The Procedure Code</param>
        /// <returns>
        /// 0: delete sucessfully;
        /// 1: it is used by registration, can not be deleted;
        /// 2: it is used by booking, can not be deleted.
        /// </returns>
        int DeleteProcedureCode(string procedureCode, string site);
        bool ModifyProcedureCode(ProcedureCodeModel model);
        DataSet QueryExamSystem(string modalityType, string bodyPart);
        DataSet QueryAllExamSystem();
        DataSet QueryBodyPart(string modalityType, string site);
        bool QueryBodyCategory(string categoryName, string description, string shortcutCode);
        bool AddBodyCategory(string tag, string categoryName, string description, string shortcutCode);
        bool IsBodyPartExist(string modalityType, string bodyPart, string examSystem);
        bool AddBodyPart(string modalityType, string bodyPart, string examSystem, string domain);
        DataSet QueryChargeTypeFee(string procedureCode);
        bool ModifyProcedureCodeFrequency(ProcedureCodeModel model);
        bool Copy2Site(string site, string domain);
        bool Delall4Site(string site, string domain);
        DataSet QueryCheckingItem(string modalityType);
        DataSet GetSiteProcedureCode(string site);

    }
}
