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
using Server.DAO.Templates;
using System.Data;
using Server.Business.Templates;
using CommonGlobalSettings;

namespace Server.Business.Templates.Impl
{
    public class ReportTemplateServiceImpl:IReportTemplateService,IReportTemplateDirectoryService
    {
        private IDBProvider reportTemplateDAO = DataBasePool.Instance.GetDBProvider(); 
       

        //public virtual bool AddReportTemplate(string strGuid,ReportTemplateModel model)
        //{
        //    return reportTemplateDAO.AddReportTemplate(strGuid,model);
        //}
        //The follow functions have same comments as commented in AbstractDBProvider
        public virtual bool ModifyReportTemplate(string strTemplateGuid, ReportTemplateModel model)
        {
            return reportTemplateDAO.ModifyReportTemplate(strTemplateGuid, model);
        }

        public virtual bool DeleteReportTemplate(string strTemplateGuid)
        {
            return reportTemplateDAO.DeleteReportTemplate(strTemplateGuid);
        }

        public virtual bool GetReportTemplateByShortcut(string strShortcutCode, string strUserGuid,int type)
        {
            return reportTemplateDAO.GetReportTemplateByShortcut(strShortcutCode,strUserGuid,type);
        }

        public virtual DataSet GetReportTemplateByName(string strTemplateName)
        {
            return reportTemplateDAO.GetReportTemplateByName(strTemplateName);
        }

        public virtual DataSet GetReportTemplate(string itemGuid, string templateGuid, string shortcutCode)
        {
            return reportTemplateDAO.GetReportTemplate(itemGuid, templateGuid, shortcutCode);
        }

        public virtual bool AddNewNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID)
        {
            return reportTemplateDAO.AddNewNode(strItemGuid, strParentID, depth, strItemName, itemOrder, type, strUserID);
        }

        public virtual bool AddNewLeafNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID, string strTemplateGuid, string strGender, ReportTemplateModel model)
        {
            return reportTemplateDAO.AddNewLeafNode(strItemGuid, strParentID, depth, strItemName, itemOrder, type, strUserID, strTemplateGuid, strGender, model);
        }

        public virtual bool EditNodeName(string strItemGuid, string strItemName)
        {
            return reportTemplateDAO.EditNodeName(strItemGuid, strItemName);
        }

        public virtual bool EditNodeParentID(string strItemGuid, string strParentID, int curNodeOrder, int nextNodeOrder, int depth)
        {
            return reportTemplateDAO.EditNodeParentID(strItemGuid, strParentID, curNodeOrder, nextNodeOrder, depth);
        }

        public virtual bool AddNodeItemOrder(string strParentGuid, int curNodeOrder,string strNextGuid)
        {
            return reportTemplateDAO.AddNodeItemOrder(strParentGuid, curNodeOrder,strNextGuid);
        }
        public virtual bool MinusNodeItemOrder(string strParentGuid, int curNodeOrder,string strPrevGuid)
        {
            return reportTemplateDAO.MinusNodeItemOrder(strParentGuid, curNodeOrder,strPrevGuid);
        }
        public virtual bool DeleteNode(string strItemGuid, string strParentID, int curNodeOrder)
        {
            
            return reportTemplateDAO.DeleteNode(strItemGuid, strParentID, curNodeOrder);
        }
        public virtual DataSet GetSubNodes(string strItemGuid,string strUserGuid)
        {
            return reportTemplateDAO.GetSubNodes(strItemGuid,strUserGuid);
        }
        public virtual bool IsLeaf(string strItemGuid)
        {
            return reportTemplateDAO.IsLeaf(strItemGuid);
        }
        public virtual string GetTemplateGuid(string strItemGuid)
        { 
            return reportTemplateDAO.GetTemplateGuid(strItemGuid);
        }

        public virtual string GetItemGuid(string strShortcut,string strUserGuid)
        {
            return reportTemplateDAO.GetItemGuid(strShortcut,strUserGuid);
        }
        public virtual string GetParentGuid(string strItemGuid)
        {
            return reportTemplateDAO.GetParentGuid(strItemGuid);
        }
        public virtual DataSet GetReportInfo(string strReportGuid)
        {
            return reportTemplateDAO.GetReportInfo(strReportGuid);
        }

        public virtual bool CopyLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string strNewTemplateName, ref string strNewItemID, ref string orderIndex)
        {
            return reportTemplateDAO.CopyLeafNode(strItemID, strParentItemID, strUserUguid, ref strNewTemplateName, ref strNewItemID,ref orderIndex );
        }

        public virtual bool MoveLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string strNewTemplateName, ref string orderIndex)
        {
            return reportTemplateDAO.MoveLeafNode(strItemID, strParentItemID, strUserUguid,ref strNewTemplateName,ref orderIndex);
        }

    }
}
