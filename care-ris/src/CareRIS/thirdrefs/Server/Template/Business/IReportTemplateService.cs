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
using CommonGlobalSettings;

namespace Server.Business.Templates
{
    public interface IReportTemplateService
    {
        //The follow functions have same comments as commented in DAO
        bool ModifyReportTemplate(string strTemplateGuid, ReportTemplateModel model);
        bool DeleteReportTemplate(string strTemplateGuid);
        bool GetReportTemplateByShortcut(string strShortcutCode, string strUserGuid,int type);
        DataSet GetReportTemplateByName(string strTemplateName);
        DataSet GetReportInfo(string strReportGuid);
        DataSet GetReportTemplate(string itemGuid, string templateGuid, string shortcutCode);
    }
}
