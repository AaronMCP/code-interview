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

namespace Server.Business.QualityControl
{
    /// <summary>
    /// this class uses to manage the the Quality Control business logic
    /// on server side
    /// </summary>
    public interface IQualityControlBusiness
    {
        /// <summary>
        /// Query list of patients 
        /// </summary>
        /// <param name="strPatientName">Local name of target patient</param>
        /// <param name="strPatientID">PatientID of target patient</param>
        /// <param name="strBeginDt">Begin create date for query</param>
        /// <param name="strEndDt">End create date for query</param>
        /// <param name="bIsVIP">True means VIP patients will be queried</param>
        /// <param name="bar">Return search result</param>
        /// <returns></returns>
        int QueryPatient(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, BaseActionResult bar);
        /// <summary>
        /// Query list of patientList table 
        /// </summary>
        /// <param name="strPatientName">Local name of target patient</param>
        /// <param name="strPatientID">PatientID of target patient</param>
        /// <param name="strBeginDt">Begin create date for query</param>
        /// <param name="strEndDt">End create date for query</param>
        /// <param name="bIsVIP">True means VIP patients will be queried</param>
        /// <param name="bar">Return search result</param>
        /// <returns></returns>
        int QueryPatientList(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, BaseActionResult bar);

        /// <summary>
        /// Query list of Orders >= exam status
        /// </summary>
        /// <param name="strPatientGuid">
        /// GUID of the patient whose orders will be queried
        /// </param>
        /// <param name="bar">
        /// Return search result
        /// </param>
        /// <returns></returns>
        int QueryOrder(string strPatientGUID, string status, BaseActionResult bar);

        /// <summary>
        /// Query list of RP
        /// </summary>
        /// <param name="strOrderGUID">
        /// GUID of the order that RP belongs to
        /// </param>
        /// <param name="bar">
        /// Return search result
        /// </param>
        /// <returns></returns>
        int QueryRP(string strOrderGUID, BaseActionResult bar, string strRPGuid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bExcludeDeletedReport"></param>
        /// <param name="pID"></param>
        /// <param name="patientName"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        int QueryRPwithDuplicatedReport(bool bExcludeDeletedReport, string pID, string patientName, string dt1, string dt2, BaseActionResult bar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rpGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        int QueryDuplicatedReport(string rpGuid, BaseActionResult bar);

        /// <summary>
        /// Update register information of a patient
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of patient 
        /// </param>
        /// <param name="bar">
        /// Contains result of the execution
        /// </param>
        /// <returns></returns>
        int UpdatePatient(bool SendToGateWay, DataSet ds, BaseActionResult bar);
        /// <summary>
        /// Update information of one Order
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of order
        /// </param>
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int UpdateOrder(bool SendToGateWay, DataSet ds, BaseActionResult bar);
        /// <summary>
        /// Update information of one RP
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="ds">
        /// Contains updated information of RP
        /// </param>
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int UpdateRP(bool SendToGateWay, DataSet ds, BaseActionResult bar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportGuid"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        int UpdateActiveReport(string reportGuid, BaseActionResult bar);

        /// <summary>
        /// Delete one order from database
        /// </summary>
        /// <param name="SendToGateWay">
        /// True means that updating will be sent to Gateway
        /// </param>
        /// <param name="strOrderGuid">
        /// GUID of target order
        /// </param>
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int DeleteOrder(bool SendToGateWay, string strOrderGuid, string strLoginName, string strLocalName, BaseActionResult bar);
        #region EK_HI00063904 jameswei 2007-12-13 get the acc's requisition file name and relativepath
        int GetRequisitionInfo(string strAccNo, BaseActionResult bar);
        #endregion
        int DeleteRP(bool SendToGateWay, string strVisitGuid, string strOrderGuid, string strRPs, string strLoginName, string strLocalName, BaseActionResult bar);

        int DeletePatient(bool sendToGateway, string patientGuid);

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
        /// </param>
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int AssignOrderToPatient(bool SendToGateWay, string strVisitGuid, string strOrderGuid, string strPatientGUID, string bDelAfterMerge, BaseActionResult bar);
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
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int MergePatients(bool SendToGateWay, Dictionary<int, string> dicPatients, string bDelAfterMerge, BaseActionResult bar);

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
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int QueryLock(string stOwner, string stBeginTime, string stEndTime, BaseActionResult bar);

        /// <summary>
        /// <param name="strObjectType">
        /// Object type
        /// </param>
        /// <param name="strObjectGuid">
        /// Object Guid
        /// </param>
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int QueryLock(string strObjectType, string strObjectGuid, BaseActionResult bar);

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
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int LockObject(int strObjectType, string strObjectGuid, string strOwner, string strOwnerIP, BaseActionResult bar);
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
        /// <param name="bar">
        /// Return execution result
        /// </param>
        /// <returns></returns>
        int UnLockObject(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, BaseActionResult bar);

        /// <summary>
        /// merge all gived orders's rps to target order and delete the gived orders
        /// </summary>
        /// <param name="dicOrders">the orders to be merged</param>
        /// <param name="bDelAfterMerge">current always true</param>
        /// <param name="strTargetOrderGUID">the target order to be merged to</param>
        /// <param name="bSendToGateWay">whether to send to gateway</param>
        /// <param name="bChargeFuncActive">whether to update charge associated info</param>/// 
        /// <param name="strError">the error message will be return</param>
        /// <param name="dtInfo">the detail info of why do false,current only use for lock info</param>
        bool MergeOrders(Dictionary<string, string> dicOrders, string bDelAfterMerge, string strTargetOrderGUID, string strTargetAccessionNumber, string stringSrcPatientName, bool bSendToGateWay, bool bChargeFuncActive, DataSetActionResult dsar);

        //merge some source patients to one target patient
        /// <summary>
        /// merge patients to target patient
        /// </summary>
        /// <param name="dicPatients">the source patients to be merged</param>
        /// <param name="bDelAfterMerge">wether delete the source patients after merge</param>
        /// <param name="strTargetPatientGUID">the destination patient guid to merged to</param>
        /// <param name="bSendToGateWay">wether to notify gateway</param>
        /// <param name="dsar">DataSetActionResult dsar</param>
        /// <returns>if successfull return true else false</returns>
        bool MergePatients(Dictionary<string, string> dicPatients, string bDelAfterMerge, string strTargetPatientGUID, string strTargetPatientName, bool bSendToGateWay, DataSetActionResult dsar);

        /// <summary>
        /// move orders to a target patient
        /// </summary>
        /// <param name="dicOrders">the orders' guid dictionary</param>
        /// <param name="strTargetPatientGUID">the target patient guid</param>
        /// <param name="strTargetPatientName">the target patient guid</param>/// 
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="bar">BaseActionResult bar</param>
        bool MoveOrders(Dictionary<string, string> dicOrders, string strTargetPatientGUID, string strSrcPatientName, string strTargetPatientName, bool bSendToGateWay, DataSetActionResult bar);

        /// <summary>
        /// move all rps to a target order
        /// </summary>
        /// <param name="dicRPs">the rps' guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="bar">DataSetActionResult bar</param>
        /// <returns>if successfull return true else false</returns>
        bool MoveRPs(Dictionary<string, string> dicRPs, string strTargetOrderGUID, string strTargetAccessionNumberstring, string strSrcPatientName, bool bSendToGateWay, DataSetActionResult bar);

        #region merge order by level, US26313, 2015-07-30
        /// <summary>
        /// merge requisition to a target order
        /// </summary>
        /// <param name="dicRequisition">the requisition guid dictionary</param>
        /// <param name="strTargetAccNo">the target AccNo</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        bool MergeRequisition(Dictionary<string, string> dicRequisition, string strTargetAccNo, bool bSendToGateWay, DataSetActionResult bar);
        /// <summary>
        /// merge charge to a target order
        /// </summary>
        /// <param name="dicCharge">the charge guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="bar">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        bool MergeCharge(Dictionary<string, string> dicCharge, string strTargetOrderGUID, bool bSendToGateWay, DataSetActionResult bar);

        #endregion

        /// <summary>
        /// query the chage order list
        /// </summary>
        /// <param name="strCondition">where condition</param>
        /// <param name="bar">DataSetActionResult bar</param>
        /// <returns>if successfull return true else false</returns>
        int ChargeQueryOrderList(string strCondition, BaseActionResult bar);
        /// <summary>
        /// query the chage item list
        /// </summary>
        /// <param name="strCondition">where condition</param>
        /// <param name="bar">DataSetActionResult bar</param>
        /// <returns>if successfull return true else false</returns>
        int ChargeQueryItemList(string strCondition, BaseActionResult bar);

        /// <summary>
        /// Add charge
        /// </summary>
        /// <param name="model">chargeModel</param>
        /// <param name="bar"></param>
        /// <returns></returns>
        int AddChargeModel(BaseModel model, decimal totalFee, BaseActionResult bar);
        
        /// <summary>        
        /// Confirm,Refund,Cancel,Deduct.        
        /// </summary>
        /// <param name="action"></param>
        /// <param name="model"></param>
        /// <param name="totalFee"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        int ChargeOperation(Charge_Action action, ChargeModel model, decimal totalFee, BaseActionResult bar);

        /// <summary>
        /// relate the patients
        /// </summary>
        /// <param name="strPatientGuids">Patient guids</param>
        /// <returns>if successfull return true else false</returns>
        bool RelatePatient(string strPatientGuids, BaseActionResult bar);
        /// <summary>
        /// UnRelate the patient
        /// </summary>
        /// <param name="strPatientGuids">Patient guid</param>
        /// <returns>if successfull return true else false</returns>
        bool UnRelatePatient(string strPatientGuid, BaseActionResult bar);
        /// <summary>
        /// QueryPatientByRelatedID
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="strRelateID"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        int QueryPatientByRelatedID(string strPatientGuid, string strRelateID, BaseActionResult bar);
        /// <summary>
        /// HasJustTwoPatientRelated
        /// </summary>
        /// <param name="patientOneGuid"></param>
        /// <param name="patientTwoGuid"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        int HasOtherPatientRelated(string patientOneGuid, string patientTwoGuid, BaseActionResult bar);
        /// <summary>
        /// Auto relate the patients with patins in DB by the condition
        /// </summary>
        /// <param name="strPatientGuids"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        bool RelatePatientsByCondition(string strPatientGuids, string birthDateOffset,string createDateRange, BaseActionResult bar);

        #region QC_QueryNotRefundedOrderCharge, 2015-07-20, Oscar added (US26283)

        /// <summary>
        /// Get the count of charges that haven't been refunded.
        /// </summary>
        /// <param name="patientGuid"></param>
        /// <param name="orderGuid"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        int QueryNotRefundedOrderCharge(string patientGuid, string orderGuid, BaseActionResult bar);

        #endregion
    }
}
