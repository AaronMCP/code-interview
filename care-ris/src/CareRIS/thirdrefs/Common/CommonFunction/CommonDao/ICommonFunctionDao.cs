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
/*                        Author : Bruce Deng
/****************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CommonGlobalSettings;

namespace Server.DAO.Common
{
    public interface ICommonFunctionDao
    {


        /// <summary>
        /// Load All Modality Type
        /// </summary>
        /// <param name="ds">DataSet to be added result table</param>
        /// <returns>true if success false if fail</returns>
        bool GetModalityType(DataSet ds);

        /// <summary>
        /// Get body Category by modality type
        /// </summary>
        /// <param name="strModalityType">Modality type will be used as query condition</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetBodyCategory(string strModalityType, DataSet ds);


        /// <summary>
        /// Get Bodypart by modalitytype and bodycategory
        /// </summary>
        /// <param name="strBodyCategory"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetBodyPart(string strModalityType, string strBodyCategory, DataSet ds);

        /// <summary>
        /// Get Checking item by ModalityType and BodyCategory and BodyPart
        /// </summary>
        /// <param name="strModality"></param>
        /// <param name="strBodyCategory"></param>
        /// <param name="strBodyPart"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetCheckingItem(string strModalityType, string strBodyCategory, string strBodyPart, DataSet ds);


        /// <summary>
        /// Get examination system by modalitytype and bodypart
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="strBodyPart"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetExamSystem(string strModalityType, string strBodyPart, DataSet ds);

        /// <summary>
        /// Get Modality by modality type
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetModality(string strModalityType, DataSet ds, string site, string withPublic);
        bool GetAllModality(string strModalityType, DataSet ds);
        /// <summary>
        /// Load the global shortcut and user shortcut
        /// </summary>
        /// <param name="category"></param>
        /// <param name="userID"></param>
        /// <param name="reDataSet">
        /// Return the result in dataset type
        /// </param>

        /// <returns></returns>
        bool LoadShortcut(int category, string strUserID, ref DataSet reDataSet);

        /// <summary>
        /// Get Shortcut name
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        string GetShorcutName(string strGuid);

        /// <summary>
        /// Add a new shortcut
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        int AddShortcut(int type, int category, string strName, string strValue, string strUserID, ref DataSet reDataSet);

        /// <summary>
        /// Update a existing shortcut. At first delete the old one,then add the new one ;
        /// </summary>
        /// <param name="guid">
        ///   A unique value of each shortcut
        /// </param>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        int EditShortcut(string strGuid, int type, int category, string strName, string strValue, string strUserID, string strCurUser, string strManageSS, ref DataSet reDataSet);

        /// <summary>
        /// Delete the shortcut
        /// </summary>
        /// <param name="strGuid">
        /// A unique value of each shortcut
        /// </param>
        /// <returns></returns>
        bool DeleteShortcut(string strGuid, int category, string strUserID, string strSite, ref DataSet reDataSet);


        /// <summary>
        /// Get staff by role
        /// </summary>
        /// <param name="strDegreeName"></param>
        /// <returns></returns>
        bool GetStaff(string strDegreeName, DataSet ds);

        /// <summary>
        /// Get Title
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <returns></returns>
        string GetLocalName(string strLoginName);

        string GetLocalNameByUserGuid(string userGuid);

        /// <summary>
        /// Get ProcedureCode default value
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="strBodyCategory"></param>
        /// <param name="strBodyPart"></param>
        /// <param name="strCheckingItem"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetProcedureDefaultValue(string strModalityType, string strBodyCategory, string strBodyPart, string strCheckingItem, DataSet ds);

        /// <summary>
        /// Get Procedurecode record by procedurecode or ShortcutCode
        /// </summary>
        /// <param name="strProcedureCode"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetProcedureCode(string strProcedureCode, string strShortcutCode, DataSet ds);


        /// <summary>
        /// Reclaim the id as BookingNo,PatientID and AccNo
        /// </summary>
        /// <param name="nTag"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        bool ReclaimID(int nTag, string strValue);


        /// <summary>
        /// Get the optional column for the optional table
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetExtOptional(string strTableName, DataSet ds);

        FtpModel GetFtpParameters();

        DataTable GetBackupFileList(string stTables, bool bFailRetry, string stRetryFlag);

        /// <summary>
        /// Used in Backup module for updating the Backupmark in table  tRequisition
        /// </summary>
        /// <param name="stRequGUID"> the GUID of requisition record that has been backuped</param>
        void UpdateDatabaseMark(bool bSuccess, string stUnicGUID, string stComment, string stTableName);



        /// <summary>
        /// Get requisition by orderguid
        /// </summary>
        /// <param name="strOrderGuid"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        bool GetRequisition(string strAccNo, string strDataCenter, string strDomain, DataSet ds);
        bool GetRequisition(bool loadArchive, string strAccNo, string strDataCenter, string strDomain, DataSet ds);
        string GetFieldValue(string selectTable, string selectField, string uniqueIDField, string uniqueIDFieldValue);
        bool GetAllProcedureCode(DataSet ds);
        bool GetDateTypeByCalendar(string bookingDate, string modality, ref string dateType, ref string availableDate);
        bool GetCurSiteProcedureCode(DataSet ds);
        bool GetCurSiteExamSystem(string strModalityType, string strBodyPart, DataSet ds);
        bool GetProcedureCodeBySite(string strSite,DataSet ds);


        bool CheckClinic(string roleName);
        /// <summary>
        /// Get all WarningTime 
        /// </summary>
        /// <returns></returns>
        bool GetAllWarningTime(DataSet ds);

        bool UpdateCertificate(string strUserGuid, string strIkeySn, string strPrivateKey, string strPublicKey, string strOrgOwner, DataSet ds);
        bool GetCertificate(string strUserGuid, string strIkeySn, DataSet ds);
        bool SetCertificatePassword(string strCertificatePassword);
        bool GetCertificatePassword(ref string strCertificatePassword);
        bool GetProfileInfo(string strRoleName, DataSet ds);

        DataSet GetConditionColumn(Dictionary<string, object> paramMap);
        DataSet GetGridColumn(Dictionary<string, object> paramMap);
        DataSet GetUserList();
        DataSet GetIMUserList(string type);
        DataSet GetIMLog(string condition);
        DataSet GetConditionRelatedControlData(Dictionary<string, object> paramMap);
        DataSet GetDictionaryValue(string tag);

        bool GeneratePatientIDEx(ref string strPatientID, ref string strError);
        bool GenerateAccNoEx(ref string strAccNo, ref string strError, ref int iPolicy);
        bool RegIntegrationSavePatient(PatientModel pModel, ref string strError);
        bool LoadPatient(string strKey, string strValue, DataTable dtPatient, ref string strError);
        bool LocatePatientByHISInfo(string strLocalPID, string HISID, string strPatientName, ref DataTable dtPatient, ref string szError);
        bool CheckOrderStatus(string strPatientGuid, string strOrderGuid, string strCurUser, string strLockType, ref int nRetCode, ref string szError);
        int GetRequisitionType(string strAccNo, DataSet ds);
        int GetRequisitionType(bool loadArchive, string strAccNo, DataSet ds);
        bool SaveERequisitionTemplateGuid(string strAccNo, string printTemplateGuid);
        /// <summary>
        /// Get all booking notice template
        /// </summary>
        /////////////////////////////////////////
        bool GetBookingNoticeTemplate(DataSet ds);
        DataSet GetDoctorSupervisor();
        DataSet QueryAllOnlineOfflineUser();
        bool GetOrderInfo(string strOrderGuid, DataSet dsOrderInfo);
        bool UpdateOrderMessage(string strOrderGuid, string strOrderMessage, string strApplyDept, string strApplyDoctor);

        bool IsArchived(string strPatientID, ref int nArchived);
        bool SendFetchCommand(string strPatientID);

        DataSet COMMON_getDataTable(string tableName, string matchingKey, string matchingValue);
        object COMMON_queryDataTable(object parameters);
        DataSet getDynamicFormStructure();

        bool PostEvent(DataTable dtModel);
        bool PostMessage(DataTable dtModel);
        DataSet GetValidUserList();
        DataSet GetValidApproverDoctor();

        bool CanEditOrderMessage(string strOrderGuid);

        #region Profile Method

        ///////////Get Profile Value//////////////////////
        string GetSystemProfileValue(string name, string moduleId);
        string GetProfileValue(string name, string roleName, string moduleId);
        void UpdateSystemProfile(string name, string moduleId, string value);
        #endregion

        #region DigitalSign

        void SaveSignedHistory(SignHistoryModel model);

        void SaveSignedData(SignHistoryModel model);

        DataTable GetLatestSignedData(string reportGuid);

        DataTable GetLatestEveryActionSignedData(string reportGuid);

        #endregion

        string GetUserTopLevleRole(string loginName);

        DataTable GetUserAllRoles(string loginName);

        bool IsReferralAndReadOnly(string orderGuid);


        bool GetRoleBySite(string strSite, DataSet ds);

        DataTable GetClientProfile(string clientID);

        bool SaveClientProfile(DataTable dtClientProfiles);

        bool SaveSystemProfile(DataTable dtProfiles);

        bool LastReferralStatus(string strReferralID,ref int nLastRefStatus);

        bool RemoveOrderMessageFlag(string param, ref string errorinfo);
        bool AddOrderMessageFlag(string param, ref string errorinfo);
    }
}
