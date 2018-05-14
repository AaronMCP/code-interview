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
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using Server.Utilities.LogFacility;

namespace Server.OamAction.Action
{
    public class AnatomyAction:BaseAction
    {
        private IAnatomyService anatomyService = BusinessFactory.Instance.GetAnatomyService();
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
                case "LoadMainAnatomy":
                    {
                        try
                        {
                            tempTable = anatomyService.LoadMainAnatomy();
                            tempDataSet = new DataSet();
                            tempDataSet.Tables.Add(tempTable);
                            dtActionResult.DataSetData = tempDataSet;
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName().ToString(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "LoadSubAnatomy":
                    {
                        try
                        {
                            int aid = int.Parse(CommonGlobalSettings.Utilities.GetParameter("AID", strParameters).Trim());
                            tempTable = anatomyService.LoadSubAnatomy(aid);
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
                case "AddNewAnatomy":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameters);
                            dtActionResult.Result = anatomyService.AddNewAnatomy(strAID, strSID, strDESC, null, strDomain);
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }


                    }
                    break;
                case "AddNewAnatomyStorProc":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameters);
                            dtActionResult.ReturnMessage = anatomyService.AddNewAnatomyStorProc(strAID, strDESC, null,strDomain);
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "UpdateMainAnatomy":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            dtActionResult.Result = anatomyService.UpdateMainAnatomy(strAID, strDESC, null);
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "SameAnatomyDesc")
                                throw e;
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "UpdateSubAnatomy":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            dtActionResult.Result = anatomyService.UpdateSubAnatomy(strAID, strSID, strDESC, null);
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "SameAnatomyDesc")
                                throw e;
                           
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "DeleteAnatomy":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strTemp= null;
                            dtActionResult.Result = anatomyService.DeleteAnatomy(strAID, strSID, out strTemp);
                            dtActionResult.ReturnMessage = strTemp;
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
