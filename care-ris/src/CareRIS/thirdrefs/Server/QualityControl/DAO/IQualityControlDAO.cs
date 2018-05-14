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
using CommonGlobalSettings;
using Common.ActionResult;


namespace Server.DAO.QualityControl
{
    public enum LockEnum : int
    {
        // Summary:
        //     Lock a patient, all orders belong to this patient will be locked
        LockPatient = 1,
        // Summary:
        //     Lock a Visit, all orders belong to this visit will be locked
        LockVisit = 2,
        // Summary:
        //     Lock a Order
        LockOrder = 3,
        // Summary:
        //     Lock a Exam, order contains this exam will be locked
        LockExam = 4,
        // Summary:
        //     Lock a Report, all orders relate to this report will be locked
        LockReport = 5
    }
    public interface IQualityControlDAO
    {
        /// <summary>
        /// Query Patient by PatientName or patient id and so on
        /// </summary>
        /// <param name="strPatientName">Local name of target patient</param>
        /// <param name="strPatientID">PatientID of target patient</param>
        /// <param name="strBeginDt">Begin create date for query</param>
        /// <param name="strEndDt">End create date for query</param>
        /// <param name="bIsVIP">True means VIP patients will be queried</param>
        /// <param name="ds">Returns result of query</param> 
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryPatient(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool isVIP, DataSet ds, ref string strError);
        /// <summary>
        /// Query PatientList by PatientName or patient id and so on
        /// </summary>
        /// <param name="strPatientName">Local name of target patient</param>
        /// <param name="strPatientID">PatientID of target patient</param>
        /// <param name="strBeginDt">Begin create date for query</param>
        /// <param name="strEndDt">End create date for query</param>
        /// <param name="bIsVIP">True means VIP patients will be queried</param>
        /// <param name="ds">Returns result of query</param> 
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryPatientList(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool isVIP, DataSet ds, ref string strError);

        /// <summary>
        /// Query list of Orders
        /// </summary>
        /// <param name="strPatientGuid">
        /// GUID of the patient whose orders will be queried
        /// </param>
        /// <param name="ds">
        /// Returns result of query
        /// </param> 
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryOrder(string strPatientGuid, DataSet ds, ref string strError);

        /// <summary>
        /// Query list of Orders >= that status
        /// </summary>
        /// <param name="strPatientGuid">
        /// GUID of the patient whose orders will be queried
        /// </param>
        /// <param name="ds">
        /// Returns result of query
        /// </param> 
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryOrder(string strPatientGuid, DataSet ds, string status, ref string strError);

        /// <summary>
        /// Query list of RP
        /// </summary>
        /// <param name="strOrderGUID">
        /// GUID of the order that RP belongs to
        /// </param>
        /// <param name="ds">
        /// Returns result of query
        /// </param> 
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryRP(string strOrderGuid, DataSet ds, ref string strError, ref string strRPGuid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="patientName"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool QueryRPwithDuplicatedReport(bool bExcludeDeletedReport, string pID, string patientName, string dt1, string dt2, DataSet ds, ref string strError);

        bool QueryDuplicatedReport(string rpGuid, DataSet ds, ref string strError);



        /// <summary>
        /// Update register information of a patient
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of patient 
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool UpdatePatient(bool SendToGateWay, string strPatientID, DataSet ds, ref string strError);

        /// <summary>
        /// Update information of one Order
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of order
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool UpdateOrder(bool SendToGateWay, string strOrderGuid, DataSet ds, ref string strError);

        /// <summary>
        /// Update information of one RP
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of RP
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool UpdateRP(bool SendToGateWay, string strRPGuid, DataSet ds, ref string strError);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportGuid"></param>
        /// <returns></returns>
        bool UpdateActiveReport(string reportGuid, ref string strError);

        /// <summary>
        /// Delete one order from database
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="strOrderGuid">
        /// GUID of target order
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool DeleteOrder(bool SendToGateWay, string strOrderGuid, string strLoginName, string strLocalName, ref string strError, ref string strRPGuid);
        /// <summary>
        /// Delete one RP from database
        /// </summary>
        /// <param name="sendToGW">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="strVisitGuid">
        /// GUID of target Visit
        /// </param>
        /// <param name="strOrderGuid">
        /// GUID of target order
        /// </param> 
        /// <param name="strRPs">RP</param>
        /// <returns>True means success, False means fail</returns>
        bool DeleteRP(bool sendToGW, string strVisitGuid, string strOrderGuid, string strRPs,string strLoginName,string strLocalName, BaseActionResult bar);
        bool GetRequisitionInfo(string strAccNo, DataSet ds);
        bool DeletePatient(bool sendToGateway, string patientGuid);

        //merge
        /// <summary>
        /// Assign one visit to another patient
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="strVisitGuid">
        /// GUID of the visit that will be assigned to another patient
        /// </param>
        /// <param name="strPatientGUID">
        /// GUID of target patient the visit will be assigned to
        /// </param>
        /// <param name="bDelAfterMerge">
        /// True means current owner patient of Visit will be deleted if no more visits belongs to if after this operation
        /// </param
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool AssignOrderToPatient(bool SendToGateWay, string strVisitGuid, string strOrderGuid, string strPatientID, string bDelAfterMerge, ref string strError);

        /// <summary>
        /// Merge patients into one patient
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="dicPatients">
        /// Contains list of all patients that should be merged
        /// </param>
        /// <param name="bDelAfterMerge">
        /// True meas that patients will no visits will be deleted after merge, 
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool MergePatients(bool SendToGateWay, Dictionary<int, string> m_dicPatients, string bDelAfterMerge, ref string strTargetPatientGUID, ref string strTargetPatientName, ref string strPatientsList, ref string strError);

        //lock
        //lock one object
        /// <summary>
        /// Query list of locked objects
        /// </summary>
        /// <param name="stOwner">
        /// The login name of the owner, who locked the object
        /// </param>
        /// <param name="stBeginTime">
        /// Begin time for this query
        /// </param>
        /// <param name="stEndTime">
        /// End time for this query
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryLock(string stOwner, string stBeginTime, string stEndTime, DataSet ds, ref string strError);


        //query ock
        /// <summary>
        /// Query locked info
        /// </summary>
        /// <param name="strObjectType">
        /// ObjectType
        /// </param>
        /// <param name="strObjectGuid">
        /// ObjectGuid
        /// </param>
        /// <param name="ds">
        /// Lock Info dataset to return
        /// </param>
        /// <param name="strError">Returns info of lock</param>
        /// <returns>True means locked, False means no locked</returns>
        bool QueryLock(string strObjectType, string strObjectGuid, DataSet ds);

        /// <summary>
        /// Lock one object
        /// </summary>
        /// <param name="strObjectType">
        /// Lock type
        /// </param>
        /// <param name="strObjectGuid">
        /// Identification information of target object
        /// </param>
        /// <param name="strOwner">
        /// GUID of owner
        /// </param>
        /// <param name="strOwnerIP">
        /// IP address that the lock comes from
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool LockObject(int strObjectType, string strObjectGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError);

        /// <summary>
        /// Unlock one object
        /// </summary>
        /// <param name="strObjectType">
        /// Lock type
        /// </param>
        /// <param name="strObjectGuid">
        /// Identification information of target object
        /// </param>
        /// <param name="strOwner">
        /// GUID of owner
        /// </param>
        /// <param name="strOwnerIP">
        /// IP address that the lock comes from
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool UnLockObject(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, ref string strError);


        /// <summary>
        /// Query Patient of one order
        /// Used to get information for HIPPA log
        /// </summary>
        /// <param name="strOrderGuid">
        /// GUID of the order
        /// </param>
        /// <param name="strPatientGUID">
        /// Return GUID of patient
        /// </param>
        /// <param name="strPatientName">
        /// Return Name of the patient
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryPatientWithOrderID(string strOrderGuid, ref string strPatientGUID, ref string strPatientName, ref string strError);

        /// <summary>
        /// Query Access no of a order
        /// Used to get information for HIPPA log
        /// </summary>
        /// <param name="strOrderGuid">
        /// GUID of the order
        /// </param>
        /// <param name="strAccNo">
        /// Return access no. of the order
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means success, False means fail</returns>
        bool QueryAccNoWithOrderID(string strOrderGuid, ref string strAccNo, ref string strError);

        bool QueryPatientWithVisitGUID(string stVisitGUID, DataSet dsPatient);

        /// <summary>
        /// To verify whether patient with specified GUID exist in database
        /// </summary>
        /// <param name="strPatientGUID">
        /// GUID for search
        /// </param>
        /// <param name="strPatientName">
        /// Return local name of target patient
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means patient found, False means not</returns>
        bool PatientExist(string strPatientGUID, ref string strPatientName, ref string strError);

        /// <summary>
        /// To verify whether patient with specified GUID exist in database
        /// </summary>
        /// <param name="strPatientGUID">
        /// GUID  for search
        /// </param>
        /// <param name="strError">Returns description of error if error occurs</param>
        /// <returns>True means patient found, False means not</returns>
        bool PatientExist(string strPatientGUID, ref string strError);

        /// <summary>
        /// merge all gived orders's rps to target order and delete the gived orders
        /// </summary>
        /// <param name="dicOrders">the orders to be merged</param>
        /// <param name="bDelAfterMerge">current always true</param>
        /// <param name="strTargetOrderGUID">the target order to be merged to</param>
        /// <param name="bSendToGateWay">whether to send to gateway</param>
        /// <param name="bChargeFuncActive">whether to update charge associted info</param>/// 
        /// <param name="strError">the error message will be return</param>
        /// <param name="dtInfo">the detail info of why do false,current only use for lock info</param>
        bool MergeOrders(Dictionary<string, string> dicOrders, string bDelAfterMerge, string strTargetOrderGUID, bool bSendToGateWay, bool bChargeFuncActive, ref string strError, ref DataTable dtInfo);

        //merge some source patients to one target patient
        /// <summary>
        /// merge patients to target patient
        /// </summary>
        /// <param name="dicPatients">the source patients to be merged</param>
        /// <param name="bDelAfterMerge">wether delete the source patients after merge</param>
        /// <param name="strTargetPatientGUID">the destination patient guid to merged to</param>
        /// <param name="bSendToGateWay">wether to notify gateway</param>
        /// <param name="strError">the function isued error</param>
        /// <returns>if successfull return true else false</returns>
        bool MergePatients(Dictionary<string, string> dicPatients, string bDelAfterMerge, string strTargetPatientGUID, bool bSendToGateWay, ref string strError);

        /// <summary>
        /// move orders to a target patient
        /// </summary>
        /// <param name="dicOrders">the orders' guid dictionary</param>
        /// <param name="strTargetPatientGUID">the target patient guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        bool MoveOrders(Dictionary<string, string> dicOrders, string strTargetPatientGUID, bool bSendToGateWay, ref string strError);


        bool ExistQuashStatus(Dictionary<string, string> dicRPs,Dictionary<string, string> dicOrders, string strTargetOrder,ref string strError);


        /// <summary>
        /// move all rps to a target order
        /// </summary>
        /// <param name="dicRPs">the rps' guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        bool MoveRPs(Dictionary<string, string> dicRPs, string strTargetOrderGUID, bool bSendToGateWay, ref string strError);

        #region merge order by level, US26313, 2015-07-30
 
        /// <summary>
        /// merge Requisition to a target order
        /// </summary>
        /// <param name="dicRequisition">the Requisition guid dictionary</param>
        /// <param name="strTargetAccNo">the target AccNo</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        bool MergeRequisition(Dictionary<string, string> dicRequisition, string strTargetAccNo, bool bSendToGateWay, ref string strError);
        /// <summary>
        /// merge Charge to a target order
        /// </summary>
        /// <param name="dicCharge">the Charge guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        bool MergeCharge(Dictionary<string, string> dicCharge, string strTargetOrderGUID, bool bSendToGateWay, ref string strError);
        #endregion
        

        #region Charge Data Management
        /// <summary>
        /// query the order table for chage data management
        /// </summary>
        /// <param name="condition">query condition</param>
        /// <param name="ds">dataset for query</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns></returns>
        bool ChargeQueryOrderList(string condition, DataSet ds, ref string strError);
        /// <summary>
        /// query the items table for chage data management
        /// </summary>
        /// <param name="condition">query condition</param>
        /// <param name="ds">dataset for query</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns></returns>
        bool ChargeQueryItemList(string condition, DataSet ds, ref string strError);
        /// <summary>
        /// Add charge
        /// </summary>
        /// <param name="model"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool AddCharge(ChargeModel model, decimal totalFee, ref string strError);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="chargeModel"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool ChargeOperation(Charge_Action action, ChargeModel chargeModel, decimal totalFee, ref string strError);
        /// <summary>
        /// Get LockInfo if lock exists
        /// </summary>
        /// <param name="_orderId">OrderGuid</param>
        /// <param name="_lockInfo">For return lock info</param>
        /// <returns></returns>
        bool CheckOrderLock(string _orderId, ref string _lockInfo);
        #endregion

        #region RelatePatient
        /// <summary>
        /// relate the patient
        /// </summary>
        /// <param name="strPatientGuids"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool RelatePatient(string strPatientGuids, ref string strError);
        /// <summary>
        /// UnRelate the patient
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool UnRelatePatient(string strPatientGuid, ref string strError);
        /// <summary>
        /// QueryPatientByRelatedID
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="strRelateID"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool QueryPatientByRelatedID(string strPatientGuid, string strRelateID, DataSet ds, ref string strError);
        /// <summary>
        /// HasOtherPatientRelated
        /// </summary>
        /// <param name="patientOneGuid"></param>
        /// <param name="patientTwoGuid"></param>
        /// <returns></returns>
        bool HasOtherPatientRelated(string patientOneGuid, string patientTwoGuid);
        /// <summary>
        /// Auto relate the patients with patins in DB by the condition
        /// </summary>
        /// <param name="strPatientGuids"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool RelatePatientsByCondition(string patientGuids, string birthDateOffset, string createDateRange, DataSet dsResult, ref string strError);
        #endregion

        #region QC_QueryNotRefundedOrderCharge, 2015-07-20, Oscar added (US26283)

        /// <summary>
        /// Get the count of charges that haven't been refunded.
        /// </summary>
        /// <param name="patientGuid"></param>
        /// <param name="orderGuid"></param>
        /// <param name="ds"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool QueryNotRefundedOrderCharge(string patientGuid, string orderGuid, DataSet ds, ref string error);

        #endregion
    }

    public class GWInfo
    {
        public GWInfo()
        {
            PatientID = "";
            RemotePID = "";
            EnglishName = "";
            LocalName = "";
            Birthday = DateTime.Now.ToString("yyyy-MM-dd");
            Gender = "";
            Alias = "";
            Address = "";
            Telephone = "";
            Marital = "";
            PatientType = "";
            Region = "";
            ClinicNo = "";
            BedNo = "";
            IsVip = "0";
            InHospitalNo = "";
            PatientComment = "";
            RemoteAccNo = "";
            HisID = "";
            CardNo = "";
            MedicareNo = "";
            AccNo = "";
            ApplyDept = "";
            AppDoctor = "";
            Observation = "";
            VisitComment = "";
            CheckNotice = "";
            RegisterDt = DateTime.Now.ToString("yyyy-MM-dd");
            OrderComment = "";
            StudyInstanceUID = "";


        }


        private string _PatientID;

        public string PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        private string _RemotePID;

        public string RemotePID
        {
            get { return _RemotePID; }
            set { _RemotePID = value; }
        }

        private string _EnglishName;

        public string EnglishName
        {
            get { return _EnglishName; }
            set { _EnglishName = value; }
        }

        private string _LocalName;

        public string LocalName
        {
            get { return _LocalName; }
            set { _LocalName = value; }
        }

        private string _Birthday;

        public string Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private string _Alias;

        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }
        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Telephone;

        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }
        private string _Marital;

        public string Marital
        {
            get { return _Marital; }
            set { _Marital = value; }
        }
        private string _PatientType;

        public string PatientType
        {
            get { return _PatientType; }
            set { _PatientType = value; }
        }
        private string _Region;

        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }
        private string _ClinicNo;

        public string ClinicNo
        {
            get { return _ClinicNo; }
            set { _ClinicNo = value; }
        }
        private string _BedNo;

        public string BedNo
        {
            get { return _BedNo; }
            set { _BedNo = value; }
        }
        private string _IsVip;

        public string IsVip
        {
            get { return _IsVip; }
            set { _IsVip = value; }
        }
        private string _InHospitalNo;

        public string InHospitalNo
        {
            get { return _InHospitalNo; }
            set { _InHospitalNo = value; }
        }
        private string _PatientComment;

        public string PatientComment
        {
            get { return _PatientComment; }
            set { _PatientComment = value; }
        }
        private string _RemoteAccNo;

        public string RemoteAccNo
        {
            get { return _RemoteAccNo; }
            set { _RemoteAccNo = value; }
        }

        private string _HisID;

        public string HisID
        {
            get { return _HisID; }
            set { _HisID = value; }
        }

        private string _CardNo;

        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }

        private string _MedicareNo;

        public string MedicareNo
        {
            get { return _MedicareNo; }
            set { _MedicareNo = value; }
        }


        private string _AccNo;

        public string AccNo
        {
            get { return _AccNo; }
            set { _AccNo = value; }
        }
        private string _ApplyDept;

        public string ApplyDept
        {
            get { return _ApplyDept; }
            set { _ApplyDept = value; }
        }
        private string _AppDoctor;

        public string AppDoctor
        {
            get { return _AppDoctor; }
            set { _AppDoctor = value; }
        }

        private string _Observation;

        public string Observation
        {
            get { return _Observation; }
            set { _Observation = value; }
        }


        private string _VisitComment;

        public string VisitComment
        {
            get { return _VisitComment; }
            set { _VisitComment = value; }
        }

        private string _CheckNotice;

        public string CheckNotice
        {
            get { return _CheckNotice; }
            set { _CheckNotice = value; }
        }

        private string _RegisterDt;

        public string RegisterDt
        {
            get { return _RegisterDt; }
            set { _RegisterDt = value; }
        }

        private string _OrderComment;

        public string OrderComment
        {
            get { return _OrderComment; }
            set { _OrderComment = value; }
        }
        private string _StudyInstanceUID;

        public string StudyInstanceUID
        {
            get { return _StudyInstanceUID; }
            set { _StudyInstanceUID = value; }
        }
    }
}
