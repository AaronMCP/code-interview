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
    public interface  IReportTemplateDirectoryService
    {
        //The follow functions have same comments as commented in DAO
        bool AddNewNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID);
        bool AddNewLeafNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID, string strTemplateGuid, string strGender, ReportTemplateModel model);
        bool EditNodeName(string strItemGuid, string strItemName);
        bool EditNodeParentID(string strItemGuid, string strParentID, int curNodeOrder, int nextNodeOrder, int depth);
        bool AddNodeItemOrder(string strParentGuid, int curNodeOrder,string strNextGuid);
        bool MinusNodeItemOrder(string strParentGuid, int curNodeOrder,string strPrevGuid);
        bool DeleteNode(string strItemGuid, string strParentID, int curNodeOrder);
        DataSet GetSubNodes(string strItemGuid,string strUserGuid);
        bool IsLeaf(string strItemGuid);
        string GetTemplateGuid(string strItemGuid);
        string GetItemGuid(string strShortcut, string strUserGuid);
        string GetParentGuid(string strItemGuid);
        bool CopyLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string newItemID, ref string orderIndex);
        bool MoveLeafNode(string strItemID, string strParentItemID, string strUserUguid, ref string newTemplateName, ref string orderIndex);
    }
}
