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
    public interface IPhraseTemplateService
    {
        //The follow functions have same comments as commented in DAO
        DataSet GetModalityType();
        DataSet LoadPhraseTemplatesByModality(string strModalityType, int type, string strUserGuid);
        DataSet LoadPhraseTemplateByGuid(string strTemplateGuid);
        bool AddPhraseTemplate(PhraseTemplateModel model);
        bool EditPhraseTemplate(PhraseTemplateModel model);
        bool DeletePhraseTemplate(string strTemplateGuid, string strItemGuid);
        bool UpDownNode(string strItemGuid1, string strItemGuid2);
        bool CopyPhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string newItemID, ref string newTemplateID, ref string orderIndex);
        bool MovePhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string orderIndex);
        DataSet LoadPhraseTemplateByShortcut(string strShortcut,string strUserGuid);
        bool ExistShortcut(string strShortcut, string strUserGuid, int type, string templateGuid);
        bool ExistPhaseTemplateName(string strTemplateName, string strUserGuid, int type);
        DataSet LoadChildren(string strParentGuid, int type, string strUserGuid);
    }
}
