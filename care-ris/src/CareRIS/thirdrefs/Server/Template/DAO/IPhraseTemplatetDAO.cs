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
    public interface  IPhraseTemplatetDAO
    {
        
        /// <summary>
        /// Name:GetModalityType
        /// Function:Get modality types from modality table
        /// </summary>
        /// <returns>Return dataset contains one table, this table contains modality types</returns>
        DataSet GetModalityType();

        /// <summary>
        /// Name:LoadPhraseTemplatesByModality
        /// Function:Load phrase templates by modality type
        /// </summary>
        /// <param name="strModalityType">Modality type</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains phrase templates</returns>
        DataSet LoadPhraseTemplatesByModality(string strModalityType, int type, string strUserGuid);

        /// <summary>
        /// Name:LoadPhraseTemplateByGuid
        /// Function:Load phrase template by template guid
        /// </summary>
        /// <param name="strTemplateGuid">Phrase template guid</param>
        /// <returns>Return dataset contains one table, this table contains phrase template information</returns>
        DataSet LoadPhraseTemplateByGuid(string strTemplateGuid);

        /// <summary>
        /// Name:AddPhraseTemplate
        /// Function:Add a new phrase template
        /// </summary>
        /// <param name="model">The model contains values of Phrase template</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddPhraseTemplate(PhraseTemplateModel model);

        /// <summary>
        /// Name:EditPhraseTemplate
        /// Function:Modify phrase template value
        /// </summary>
        /// <param name="model">The model contains new values of Phrase template</param>
        /// <returns>True:successful    False:failed</returns>
        bool EditPhraseTemplate(PhraseTemplateModel model);

        /// <summary>
        /// Name:DeletePhraseTemplate
        /// Function:Delete phrase template
        /// </summary>
        /// <param name="strTemplateGuid">Phrase template guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeletePhraseTemplate(string strTemplateGuid,string strItemGuid);
        bool UpDownNode(string strItemGuid1, string strItemGuid2);
        bool CopyPhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string newItemID, ref string newTemplateID, ref string orderIndex);
        bool MovePhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string orderIndex);
        /// <summary>
        /// Name:LoadPhraseTemplateByShortcut
        /// Function:Load phrase template by shortcut
        /// </summary>
        /// <param name="strShortcut">Phrase template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Return dataset contains one table, this table contains phrase template information</returns>
        DataSet LoadPhraseTemplateByShortcut(string strShortcut,string strUserGuid);

        /// <summary>
        /// Name:ExistShortcut
        /// Funtion:Checking if the shortcut is existed
        /// </summary>
        /// <param name="strShortcut">Phrase template shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True:successful    False:failed</returns>
        bool ExistShortcut(string strShortcut, string strUserGuid, int type, string templateGuid);

        /// <summary>
        /// Name:ExistPhaseTemplateName
        /// Function:Check if there is the same name Phrase Template
        /// </summary>
        /// <param name="strTemplateName">New Template Name</param>
        /// <param name="strUserGuid">User Guid</param>
        /// <param name="type">Global:0 User:1</param>
        /// <returns>True: Success False:Fail</returns>
        bool ExistPhaseTemplateName(string strTemplateName, string strUserGuid,int type);

        DataSet LoadChildren(string strParentGuid, int type, string strUserGuid);
    }
}
