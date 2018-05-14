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
    public interface  IReportTemplateDirectoryDAO
    {
       
        /// <summary>
        /// Name:AddNewNode
        /// Function:Add a new directory node of report template
        /// </summary>
        /// <param name="strItemGuid">New directory node guid</param>
        /// <param name="strParentID">New directory node's parent guid</param>
        /// <param name="depth">The depth of new directory node in the tree</param>
        /// <param name="strItemName">New directory node name</param>
        /// <param name="itemOrder">New directory node order number in the same level nodes of  the tree </param>
        /// <param name="type">Global:0  User:1</param>
        /// <param name="strUserID">Current user's UserGuid</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddNewNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID);

        /// <summary>
        /// Name:AddNewLeafNode
        /// Function:Add a New report template node to the tReportTemplateDirec table,add a new report template to the tReportTemplate
        /// </summary>
        /// <param name="strItemGuid">New report template node guid </param>
        /// <param name="strParentID">New report template node 's parent guid</param>
        /// <param name="depth">The depth of new report template node in the tree</param>
        /// <param name="strItemName">New report template node name</param>
        /// <param name="itemOrder">New report template node order number in the same level nodes of the tree</param>
        /// <param name="type">Global:0 User:1</param>
        /// <param name="strUserID">Current user's UserGuid</param>
        /// <param name="strTemplateGuid">New report template guid</param>
        /// <param name="model">This report template model contains the information of new report template </param>
        /// <returns>True:successful    False:failed</returns>
        bool AddNewLeafNode(string strItemGuid, string strParentID, int depth, string strItemName, int itemOrder, int type, string strUserID, string strTemplateGuid, string strGender, ReportTemplateModel model);

        /// <summary>
        /// Name : EditNodeName
        /// Function:Modify the name of node,include report template directory node's name and report template node's name
        /// </summary>
        /// <param name="strItemGuid">Current node 's guid</param>
        /// <param name="strItemName">New node name</param>
        /// <returns>True:successful    False:failed</returns>
        bool EditNodeName(string strItemGuid, string strItemName);

        /// <summary>
        /// Name:EditNodeParentID
        /// Function:It have not used
        /// </summary>
        /// <param name="strItemGuid"></param>
        /// <param name="strParentID"></param>
        /// <param name="curNodeOrder"></param>
        /// <param name="nextNodeOrder"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        bool EditNodeParentID(string strItemGuid, string strParentID, int curNodeOrder, int nextNodeOrder,int depth);

        /// <summary>
        /// Name:AddNodeItemOrder
        /// Function:Add 1 to current node order number ,make it go down one step
        /// </summary>
        /// <param name="strParentGuid">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node's order number</param>
        /// <param name="strNextGuid">The next node guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddNodeItemOrder(string strParentGuid, int curNodeOrder,string strNextGuid);

        /// <summary>
        /// Name:MinusNodeItemOrder
        /// Function:Minus 1 to current node order number ,make it go up one step
        /// </summary>
        /// <param name="strParentGuid">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node's order number</param>
        /// <param name="strPreGuid">The prev node guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool MinusNodeItemOrder(string strParentGuid, int curNodeOrder,string strPreGuid);

        /// <summary>
        /// Name:DeleteNode
        /// Function:Delete node
        /// </summary>
        /// <param name="strItemGuid">Current node guid</param>
        /// <param name="strParentID">Current node's parent guid</param>
        /// <param name="curNodeOrder">Current node order number</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteNode(string strItemGuid, string strParentID, int curNodeOrder);

        /// <summary>
        /// Name:GetSubNodes
        /// Function:Get child nodes of the directory node
        /// </summary>
        /// <param name="strItemGuid">Report Template directory node guid</param>
        /// <param name="strUserGuid">current user's UserGuid</param>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        DataSet GetSubNodes(string strItemGuid,string strUserGuid);

        /// <summary>
        /// Name:IsLeaf
        /// Function:Checking current node if is a leaf node
        /// </summary>
        /// <param name="strItemGuid">Current node guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool IsLeaf(string strItemGuid);

        /// <summary>
        /// Name:GetTemplateGuid
        /// Funtion:Get report template guid by its name
        /// </summary>
        /// <param name="strItemGuid">report template node guid</param>
        /// <returns>Report template guid</returns>
        string GetTemplateGuid(string strItemGuid);

        /// <summary>
        /// Name:GetItemGuid
        /// Function:Get report template node 's guid by its shortcut
        /// </summary>
        /// <param name="strShortcut">Report template node shortcut</param>
        /// <param name="strUserGuid">Current user's UserGuid</param>
        /// <returns>Report template node guid, if it isn't found, return null</returns>
        string GetItemGuid(string strShortcut,string strUserGuid);

        /// <summary>
        /// Name:GetParentGuid
        /// Function:Get node's parent node
        /// </summary>
        /// <param name="strItemGuid">Node guid</param>
        /// <returns>Parent node's guid, if it isn't found , return null</returns>
        string GetParentGuid(string strItemGuid);
    }
}
