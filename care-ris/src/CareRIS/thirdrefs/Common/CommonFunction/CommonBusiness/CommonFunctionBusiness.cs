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
using Common.ActionResult;
using CommonGlobalSettings;

namespace Server.Business.Common
{
    public interface ICommonFunctionBusiness
    {
        bool SetCertificatePassword(string strCertificatePassword);
        bool GetCertificatePassword(ref string strCertificatePassword);
        bool UpdateCertificate(string strUserGuid, string strIkeySn, string strPrivateKey, string strPublicKey, string strOrgOwner, DataSet ds);
        bool GetCertificate(string strUserGuid, string strIkeySn, DataSet ds);
        bool GetProfileInfo(string strRoleName, DataSet ds);

        bool GetModalityType(DataSet ds);
        bool GetBodyCategory(string strModalityType, DataSet ds);
        bool GetBodyPart(string strModalityType, string strBodyCategory, DataSet ds);
        bool GetCheckingItem(string strModalityType, string strBodyCategory, string strBodyPart, DataSet ds);
        bool GetExamSystem(string strModalityType, string strBodyPart, DataSet ds);
        bool GetModality(string strModalityType, DataSet ds, string site, string withPublic);
        bool LoadShortcut(int category, string strUserID, ref DataSet reDataSet);
        int AddShortcut(int type, int category, string strName, string strValue, string strUserID, ref DataSet reDataSet);
        int EditShortcut(string strGuid, int type, int category, string strName, string strValue, string strUserID, string strCurUser, string strManageSS, ref DataSet reDataSet);
        bool DeleteShortcut(string strGuid, int category, string strUserID, string strSite, ref DataSet reDataSet);
        bool GetStaff(string strDegreeName, DataSet ds);
        string GetLocalName(string strLoginName);
        string GetLocalNameByUserGuid(string userGuid);
        bool GetProcedureDefaultValue(string strModalityType, string strBodyCategory, string strBodyPart, string strCheckingItem, DataSet ds);
        bool GetProcedureCode(string strProcedureCode, string strShortcutCode, DataSet ds);
        bool ReclaimID(int nTag, string strValue, int nLength);
        bool GetExtOptional(string strTableName, DataSet ds);
        string GetShorcutName(string strGuid);
        FtpModel GetFtpParameters();
        DataTable GetBackupFileList(string stTables, bool bFailRetry, string stRetryFlag);
        void UpdateDatabaseMark(bool bSuccess, string stUnicGUID, string stComment, string stTableName);
        bool GetRequisition(string strAccNo, string strDataCenter, string strDomain, DataSet ds);
        bool GetRequisition(bool loadArchive, string strAccNo, string strDataCenter, string strDomain, DataSet ds);
        string GetFieldValue(string selectTable, string selectField, string uniqueIDField, string uniqueIDFieldValue);
        bool GetAllProcedureCode(DataSet ds);
        bool GetDateTypeByCalendar(string bookingDate, string modality, ref string dateType, ref string availableDate);
        bool GetCurSiteProcedureCode(DataSet ds);
        bool GetProcedureCodeBySite(string strSite, DataSet ds);
        bool CheckClinic(string roleName);

        DataSet GetConditionColumn(Dictionary<string, object> paramMap);
        DataSet GetGridColumn(Dictionary<string, object> paramMap);
        DataSet GetUserList();
        DataSet GetIMUserList(string type);
        DataSet GetIMLog(string strCondition);
        DataSet GetConditionRelatedControlData(Dictionary<string, object> paramMap);

        int GeneratePatientIDEx(BaseActionResult bar);
        int GenerateAccNoEx(BaseActionResult bar);
        int QueryPatientEx(string strKey, string strValue, bool bIsVIP, DataSetActionResult dsar);
        int RegIntegrationSavePatient(PatientModel pModel, BaseActionResult bar);
        int LocatePatientByHISInfo(string strLocalPID, string HISID, string strPatientName, DataSetActionResult dsar);
        int CheckOrderStatus(string strPatientGuid, string strOrderGuid, string strCurUser, string strLockType, BaseActionResult bar);
        DataSet GetDictionaryValue(string tag);
        int GetRequisitionType(string strAccNo, DataSet ds);
        int GetRequisitionType(bool loadArchive, string strAccNo, DataSet ds);
        bool SaveERequisitionTemplateGuid(string strAccNo, string printTemplateGuid);
        bool GetBookingNoticeTemplate(DataSet ds);
        DataSet GetDoctorSupervisor();
        DataSet QueryAllOnlineOfflineUser();
        string GetSystemProfileValue(string name, string moduleId);
        bool GetOrderInfo(string strOrderGuid, DataSet dsOrderMessage);
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

        void SaveSignedHistory(SignHistoryModel model);
        void SaveSignedData(SignHistoryModel model);
        DataTable GetLatestSignedData(string reportGuid);
        DataTable GetLatestEveryActionSignedData(string reportGuid);

        string GetUserTopLevleRole(string loginName);
        DataTable GetUserAllRoles(string loginName);
        bool IsReferralAndReadOnly(string orderGuid);
        bool GetRoleBySite(string strSite, DataSet ds);
        DataTable GetClientProfile(string clientID);
        bool SaveClientProfile(DataTable dtClientProfiles);
        bool SaveSystemProfile(DataTable dtProfiles);
        bool LastReferralStatus(string strReferralID, ref int nLastRefStatus);
        bool RemoveOrderMessageFlag(string param, ref string errorinfo);
        bool AddOrderMessageFlag(string param, ref string errorinfo);
    }
}
