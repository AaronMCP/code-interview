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
    public interface ITemplateIEDAO
    {
        DataSet GetAllPhraseTemplate(string strUserGuid,string Site);
        bool ImportPhraseTemplate(bool isClear, string strUserGuid, DataSet phraseTemplateDataSet, string site);
        //if(typeof==-100) ,load all pint template
        DataSet GetAllPrintTemplate(int type, string site);
        bool ImportPrintTemplate(bool isClear, int type, DataSet printTemplateDataSet, string site,ref string errorInfo);
        DataSet GetALLReportTemplate(string Site);
        bool ImportReportTemplate(bool isClear, DataSet reportTemplateDataSet,string site);
      //  bool ImportPhraseTemplate(DataSet phraseTemplateDataSet, string strUserGuid, bool isClear);
        bool ImportBookingNoticeTemplate(bool isClear, DataSet noticeTemplateDataset);
        DataSet GetAllBookingNoticeTemplate();
    }
}
