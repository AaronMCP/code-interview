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
using Common.Action;
using Server.Business.Oam;
using Common.ActionResult;
using CommonGlobalSettings;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;

using Server.Utilities.LogFacility;
namespace Server.OamAction.Action
{
    public class PathologyAction : BaseAction
    {
        IPathologyService pathologyService = BusinessFactory.Instance.GetPathologyService();
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
                case "LoadMainPathology":
                    {
                        try
                        {
                            int aid = int.Parse(CommonGlobalSettings.Utilities.GetParameter("AID", strParameters).Trim());
                            tempTable = pathologyService.LoadMainPathology(aid);
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
                case "LoadSubPathology":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            tempTable = pathologyService.LoadSubPathology(int.Parse(strAID.Trim()), int.Parse(strPID.Trim()));
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
                case "AddNewPathology":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameters);
                            dtActionResult.Result = pathologyService.AddNewPathology(strAID, strPID, strSID, strDESC, null,strDomain);
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }


                    }
                    break;
                case "AddNewPathologyStorProc":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", strParameters);
                            dtActionResult.ReturnMessage = pathologyService.AddNewPathologyStorProc(strAID, strPID, strDESC, null,strDomain);
                        }
                        catch (Exception e)
                        {
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "UpdateMainPathology":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            dtActionResult.Result = pathologyService.UpdateMainPathology(strAID, strPID, strDESC, null);
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "SamePathologyDesc")
                                throw e;
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "UpdateSubPathology":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strDESC = CommonGlobalSettings.Utilities.GetParameter("DESC", strParameters);
                            dtActionResult.Result = pathologyService.UpdateSubPathology(strAID, strPID, strSID, strDESC, null);

                        }
                        catch (Exception e)
                        {
                            if (e.Message == "SamePathologyDesc")
                                throw e;
                            logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        }

                    }
                    break;
                case "DeletePathology":
                    {
                        try
                        {
                            string strAID = CommonGlobalSettings.Utilities.GetParameter("AID", strParameters);
                            string strPID = CommonGlobalSettings.Utilities.GetParameter("PID", strParameters);
                            string strSID = CommonGlobalSettings.Utilities.GetParameter("SID", strParameters);
                            string strTemp = null;
                            dtActionResult.Result = pathologyService.DeletePathology(strAID, strPID, strSID,out strTemp);
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
