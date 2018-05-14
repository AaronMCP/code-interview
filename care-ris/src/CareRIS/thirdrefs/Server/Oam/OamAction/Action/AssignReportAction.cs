using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kodak.GCRIS.Common.Action;
using Kodak.GCRIS.Server.Business.Oam;
using Kodak.GCRIS.Common.ActionResult;
using Kodak.GCRIS.Common.Utility;
using System.Data;
using Kodak.GCRIS.Common.GlobalSettings;
using System.Windows.Forms;
using Kodak.GCRIS.Common.Model.Report;
using RISGC.AssignmentCommon.Entity;

namespace Kodak.GCRIS.Server.OamAction.Action
{
    public class AssignReportAction : BaseAction
    {
        private IAssignReportService assignReportService = BusinessFactory.Instance.GetAssignReportService();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dtActionResult = new DataSetActionResult();
            string actionName = GCRIS.Common.Utility.Utilities.GetParameter("actionName", context.Parameters);            
            switch (actionName)
            {
                case "GetUnarrangedDoctorList":
                    try
                    {
                        string reportType = GCRIS.Common.Utility.Utilities.GetParameter("reportType", context.Parameters);   
                        DataSet ds = assignReportService.GetUnarrangedDoctorList(reportType);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetArrangedDoctorList":
                    try
                    {
                        string reportType = GCRIS.Common.Utility.Utilities.GetParameter("reportType", context.Parameters);
                        DataSet ds = assignReportService.GetArrangedDoctorList(reportType);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "InsertWriteReportDoctor":
                    try
                    {
                        WriteReportDoctor doctorModel = GenerateDoctorModel(context.Parameters);            
                        dtActionResult.Result = assignReportService.InsertWriteReportDoctor(doctorModel);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "RemoveWriteReportDoctor":
                    try
                    {
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        dtActionResult.Result = assignReportService.DeleteWriteReportDoctor(doctorGuid);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetAssignedReportCountByDoctor":
                    try
                    {
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        dtActionResult.ReturnString = assignReportService.GetAssignedReportCountByDoctor(doctorGuid).ToString();
                        dtActionResult.Result = true;
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "UpdatePreferredTypeByDoctor":
                    try
                    {
                        WriteReportDoctor doctorModel = GenerateDoctorModel(context.Parameters);            
                        dtActionResult.Result = assignReportService.UpdatePreferredTypeByDoctor(doctorModel);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetUnassignedReportList":
                    try
                    {
                        Dictionary<string, object> paramMap = ReportCommon.ReportCommon.GetParameters(context.Parameters);
                        paramMap.Add("UserID", context.UserID);
                        DataSet ds = assignReportService.GetUnwrittenReportList(paramMap, false);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                                                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetAssignedReportList":
                    try
                    {
                        Dictionary<string, object> paramMap = ReportCommon.ReportCommon.GetParameters(context.Parameters);
                        paramMap.Add("UserID", context.UserID);
                        DataSet ds = assignReportService.GetUnwrittenReportList(paramMap, true);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                                                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                                    Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetLockInfoByRPGuid":
                    try
                    {
                        string procedureGuid = GCRIS.Common.Utility.Utilities.GetParameter("procedureGuid", context.Parameters);
                        DataSet ds = assignReportService.GetLockInfoByRPGuid(procedureGuid);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "UpdateRPOwner":
                    try
                    {
                        UnwrittenReport unwrittenReport = GenerateUnwrittenReportModel(context.Parameters);
                        dtActionResult.Result = assignReportService.UpdateRPOwner(unwrittenReport);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetUnassignedUnwrittenReportCount":
                    try
                    {
                        dtActionResult.ReturnString = assignReportService.GetUnassignedUnwrittenReportCount().ToString();
                        dtActionResult.Result = true;
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetArrangedDoctorInfoByGuid":
                    try
                    {
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        dtActionResult.DataSetData = assignReportService.GetArrangedDoctorInfoByGuid(doctorGuid);
                        dtActionResult.Result = true;
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "UpdateWriteReportDoctorStatus":
                    try
                    {
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        bool canReceiveReport = Convert.ToBoolean(GCRIS.Common.Utility.Utilities.GetParameter("canReceiveReport", context.Parameters));
                        dtActionResult.Result = assignReportService.UpdateWriteReportDoctorStatus(doctorGuid, canReceiveReport);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "UpdateWriteReportDoctorIMStatus":
                    try
                    {
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        string imStatus = GCRIS.Common.Utility.Utilities.GetParameter("imStatus", context.Parameters);
                        dtActionResult.Result = assignReportService.UpdateWriteReportDoctorIMStatus(doctorGuid, imStatus);
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetLogProfile":
                    try
                    {
                        DateTime beginTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("beginTime", context.Parameters));
                        DateTime endTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("endTime", context.Parameters));
                        DataSet ds = assignReportService.GetLogProfile(beginTime, endTime);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetLogDetail":
                    try
                    {
                        DateTime beginTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("beginTime", context.Parameters));
                        DateTime endTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("endTime", context.Parameters));
                        string doctorGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", context.Parameters);
                        DataSet ds = assignReportService.GetLogDetail(doctorGuid, beginTime, endTime);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
                case "GetOperationHistory":
                    try
                    {
                        string operationType = GCRIS.Common.Utility.Utilities.GetParameter("operationType", context.Parameters);
                        string assignee = GCRIS.Common.Utility.Utilities.GetParameter("assignee", context.Parameters);
                        string assigner = GCRIS.Common.Utility.Utilities.GetParameter("assigner", context.Parameters);
                        DateTime beginTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("beginTime", context.Parameters));
                        DateTime endTime = DateTime.Parse(GCRIS.Common.Utility.Utilities.GetParameter("endTime", context.Parameters));
                        DataSet ds = assignReportService.GetOperationHistory(operationType, assigner, assignee, beginTime, endTime);
                        if (ds.Tables.Count > 0)
                        {
                            dtActionResult.Result = true;
                            dtActionResult.DataSetData = ds;
                        }
                        else
                        {
                            dtActionResult.Result = false;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(Convert.ToInt64(ModuleEnum.Oam_WS.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                           (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                        dtActionResult.Result = false;
                    }
                    break;
            }
            return dtActionResult;
        }

        private WriteReportDoctor GenerateDoctorModel(string parameters)
        {
            WriteReportDoctor doctor = new WriteReportDoctor();
            doctor.UserGuid = GCRIS.Common.Utility.Utilities.GetParameter("doctorGuid", parameters);
            doctor.Status = Convert.ToInt32(GCRIS.Common.Utility.Utilities.GetParameter("canReceiveReport", parameters)) == 1 ? AssignmentCommon.Constraints.DoctorStatus.Normal : AssignmentCommon.Constraints.DoctorStatus.RejectReport;
            doctor.Domain = GCRIS.Common.Utility.Utilities.GetParameter("domain", parameters);
            doctor.Name = GCRIS.Common.Utility.Utilities.GetParameter("localName", parameters);
            doctor.PreferredModalityType = GCRIS.Common.Utility.Utilities.GetParameter("preferredModalityType", parameters);
            doctor.PreferredPhysiologicalSystem = GCRIS.Common.Utility.Utilities.GetParameter("preferredPhysiologicalSystem", parameters);
            doctor.PreferredPatientType = GCRIS.Common.Utility.Utilities.GetParameter("preferredPatientType", parameters);
            doctor.MaxHoldUnwrittenReportCount = Convert.ToInt32(GCRIS.Common.Utility.Utilities.GetParameter("maxHoldUnwrittenReportCount", parameters));
            doctor.Supervisor = GCRIS.Common.Utility.Utilities.GetParameter("supervisor", parameters);
            return doctor;
        }

        private UnwrittenReport GenerateUnwrittenReportModel(string parameters)
        {
            UnwrittenReport report = new UnwrittenReport();
            report.CurrentOwner = GCRIS.Common.Utility.Utilities.GetParameter("currentOwner", parameters);
            report.PreviousOwner = GCRIS.Common.Utility.Utilities.GetParameter("previousOwner", parameters);
            report.ProcedureGuid = GCRIS.Common.Utility.Utilities.GetParameter("procedureGuid", parameters);
            report.Remark = GCRIS.Common.Utility.Utilities.GetParameter("remark", parameters);
            return report;
        }
    }
}
