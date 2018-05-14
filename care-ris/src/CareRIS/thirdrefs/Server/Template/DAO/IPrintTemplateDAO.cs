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
namespace Server.DAO.Templates
{
    public interface IPrintTemplateDAO
    {
        void GetPrintData(string accno, string modalityType, string templateType, ref string template, ref DataTable data);
       /// <summary>
       /// Name:LoadPrintTemplateInfo
       /// Function:Load print template info
       /// </summary>
       /// <param name="strTemplateGuid">Print template Guid</param>
       /// <returns>Print template content</returns>
        string LoadPrintTemplateInfo(string strTemplateGuid);

     
        /// <summary>
        /// Name:LoadSubPrintTemplateInfo
        /// Function:Load child print templates part information by type for displaying in tree
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <returns>Return dataset contains one table, the table contains child print templates part information </returns>
        DataSet LoadSubPrintTemplateInfo(int type);

        /// <summary>
        /// Name:AddPrintTemplate
        /// Function:Add a new print template
        /// </summary>
        /// <param name="model"> The model contains the value of pint template</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddPrintTemplate(BaseDataSetModel model);

        /// <summary>
        /// Name:DeletePrintTemplate
        /// Function:Delete the print template
        /// </summary>
        /// <param name="strTemplateGuid">Print template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeletePrintTemplate(string strTemplateGuid);

        /// <summary>
        /// Name:LoadPrintTemplateType
        /// Function:Load print template types from dictionary
        /// </summary>
        /// <returns>Return dataset contains one table, the table contains print template types</returns>
        DataSet LoadPrintTemplateType();

        /// <summary>
        /// Name:SetDefault
        /// Function:Set the print  template to be default one
        /// </summary>
        /// <param name="type">Print template type</param>
        /// <param name="strModalityType">Modality Type</param>
        /// <param name="strTemplateGuid">Print template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool SetDefault(int type, string strModalityType, string strTemplateGuid, string site);
        /// <summary>
        ///  Name:SetDefault
        /// Function:Set the print  template to be default one
        /// </summary>
        /// <param name="strTemplateGuid"></param>
        /// <returns></returns>
        bool SetDefault(string strTemplateGuid, string site);
        /// <summary>
        /// Name:LoadPrintTemplateField
        /// Function:Load print template optional field
        /// </summary>
        /// <param name="type">Template Type(Check Dictionary Tag 14)</param>
        /// <returns></returns>
        DataSet LoadPrintTemplateField(int type);

        /// <summary>
        /// Name:ModifyPrintTemplateFieldInfo
        /// Function:Modify print template content, and add version number
        /// </summary>
        /// <param name="strTemplateGuid"></param>
        /// <param name="strTemplateInfo"></param>
        /// <returns>True: Success False:Fail</returns>
        bool ModifyPrintTemplateFieldInfo(string strTemplateGuid,string strTemplateInfo);

        /// <summary>
        /// Name:ModifyPrintTemplateName
        /// Function:Modify print template name
        /// </summary>
        /// <param name="strTemplateGuid">Template Guid</param>
        /// <param name="strTemplateName">New Template Name</param>
        /// <returns>True:Success False:Fail</returns>
        bool ModifyPrintTemplateName(string strTemplateGuid, string strTemplateName);

        bool ModifyPrintTemplatePropertyTag(string strTemplateGuid, string strPropertyTag);

        /// <summary>
        /// Name:GetLatestVersion
        /// Function:Get the print template the latest version number
        /// </summary>
        /// <param name="strTemplateGuid">Template Guid</param>
        /// <returns>The print template latest version number</returns>
        int GetLatestVersion(string strTemplateGuid);

        /// <summary>
        /// Name:IsPrintTemplateSameName
        /// Function:Check if there is same name template name
        /// </summary>
        /// <param name="strName">Template Name</param>
        /// <param name="type">Template Type</param>
        /// <returns>True: Success  False:Fail</returns>
        bool IsPrintTemplateSameName(string strName, int type, string site);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataSet GetDefaultPrintTemplate();

        /// <summary>
        /// Name:GetTypeDesc
        /// Function:Get print template desciption
        /// </summary>
        /// <param name="type">Print template type(Dictionaryvalue (tag = 14))</param>
        /// <returns></returns>
        string GetTypeDesc(int type);

        /// <summary>
        /// Name:LoadPrintTemplateByName
        /// Function:Load print template by template Name
        /// </summary>
        /// <param name="type">Print template type(DictionaryValue tag =14)</param>
        /// <param name="strPrintTemplateName">Print Template Name</param>
        /// <returns>Print template DataSet</returns>
        DataSet LoadPrintTemplateByName(int type, string strPrintTemplateName);

        /// <summary>
        /// Name:LoadGeneralStatType
        /// Function:Load general statistic  types from tQuery
        /// </summary>
        /// <returns>Return dataset contains one table, the table contains general statistic types</returns>
        DataSet LoadGeneralStatType();
        /// <summary>
        /// Name:LoadPrintTemplateType
        /// Function:Load print template types from dictionary
        /// </summary>
        /// <returns>Return dataset contains one table, the table contains print template types</returns>
        DataSet LoadExportTemplateType();
        /// <summary>
        /// Name:LoadSubExportTemplateInfo
        /// Function:Load child Export templates part information by type for displaying in tree
        /// </summary>
        /// <param name="type">Export template type</param>
        /// <returns>Return dataset contains one table, the table contains child Export templates part information </returns>
        DataSet LoadSubExportTemplateInfo();
         /// <summary>
        /// Name:LoadExportTemplateInfo
        /// Function:Load export templates by TemplateGuid for displaying in tree
        /// </summary>
        /// <param name="strTemplateGuid">TemplateGuid</param>
        /// <returns>Return dataset contains one table, the table contains export templates information </returns>
        DataSet LoadExportTemplateInfo(string strTemplateGuid);
        /// <summary>
        /// Name:AddPrintTemplate
        /// Function:Add a new export template
        /// </summary>
        /// <param name="model"> The model contains the value of export template</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddExportTemplate(BaseDataSetModel model);
        /// <summary>
        /// Name:ModifyExportTemplateInfo
        /// Function:Modify export template 
        /// </summary>
        /// <param name=" model">The model contains the value of export template</param>
        /// <returns>True: Success False:Fail</returns>
        bool ModifyExportTemplateInfo(BaseDataSetModel model);
        /// <summary>
        /// Name:ModifyExportTemplate without templateinfo
        /// Function:Modify export template 
        /// </summary>
        /// <param name=" model">The model contains the value of export template</param>
        /// <returns>True: Success False:Fail</returns>
        bool ModifyExportTemplate(BaseDataSetModel model);
        /// <summary>
        /// Name:DeleteExportTemplate
        /// Function:Delete the Export template
        /// </summary>
        /// <param name="strTemplateGuid">Export template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteExportTemplate(string strTemplateGuid);
        /// <summary>
        /// Name:GetPrintTemplateNameByGuid
        /// Function:Get PrintTemplateName By Guid
        /// </summary>
        /// <param name="strTemplateGuid">template guid</param>
        /// <returns>print template name</returns>
        string GetPrintTemplateNameByGuid(string strTemplateGuid);
    }
}
