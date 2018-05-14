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
using Server.Business.Templates;
using Server.DAO.Templates;
using System.Data;
using CommonGlobalSettings;

namespace Server.Business.Templates.Impl
{
    public class PrintTemplateServiceImpl:IPrintTemplateService
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();
        //The follow functions have same comments as commented in AbstractDBProvider

        public void GetPrintData(string accno, string modalityType, string templateType, ref string template, ref DataTable data)
        {
            dbProvider.GetPrintData(accno, modalityType, templateType, ref template, ref data);
        }
        public virtual DataSet LoadSubPrintTemplateInfo(int type)
        {
            return dbProvider.LoadSubPrintTemplateInfo(type);
        }
        public virtual bool AddPrintTemplate(BaseDataSetModel model)
        {
            return dbProvider.AddPrintTemplate(model);
        }
        public virtual bool DeletePrintTemplate(string strTemplateGuid)
        {
            return dbProvider.DeletePrintTemplate(strTemplateGuid);
        }
        public virtual DataSet LoadPrintTemplateType()
        {
            return dbProvider.LoadPrintTemplateType();
        }
        public virtual bool SetDefault(int type, string strModalityType, string strTemplateGuid, string site)
        {
            return dbProvider.SetDefault(type, strModalityType, strTemplateGuid, site);
        }
        public virtual DataSet LoadPrintTemplateField(int type)
        {
            return dbProvider.LoadPrintTemplateField(type);
        }
        public virtual bool ModifyPrintTemplateFieldInfo(string strTemplateGuid, string strTemplateInfo)
        {
            return dbProvider.ModifyPrintTemplateFieldInfo(strTemplateGuid, strTemplateInfo);
        }
        public virtual bool ModifyPrintTemplateName(string strTemplateGuid, string strTemplateName)
        {
            return dbProvider.ModifyPrintTemplateName(strTemplateGuid, strTemplateName);
        }
        public virtual bool ModifyPrintTemplatePropertyTag(string strTemplateGuid, string strPropertyTag)
        {
            return dbProvider.ModifyPrintTemplatePropertyTag(strTemplateGuid, strPropertyTag);
        }
        public virtual string LoadPrintTemplateInfo(string strTemplateGuid)
        {
            return dbProvider.LoadPrintTemplateInfo(strTemplateGuid);
        }
        public virtual int GetLatestVersion(string strTemplateGuid)
        {
            return dbProvider.GetLatestVersion(strTemplateGuid);
        }
        public virtual bool IsPrintTemplateSameName(string strTemplateName, int type, string site)
        {
            return dbProvider.IsPrintTemplateSameName(strTemplateName, type, site);
        }
        public virtual DataSet GetDefaultPrintTemplate()
        {
            return dbProvider.GetDefaultPrintTemplate();
        }
        public virtual string GetTypeDesc(int type)
        {
            return dbProvider.GetTypeDesc(type);
        }
        public virtual DataSet LoadPrintTemplateByName(int type, string strPrintTemplateName)
        {
            return dbProvider.LoadPrintTemplateByName(type, strPrintTemplateName);
        }
        public virtual DataSet LoadGeneralStatType()
        {
            return dbProvider.LoadGeneralStatType();
        }
        public virtual DataSet LoadExportTemplateType()
        {
            return dbProvider.LoadExportTemplateType();
        }
        public virtual DataSet LoadSubExportTemplateInfo()
        {
            return dbProvider.LoadSubExportTemplateInfo();
        }
        public virtual DataSet LoadExportTemplateInfo(string strTemplateGuid)
        {
            return dbProvider.LoadExportTemplateInfo(strTemplateGuid);
        }
        public virtual bool AddExportTemplate(BaseDataSetModel model)
        {
            return dbProvider.AddExportTemplate(model);
        }
        public virtual bool ModifyExportTemplateInfo(BaseDataSetModel model)
        {
            return dbProvider.ModifyExportTemplateInfo(model);
        }
        public virtual bool ModifyExportTemplate(BaseDataSetModel model)
        {
            return dbProvider.ModifyExportTemplate(model);
        }
        public virtual bool DeleteExportTemplate(string strTemplateGuid)
        {
            return dbProvider.DeleteExportTemplate(strTemplateGuid);
        }
        public virtual bool SetDefault(string strTemplateGuid, string site)
        {
            return dbProvider.SetDefault(strTemplateGuid, site);
        }
        public virtual string GetPrintTemplateNameByGuid(string strTemplateGuid)
        {
            return dbProvider.GetPrintTemplateNameByGuid(strTemplateGuid);
        }

    }
}
