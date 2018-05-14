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
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using System.Text.RegularExpressions;

namespace Server.Business.Oam.Impl
{
    public class ACRCodeServiceImpl : IACRCodeService
    {
        private IACRCodeDAO acrCodeDAO = DataBasePool.Instance.GetDBProvider();
        //The follow functions have same comments as commented in AbstractDBProvider
        public virtual bool ImportProcedureCode(DataSet procedureCodeDataSet, bool isClear,string Site)
        {
            return acrCodeDAO.ImportProcedureCode(procedureCodeDataSet, isClear, Site);
        }

        public virtual bool ImportProcedureCode(DataSet procedureCodeDataSet, ref int errorCode, ref string errorString, string Site)
        {
            return acrCodeDAO.ImportProcedureCode(procedureCodeDataSet, ref errorCode, ref errorString,Site);
        }

        public virtual bool ImportACRcode(DataSet acrcodeDataSet, bool isClear)
        {
            return acrCodeDAO.ImportACRcode(acrcodeDataSet, isClear);
        }
        public virtual DataSet GetAllAcrCode()
        {
            return acrCodeDAO.GetAllAcrCode();
        }
        public virtual DataSet GetAllProcedureCode(string Site)
        {
            return acrCodeDAO.GetAllProcedureCode(Site);
        }
        public virtual DataSet GetAllUser()
        {
            return acrCodeDAO.GetAllUser();
        }
        public virtual string GetACRCodeDesc(string strACRCode)
        {

            string strACode = GetAID(strACRCode);
            string strASubCode = GetASID(strACRCode);
            string strPCode = GetPID(strACRCode);
            string strPSubCode = GetPSID(strACRCode);
            return acrCodeDAO.GetACRCodeDesc(strACode, strASubCode,  strPCode, strPSubCode);

        }
        public virtual DataTable SearchACRCode(string strADesc, string strPDesc, string strACRCode)
        {

            if (strACRCode == null && strACRCode == "")
            {
                return null;
            }
            string strACode = GetAID(strACRCode);
            string strASubCode = GetASID(strACRCode);
            string strPCode = GetPID(strACRCode);
            string strPSubCode = GetPSID(strACRCode);
            return acrCodeDAO.SearchACRCode(strADesc, strPDesc, strACode, strASubCode, strPCode, strPSubCode);
        }
        private string GetAID(string strACRCode)
        { 
            strACRCode.Trim();
            if ((strACRCode != null) && (strACRCode != "") && Regex.IsMatch(strACRCode, @"^[-]?(\d+\.?\d*|\.\d+)$"))
            {
                int dotP = strACRCode.IndexOf('.');
                if (dotP > 0)
                    return strACRCode.Substring(0, 1);
            }
            return "";
        }

        private  string GetPID(string strACRCode)
        {
            strACRCode.Trim();
            if ((strACRCode != null) && (strACRCode != "") && Regex.IsMatch(strACRCode, @"^[-]?(\d+\.?\d*|\.\d+)$"))
            {
                int dotP = strACRCode.IndexOf('.');
                string subStr = strACRCode.Substring(dotP);
                if (subStr.Length>1)
                 return strACRCode.Substring(dotP+1,1);
            }
            return "";
        }
        private  string GetASID(string strACRCode)
        {
            strACRCode.Trim();
            if ((strACRCode != null) && (strACRCode != "") && Regex.IsMatch(strACRCode, @"^[-]?(\d+\.?\d*|\.\d+)$"))
            {
                int dotP = strACRCode.IndexOf('.');
                if (dotP > 1)
                    return strACRCode.Substring(1, dotP - 1);
            }
            return "";
        }
        private  string GetPSID(string strACRCode)
        {
            strACRCode.Trim();
            if ((strACRCode != null) && (strACRCode != "") && Regex.IsMatch(strACRCode, @"^[-]?(\d+\.?\d*|\.\d+)$"))
            {
                int dotP = strACRCode.IndexOf('.');
                string subStr = strACRCode.Substring(dotP);
                if (subStr.Length > 2)
                    return strACRCode.Substring(dotP + 2, strACRCode.Length - dotP - 2);
            }
            return "";
        }
        
        #region Modified by Blue Chen for US19895, 10/30/2014
        public bool ImportBodyPartSystemMap(DataSet dsBodyPartSystemMap, ref int errorCode, ref string errorString)
        {
            return acrCodeDAO.ImportBodyPartSystemMap(dsBodyPartSystemMap, ref errorCode, ref errorString);
        }
        #endregion

    }
}
