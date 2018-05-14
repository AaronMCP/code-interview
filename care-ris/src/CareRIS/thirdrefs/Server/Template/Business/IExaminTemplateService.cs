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
    public interface IExaminTemplateService
    {
        //The follow functions have same comments as commented in DAO
        DataSet LoadExaminTemplatesByModality(string strModalityType, int type, string strUserGuid, string site);
        DataSet LoadExaminTemplateByGuid(string strTemplateGuid);
        bool AddExaminTemplate(ExaminTemplateModel model);
        bool EditExaminTemplate(ExaminTemplateModel model);
        bool DeleteExaminTemplate(string strTemplateGuid);
        DataSet LoadExaminTemplateByShortcut(string strShortcut, string strUserGuid, string site);
        bool ExistExaminShortcut(string strShortcut, string strUserGuid, int type, string site);
        bool ExistExaminTemplateName(string strTemplateName, string strUserGuid, int type);
    }
}
