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

namespace Server.Business.Oam.Impl
{
    public class AnatomyServiceImpl:IAnatomyService
    {
        private  IACRCodeDAO acrCodeDAO = DataBasePool.Instance.GetDBProvider();
        //The follow functions have same comments as commented in AbstractDBProvider
        public  DataTable LoadMainAnatomy()
        {

            DataTable mainAnatomyTable = new DataTable();
            mainAnatomyTable = acrCodeDAO.GetMainAnatomy();
            return mainAnatomyTable;
        }
        public  DataTable LoadSubAnatomy(int aid)
        {
            if ((aid < 0) || (aid > 9))
            {
                return null;
            }
            DataTable subAnatomyTable = new DataTable();
            subAnatomyTable = acrCodeDAO.GetSubAnatomy(aid);
            return subAnatomyTable;
        }
        public bool AddNewAnatomy(string strAid, string strSid, string strDesc, string strDesc_en, string strDomain)
        {

            return acrCodeDAO.AddNewAnatomy(strAid, strSid, strDesc, strDesc_en,strDomain);


        }
        public string AddNewAnatomyStorProc(string strAid, string strDesc, string strDesc_en, string strDomain)
        {
            return acrCodeDAO.AddNewAnatomyStorProc(strAid, strDesc, strDesc_en,strDomain);
        }

        public  bool UpdateMainAnatomy(string strAid, string strDesc, string strDesc_en)
        {
            return acrCodeDAO.UpdateMainAnatomy(strAid, strDesc, strDesc_en);
        }

        public  bool UpdateSubAnatomy(string strAid, string strSid, string strDesc, string strDesc_en)
        {
            return acrCodeDAO.UpdateSubAnatomy(strAid, strSid, strDesc, strDesc_en);
        }
        public bool DeleteAnatomy(string strAid, string strSid,out string returnMessage)
        {
            if (!acrCodeDAO.IsExistAnatomay(strAid, strSid, out returnMessage))
            {
                return acrCodeDAO.DeleteAnatomy(strAid, strSid);
            }
            else
            {
                return false;
            }
        }

    }
}
