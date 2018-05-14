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
    public class TemplateIEServiceImpl:ITemplateIEService
    {
        private ITemplateIEDAO templateIEDAO = DataBasePool.Instance.GetDBProvider();
        public virtual DataSet GetAllPhraseTemplate(string strUserGuid,string Site)
        {
            return templateIEDAO.GetAllPhraseTemplate(strUserGuid,Site);
        }
        public virtual bool ImportPhraseTemplate(bool isClear, string strUserGuid, DataSet phraseTemplateDataSet, string site)
        {
            return templateIEDAO.ImportPhraseTemplate(isClear, strUserGuid, phraseTemplateDataSet,site);
        }
        public virtual DataSet GetAllPrintTemplate(int type, string site)
        {
            return templateIEDAO.GetAllPrintTemplate(type,site);
        }
        public virtual bool ImportPrintTemplate(bool isClear, int type, DataSet printTemplateDataSet, string site,ref string errorInfo)
        {
            return templateIEDAO.ImportPrintTemplate(isClear, type, printTemplateDataSet,site,ref errorInfo);
        }
        public virtual DataSet GetALLReportTemplate(string Site)
        {
            return templateIEDAO.GetALLReportTemplate(Site);
        }
        public virtual bool ImportReportTemplate(bool isClear, DataSet reportTemplateDataSet, string site)
        {
            return templateIEDAO.ImportReportTemplate(isClear, reportTemplateDataSet,site);
        }
        public virtual bool ImportBookingNoticeTemplate(bool isClear, DataSet noticeTemplateDataset)
        {
            return templateIEDAO.ImportBookingNoticeTemplate(isClear, noticeTemplateDataset);
        }
        public virtual DataSet GetAllBookingNoticeTemplate()
        {
            return templateIEDAO.GetAllBookingNoticeTemplate();
        }
    }
}
