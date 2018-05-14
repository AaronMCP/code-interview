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
    public interface  IACRCodeDAO:IAnatomyDAO,IPathologyDAO
    {
        // //The follow functions have same comments as commented in AbstractDBProvider
        string GetACRCodeDesc(string strAMainCode, string strASubCode, string strPMainCode, string strPSubCode);
        DataTable SearchACRCode(string strADesc, string strPDesc, string strACode, string strASubCode, string strPCode, string strPSubCode);
        DataSet GetAllAcrCode();
        DataSet GetAllProcedureCode(string Site);
        DataSet GetAllUser();
        bool ImportACRcode(DataSet acrcodeDataSet, bool isClear);
        bool ImportProcedureCode(DataSet procedureCodeDataSet, bool isClear, string Site);
        bool ImportProcedureCode(DataSet procedureCodeDataSet, ref int errorCode, ref string errorString, string Site);
        #region Modified by Blue Chen for US19895, 10/30/2014
        bool ImportBodyPartSystemMap(DataSet dsBodyPartSystemMap, ref int errorCode, ref string errorString);
        #endregion

    }
}
