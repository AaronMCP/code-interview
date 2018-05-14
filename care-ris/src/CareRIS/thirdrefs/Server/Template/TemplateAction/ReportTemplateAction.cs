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
using Common.Action;
using Common.ActionResult;
using Server.Business.Templates;
using System.Data;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using UC=CommonGlobalSettings;
using Server.Utilities.LogFacility;
namespace Server.TemplatesActions.Action
{
    public class ReportTemplateAction : BaseAction
    {
        private IReportTemplateService reportTemplateService = BusinessFactory.Instance.GetReportTemplateService();
        private IReportTemplateDirectoryService reportTemplateDirectoryService = BusinessFactory.Instance.GetReportTemplateDirectoryService();
        LogManagerForServer logger = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {
                
                switch (context.MessageName.Trim())
                {
                    //case "AddReportTemplate":
                    //    return AddReportTemplate(context.Parameters,context.Model as ReportTemplateModel);
                    case "ModifyReportTemplate":
                        return ModifyReportTemplate(context.Parameters, context.Model as ReportTemplateModel);
                    case "DeleteReportTemplate":
                        return DeleteReportTemplate(context.Parameters);
                    case "GetReportTemplateByShortcut":
                        return GetReportTemplateByShortcut(context.Parameters);
                    case "GetReportTemplateByName":
                        return GetReportTemplateByName(context.Parameters);
                    case "GetReportTemplate":
                        return GetReportTemplate(context.Parameters);
                    case "AddNewNode":
                        return AddNewNode(context.Model as BaseDataSetModel);
                    case "AddNewLeafNode":
                        return AddNewLeafNode(context.Model as ReportTemplateModel);
                    case "EditNodeName":
                        return EditNodeName(context.Model as BaseDataSetModel);
                    case "AddNodeItemOrder":
                        return AddNodeItemOrder(context.Parameters);
                    case "MinusNodeItemOrde":
                        return MinusNodeItemOrder(context.Parameters);
                    case "DeleteNode":
                        return DeleteNode(context.Parameters);
                    case "GetSubNodes":
                        return GetSubNodes(context.Parameters);
                    case "IsLeaf":
                        return IsLeaf(context.Parameters);
                    case "GetTemplateGuid":
                        return GetTemplateGuid(context.Parameters);
                    case "GetItemGuid":
                        return GetItemGuid(context.Parameters);
                    case "GetParentGuid":
                        return GetParentGuid(context.Parameters);
                    case "GetReportInfo":
                        return GetReportInfo(context.Parameters);
                    case "MoveLeafNode":
                        return MoveLeafNode(context.Parameters, context.UserID);
                    case "CopyLeafNode":
                        return CopyLeafNode(context.Parameters, context.UserID);
                    default:
                        {
                            dsAr.ReturnMessage = null;
                            dsAr.Result = false;
                            return dsAr;
                        }

                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_WS, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsAr.ReturnMessage = null;
                dsAr.Result = false;
                return dsAr;
              
            }
        }
        //The follow functions have same comments as commented in AbstractDBProvider
        private BaseActionResult GetReportInfo(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strReportGuid = UC.Utilities.GetParameter("ReportGuid", parameters);
            DataSet myDataSet = reportTemplateService.GetReportInfo(strReportGuid);
            if (myDataSet!=null&&myDataSet.Tables!=null&&myDataSet.Tables.Count == 2)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult GetTemplateGuid(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters);
            result.ReturnMessage = reportTemplateDirectoryService.GetTemplateGuid(strItemGuid);
            return result;
        }

        private BaseActionResult IsLeaf(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters);
            bool isLeaf = reportTemplateDirectoryService.IsLeaf(strItemGuid);
            if (isLeaf)
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            result.ReturnString = isLeaf.ToString();
            return result;
        }
        //private BaseActionResult AddReportTemplate(string parameters,ReportTemplateModel model)
        //{
        //    BaseActionResult result = new BaseActionResult();
        //    string strGuid = UC.Utilities.GetParameter("ReportTemplateGUID", parameters);
        //    if (reportTemplateService.AddReportTemplate(strGuid,model))
        //    {
        //        result.Result = true;
        //    }
        //    else
        //    {
        //        result.Result = false;
        //    }

        //    return result;
        //}
        private BaseActionResult ModifyReportTemplate(string parameters, ReportTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();
            string strGUID = UC.Utilities.GetParameter("ReportTemplateGUID", parameters);
            if (reportTemplateService.ModifyReportTemplate(strGUID,model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult DeleteReportTemplate(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strGUID = UC.Utilities.GetParameter("ReportTemplateGUID", parameters);
            if (reportTemplateService.DeleteReportTemplate(strGUID))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult GetReportTemplateByShortcut(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strShortcut = UC.Utilities.GetParameter("ReportTemplateShortcut", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
           if( reportTemplateService.GetReportTemplateByShortcut(strShortcut,strUserGuid,type))
           
            {
                
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult GetReportTemplateByName(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strName = UC.Utilities.GetParameter("ReportTemplateName", parameters);
            DataSet myDataSet = reportTemplateService.GetReportTemplateByName(strName);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult GetReportTemplate(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();

            string itemGuid = UC.Utilities.GetParameter("ItemGuid", parameters);
            string templateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            string shortcutCode = UC.Utilities.GetParameter("ShortcutCode", parameters);

            DataSet myDataSet = reportTemplateService.GetReportTemplate(itemGuid, templateGuid, shortcutCode);

            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        //private BaseActionResult GetDirectoryField(string parameters)
        //{
        //    DataSetActionResult result = new DataSetActionResult();
        //    string strName = Utilities.GetParameter("TableName", parameters);
        //    DataSet myDataSet = reportTemplateDirectoryService.GetDirectoryField(strName.Trim());
        //    if (myDataSet.Tables.Count > 0)
        //    {
        //        result.DataSetData = myDataSet;
        //        result.Result = true;
        //    }
        //    else
        //    {
        //        result.Result = false;
        //    }

        //    return result;
        //}

        private BaseActionResult AddNewNode(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            //string strItemGuid = Utilities.GetParameter("ItemGuid", parameters).Trim();
            //string strParentID = Utilities.GetParameter("ParentID", parameters).Trim();
            //string strDepth = Utilities.GetParameter("Depth", parameters).Trim();
            //string strItemName = Utilities.GetParameter("ItemName", parameters).Trim();
            //string strItemOrder = Utilities.GetParameter("ItemOrder", parameters).Trim();
            //string strType = Utilities.GetParameter("Type", parameters).Trim();
            //string strUserID = Utilities.GetParameter("UserID", parameters).Trim();
            if(model == null || model.DataSetParameter == null||model.DataSetParameter.Tables.Count==0||model.DataSetParameter.Tables[0].Rows.Count != 1)
            {
                result.Result = false;
                return result;
            }
           
            DataTable myTable = model.DataSetParameter.Tables[0];
            string strItemGuid = Convert.ToString( myTable.Rows[0]["ItemGuid"]).Trim();
            string strParentID = Convert.ToString(myTable.Rows[0]["ParentID"]).Trim();
            string strDepth = Convert.ToString(myTable.Rows[0]["Depth"]).Trim();
            string strItemName = Convert.ToString(myTable.Rows[0]["ItemName"]).Trim();
            string strItemOrder = Convert.ToString(myTable.Rows[0]["ItemOrder"]).Trim();
            string strType = Convert.ToString(myTable.Rows[0]["Type"]).Trim();
            string strUserID = Convert.ToString(myTable.Rows[0]["UserID"]).Trim();
            if (reportTemplateDirectoryService.AddNewNode(strItemGuid,strParentID,int.Parse(strDepth),strItemName,int.Parse(strItemOrder),int.Parse(strType),strUserID))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult AddNewLeafNode(ReportTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();
            //string strItemGuid = Utilities.GetParameter("ItemGuid", parameters).Trim();
            //string strParentID = Utilities.GetParameter("ParentID", parameters).Trim();
            //string strDepth = Utilities.GetParameter("Depth", parameters).Trim();
            //string strItemName = Utilities.GetParameter("ItemName", parameters).Trim();
            //string strItemOrder = Utilities.GetParameter("ItemOrder", parameters).Trim();
            //string strType = Utilities.GetParameter("Type", parameters).Trim();
            //string strUserID = Utilities.GetParameter("UserID", parameters).Trim();
            //string strTemplateGuid = Utilities.GetParameter("TemplateGuid", parameters).Trim();

            if (model == null || model.DirectoryDataSet == null||model.DirectoryDataSet.Tables.Count==0||model.DirectoryDataSet.Tables[0].Rows.Count!=1)
            {
                result.Result = false;
                return result;
            }
            DataTable myTable = model.DirectoryDataSet.Tables[0];
            string strItemGuid = Convert.ToString(myTable.Rows[0]["ItemGuid"]).Trim();
            string strParentID = Convert.ToString(myTable.Rows[0]["ParentID"]).Trim();
            string strDepth = Convert.ToString(myTable.Rows[0]["Depth"]).Trim();
            string strItemName = Convert.ToString(myTable.Rows[0]["ItemName"]).Trim();
            string strItemOrder = Convert.ToString(myTable.Rows[0]["ItemOrder"]).Trim();
            string strType = Convert.ToString(myTable.Rows[0]["Type"]).Trim();
            string strUserID = Convert.ToString(myTable.Rows[0]["UserID"]).Trim();
            string strTemplateGuid = Convert.ToString(myTable.Rows[0]["TemplateGuid"]).Trim();
            string strGender = Convert.ToString(myTable.Rows[0]["Gender"]).Trim();

            if (reportTemplateDirectoryService.AddNewLeafNode(strItemGuid, strParentID, int.Parse(strDepth), strItemName, int.Parse(strItemOrder), int.Parse(strType), strUserID, strTemplateGuid, strGender, model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult EditNodeName(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            if (model == null || model.DataSetParameter == null||model.DataSetParameter.Tables.Count==0||model.DataSetParameter.Tables[0].Rows.Count!=1)
            {
                result.Result = false;
                return result;
            }
            DataTable myTable = model.DataSetParameter.Tables[0];
            //string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters).Trim();
            //string strItemName = UC.Utilities.GetParameter("ItemName", parameters).Trim();
            string strItemGuid = Convert.ToString(myTable.Rows[0]["ItemGuid"]).Trim();
            string strItemName = Convert.ToString(myTable.Rows[0]["ItemName"]).Trim();
            if (reportTemplateDirectoryService.EditNodeName(strItemGuid,strItemName))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult AddNodeItemOrder(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strParentGuid = UC.Utilities.GetParameter("ParentGuid", parameters).Trim();
            string strCurNodeOrder = UC.Utilities.GetParameter("CurNodeOrder", parameters).Trim();
            string strNextGuid = UC.Utilities.GetParameter("NextGuid", parameters).Trim();
            if (reportTemplateDirectoryService.AddNodeItemOrder(strParentGuid,int.Parse(strCurNodeOrder),strNextGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult MinusNodeItemOrder(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strParentGuid = UC.Utilities.GetParameter("ParentGuid", parameters).Trim();
            string strCurNodeOrder = UC.Utilities.GetParameter("CurNodeOrder", parameters).Trim();
            string strPrevGuid = UC.Utilities.GetParameter("PrevGuid", parameters).Trim();
            if (reportTemplateDirectoryService.MinusNodeItemOrder(strParentGuid, int.Parse(strCurNodeOrder),strPrevGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult DeleteNode(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters).Trim();
            string strParentGuid = UC.Utilities.GetParameter("ParentGuid", parameters).Trim();
            string strCurNodeOrder = UC.Utilities.GetParameter("CurNodeOrder", parameters).Trim();
            if (reportTemplateDirectoryService.DeleteNode(strItemGuid,strParentGuid, int.Parse(strCurNodeOrder)))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult GetSubNodes(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters).Trim();
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters).Trim();
            DataSet myDataSet =reportTemplateDirectoryService.GetSubNodes(strItemGuid,strUserGuid);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult GetItemGuid(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strShortcut = UC.Utilities.GetParameter("Shortcut", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            result.ReturnMessage = reportTemplateDirectoryService.GetItemGuid(strShortcut,strUserGuid);
            return result;
        }
        private BaseActionResult GetParentGuid(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters);
            result.ReturnMessage = reportTemplateDirectoryService.GetParentGuid(strItemGuid);
            return result;
        }

        private BaseActionResult CopyLeafNode(string parameters ,string userguid)
        {
            string strItemID, strParentItemID, strUserUguid, strNewTemplateName, strNewItemID ,strOrderIndex;
            strItemID = strParentItemID = strNewTemplateName = strNewItemID = strOrderIndex ="";
            BaseActionResult result = new BaseActionResult();
             strItemID = UC.Utilities.GetParameter("ItemGuid", parameters);
             strParentItemID = UC.Utilities.GetParameter("ParentItemGuid", parameters);
             result.Result = reportTemplateDirectoryService.CopyLeafNode(strItemID, strParentItemID, userguid, ref strNewTemplateName, ref strNewItemID, ref strOrderIndex);
             result.ReturnString = strNewTemplateName + "|" + strNewItemID + "|" + strOrderIndex;
            return result;
        }

        private BaseActionResult MoveLeafNode(string parameters, string userguid)
        {
            string strItemID, strParentItemID, strUserUguid, strNewTemplateName, strOrderIndex;
            int  orderIndex = 0;
            strItemID = strParentItemID = strNewTemplateName =strOrderIndex="";
            BaseActionResult result = new BaseActionResult();
            strItemID = UC.Utilities.GetParameter("ItemGuid", parameters);
            strParentItemID = UC.Utilities.GetParameter("ParentItemGuid", parameters);
            result.Result = reportTemplateDirectoryService.MoveLeafNode(strItemID, strParentItemID, userguid, ref strNewTemplateName, ref strOrderIndex);
            result.ReturnString = strNewTemplateName + "|" + strOrderIndex;
            return result;
        }
    }

    

}
