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
    public class PhraseTemplateServiceImpl:IPhraseTemplateService
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();
        //The follow functions have same comments as commented in AbstractDBProvider
        public virtual DataSet GetModalityType()
        {
            return dbProvider.GetModalityType();
        }
        public virtual DataSet LoadPhraseTemplatesByModality(string strModalityType, int type, string strUserGuid)
        {
            return dbProvider.LoadPhraseTemplatesByModality(strModalityType,type,strUserGuid);
        }
        public virtual DataSet LoadChildren(string strParentGuid, int type, string strUserGuid)
        {
            return dbProvider.LoadChildren(strParentGuid, type, strUserGuid);
        }        
        public virtual bool AddPhraseTemplate(PhraseTemplateModel model)
        {
            return dbProvider.AddPhraseTemplate(model);
        }
        public virtual bool EditPhraseTemplate(PhraseTemplateModel model)
        {
            return dbProvider.EditPhraseTemplate(model);
        }
        public virtual bool DeletePhraseTemplate(string strTemplateGuid, string strItemGuid)
        {
            return dbProvider.DeletePhraseTemplate(strTemplateGuid,strItemGuid);
        }
        public virtual bool UpDownNode(string strItemGuid1, string strItemGuid2)
        {
            return dbProvider.UpDownNode(strItemGuid1, strItemGuid2);
        }
        public virtual bool CopyPhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string newItemID, ref string newTemplateID, ref string orderIndex)
        {
            return dbProvider.CopyPhraseLeafNode( strItemID,  strParentItemID,  strUserUguid, ref  newTemplateName, ref  newItemID, ref  newTemplateID, ref  orderIndex);
        }
        public virtual bool MovePhraseLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string orderIndex)
        {
            return dbProvider.MovePhraseLeafNode( strItemID,  strParentItemID,  strUserUguid, ref  newTemplateName, ref  orderIndex);
        }
        public virtual DataSet LoadPhraseTemplateByGuid(string strTemplateGuid)
        {
            return dbProvider.LoadPhraseTemplateByGuid(strTemplateGuid);
        }
        public virtual DataSet LoadPhraseTemplateByShortcut(string strShortcut,string strUserGuid)
        {
            return dbProvider.LoadPhraseTemplateByShortcut(strShortcut,strUserGuid);
        }
        public virtual bool ExistShortcut(string strShortcut, string strUserGuid, int type, string templateGuid)
        {
            return dbProvider.ExistShortcut(strShortcut, strUserGuid,type,templateGuid);
        }
        public bool ExistPhaseTemplateName(string strTemplateName, string strUserGuid, int type)
        {
            return dbProvider.ExistPhaseTemplateName(strTemplateName, strUserGuid, type);
        }

    }
}
