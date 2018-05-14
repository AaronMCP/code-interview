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
using CommonGlobalSettings;
using System.Data;
namespace Server.DAO.Templates
{
    public interface IExaminTemplateDAO
    {

        /// <summary>
        /// Name:LoadExaminTemplatesByModality
        /// Funtion:Load examine templates by modality
        /// </summary>
        /// <param name="strModalityType">Modality type</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains examine templates</returns>
        DataSet LoadExaminTemplatesByModality(string strModalityType, int type, string strUserGuid, string site);

        /// <summary>
        /// Name:LoadExaminTemplateByGuid
        /// Function:Load examine template by guid
        /// </summary>
        /// <param name="strTemplateGuid">Examine template guid</param>
        /// <returns>Return dataset contains one table, this table contains examine template information</returns>
        DataSet LoadExaminTemplateByGuid(string strTemplateGuid);

        /// <summary>
        /// Name:AddExaminTemplate
        /// Function:Add a new examine template
        /// </summary>
        /// <param name="model">The model contains values of  the examine template</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddExaminTemplate(ExaminTemplateModel model);

        /// <summary>
        /// Name:EditExaminTemplate
        /// Function:Modify the examine template
        /// </summary>
        /// <param name="model">The model contains values of the examine template</param>
        /// <returns>True:successful    False:failed</returns>
        bool EditExaminTemplate(ExaminTemplateModel model);

        /// <summary>
        /// Name:DeleteExaminTemplate
        /// Function:Delete examine template
        /// </summary>
        /// <param name="strTemplateGuid">Examine template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteExaminTemplate(string strTemplateGuid);

        /// <summary>
        /// Name:LoadExaminTemplateByShortcut
        /// Function:Load examine template by shortcut
        /// </summary>
        /// <param name="strShortcut">Examine template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains examine template information</returns>
        DataSet LoadExaminTemplateByShortcut(string strShortcut, string strUserGuid, string site);

        /// <summary>
        /// Name:ExistExaminShortcut
        /// Function:Checking if the shortcut is existed
        /// </summary>
        /// <param name="strShortcut">Examine shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True:successful    False:failed</returns>
        bool ExistExaminShortcut(string strShortcut, string strUserGuid, int type, string site);
        /// <summary>
        /// Name:ExistExaminTemplateName
        /// Function:Check if there is the same name examin template
        /// </summary>
        /// <param name="strTemplateName">New Template Name</param>
        /// <param name="strUserGuid">User Guid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True: Success False:Fail</returns>
        bool ExistExaminTemplateName(string strTemplateName, string strUserGuid, int type);
    }
}
