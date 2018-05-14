using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;

namespace Server.Business.Oam
{
    public interface IKMSService
    {

        /// <summary>
        /// Name:AddNewNode
        /// Function:Add a new directory node of KMS
        /// </summary>
        /// <param name="strGuid">New directory node guid</param>
        /// <param name="strParentID">New directory node's parent guid</param>
        /// <param name="strPath">The path from root ex: "...root/node"</param>
        /// <param name="strName">Name of the node</param>
        /// <param name="strUserGuid">UserGuid</param>/// 
        /// <param name="strComments">comments of this node</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddNewInnerNode(string strGuid, string strParentID, string strPath, string strName, string strUserGuid, string strComments ,string strNodeOrder);

        /// <summary>
        /// Name:AddNewLeafNode
        /// Function:Add a New KMS leaf node to the tKnowledgeFiles table
        /// </summary>
        /// <param name="strGuid">New KMS leaf node guid </param>
        /// <param name="strParentID">New KMS leaf node 's parent guid</param>
        /// <param name="strFileName">New KMS leaf node's FileName</param>
        /// <returns>True:successful    False:failed</returns>
        bool AddNewLeafNode(string strGuid, string strParentID, string strFileName ,string strNodeOrder);

        /// <summary>
        /// Name : UpdateInnerNodeName
        /// Function:Modify the name of node(tKnowledge node's Name and Comments only)
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <param name="strName">New node name</param>
        /// <param name="strName">New node comments</param>
        /// <returns>True:successful    False:failed</returns>
        bool UpdateInnerNodeName(string strGuid, string strName, string strComments);
        /// <summary>
        /// Name : DeleteNode
        /// Function:Delete the node by guid
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteInnerNode(string strGuid);
        /// <summary>
        /// Name : DeleteLeafNode
        /// Function:Delete the node by guid
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool DeleteLeafNode(string strGuid);

        /// <summary>
        /// Name:GetAllInnerNodes
        /// Function:Get all nodes of the directory node(exclude the leaf nodes)
        /// </summary>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        DataSet GetAllInnerNodes();
        /// <summary>
        /// Name:GetAllLeafNodes
        /// Function:Get all nodes of the directory node(exclude the leaf nodes)
        /// </summary>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        DataSet GetAllLeafNodes();
        /// <summary>
        /// Name:IsLeaf
        /// Function:Checking current node if is a leaf node
        /// </summary>
        /// <param name="strGuid">Current node guid</param>
        /// <returns>True:successful    False:failed</returns>
        bool IsLeaf(string strGuid);
        /// <summary>
        /// Name:GetInnerNodeInfo
        /// Function:Get InnerNodeInfo
        /// </summary>
        /// <returns>Return dataset have one table,it contains InnerNodeInfo</returns>
        DataSet GetInnerNodeInfo(string strGuid);
        /// <summary>
        /// Name:UpdateFath
        /// Function:UpdateFath(string strGuid, string strPath)
        /// </summary>
        /// <returns>UpdateFath if rename node</returns>
        bool UpdatePath(string strGuid, string strPath);
        /// <summary>
        /// Name:MoveTo
        /// Function:MoveTo(string strGuid,string strParentGuid,string isLeaf)
        /// </summary>
        /// <returns>Move to exist node</returns>
        bool MoveTo(string strGuid, string strParentGuid, string isLeaf,string strNodeOrder);
        /// <summary>
        /// Name:CopyTo
        /// Function:CopyTo(string strGuid,string strParentGuid, string isLeaf)
        /// </summary>
        /// <returns>Copy  to exist node</returns>
        bool CopyTo(string strGuid, string strParentID, string strSrcID, string strUserGuid, string isLeaf,string strNodeOrder);
        /// <summary>
        /// Name:NodeNameExisted
        /// Function:NodeNameExisted(string strName, string strParentID,string isLeaf)
        /// </summary>
        /// <returns>return true if existed else false</returns>
        bool NodeNameExisted(string strName, string strParentID, string isLeaf);
        /// <summary>
        /// Name:SwapNodeOrder
        /// Function:SwapNodeOrder(string strGuidUp, string strGuidDown, string isLeaf)
        /// </summary>
        /// <returns>return true if existed else false</returns>
        bool SwapNodeOrder(string strGuidUp, string strGuidDown, string isLeaf);
    }
}
