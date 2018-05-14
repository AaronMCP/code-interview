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
using Common.Action;
using Common.ActionResult;
using Server.Business.Templates;
using System.Data;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using CommonGlobalSettings;
using CommonGlobalSettings;
using UC=CommonGlobalSettings;
using Server.Utilities.LogFacility;
namespace Server.TemplatesActions.Action
{
    public class PrintTemplateAction:BaseAction
    {
        private IPrintTemplateService printTemplateService = BusinessFactory.Instance.GetPrintTemplateService();
        LogManagerForServer logger = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {

                switch (context.MessageName.Trim())
                {
                    case"LoadPrintTemplateInfo":
                        return LoadPrintTemplateInfo(context.Parameters);
                    case "AddPrintTemplate":
                        return AddPrintTemplate(context.Model as BaseDataSetModel);
                    case "DeletePrintTemplate":
                        return DeletePrintTemplate(context.Parameters);
                    case "LoadPrintTemplateType":
                        return LoadPrintTemplateType();
                    case "LoadSubPrintTemplateInfo":
                        return LoadSubPrintTemplateInfo(context.Parameters);
                    case "SetDefault":
                        return SetDefault(context.Parameters);
                    case "LoadPrintTemplateField":
                        return LoadPrintTemplateField(context.Parameters);
                    case "ModifyPrintTemplateFieldInfo":
                        return ModifyPrintTemplateFieldInfo(context.Model as BaseDataSetModel);
                    case "ModifyPrintTemplateName":
                        return ModifyPrintTemplateName(context.Model as BaseDataSetModel);
                    case "GetLatestVersion":
                        return GetLatestVersion(context.Parameters);
                    case "IsPrintTemplateSameName":
                        return IsPrintTemplateSameName(context.Model as BaseDataSetModel);
                    case "GetDefaultPrintTemplate":
                        return GetDefaultPrintTemplate(context.Parameters);
                    case "LoadPrintTemplateByName":
                        return LoadPrintTemplateInfoByName(context.Model as BaseDataSetModel);
                    case "LoadGeneralStatType":
                        return LoadGeneralStatType();
                    case "LoadExportTemplateType":
                        return LoadExportTemplateType();
                    case "LoadSubExportTemplateInfo":
                        return LoadSubExportTemplateInfo();
                    case "LoadExportTemplateInfo":
                        return LoadExportTemplateInfo(context.Parameters);
                    case "AddExportTemplate":
                        return AddExportTemplate(context.Model as BaseDataSetModel);
                    case "ModifyExportTemplate":
                        return ModifyExportTemplate(context.Model as BaseDataSetModel);
                    case "ModifyExportTemplateInfo":
                        return ModifyExportTemplateInfo(context.Model as BaseDataSetModel);
                    case "DeleteExportTemplate":
                        return DeleteExportTemplate(context.Parameters);
                    case "GetPrintTemplateNameByGuid":
                        return GetPrintTemplateNameByGuid(context.Parameters);
                    case "GetPrintData":
                        return GetPrintData(context.Parameters);
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

        private DataSetActionResult GetPrintData(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string accno = UC.Utilities.GetParameter("Accno", parameters);
            string modalityType = UC.Utilities.GetParameter("ModalityType", parameters);
            string templateType = UC.Utilities.GetParameter("TemplateType", parameters);
            DataTable data = new DataTable();
            string template = "";
            printTemplateService.GetPrintData(accno, modalityType, templateType, ref template, ref data);

            if (!string.IsNullOrWhiteSpace(template) && data != null && data.Rows.Count > 0)
            {
                result.ReturnString = template;
                result.DataSetData = new DataSet();
                DataTable dt = data.Copy();
                dt.Rows[0]["Image"] = null;
                dt.TableName = "PrintData";
                result.DataSetData.Tables.Add(dt);
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        //The follow functions have same comments as commented in AbstractDBProvider
        private BaseActionResult LoadPrintTemplateInfoByName(BaseDataSetModel model)
        {
            DataTable myTable = model.DataSetParameter.Tables[0];
            string strTemplateName = Convert.ToString(myTable.Rows[0]["TemplateName"]);
            int type = Convert.ToInt32(myTable.Rows[0]["Type"]);
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadPrintTemplateByName(type,strTemplateName);
            if (myDataSet != null && myDataSet.Tables != null && myDataSet.Tables[0].Rows.Count >0)
            {
                result.DataSetData = myDataSet;
                result.ReturnString = printTemplateService.GetTypeDesc(type);
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
         
        }
        private BaseActionResult GetDefaultPrintTemplate(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();          
            DataSet myDataSet = printTemplateService.GetDefaultPrintTemplate();
            if (myDataSet != null&&myDataSet.Tables!=null&&myDataSet.Tables[0].Rows.Count>0)
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
        private BaseActionResult IsPrintTemplateSameName(BaseDataSetModel model)
        {
            DataTable myTable = model.DataSetParameter.Tables[0];

            string strTemplateName = Convert.ToString(myTable.Rows[0]["TemplateName"]);
            int type = Convert.ToInt32(myTable.Rows[0]["Type"]);
            string site = string.Empty;
            if (myTable.Columns.Contains("Site"))
            {
                site = Convert.ToString(myTable.Rows[0]["Site"]);
            }
            BaseActionResult result = new BaseActionResult();
            if (printTemplateService.IsPrintTemplateSameName(strTemplateName, type, site))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }
        private BaseActionResult GetLatestVersion(string parameters)
        {
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            BaseActionResult result = new BaseActionResult();
            int latestVersion = printTemplateService.GetLatestVersion(strTemplateGuid);
            if (latestVersion!=-1)
            {
                result.ReturnString = latestVersion.ToString();
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }
        private BaseActionResult ModifyPrintTemplateName(BaseDataSetModel model)
        {
            string strTemplateGuid = Convert.ToString(model.DataSetParameter.Tables[0].Rows[0]["TemplateGuid"]);
            string strTemplateName = Convert.ToString(model.DataSetParameter.Tables[0].Rows[0]["TemplateName"]);

            string strAction = string.Empty;
            string strPropertyTag = string.Empty;

            if(model.DataSetParameter.Tables[0].Columns.Contains("Action"))
                strAction = Convert.ToString(model.DataSetParameter.Tables[0].Rows[0]["Action"]);

            if (model.DataSetParameter.Tables[0].Columns.Contains("PropertyTag"))
                strPropertyTag = Convert.ToString(model.DataSetParameter.Tables[0].Rows[0]["PropertyTag"]);

            BaseActionResult result = new BaseActionResult();
            result.Result = false;

            if ("1" == strAction)
            {
                // Modify the property tag
                if (printTemplateService.ModifyPrintTemplatePropertyTag(strTemplateGuid, strPropertyTag))
                {
                    result.Result = true;
                }
            }
            else if (printTemplateService.ModifyPrintTemplateName(strTemplateGuid, strTemplateName))
            {
                // Modify the name
                result.Result = true;
            }

            return result;
        }
        private BaseActionResult ModifyPrintTemplateFieldInfo(BaseDataSetModel model)
        {

            string strTemplateGuid =Convert.ToString( model.DataSetParameter.Tables[0].Rows[0]["TemplateGuid"]);
            string strTemplateInfo = Convert.ToString(model.DataSetParameter.Tables[0].Rows[0]["TemplateInfo"]);
            BaseActionResult result = new BaseActionResult();
            if (printTemplateService.ModifyPrintTemplateFieldInfo(strTemplateGuid, strTemplateInfo))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }
            return result;
        }
        private BaseActionResult LoadPrintTemplateField(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strType = UC.Utilities.GetParameter("Type", parameters);
            DataSet myDataSet = printTemplateService.LoadPrintTemplateField(Convert.ToInt32(strType));
            if (myDataSet!=null)
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
        private BaseActionResult LoadPrintTemplateInfo(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            result.ReturnString  = printTemplateService.LoadPrintTemplateInfo(strTemplateGuid);
            if (result.ReturnString == null )
            {
                result.Result = false;
               
            }
            else
            {
                result.Result = true;
            }

            return result;
        }
        private BaseActionResult LoadSubPrintTemplateInfo(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strType = UC.Utilities.GetParameter("Type", parameters);
            DataSet myDataSet = printTemplateService.LoadSubPrintTemplateInfo(Convert.ToInt32(strType));
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
        private BaseActionResult AddPrintTemplate( BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            if(model!=null&&model.DataSetParameter.Tables.Count == 1)
            {
                
                if (printTemplateService.AddPrintTemplate(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }

                return result;
            }
            else
            {
               result.Result = false;
                return result;
            }
        }
        private BaseActionResult DeletePrintTemplate(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            if (printTemplateService.DeletePrintTemplate(strTemplateGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private BaseActionResult LoadPrintTemplateType()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadPrintTemplateType();
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

        private BaseActionResult SetDefault(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strType = UC.Utilities.GetParameter("Type", parameters);
            string strModalityType = UC.Utilities.GetParameter("ModalityType", parameters);
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            string site = UC.Utilities.GetParameter("Site", parameters);
            bool bReturn = true;
            if (strType == "")
                bReturn = printTemplateService.SetDefault(strTemplateGuid, site);
            else
            {
                int type = Convert.ToInt32(strType);
                bReturn = printTemplateService.SetDefault(type, strModalityType, strTemplateGuid, site);
            }

            if (bReturn)
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult LoadGeneralStatType()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadGeneralStatType();
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
        private BaseActionResult LoadExportTemplateType()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadExportTemplateType();
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

        private BaseActionResult LoadSubPrintTemplateInfo()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadSubExportTemplateInfo();
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
        private BaseActionResult LoadExportTemplateInfo(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            DataSet ds = printTemplateService.LoadExportTemplateInfo(strTemplateGuid);
            if (ds.Tables[0].Rows.Count>0)
            {
                result.DataSetData = ds;
                result.Result = true;

            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult LoadSubExportTemplateInfo()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = printTemplateService.LoadSubExportTemplateInfo();
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
        private BaseActionResult AddExportTemplate(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            if (model != null && model.DataSetParameter.Tables.Count == 1)
            {

                if (printTemplateService.AddExportTemplate(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }

                return result;
            }
            else
            {
                result.Result = false;
                return result;
            }
        }

        private BaseActionResult ModifyExportTemplate(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            if (model != null && model.DataSetParameter.Tables.Count == 1)
            {

                if (printTemplateService.ModifyExportTemplate(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }

                return result;
            }
            else
            {
                result.Result = false;
                return result;
            }
        }
        private BaseActionResult ModifyExportTemplateInfo(BaseDataSetModel model)
        {
            BaseActionResult result = new BaseActionResult();
            if (model != null && model.DataSetParameter.Tables.Count == 1)
            {

                if (printTemplateService.ModifyExportTemplateInfo(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }

                return result;
            }
            else
            {
                result.Result = false;
                return result;
            }
        }

        private BaseActionResult DeleteExportTemplate(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            if (printTemplateService.DeleteExportTemplate(strTemplateGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult GetPrintTemplateNameByGuid(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            string strPrintTemplateName = "";
            strPrintTemplateName = printTemplateService.GetPrintTemplateNameByGuid(strTemplateGuid);
            result.ReturnString = strPrintTemplateName;
            return result;
        }
    }
}
