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
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using System.Data;

namespace Server.Business.Oam.Impl
{
    public class PathologyServiceImpl:IPathologyService
    {
        private IACRCodeDAO acrCodeDAO  = DataBasePool.Instance.GetDBProvider();
        //The follow functions have same comments as commented in AbstractDBProvider
        public  DataTable LoadMainPathology(int aid)
        {
            if ((aid < 0) || (aid > 9))
            {
                return null;
            }
            DataTable mainPathologyTable = new DataTable();
            mainPathologyTable = acrCodeDAO.GetMainPathology(aid);
            return mainPathologyTable;
        }
        public  DataTable LoadSubPathology(int aid, int pid)
        {
            if ((aid < 0) || (aid > 9))
            {
                return null;
            }
            DataTable subPathologyTable = new DataTable();
            subPathologyTable = acrCodeDAO.GetSubPathology(aid, pid);
            return subPathologyTable;
        }
        public bool AddNewPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en, string strDomain)
        {

            return acrCodeDAO.AddNewPathology(strAid, strPid, strSid, strDesc, strDesc_en,strDomain);


        }
        public string AddNewPathologyStorProc(string strAid, string strPid, string strDesc, string strDesc_en, string strDomain)
        {
            return acrCodeDAO.AddNewPathologyStorProc(strAid, strPid, strDesc, strDesc_en,strDomain);
        }
        public  bool UpdateMainPathology(string strAid, string strPid, string strDesc, string strDesc_en)
        {
            return acrCodeDAO.UpdateMainPathology(strAid, strPid, strDesc, strDesc_en);
        }

        public  bool UpdateSubPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en)
        {
            return acrCodeDAO.UpdateSubPathology(strAid, strPid, strSid, strDesc, strDesc_en);
        }
        public bool DeletePathology(string strAid, string strPid, string strSid,out string strReturnMessage)
        {
            if (!acrCodeDAO.IsExistPathology(strAid, strPid, strSid,out strReturnMessage))
            {
                return acrCodeDAO.DeletePathology(strAid, strPid, strSid);
            }
            else
            {
                return false;
            }
        }
    }
}
