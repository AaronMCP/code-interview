using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class KMSAction:BaseAction
    {
        IKMSService kmsService = BusinessFactory.Instance.GetKMSService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string strGuid = CommonGlobalSettings.Utilities.GetParameter("Guid", context.Parameters);
            string strParentID = CommonGlobalSettings.Utilities.GetParameter("ParentID", context.Parameters);
            string strPath = CommonGlobalSettings.Utilities.GetParameter("Path", context.Parameters);
            string strName = CommonGlobalSettings.Utilities.GetParameter("Name", context.Parameters);
            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", context.Parameters);
            string strComments = CommonGlobalSettings.Utilities.GetParameter("Comments", context.Parameters);
            string strFileName = CommonGlobalSettings.Utilities.GetParameter("FileName", context.Parameters);
            string strSrcGuid = CommonGlobalSettings.Utilities.GetParameter("SrcGuid", context.Parameters);
            string strIsLeaf = CommonGlobalSettings.Utilities.GetParameter("IsLeaf", context.Parameters);
            string strGuidUp = CommonGlobalSettings.Utilities.GetParameter("GuidUp", context.Parameters);
            string strGuidDown = CommonGlobalSettings.Utilities.GetParameter("GuidDown", context.Parameters);
            string strNodeOrder = CommonGlobalSettings.Utilities.GetParameter("NodeOrder", context.Parameters);
            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "GetAllInnerNodes";
            }
            switch (actionName)
            {
                case "AddNewInnerNode":
                    {
                        return AddNewInnerNode(strGuid, strParentID, strPath, strName, strUserGuid, strComments,strNodeOrder);
                    }
                case "AddNewLeafNode":
                    {
                        return AddNewLeafNode(strGuid, strParentID, strFileName, strNodeOrder);
                    }
                case "UpdateInnerNodeName":
                    {
                        return UpdateInnerNodeName(strGuid, strName,strComments);
                    }
                case "DeleteInnerNode":
                    {
                        return DeleteInnerNode(strGuid);
                    }
                case "DeleteLeafNode":
                    {
                        return DeleteLeafNode(strGuid);
                    }
                case "GetAllInnerNodes":
                    {
                        return GetAllInnerNodes();
                    }
                case "GetAllLeafNodes":
                    {
                        return GetAllLeafNodes();
                    }
                case "IsLeaf":
                    {
                        return IsLeaf(strGuid);
                    }
                case "GetInnerNodeInfo":
                    {
                        return GetInnerNodeInfo(strGuid);
                    }

                case "UpdatePath":
                    {
                        return UpdatePath(strGuid, strPath);
                    }
                case "MoveTo":
                    {
                        return MoveTo(strGuid,strParentID,strIsLeaf,strNodeOrder);
                    }
                case "CopyTo":
                    {
                        return CopyTo(strGuid, strParentID, strSrcGuid, strUserGuid, strIsLeaf, strNodeOrder);
                    }

                case "NodeNameExisted":
                    {
                        return NodeNameExisted(strName, strParentID, strIsLeaf);
                    }

                case "SwapNodeOrder":
                    {
                        return SwapNodeOrder(strGuidUp, strGuidDown, strIsLeaf);
                    }
                default:
                    return null;
            }
        }

        private BaseActionResult AddNewInnerNode(string strGuid, string strParentID, string strPath, string strName, string strUserGuid, string strComments, string strNodeOrder)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = kmsService.AddNewInnerNode(strGuid, strParentID, strPath, strName, strUserGuid, strComments, strNodeOrder);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult AddNewLeafNode(string strGuid, string strParentID, string strFileName, string strNodeOrder)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = kmsService.AddNewLeafNode(strGuid, strParentID, strFileName, strNodeOrder);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult UpdateInnerNodeName(string strGuid, string strName, string strComments)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.UpdateInnerNodeName(strGuid, strName, strComments);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult DeleteInnerNode(string strGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.DeleteInnerNode(strGuid);
            }
            catch (Exception ex)
            {   
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult DeleteLeafNode(string strGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result =  kmsService.DeleteLeafNode(strGuid);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult GetAllInnerNodes()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.DataSetData = kmsService.GetAllInnerNodes();
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult GetAllLeafNodes()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.DataSetData = kmsService.GetAllLeafNodes();
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult IsLeaf(string strGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.IsLeaf(strGuid);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult GetInnerNodeInfo(string strGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.DataSetData = kmsService.GetInnerNodeInfo(strGuid);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult UpdatePath(string strGuid, string strPath)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.UpdatePath(strGuid, strPath);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult MoveTo(string strGuid, string strParentGuid, string isLeaf, string strNodeOrder)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.MoveTo(strGuid, strParentGuid, isLeaf, strNodeOrder);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;

        }

        private BaseActionResult CopyTo(string strGuid, string strParentID, string strSrcID, string strUserGuid, string isLeaf,string strNodeOrder)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.CopyTo(strGuid, strParentID, strSrcID, strUserGuid, isLeaf, strNodeOrder);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult NodeNameExisted(string strName, string strParentID, string isLeaf)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.NodeNameExisted(strName, strParentID, isLeaf);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult SwapNodeOrder(string strGuidUp, string strGuidDown, string isLeaf)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                result.Result = kmsService.SwapNodeOrder(strGuidUp, strGuidDown, isLeaf);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }
    }
}
