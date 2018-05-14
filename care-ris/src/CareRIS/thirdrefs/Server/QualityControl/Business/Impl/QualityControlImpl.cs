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
/*                                                                          */
/*   Author : Paul Li                                                       */
/****************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

using Server.DAO.QualityControl;
using LogServer;
using CommonGlobalSettings;

using Common.ActionResult;
using Server.Utilities.HippaLogTool;
using Server.Utilities.LogFacility;
using CommonGlobalSettings.HippaName;

namespace Server.Business.QualityControl.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class QualityControlImpl : IQualityControlBusiness
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("QualityControlServerLoglevel", "0D00");

        #region Query
        public int QueryPatient(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryPatient(strPatientName, strPatientID, strBeginDt, strEndDt, bIsVIP, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return dsar.recode;
        }

        public int QueryPatientList(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryPatientList(strPatientName, strPatientID, strBeginDt, strEndDt, bIsVIP, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return dsar.recode;
        }

        /// <summary>
        /// Query list of orders belong to one patient
        /// </summary>
        /// <param name="strOrderGUID"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int QueryOrder(string strOrderGUID, string status, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    if (!dbProvider.QueryOrder(strOrderGUID, dsar.DataSetData, ref strError))
                    {
                        throw new Exception(strError);
                    }
                }
                else
                {
                    if (!dbProvider.QueryOrder(strOrderGUID, dsar.DataSetData, status, ref strError))
                    {
                        throw new Exception(strError);
                    }
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        /// <summary>
        /// Query list of RP belong to one order
        /// </summary>
        /// <param name="strRPGUID"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int QueryRP(string strRPGUID, BaseActionResult bar, string strRPguid)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryRP(strRPGUID, dsar.DataSetData, ref strError, ref strRPguid))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        public int QueryRPwithDuplicatedReport(bool bExcludeDeletedReport, string pID, string patientName, string dt1, string dt2, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryRPwithDuplicatedReport(bExcludeDeletedReport, pID, patientName, dt1, dt2, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        public int QueryDuplicatedReport(string rpGuid, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryDuplicatedReport(rpGuid, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        #endregion
        #region Update
        /// <summary>
        /// Update one patient with data persists in input dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int UpdatePatient(bool SendToGateWay, DataSet ds, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                DataTable dtPatient = ds.Tables["Patient"];
                if (dtPatient.Rows.Count == 0)
                {
                    throw new Exception("Patient for updating is not found at server side");
                }
                else
                    if (dtPatient.Rows.Count > 1)
                    {
                        throw new Exception("Only one patient can be updated at one time");
                    }
                DataRow drPatient = dtPatient.Rows[0];
                string strPatientID = drPatient["PatientID"].ToString();
                string strPatientName = drPatient["LocalName"].ToString();

                //first update the data
                if (!dbProvider.UpdatePatient(SendToGateWay, "", ds, ref strError))
                {
                    throw new Exception(strError);
                }
                //audit hippa
                //HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.Update, strPatientID, strPatientName, "Update Patient information in QC module");
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;


                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }

            return bar.recode;
        }
        public int UpdateOrder(bool SendToGateWay, DataSet ds, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            string strAccNo = string.Empty;
            string strOrderGUID = string.Empty;
            string strPatientGUID = "", strPatientName = "";
            try
            {
                DataTable dtOrder = ds.Tables["Order"];
                if (dtOrder.Rows.Count == 0)
                {
                    throw new Exception("Order for updating is not found at server side");
                }
                else
                    if (dtOrder.Rows.Count > 1)
                    {
                        throw new Exception("Only one Order can be updated at one time");
                    }
                DataRow drOrder = dtOrder.Rows[0];
                strAccNo = drOrder["AccNo"].ToString();
                strOrderGUID = drOrder["OrderGuid"].ToString();
                //read patient name for HIPPA
                if (!dbProvider.QueryPatientWithOrderID(strOrderGUID, ref strPatientGUID, ref strPatientName, ref strError))
                {
                    throw new Exception(strError);
                }

                if (!dbProvider.UpdateOrder(SendToGateWay, "", ds, ref strError))
                {
                    throw new Exception(strError);
                }

                //audit HIPPA
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Update, strAccNo, strPatientGUID, strPatientName, "Update Order information in QC module", true);

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Update, strAccNo, strPatientGUID, strPatientName, "Update Order information in QC module", false);
            }

            return bar.recode;
        }
        public int UpdateRP(bool SendToGateWay, DataSet ds, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            string strAccNo = string.Empty;
            string strRP = string.Empty;
            string strOrderGUID = string.Empty;
            string strPatientGUID = "", strPatientName = "";
            try
            {
                DataTable dtRP = ds.Tables["RP"];
                if (dtRP.Rows.Count == 0)
                {
                    throw new Exception("Procedure for updating is not found at server side");
                }
                else
                    if (dtRP.Rows.Count > 1)
                    {
                        throw new Exception("Only one Procedure can be updated at one time");
                    }
                DataRow drRP = dtRP.Rows[0];
                strAccNo = drRP["AccNo"].ToString();
                strRP = drRP["Description"].ToString();
                strOrderGUID = drRP["OrderGUID"].ToString();
                //read patient name for HIPPA
                if (!dbProvider.QueryPatientWithOrderID(strOrderGUID, ref strPatientGUID, ref strPatientName, ref strError))
                {
                    throw new Exception(strError);
                }
                //update RP
                if (!dbProvider.UpdateRP(SendToGateWay, "", ds, ref strError))
                {
                    throw new Exception(strError);
                }

                //audit HIPPA
                HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.Update, strAccNo, strRP, strPatientGUID, strPatientName, "Update Order information in QC module", true);

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.Update, strAccNo, strRP, strPatientGUID, strPatientName, "Update Order information in QC module", false);
            }

            return bar.recode;
        }
        public int UpdateActiveReport(string reportGuid, BaseActionResult bar)
        {
            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                if (!dbProvider.UpdateActiveReport(reportGuid, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bar.recode;
        }
        #endregion
        #region delete
        public int DeleteOrder(bool SendToGateWay, string strOrderGuid, string strLoginName, string strLocalName, BaseActionResult bar)
        {
            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                //string strLockInfo = "";
                string strRPGuid = "";
                if (!dbProvider.DeleteOrder(SendToGateWay, strOrderGuid,strLoginName, strLocalName, ref strError, ref strRPGuid))
                {
                    bar.ReturnMessage = bar.ReturnString = strError;
                    bar.recode = -1;
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return bar.recode;
        }
        #region EK_HI00063904 jameswei 2007-12-13 get the acc's requisition file name and relativepath
        public int GetRequisitionInfo(string strAccNo, BaseActionResult bar)
        {
            bar.recode = 0;
            bar.Result = true;
            DataSetActionResult dsar = bar as DataSetActionResult;
            try
            {
                if (!dbProvider.GetRequisitionInfo(strAccNo, dsar.DataSetData))
                {
                    bar.ReturnMessage = bar.ReturnString = "error GetRequisitionInfo";
                    bar.recode = -1;
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return bar.recode;
        }
        #endregion
        public int DeleteRP(bool sendToGW, string strVisitGuid, string strOrderGuid, string strRPs, string strLoginName, string strLocalName, BaseActionResult bar)
        {
            //string strError = "";

            try
            {
               // string strLockInfo = "";
                if (dbProvider.DeleteRP(sendToGW, strVisitGuid, strOrderGuid, strRPs,strLoginName, strLocalName,bar))
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return -1;
        }
        public int DeletePatient(bool sendToGateway, string patientGuid)
        {
            //string strError = "";

            try
            {
               // string strLockInfo = "";
                if (dbProvider.DeletePatient(sendToGateway, patientGuid))
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return -1;
        }
        #endregion
        #region Merge
        //
        public int MergePatients(bool SendToGateWay, Dictionary<int, string> m_dicPatients, string bDelAfterMerge, BaseActionResult bar)
        {
            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            string strPatientsList = "";
            string strTargetPatientGUID = "";
            string strTargetPatientName = "";
            try
            {               
                if (!dbProvider.MergePatients(SendToGateWay, m_dicPatients, bDelAfterMerge, ref strTargetPatientGUID, ref strTargetPatientName, ref strPatientsList, ref strError))
                {
                    throw new Exception(strError);
                }

                HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.MergePatient, strTargetPatientGUID, strTargetPatientName, string.Format("{0} are merged to {1}", strPatientsList, strTargetPatientName), true);

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.MergePatient, strTargetPatientGUID, strTargetPatientName, string.Format("{0} are merged to {1}", strPatientsList, strTargetPatientName), false);
            }

            return bar.recode;
        }
        //
        public int AssignOrderToPatient(bool SendToGateWay, string stVisitGUID, string strOrderGuid, string strPatientID, string bDelAfterMerge, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            string strPatientGUID = "", strPatientName = "";        //data of first patient
            string strPatientGUID2 = "", strPatientName2 = "";      //data of second patient
            try
            {                
                DataSet dsPatient = new DataSet();
                //get data of first patient
                if (dbProvider.QueryPatientWithVisitGUID(stVisitGUID, dsPatient))
                {
                    DataTable dtPatient = dsPatient.Tables["Patient"];
                    if (dtPatient != null)
                    {
                        DataRow drPatient = dtPatient.Rows[0];
                        if (drPatient != null)
                        {
                            strPatientGUID = drPatient["PatientGuid"].ToString();
                            strPatientName = drPatient["LocalName"].ToString();
                        }
                    }
                }

                //do merge
                if (!dbProvider.AssignOrderToPatient(SendToGateWay, stVisitGUID, strOrderGuid, strPatientID, bDelAfterMerge, ref strError))
                {
                    throw new Exception(strError);
                }

                //audit HIPPA
                //audit separate
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Separate, stVisitGUID, strPatientGUID, strPatientName,
                    string.Format("Seperate visit {0} from patient {1}, visit will be merged to patient {2}", stVisitGUID, strPatientGUID, strPatientID), true);

                dsPatient.Tables.Clear();
                dsPatient.Clear();
                //get data of second patient
                if (dbProvider.QueryPatientWithVisitGUID(stVisitGUID, dsPatient))
                {
                    DataTable dtPatient = dsPatient.Tables["Patient"];
                    if (dtPatient != null)
                    {
                        DataRow drPatient = dtPatient.Rows[0];
                        if (drPatient != null)
                        {
                            strPatientGUID2 = drPatient["PatientGuid"].ToString();
                            strPatientName2 = drPatient["LocalName"].ToString();
                        }
                    }
                }
                //audit merge
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.MergeVisit, stVisitGUID, strPatientGUID2, strPatientName2,
                    string.Format("Merge visit {0} to patient {2}, the Visit is from patient {1} ", stVisitGUID, strPatientGUID, strPatientGUID2), true);
                //audit delete
                if (bDelAfterMerge == "True")
                {
                    if (!dbProvider.PatientExist(strPatientGUID, ref strError))
                    //patient already removed
                    {
                        HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.Delete, strPatientGUID, strPatientName,
                            string.Format("Delete Patient {0}  after all Visits are seperated", stVisitGUID), true);
                    }
                }
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Separate, stVisitGUID, strPatientGUID, strPatientName,
    string.Format("Seperate visit {0} from patient {1}, visit will be merged to patient {2}", stVisitGUID, strPatientGUID, strPatientID), true);
            }

            return bar.recode;
        }
        #endregion
        #region lock

        public int QueryLock(string strObjectType, string strObjectGuid, BaseActionResult bar)
        {
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                dbProvider.QueryLock(strObjectType, strObjectGuid, dsar.DataSetData);
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        public int QueryLock(string stOwner, string stBeginTime, string stEndTime, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryLock(stOwner, stBeginTime, stEndTime, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }
        public int LockObject(int nObjectType, string strObjectGuid, string strOwner, string strOwnerIP, BaseActionResult bar)
        {
            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                string strLockInfo = "";
                if (!dbProvider.LockObject(nObjectType, strObjectGuid, strOwner, strOwnerIP, ref strLockInfo, ref strError))
                {
                    bar.ReturnString = strLockInfo;
                    bar.recode = -1;
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return bar.recode;
        }

        public int UnLockObject(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                if (!dbProvider.UnLockObject(strObjectType, nSyncType, strObjectGuid, strOwner, ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return bar.recode;
        }

        /// <summary>
        /// merge all gived orders's rps to target order and delete the gived orders
        /// </summary>
        /// <param name="dicOrders">the orders to be merged</param>
        /// <param name="bDelAfterMerge">current always true</param>
        /// <param name="strTargetOrderGUID">the target order to be merged to</param>
        /// <param name="bSendToGateWay">whether to send to gateway</param>
        /// <param name="bChargeFuncActive">whether to update charge associted info</param>/// 
        /// <param name="dsar"> DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergeOrders(Dictionary<string, string> dicOrders, string bDelAfterMerge, string strTargetOrderGUID, string strTargetAccessionNumber, string stringSrcPatientName, bool bSendToGateWay, bool bChargeFuncActive, DataSetActionResult dsar)
        {
            string strError = "";
            DataTable dt = new DataTable();
            dsar.recode = 0;
            dsar.Result = true;
            string orderID = string.Empty;
            try
            {
                if (dbProvider.ExistQuashStatus(null, dicOrders, strTargetOrderGUID, ref strError))
                {
                    dsar.recode=1;                    
                    dsar.ReturnMessage = "Can not merge this order due to quash status";
                    dsar.Result = false;
                    return false;
                }

                string strOrderList = "";
                if (!dbProvider.MergeOrders(dicOrders, bDelAfterMerge, strTargetOrderGUID, bSendToGateWay, bChargeFuncActive, ref strError, ref dt))
                {
                    throw new Exception(strError);
                }
                foreach (string strOrderGuid in dicOrders.Keys)
                {
                    orderID = strOrderGuid;
                    strOrderList += strOrderGuid + ",";
                    HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.MergeOrder, dicOrders[strOrderGuid], "", stringSrcPatientName, string.Format("{0} are merged to {1}", dicOrders[strOrderGuid], strTargetAccessionNumber), true);
                }
                strOrderList.TrimEnd(",".ToCharArray());
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.MergeOrder, dicOrders[orderID], "", stringSrcPatientName, string.Format("{0} are merged to {1}", dicOrders[orderID], strTargetAccessionNumber), false);
            }
            dsar.DataSetData.Tables.Add(dt);
            dsar.ReturnString = strError;

            return true;
        }

        //merge some source patients to one target patient
        /// <summary>
        /// merge patients to target patient
        /// </summary>
        /// <param name="dicPatients">the source patients to be merged</param>
        /// <param name="bDelAfterMerge">wether delete the source patients after merge</param>
        /// <param name="strTargetPatientGUID">the destination patient guid to merged to</param>
        /// <param name="strTargetPatientName">the destination patient name to merged to,use for hippa</param>/// 
        /// <param name="bSendToGateWay">wether to notify gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergePatients(Dictionary<string, string> dicPatients, string bDelAfterMerge, string strTargetPatientGUID, string strTargetPatientName, bool bSendToGateWay, DataSetActionResult dsar)
        {
            string strError = "";
            dsar.recode = 0;
            dsar.Result = true;
            string strPatientsList = "";
            try
            {

                if (!dbProvider.MergePatients(dicPatients, bDelAfterMerge, strTargetPatientGUID, bSendToGateWay, ref strError))
                {
                    throw new Exception(strError);
                }
                foreach (string strPatientGuid in dicPatients.Keys)
                {
                    strPatientsList += strPatientGuid + ",";
                }
                strPatientsList.TrimEnd(",".ToCharArray());
                HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.MergePatient, strTargetPatientGUID, strTargetPatientName, string.Format("{0} are merged to {1}", strPatientsList, strTargetPatientName), true);
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.MergePatient, strTargetPatientGUID, strTargetPatientName, string.Format("{0} are merged to {1}", strPatientsList, strTargetPatientName), false);

            }
            dsar.ReturnString = strError;//if have some sync check false
            return true;
        }
        /// <summary>
        /// move orders to a target patient
        /// </summary>
        /// <param name="dicOrders">the orders' guid dictionary</param>
        /// <param name="strTargetPatientGUID">the target patient guid</param>
        /// <param name="strTargetPatientName">the destination patient name to merged to,use for hippa</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MoveOrders(Dictionary<string, string> dicOrders, string strTargetPatientGUID, string strSrcPatientName, string strTargetPatientName, bool bSendToGateWay, DataSetActionResult dsar)
        {
            string strError = "";
            DataTable dt = new DataTable();
            dsar.recode = 0;
            dsar.Result = true;
            string orderID = string.Empty;
            try
            {
                string strOrderList = "";
                if (!dbProvider.MoveOrders(dicOrders, strTargetPatientGUID, bSendToGateWay, ref strError))
                {
                    throw new Exception(strError);
                }
                foreach (string strOrderGuid in dicOrders.Keys)
                {
                    orderID = strOrderGuid;
                    strOrderList += strOrderGuid + ",";
                    HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.MoveOrder, dicOrders[strOrderGuid], "", strSrcPatientName, string.Format("{0} are moved to {1}", dicOrders[strOrderGuid], strTargetPatientName), true);
                }
                strOrderList.TrimEnd(",".ToCharArray());
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.MoveOrder, dicOrders[orderID], "", strSrcPatientName, string.Format("{0} are moved to {1}", dicOrders[orderID], strTargetPatientName), false);

            }
            dsar.ReturnString = strError;

            return true;
        }
        /// <summary>
        /// move all rps to a target order
        /// </summary>
        /// <param name="dicRPs">the rps' guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MoveRPs(Dictionary<string, string> dicRPs, string strTargetOrderGUID, string strTargetAccessionNumber, string strSrcPatientName, bool bSendToGateWay, DataSetActionResult dsar)
        {
            string strError = "";
            DataTable dt = new DataTable();
            dsar.recode = 0;
            dsar.Result = true;
            string rpID = string.Empty;
            try
            {
                if (dbProvider.ExistQuashStatus(dicRPs, null, strTargetOrderGUID, ref strError))
                {
                    dsar.recode = 1;
                    dsar.ReturnMessage = "Can not move this rp due to quash status";
                    dsar.Result = false;                  
                    return false;
                }

                string strRPList = "";
                if (!dbProvider.MoveRPs(dicRPs, strTargetOrderGUID, bSendToGateWay, ref strError))
                {
                    throw new Exception(strError);
                }
                foreach (string strRPGuid in dicRPs.Keys)
                {
                    rpID = strRPGuid;
                    strRPList += strRPGuid + ",";
                    HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.MoveProcedure, dicRPs[strRPGuid], strRPGuid, "", strSrcPatientName, string.Format("{0} are moved to {1}", strRPGuid, strTargetAccessionNumber), true);
                }
                strRPList.TrimEnd(",".ToCharArray());
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.MoveProcedure, dicRPs[rpID], strTargetOrderGUID, "", strSrcPatientName, string.Format("{0} are moved to {1}", rpID, strTargetAccessionNumber), false);

            }
            dsar.ReturnString = strError;

            return true;
        }

        #region merge order by level, US26313, 2015-07-30
         /// <summary>
        /// Merge Requisition all requisition to a target order
        /// </summary>
        /// <param name="dicRequisition">the requisition guid dictionary</param>
        /// <param name="strTargetAccNo">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergeRequisition(Dictionary<string, string> dicRequisition, string strTargetAccNo, bool bSendToGateWay, DataSetActionResult dsar)
        {
            string strError = "";
            DataTable dt = new DataTable();
            dsar.recode = 0;
            dsar.Result = true;
            string requisitionID = string.Empty;
            try
            {
              

                string strRequisitionList = "";
                if (!dbProvider.MergeRequisition(dicRequisition, strTargetAccNo, bSendToGateWay, ref strError))
                {
                    throw new Exception(strError);
                }
                foreach (string strRequisitionGuid in dicRequisition.Values)
                {
                    requisitionID = strRequisitionGuid;
                    strRequisitionList += strRequisitionGuid + ",";
                }
                strRequisitionList.TrimEnd(",".ToCharArray());
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
               
            }
            dsar.ReturnString = strError;

            return true;
        }
        /// <summary>
        /// Merge Charge all ChargeGUID to a target order
        /// </summary>
        /// <param name="dicCharge">the Charge guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergeCharge(Dictionary<string, string> dicCharge, string strTargetOrderGUID, bool bSendToGateWay, DataSetActionResult dsar)
        {
            string strError = "";
            DataTable dt = new DataTable();
            dsar.recode = 0;
            dsar.Result = true;
            string chargeOrderID = string.Empty;
            try
            {
                string strChargeList = "";
                if (!dbProvider.MergeCharge(dicCharge,strTargetOrderGUID, bSendToGateWay, ref strError))
                {
                    throw new Exception(strError);
                }
                foreach (string strOrderGuid in dicCharge.Keys)
                {
                    chargeOrderID = strOrderGuid;
                    strChargeList += strOrderGuid + ",";
                }
                strChargeList.TrimEnd(",".ToCharArray());
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
              

            }
            dsar.ReturnString = strError;

            return true;
        }

        #endregion
        #endregion
        #region RelatePatient
        /// <summary>
        /// get the order list for charge data mamagement
        /// </summary>
        /// <param name="strCondition"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public bool RelatePatient(string strPatientGuids, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.RelatePatient(strPatientGuids, ref strError))
                {
                    throw new Exception(strError);
                }
                dsar.ReturnMessage = strError;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return true;
        }

        public bool UnRelatePatient(string strPatientGuid, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.UnRelatePatient(strPatientGuid, ref strError))
                {
                    throw new Exception(strError);
                }
                dsar.ReturnMessage = strError;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return true;
        }

        public int QueryPatientByRelatedID(string strPatientGuid, string strRelateID, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.QueryPatientByRelatedID(strPatientGuid, strRelateID, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return dsar.recode;
        }

        /// <summary>
        /// get the order list for charge data mamagement
        /// </summary>
        /// <param name="birthDateOffset"></param>
        /// <param name="createDateRange"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public bool RelatePatientsByCondition(string strPatientGuids,string birthDateOffset,string createDateRange, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.RelatePatientsByCondition(strPatientGuids, birthDateOffset, createDateRange, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
                dsar.ReturnMessage = strError;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return true;
        }

        #endregion
        #region Charge Data Management
        /// <summary>
        /// get the order list for charge data mamagement
        /// </summary>
        /// <param name="strCondition"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int ChargeQueryOrderList(string strCondition, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.ChargeQueryOrderList(strCondition, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        /// <summary>
        /// get the item list for charge data mamagement
        /// </summary>
        /// <param name="strCondition"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int ChargeQueryItemList(string orderGuid, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                if (!dbProvider.ChargeQueryItemList(orderGuid, dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return dsar.recode;
        }

        /// <summary>
        /// Confirm,Refund,Cancel,Deduct.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int ChargeOperation(Charge_Action action, ChargeModel model, decimal totalFee, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (dbProvider.CheckOrderLock(model.OrderGuid, ref strError))
                {
                    throw new Exception(strError);
                }

                if (!dbProvider.ChargeOperation(action, model, totalFee, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bar.recode;
        }

        public int AddChargeModel(BaseModel model, decimal totalFee, BaseActionResult bar)
        {
            string strError = "";
            ChargeModel chargeModel = model as ChargeModel;
            if (chargeModel == null)
            {
                throw new Exception("Invalid input para");
            }
            bar.recode = 0;
            bar.Result = true;
            try
            {
                if (dbProvider.CheckOrderLock(chargeModel.OrderGuid, ref strError))
                {
                    throw new Exception(strError);
                }

                if (!dbProvider.AddCharge(chargeModel, totalFee, ref strError))
                {
                    throw new Exception(strError);
                }
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bar.recode;
        }

        public int HasOtherPatientRelated(string patientOneGuid, string patientTwoGuid, BaseActionResult bar)
        {
            //string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                bar.Result = dbProvider.HasOtherPatientRelated(patientOneGuid, patientTwoGuid);
            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;

                logger.Error(
                    (long)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bar.recode;
        }
        #endregion

        #region QC_QueryNotRefundedOrderCharge, 2015-07-20, Oscar added (US26283)

        /// <summary>
        /// Get the count of charges that haven't been refunded.
        /// </summary>
        /// <param name="patientGuid"></param>
        /// <param name="orderGuid"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int QueryNotRefundedOrderCharge(string patientGuid, string orderGuid, BaseActionResult bar)
        {
            var result = bar as DataSetActionResult;
            try
            {
                string error = null;
                if (!dbProvider.QueryNotRefundedOrderCharge(patientGuid, orderGuid, result.DataSetData, ref error))
                    throw new Exception(error);
            }
            catch (Exception ex)
            {
                result.recode = -1;
                result.ReturnMessage = ex.Message;
                result.Result = false;
                var frame = new System.Diagnostics.StackFrame(true);
                logger.Error(
                    (int)ModuleEnum.QualityControl_BF,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath,
                   frame.GetFileName(),
                   frame.GetFileLineNumber());
            }
            return result.recode;
        }

        #endregion
    }
}
