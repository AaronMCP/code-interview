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
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using LogServer;
using Server.Business.QualityControl;
using Server.Utilities.LogFacility;
using System.Diagnostics;

namespace Server.QualityControl.Action
{
    /// <summary>
    /// the derived class of BaseAction.
    /// which use to process all actions of Quality Control module
    /// </summary>
    public class QualityControlAction : BaseAction
    {
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("QualityControlServerLoglevel", "0D00");
        IQualityControlBusiness QualityControlBusiness = BusinessFactory.Instance.GetQualityControl();
        /// <summary>
        /// override method from BaseAction.
        /// execute the action
        /// </summary>
        /// <param name="context">
        /// input parameter from web service
        /// </param>
        /// <returns></returns>
        public override BaseActionResult Execute(Context context)
        {
            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
            BaseActionResult bar = new BaseActionResult();

            try
            {
                switch (context.MessageName)
                {
                    #region QC_QueryPatient
                    case "QC_QueryPatient":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string strRegBeginDt = CommonGlobalSettings.Utilities.GetParameter("RegBeginDt", context.Parameters);
                            string strRegEndDt = CommonGlobalSettings.Utilities.GetParameter("RegEndDt", context.Parameters);
                            string strPatientName = CommonGlobalSettings.Utilities.GetParameter("PatientName", context.Parameters);
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            bool bIsVIP = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("IsVip", context.Parameters));
                            string strCheck = strPatientID + strPatientName + strRegEndDt + strRegBeginDt;
                            strCheck = strCheck.Trim();
                            if (strCheck.Length == 0)
                            {
                                throw new Exception("At least one query parameter should be input");
                            }
                            QualityControlBusiness.QueryPatient(strPatientName, strPatientID, strRegBeginDt, strRegEndDt, bIsVIP, bar);

                        }
                        break;
                    #endregion
                    #region QC_QueryPatientList
                    case "QC_QueryPatientList":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string strRegBeginDt = CommonGlobalSettings.Utilities.GetParameter("RegBeginDt", context.Parameters);
                            string strRegEndDt = CommonGlobalSettings.Utilities.GetParameter("RegEndDt", context.Parameters);
                            string strPatientName = CommonGlobalSettings.Utilities.GetParameter("PatientName", context.Parameters);
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            bool bIsVIP = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("IsVip", context.Parameters));
                            string strCheck = strPatientID + strPatientName + strRegEndDt + strRegBeginDt;
                            strCheck = strCheck.Trim();
                            if (strCheck.Length == 0)
                            {
                                throw new Exception("At least one query parameter should be input");
                            }
                            QualityControlBusiness.QueryPatientList(strPatientName, strPatientID, strRegBeginDt, strRegEndDt, bIsVIP, bar);

                        }
                        break;
                    #endregion
                    #region QC_QueryOrder
                    case "QC_QueryOrder":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            string strStatus = CommonGlobalSettings.Utilities.GetParameter("Status", context.Parameters);
                            strPatientID = strPatientID.Trim();
                            strStatus = strStatus.Trim();
                            if (strPatientID.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }

                            QualityControlBusiness.QueryOrder(strPatientID, strStatus, bar);


                        }
                        break;
                    #endregion
                    #region QC_QueryRP
                    case "QC_QueryRP":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string strOrderID = CommonGlobalSettings.Utilities.GetParameter("OrderID", context.Parameters);
                            string strRPID = CommonGlobalSettings.Utilities.GetParameter("RPGuid", context.Parameters);
                            strRPID = strRPID.Trim();
                            strOrderID = strOrderID.Trim();
                            if (strOrderID.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            QualityControlBusiness.QueryRP(strOrderID, bar, strRPID);

                        }
                        break;
                    #endregion
                    #region QC_QueryLock
                    case "QC_QueryLock":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            string strRegBeginDt = CommonGlobalSettings.Utilities.GetParameter("BeginTime", context.Parameters);
                            string strRegEndDt = CommonGlobalSettings.Utilities.GetParameter("EndTime", context.Parameters);
                            string strOwner = CommonGlobalSettings.Utilities.GetParameter("Owner", context.Parameters);

                            bar = dsar as BaseActionResult;
                            QualityControlBusiness.QueryLock(strOwner, strRegBeginDt, strRegEndDt, bar);
                        }
                        break;

                    case "QC_QueryLockInfo":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            string strObjectType = CommonGlobalSettings.Utilities.GetParameter("ObjectType", context.Parameters);
                            string strObjectGuid = CommonGlobalSettings.Utilities.GetParameter("ObjectGuid", context.Parameters);

                            bar = dsar as BaseActionResult;
                            QualityControlBusiness.QueryLock(strObjectType, strObjectGuid, bar);
                        }
                        break;
                    #endregion
                    #region QC_QueryRPwithDuplicatedReport
                    case "QC_QueryRPwithDuplicatedReport":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string bExcludeDeletedReport = CommonGlobalSettings.Utilities.GetParameter("bExcludeDeletedReport", context.Parameters).Trim();
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            string strPatientName = CommonGlobalSettings.Utilities.GetParameter("PatientName", context.Parameters);
                            string dt1 = CommonGlobalSettings.Utilities.GetParameter("dt1", context.Parameters);
                            string dt2 = CommonGlobalSettings.Utilities.GetParameter("dt2", context.Parameters);
                            //bool bIsVIP = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("IsVip", context.Parameters));

                            QualityControlBusiness.QueryRPwithDuplicatedReport(
                                bExcludeDeletedReport == "1" || bExcludeDeletedReport == "Y" || bExcludeDeletedReport == "y",
                                strPatientID, strPatientName, dt1, dt2, bar);
                        }
                        break;
                    #endregion
                    #region QC_QueryDuplicatedReport
                    case "QC_QueryDuplicatedReport":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string rpGuid = CommonGlobalSettings.Utilities.GetParameter("procedureGuid", context.Parameters).Trim();
                            if (rpGuid.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            QualityControlBusiness.QueryDuplicatedReport(rpGuid, bar);
                        }
                        break;
                    #endregion
                    #region Update
                    case "QC_UpdatePatient":
                        {
                            bar = new BaseActionResult();
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.UpdatePatient(bSend, bdsm.DataSetParameter, bar);
                        }
                        break;
                    case "QC_UpdateOrder":
                        {
                            bar = new BaseActionResult();
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.UpdateOrder(bSend, bdsm.DataSetParameter, bar);
                        }

                        break;
                    case "QC_UpdateRP":
                        {
                            bar = new BaseActionResult();
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.UpdateRP(bSend, bdsm.DataSetParameter, bar);
                        }

                        break;
                    case "QC_DeleteOrder":
                        {
                            bar = new BaseActionResult();
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            string strOrderID = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            string strLoginName= CommonGlobalSettings.Utilities.GetParameter("LoginName", context.Parameters);
                            string strLocalName = CommonGlobalSettings.Utilities.GetParameter("LocalName", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.DeleteOrder(bSend, strOrderID,strLoginName, strLocalName, bar);
                        }
                        break;
                    case "QC_DeleteRP":
                        {
                            bar = new BaseActionResult();
                            string strRPs = CommonGlobalSettings.Utilities.GetParameter("RPGUIDS", context.Parameters);
                            string strLoginName = CommonGlobalSettings.Utilities.GetParameter("LoginName", context.Parameters);
                            string strLocalName = CommonGlobalSettings.Utilities.GetParameter("LocalName", context.Parameters);

                            #region EK_HI00073389
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            string strVisitGuid = CommonGlobalSettings.Utilities.GetParameter("VisitGuid", context.Parameters);
                            string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            #endregion
                            QualityControlBusiness.DeleteRP(bSend, strVisitGuid, strOrderGuid, strRPs, strLoginName, strLocalName, bar);
                        }
                        break;
                    #region EK_HI00063904 jameswei 2007-12-13 get the acc's requisition file name and relativepath
                    case "QC_GetRequisitionInfo":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = new BaseActionResult();
                            bar = dsar as BaseActionResult;
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("strAccNo", context.Parameters);
                            QualityControlBusiness.GetRequisitionInfo(strAccNo, bar);
                        }
                        break;
                    #endregion
                    case "QC_DeletePatient":
                        {
                            bar = new BaseActionResult();
                            string strPatientGuid = CommonGlobalSettings.Utilities.GetParameter("PATIENTGUID", context.Parameters);
                            #region EK_HI00073389
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            #endregion
                            bar.recode = QualityControlBusiness.DeletePatient(bSend, strPatientGuid);
                        }
                        break;
                    #endregion
                    #region QC_UpdateActiveReport
                    case "QC_UpdateActiveReport":
                        {
                            bar = new BaseActionResult();

                            string reportGuid = CommonGlobalSettings.Utilities.GetParameter("reportGuid", context.Parameters).Trim();

                            QualityControlBusiness.UpdateActiveReport(reportGuid, bar);
                        }
                        break;
                    #endregion
                    #region Merge
                    case "QC_AssignOrderToPatient":
                        {
                            bar = new BaseActionResult();
                            string strVisitID = CommonGlobalSettings.Utilities.GetParameter("VisitGuid", context.Parameters);
                            string strOrderID = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientGuid", context.Parameters);
                            string DelAfterMerge = CommonGlobalSettings.Utilities.GetParameter("DelAfterMerge", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            strOrderID = strOrderID.Trim();
                            strPatientID = strPatientID.Trim();
                            if (strOrderID.Length == 0 || strPatientID.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            QualityControlBusiness.AssignOrderToPatient(bSend, strVisitID, strOrderID, strPatientID, DelAfterMerge, bar);

                        }

                        break;
                    /* old EK_HI00085711
                case "QC_MergePatients":
                    {
                        bar = new BaseActionResult();
                        string PatientsCount =CommonGlobalSettings.Utilities.GetParameter("Count", context.Parameters);
                        string DelAfterMerge =CommonGlobalSettings.Utilities.GetParameter("DelAfterMerge", context.Parameters);
                        string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                        bool bSend = false;
                        int Count = 0;

                        try
                        {
                            bSend = Convert.ToBoolean(SendGateway);
                        }
                        catch
                        {
                            bSend = false;
                        }
                            
                        try
                        {
                            Count = Convert.ToInt32(PatientsCount);
                        }
                        catch
                        {
                            Count = 0;
                        }
                        if(Count < 2)
                        {
                            throw new Exception("At least two patients should be selected");
                        }

                        Dictionary<int, string> m_dicPatients;
                        m_dicPatients = new Dictionary<int, string>();

                        for(int i = 1; i < Count+1;i++)
                        {
                            string strPatientID =CommonGlobalSettings.Utilities.GetParameter(string.Format("Patient{0}",i), context.Parameters);
                            m_dicPatients.Add(i, strPatientID);
                        }

                        QualityControlBusiness.MergePatients(bSend,m_dicPatients, DelAfterMerge, bar);

                    }
                    break;
                     * */
                    #endregion
                    #region Lock
                    case "QC_Lock":
                        {
                            bar = new BaseActionResult();
                            string strObjectGuid = CommonGlobalSettings.Utilities.GetParameter("ObjectGuid", context.Parameters);
                            string strOwner = CommonGlobalSettings.Utilities.GetParameter("Owner", context.Parameters);
                            string strOwnerIP = CommonGlobalSettings.Utilities.GetParameter("OwnerIP", context.Parameters);
                            string strObjectType = CommonGlobalSettings.Utilities.GetParameter("ObjectType", context.Parameters);
                            int nObjectType = Convert.ToInt32(strObjectType);
                            strObjectGuid = strObjectGuid.Trim();
                            strOwner = strOwner.Trim();
                            strOwnerIP = strOwnerIP.Trim();

                            if (strObjectGuid.Length == 0 || strOwner.Length == 0 || strOwnerIP.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            QualityControlBusiness.LockObject(nObjectType, strObjectGuid, strOwner, strOwnerIP, bar);
                        }
                        break;

                    case "QC_UnLock":
                        {
                            bar = new BaseActionResult();
                            string strSyncType = CommonGlobalSettings.Utilities.GetParameter("SyncType", context.Parameters);
                            string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            string strOwner = CommonGlobalSettings.Utilities.GetParameter("Owner", context.Parameters);
                            string strObjectType = CommonGlobalSettings.Utilities.GetParameter("ObjectType", context.Parameters);
                            int nObjectType = Convert.ToInt32(strObjectType);
                            int nSyncType = Convert.ToInt32(strSyncType);
                            QualityControlBusiness.UnLockObject(nObjectType, nSyncType, strOrderGuid, strOwner, bar);
                        }
                        break;

                    case "QC_MergePatients":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            Dictionary<string, string> dicSrcPatients = null;

                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcPatients = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strTargetPatientGUID = CommonGlobalSettings.Utilities.GetParameter("TargetPatientGUID", context.Parameters);
                            string strTargetPatientName = CommonGlobalSettings.Utilities.GetParameter("TargetPatientName", context.Parameters);
                            string DelAfterMerge = CommonGlobalSettings.Utilities.GetParameter("DelAfterMerge", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);

                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.MergePatients(dicSrcPatients, DelAfterMerge, strTargetPatientGUID, strTargetPatientName, bSend, dsar);
                            return dsar;
                        }

                    case "QC_MergeOrders":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            Dictionary<string, string> dicSrcOrders = null;
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcOrders = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strSrcPatientName = CommonGlobalSettings.Utilities.GetParameter("SourcePatientName", context.Parameters);
                            string strTargetAccessionNumber = CommonGlobalSettings.Utilities.GetParameter("TargetAccessionName", context.Parameters);
                            string strTargetOrderGUID = CommonGlobalSettings.Utilities.GetParameter("TargetOrderGUID", context.Parameters);
                            string DelAfterMerge = CommonGlobalSettings.Utilities.GetParameter("DelAfterMerge", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);
                            string ChargeFuncActive = CommonGlobalSettings.Utilities.GetParameter("ChargeFuncActive", context.Parameters);

                            bool bSend = false;
                            bool bChargeFuncActive = false;
                            Boolean.TryParse(SendGateway, out bSend);
                            Boolean.TryParse(ChargeFuncActive, out bChargeFuncActive);

                            QualityControlBusiness.MergeOrders(dicSrcOrders, DelAfterMerge, strTargetOrderGUID, strTargetAccessionNumber, strSrcPatientName, bSend, bChargeFuncActive, dsar);
                            return dsar;
                        }


                    case "QC_MoveOrders":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            Dictionary<string, string> dicSrcOrders = null;
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcOrders = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strTargetPatientGUID = CommonGlobalSettings.Utilities.GetParameter("TargetPatientGUID", context.Parameters);
                            string strTargetPatientName = CommonGlobalSettings.Utilities.GetParameter("TargetPatientName", context.Parameters);
                            string strSrcPatientName = CommonGlobalSettings.Utilities.GetParameter("SourcePatientName", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);

                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.MoveOrders(dicSrcOrders, strTargetPatientGUID, strSrcPatientName, strTargetPatientName, bSend, dsar);
                            return dsar;
                        }


                    case "QC_MoveRPs":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            Dictionary<string, string> dicSrcRPs = null;
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcRPs = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strTargetOrderGUID = CommonGlobalSettings.Utilities.GetParameter("TargetOrderGUID", context.Parameters);
                            string strSrcPatientName = CommonGlobalSettings.Utilities.GetParameter("SourcePatientName", context.Parameters);
                            string strTargetAccessionNumber = CommonGlobalSettings.Utilities.GetParameter("TargetAccessionNumber", context.Parameters);

                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);

                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.MoveRPs(dicSrcRPs, strTargetOrderGUID, strTargetAccessionNumber, strSrcPatientName, bSend, dsar);
                            return dsar;
                        }
                    #region  merge order by level, US26313, 2015-07-30 Requisition
                    case "QC_MergeRequisition":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            Dictionary<string, string> dicSrcRequisition = null;
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcRequisition = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strTargetAccNo = CommonGlobalSettings.Utilities.GetParameter("strTargetAccNo", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);

                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.MergeRequisition(dicSrcRequisition, strTargetAccNo, bSend, dsar);
                            return dsar;
                        }
                    case "QC_MergeCharge":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            Dictionary<string, string> dicSrcCharge = null;
                            BaseDataSetModel dataSetModel = context.Model as BaseDataSetModel;
                            if (dataSetModel.DataSetParameter != null && dataSetModel.DataSetParameter.Tables.Count > 0)
                            {
                                dicSrcCharge = dataTable2Dictionary(dataSetModel.DataSetParameter.Tables[0]);
                            }
                            string strTargetOrderGUID = CommonGlobalSettings.Utilities.GetParameter("TargetOrderGUID", context.Parameters);
                            string SendGateway = CommonGlobalSettings.Utilities.GetParameter("SendGateway", context.Parameters);

                            bool bSend = false;
                            try
                            {
                                bSend = Convert.ToBoolean(SendGateway);
                            }
                            catch
                            {
                                bSend = false;
                            }
                            QualityControlBusiness.MergeCharge(dicSrcCharge, strTargetOrderGUID, bSend, dsar);
                            return dsar;
                        }
                    #endregion
                  

                    #endregion

                    #region RelatePatient
                    case "QC_RelatePatient":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            string strPatientGuids = CommonGlobalSettings.Utilities.GetParameter("PatientGuids", context.Parameters);
                            QualityControlBusiness.RelatePatient(strPatientGuids, bar);
                        }
                        break;
                    case "QC_UnRelatePatient":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            string strPatientGuid = CommonGlobalSettings.Utilities.GetParameter("PatientGuid", context.Parameters);
                            QualityControlBusiness.UnRelatePatient(strPatientGuid, bar);
                        }
                        break;
                    case "QC_QueryPatientByRelateID":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            string strPatientGuid = CommonGlobalSettings.Utilities.GetParameter("PatientGuid", context.Parameters);
                            string strRelatedID = CommonGlobalSettings.Utilities.GetParameter("RelatedID", context.Parameters);
                            QualityControlBusiness.QueryPatientByRelatedID(strPatientGuid,strRelatedID, bar);
                        }
                        break;
                    case "QC_HasOtherPatientRelated":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            string strPatientGuidA = CommonGlobalSettings.Utilities.GetParameter("PatientGuidA", context.Parameters);
                            string strPatientGuidB = CommonGlobalSettings.Utilities.GetParameter("PatientGuidB", context.Parameters);
                            QualityControlBusiness.HasOtherPatientRelated(strPatientGuidA, strPatientGuidB, bar);
                        }
                        break;
                    case "QC_RelatePatientsByCondition":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            string strPatientGuids = CommonGlobalSettings.Utilities.GetParameter("PatientGuids", context.Parameters);
                            string strBirthDateOffset = CommonGlobalSettings.Utilities.GetParameter("BirthDateOffset", context.Parameters);
                            string strCreateDateRange = CommonGlobalSettings.Utilities.GetParameter("CreateDateRange", context.Parameters);
                            QualityControlBusiness.RelatePatientsByCondition(strPatientGuids, strBirthDateOffset, strCreateDateRange,bar);
                        }
                        break;
                    #endregion
                    #region Charge Data Management
                    case "QC_ChargeQueryOrderList":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;
                            StringBuilder sb = new StringBuilder();
                            string strPatientID = CommonGlobalSettings.Utilities.GetParameter("PatientID", context.Parameters);
                            string strPatientName = CommonGlobalSettings.Utilities.GetParameter("PatientName", context.Parameters);
                            string strAccNo = CommonGlobalSettings.Utilities.GetParameter("AccNo", context.Parameters);
                            string strFrom = CommonGlobalSettings.Utilities.GetParameter("RegistDtFrom", context.Parameters);
                            string strTo = CommonGlobalSettings.Utilities.GetParameter("RegistDtTo", context.Parameters);
                            string strStatus = CommonGlobalSettings.Utilities.GetParameter("ChargeStatus", context.Parameters);
                            string strHisID = CommonGlobalSettings.Utilities.GetParameter("HISID", context.Parameters);
                            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
                            string strInhospitalNo = CommonGlobalSettings.Utilities.GetParameter("InhospitalNo", context.Parameters);

                            if (!string.IsNullOrEmpty(strPatientID))
                            {
                                sb.AppendFormat("tRegPatient.PatientID ='{0}' And ", strPatientID);
                            }
                            if (!string.IsNullOrEmpty(strPatientName))
                            {
                                sb.AppendFormat("tRegPatient.LocalName ='{0}' And ", strPatientName);
                            }
                            if (!string.IsNullOrEmpty(strHisID))
                            {
                                sb.AppendFormat("tRegOrder.HISID ='{0}' And ", strHisID);
                            }
                            if (!string.IsNullOrEmpty(strAccNo))
                            {
                                sb.AppendFormat("tRegOrder.AccNo ='{0}' And ", strAccNo);
                            }
                            if (!string.IsNullOrEmpty(strFrom) && !string.IsNullOrEmpty(strTo))
                            {
                                sb.AppendFormat("tRegOrder.CreateDt between '{0}' ", strFrom);
                                sb.AppendFormat("and '{0}' and ", strTo);
                            }

                            if (!string.IsNullOrEmpty(strStatus))
                            {
                                sb.AppendFormat("tOrderCharge.LastStatus = {0} and ", strStatus);
                            }
                            if (!string.IsNullOrEmpty(strModalityType))
                            {
                                sb.AppendFormat("tRegprocedure.ModalityType in ({0}) And ", strModalityType);
                            }

                            if (!string.IsNullOrEmpty(strInhospitalNo))
                            {
                                sb.AppendFormat("tRegOrder.InhospitalNo = '{0}' And ", strInhospitalNo);
                            }

                            sb.Append(" 1 = 1");
                            Debug.WriteLine("QC_ChargeQueryOrderList condition:" + sb.ToString());
                            QualityControlBusiness.ChargeQueryOrderList(sb.ToString(), bar);
                        }
                        break;
                    case "QC_ChargeQueryItemList":
                        {
                            DataSetActionResult dsar = new DataSetActionResult();
                            dsar.DataSetData = new DataSet();
                            bar = dsar as BaseActionResult;

                            string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                            QualityControlBusiness.ChargeQueryItemList(strOrderGuid, bar);
                        }
                        break;
                    case "QC_ChargeOperation":
                        {
                            string strActionName = CommonGlobalSettings.Utilities.GetParameter("ActionName", context.Parameters);
                            ChargeModel chargeMode = context.Model as ChargeModel;
                            string strTotalCharge = CommonGlobalSettings.Utilities.GetParameter("TotalCharge", context.Parameters);
                            decimal totalFee = 0;
                            decimal.TryParse(strTotalCharge, out totalFee);
                            switch (strActionName.ToUpper())
                            {
                                case "CONFIRM":
                                    QualityControlBusiness.ChargeOperation(Charge_Action.Confirm, chargeMode, totalFee, bar);
                                    break;
                                case "DEDUCT":
                                    QualityControlBusiness.ChargeOperation(Charge_Action.Deduct, chargeMode, totalFee, bar);
                                    break;
                                case "REFUND":
                                    QualityControlBusiness.ChargeOperation(Charge_Action.Refund, chargeMode, totalFee, bar);
                                    break;
                                case "CANCEL":
                                    QualityControlBusiness.ChargeOperation(Charge_Action.Cancel, chargeMode, totalFee, bar);
                                    break;
                                case "ADDCHARGE":
                                    QualityControlBusiness.AddChargeModel(context.Model, totalFee, bar);
                                    break;
                                default:
                                    throw new Exception(string.Format("ActionName {0} is not surpported!", strActionName));
                            }
                        }
                        break;

                    #endregion

                    #region QC_QueryNotRefundedOrderCharge, 2015-07-20, Oscar added (US26283)

                    case "QC_QueryNotRefundedOrderCharge":
                        var result = new DataSetActionResult(true)
                        {
                            DataSetData = new DataSet()
                        };
                        bar = result;

                        var patientGuid = CommonGlobalSettings.Utilities.GetParameter("PatientGuid", context.Parameters);
                        if (string.IsNullOrWhiteSpace(patientGuid))
                        {
                            throw new Exception("Param is invalid");
                        }
                        var orderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", context.Parameters);
                        
                        
                        QualityControlBusiness.QueryNotRefundedOrderCharge(patientGuid, orderGuid, bar);
                        break;

                    #endregion

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                bar.ReturnMessage = ex.Message;
                bar.Result = false;
                bar.recode = -1;
                logger.Error(
                    (long)ModuleEnum.QualityControl_WS,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bar;
        }

        /// <summary>
        /// trans datatable to dictionary.
        /// 
        /// </summary>
        /// <param name="context">
        /// the datatable which have two columns, col1 is key ,col2 is value
        /// </param>
        /// <returns></returns>
        private Dictionary<string, string> dataTable2Dictionary(DataTable dt)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (!dic.ContainsKey(dr[0].ToString()))
                {
                    dic.Add(dr[0].ToString(), dr[1].ToString());
                }
            }
            return dic;
        }
    }
}
