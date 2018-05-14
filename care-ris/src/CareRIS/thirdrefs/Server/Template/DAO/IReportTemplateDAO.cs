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
    public interface IReportTemplateDAO
    {
        
        /// <summary>
        /// Name: ModifyReportTemplate
        /// Function:Modify the report template 
        /// </summary>
        /// <param name="strTemplateGuid"> Report template guid</param>
        /// <param name="model"> The report model contains new values of report template </param>
        /// <returns>True:successful    False:failed</returns>
        bool ModifyReportTemplate(string strTemplateGuid, ReportTemplateModel model);

        /// <summary>
        /// Name:DeleteReportTemplate
        /// Function:Delete the report template
        /// </summary>
        /// <param name="strTemplateGuid">Report template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteReportTemplate(string strTemplateGuid);

        /// <summary>
        /// Name:GetReportTemplateByShortcut
        /// Function:Find if there is a report template which have a shortcut value equals strShortcutCode
        /// </summary>
        /// <param name="strShortcutCode">Shortcut value</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Shortcut type,Global or User</param>
        /// <returns>True:successful    False:failed</returns>
        bool GetReportTemplateByShortcut(string strShortcutCode,string strUserGuid,int type);

        /// <summary>
        /// Name:GetReportTemplateByName
        /// Funtion:Get ReportTemplate information by template name
        /// </summary>
        /// <param name="strTemplateName">Report template name</param>
        /// <returns>Retrun dataset contains one table , this table contains the report information</returns>
        DataSet GetReportTemplateByName(string strTemplateName);

        /// <summary>
        /// Name: GetReportInfo 
        /// Function:Get back report informations for report template by report guid
        /// </summary>
        /// <param name="strReportGuid"> Report Guid</param>
        /// <returns>The return dataset have tow tables</returns>
        DataSet GetReportInfo(string strReportGuid);

        /// <summary>
        /// Name: CopyLeafNode 
        /// Function:Copy a leaf node into a inner node
        /// </summary>
        /// <param name="strItemID"> the leaf node id</param>
        /// <param name="strParentItemID">the inner node id</param>
        /// <param name="strUserUguid"> user guid</param>
        /// <param name="strNewTemplateName"> NewTemplateName</param>
        /// <returns>if copy ok return true, otherwise false </returns>
        /// <returns>newTemplateName is null if have no same name else the new name </returns>
        /// <returns>newItemID if copy successfully </returns>
        /// <returns>the new template name</returns>
        /// <returns>the new item id</returns>
        /// <returns>the new item order</returns> 
        bool CopyLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string newItemID, ref string orderIndex);

        /// <summary>
        /// Name: MoveLeafNode 
        /// Function:Move a leaf node into a inner node
        /// </summary>
        /// <param name="strItemID"> the leaf node id</param>
        /// <param name="strParentItemID">the inner node id</param>
        /// <param name="strNewTemplateName"> NewTemplateName</param> 
        /// <param name="strUserUguid"> user guid</param>
        /// <returns>if copy ok return true, otherwise false  </returns>
        /// <returns>the new template name</returns>
        /// <returns>the new item order</returns> 
        bool MoveLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string orderIndex);

        DataSet GetReportTemplate(string itemGuid, string templateGuid, string shortcutCode);
    }
}
