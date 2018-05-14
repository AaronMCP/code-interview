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
using Common.Action;
using Server.Business.Oam;
using CommonGlobalSettings;
using Common.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using Server.Utilities.LogFacility;
namespace Server.OamAction.Action
{
    public class TemplateIEAction:BaseAction
    {
        private ITemplateIEService templateIEService = BusinessFactory.Instance.GetTemplateIEService();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dtActionResult = new DataSetActionResult();
            string action = context.MessageName;
            string strParameters = context.Parameters;
            DataTable tempTable = null;
            DataSet tempDataSet = null;
            switch (action)
            {
                case "GetAllPhraseTemplate":
                    {
                        try
                        {
                            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", strParameters);
                            string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            dtActionResult.DataSetData = templateIEService.GetAllPhraseTemplate(strUserGuid,Site);
                            dtActionResult.Result = true;
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "ImportPhraseTemplate":
                    {
                        try
                        {
                            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", strParameters);
                            string strIsClear = CommonGlobalSettings.Utilities.GetParameter("IsClear", strParameters);
                            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            bool isclear;
                            if (strIsClear == "0")
                            {
                                isclear = false;
                            }
                            else
                            {
                                isclear = true;
                            }
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            dtActionResult.Result = templateIEService.ImportPhraseTemplate(isclear,strUserGuid,model.DataSetParameter,strSite);
                            
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "GetAllPrintTemplate":
                    {
                        try
                        {
                            int type = Convert.ToInt32(CommonGlobalSettings.Utilities.GetParameter("Type", strParameters));
                            string site = Convert.ToString(CommonGlobalSettings.Utilities.GetParameter("Site", strParameters));
                           
                            dtActionResult.DataSetData = templateIEService.GetAllPrintTemplate(type,site);
                            dtActionResult.Result = true;

                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "ImportPrintTemplate":
                    {
                        try
                        {
                            string errorInfo = "";
                            int type = Convert.ToInt32(CommonGlobalSettings.Utilities.GetParameter("Type", strParameters));
                            string strIsClear = CommonGlobalSettings.Utilities.GetParameter("IsClear", strParameters);
                            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            bool isclear;
                            if (strIsClear == "0")
                            {
                                isclear = false;
                            }
                            else
                            {
                                isclear = true;
                            }
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            dtActionResult.Result = templateIEService.ImportPrintTemplate(isclear,type,model.DataSetParameter,strSite,ref errorInfo);
                            dtActionResult.ReturnMessage = errorInfo;
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "GetAllReportTemplate":
                    {
                        try
                        {
                            string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            dtActionResult.DataSetData = templateIEService.GetALLReportTemplate(Site);
                            dtActionResult.Result = true;
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "ImportReportTemplate":
                    {
                        try
                        {
                            string strIsClear = CommonGlobalSettings.Utilities.GetParameter("IsClear", strParameters);
                            string strSite = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            bool isclear;
                            if (strIsClear == "0")
                            {
                                isclear = false;
                            }
                            else
                            {
                                isclear = true;
                            }
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            dtActionResult.Result = templateIEService.ImportReportTemplate(isclear,model.DataSetParameter,strSite);

                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }


                    }
                    break;
                case "ImportBookingNoticeTemplate":
                    {
                        try
                        {
                            string strIsClear = CommonGlobalSettings.Utilities.GetParameter("IsClear", strParameters);
                            bool isclear;
                            if (strIsClear == "0")
                            {
                                isclear = false;
                            }
                            else
                            {
                                isclear = true;
                            }
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            dtActionResult.Result = templateIEService.ImportBookingNoticeTemplate(isclear, model.DataSetParameter);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }
                    }
                    break;
                case "GetAllBookingNoticeTemplate":
                    {
                        try
                        {
                            dtActionResult.DataSetData = templateIEService.GetAllBookingNoticeTemplate();
                            dtActionResult.Result = true;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                              (new System.Diagnostics.StackFrame(true)).GetFileName(),
                               Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                            dtActionResult.Result = false;
                        }
                    }
                    break;
            }

            return dtActionResult;
        }
    }
}
