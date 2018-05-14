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
    public class ACRCodeAction : BaseAction
    {
        private IACRCodeService acrCodeService = BusinessFactory.Instance.GetACRCodeService();
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
                case "ImportProcedureCode":
                    {
                        try
                        {
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            bool isClear = false;
                            string strClear = CommonGlobalSettings.Utilities.GetParameter("IsClear", strParameters);
                            string site = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            bool.TryParse(strClear, out isClear);
                            dtActionResult.Result = acrCodeService.ImportProcedureCode(model.DataSetParameter, isClear,site);
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

                case "ImportProcedureCodeXls":
                    {
                        try
                        {
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            string errorStr = "";
                            int errorCode = -1;
                            string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            dtActionResult.Result = acrCodeService.ImportProcedureCode(model.DataSetParameter,ref errorCode,ref errorStr ,Site);
                            dtActionResult.recode = errorCode;
                            dtActionResult.ReturnMessage = errorStr;
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
                #region Modified by Blue Chen for US19895, 10/30/2014
                case "ImportBodyPartSystemMapXls":
                    try
                    {
                        BaseDataSetModel model = context.Model as BaseDataSetModel;
                        string errorStr = "";
                        int errorCode = -1;
                        dtActionResult.Result = acrCodeService.ImportBodyPartSystemMap(model.DataSetParameter, ref errorCode, ref errorStr);
                        dtActionResult.recode = errorCode;
                        dtActionResult.ReturnMessage = errorStr;
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                #endregion
                case "ImportACRCode":
                    {
                        try
                        {
                            BaseDataSetModel model = context.Model as BaseDataSetModel;
                            bool isClear;
                            if (strParameters == "0")
                                isClear = false;
                            else
                                isClear = true;
                            dtActionResult.Result = acrCodeService.ImportACRcode(model.DataSetParameter,isClear);
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
                case "GetAllACRCode":
                    {
                        try
                        {
                            dtActionResult.DataSetData = acrCodeService.GetAllAcrCode();
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
                case "GetAllUser":
                    {
                        try
                        {
                            dtActionResult.DataSetData = acrCodeService.GetAllUser();
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
                case "GetAllProcedureCodeForExport":
                    {
                        try
                        {
                            string site = CommonGlobalSettings.Utilities.GetParameter("Site", strParameters);
                            dtActionResult.DataSetData = acrCodeService.GetAllProcedureCode(site);
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
                case "GetACRCodeDesc":
                    {
                        try
                        {
                            string strACRCode = CommonGlobalSettings.Utilities.GetParameter("ACRCode", strParameters);
                            dtActionResult.ReturnMessage = acrCodeService.GetACRCodeDesc(strACRCode);
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                               (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }


                    }
                    break;
                case "SearchACRCode":
                    {
                        try
                        {
                            string strADesc = CommonGlobalSettings.Utilities.GetParameter("ADesc", strParameters);
                            string strPDesc = CommonGlobalSettings.Utilities.GetParameter("PDesc", strParameters);
                            string strACRCode = CommonGlobalSettings.Utilities.GetParameter("ACRCode", strParameters);
                            tempTable = acrCodeService.SearchACRCode(strADesc, strPDesc, strACRCode);
                            tempDataSet = new DataSet();
                            tempDataSet.Tables.Add(tempTable);
                            dtActionResult.DataSetData = tempDataSet;
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;

            }

            return dtActionResult;
        }
    }
}
