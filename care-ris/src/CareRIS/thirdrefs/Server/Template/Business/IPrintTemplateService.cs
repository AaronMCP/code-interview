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
    public interface IPrintTemplateService
    {
        //The follow functions have same comments as commented in DAO
        void GetPrintData(string accno, string modalityType, string templateType, ref string template, ref DataTable data);
        DataSet LoadSubPrintTemplateInfo(int type);
        bool AddPrintTemplate(BaseDataSetModel model);
        bool DeletePrintTemplate(string strTemplateGuid);
        DataSet LoadPrintTemplateType();
        bool SetDefault(int type,string strModalityType,string strTemplateGuid, string site);
        bool SetDefault(string strTemplateGuid, string site);
        DataSet LoadPrintTemplateField(int type);
        bool ModifyPrintTemplateFieldInfo(string strTemplateGuid, string strTemplateInfo);
        bool ModifyPrintTemplateName(string strTemplateGuid, string strTemplateName);
        bool ModifyPrintTemplatePropertyTag(string strTemplateGuid, string strPropertyTag);
        string LoadPrintTemplateInfo(string strTemplateGuid);
        int GetLatestVersion(string strTemplateGuid);
        bool IsPrintTemplateSameName(string strTemplateName, int type, string site);
        DataSet GetDefaultPrintTemplate();
        string GetTypeDesc(int type);
        DataSet LoadPrintTemplateByName(int type, string strPrintTemplateName);
        DataSet LoadGeneralStatType();
        DataSet LoadExportTemplateType();
        DataSet LoadSubExportTemplateInfo();
        DataSet LoadExportTemplateInfo(string strTemplateGuid);
        bool AddExportTemplate(BaseDataSetModel model);
        bool ModifyExportTemplate(BaseDataSetModel model);
        bool ModifyExportTemplateInfo(BaseDataSetModel model);
        bool DeleteExportTemplate(string strTemplateGuid);
        string GetPrintTemplateNameByGuid(string strTemplateGuid);
    }
}
