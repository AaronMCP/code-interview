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
using Server.Business.Templates;
using Server.DAO.Templates;

namespace Server.Business.Templates.Impl
{
    public class ExaminTemplateServiceImpl : IExaminTemplateService
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();

        //The follow functions have same comments as commented in AbstractDBProvider

        public virtual bool AddExaminTemplate(ExaminTemplateModel model)
        {
            return dbProvider.AddExaminTemplate(model);
        }
        public virtual bool EditExaminTemplate(ExaminTemplateModel model)
        {
            return dbProvider.EditExaminTemplate(model);
        }
        public virtual bool DeleteExaminTemplate(string strTemplateGuid)
        {
            return dbProvider.DeleteExaminTemplate(strTemplateGuid);
        }
        public virtual DataSet LoadExaminTemplateByGuid(string strTemplateGuid)
        {
            return dbProvider.LoadExaminTemplateByGuid(strTemplateGuid);
        }
        public virtual DataSet LoadExaminTemplateByShortcut(string strShortcut, string strUserGuid, string site)
        {
            return dbProvider.LoadExaminTemplateByShortcut(strShortcut, strUserGuid, site);
        }
        public virtual bool ExistExaminShortcut(string strShortcut, string strUserGuid, int type, string site)
        {
            return dbProvider.ExistExaminShortcut(strShortcut, strUserGuid, type, site);
        }
        public virtual DataSet LoadExaminTemplatesByModality(string strModalityType, int type, string strUserGuid, string site)
        {
            return dbProvider.LoadExaminTemplatesByModality(strModalityType, type, strUserGuid, site);
        }
        public virtual bool ExistExaminTemplateName(string strTemplateName, string strUserGuid, int type)
        { 
            return dbProvider.ExistExaminTemplateName(strTemplateName,strUserGuid, type);
        }

    }
}
