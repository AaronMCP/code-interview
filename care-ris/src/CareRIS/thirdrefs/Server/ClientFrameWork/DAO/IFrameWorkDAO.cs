using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Panels;
using Server.ClientFramework.Common.Data.Login;
using Server.ClientFramework.Common.Data.Profile;
using System.Data;
namespace Server.DAO.ClientFramework
{
    public interface IFrameWorkDAO
    {
        DsPanelInfo LoadDsPanelInfo();
        DsProfile LoadSystemProfile(string ModuleID);
        DsProfile LoadSiteProfile(string ModuleID, string SiteName);
        DataSet LoadAllSiteProfile(string ModuleID);
        DsProfile LoadRoleProfile(string ModuleID, string RoleID);
        string GetRoleProfileValue(string name, string roleId);
        DsProfile LoadUserProfile(string ModuleID, string RoleID, string UserID);
        int SaveUserProfile(DataSet Ds, string RoleName, string UserGUID);
        string GetUserGuid(string LoginName, string Password, string RoleName);
        string GetUserGuidByDmnLgnName(string DomainLoginName, string RoleName);
        string GetUserGuidByLoginName(string LoginName, string RoleName);
        string GetLocalName(string strUserGuid);
        string GetRoleName(string strUserGuid);
        int IsOnLine(string szUserGuid, string szRoleName, string szIpAddress, string szUrl, string szSessionID, bool IsLogined, bool IsWebAccess, bool IsHijackLogin, bool IsSelfService);
        void LogOutBySessionID(string SessionID);
        string GetDbServerTime();
        void LogOut(string UserGuid, bool bWebUser);
        //void LogOut(TimeSpan objSpan);
        int GetOnlineUserNo(bool bWebUser, bool bSelfUser, string ipaddress);
        DsRole LoadAllRole();
        DsConfigDic LoadConfigDic(int Type);
        DataSet LoadDictionary();
        DataSet GetExamInfo(string strExamDomain, string strAccNo);
        DataSet GetOnlineClients();
        DataSet GetFilterSite(string strUserGuid, string strRoleName, string strCurSite, string strMatchingName);
        string GetSite(string settingSite);
        //void UpdateLatestAccessTime(string UserGuid);
        void OnlineStatusInit();
        int WriteConfigDicRow(DsConfigDic.ConfigDicRow row, int iType);
        KeyValuePair<int, int> ExpireDayCheck(string LoginName);
        KeyValuePair<int, int> GetExpireDays(string LoginName);

        DataSet LoadAllProfile();

        #region HIPPA Interface
        int AuditUserAuthMsg(string EventTypeCode, bool bHijackLogin, bool isSuccess);
        int AuditPatientRecordEvtMsg(string ActionCode, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess);
        int AuditPatientRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess);
        int AuditOrderRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess);
        int AuditOrderRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess);
        int AuditEvtMsg(string EventID, string ActionCode, string TypeCode, string EventOutComeIndicator, string UserID, string UserName, string RoleName, string NetworkIP, string UserIsRequestor, string ObjectTypeCode, string ObjectTypeCodeRole, string ObjectIDTypeCode, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess);
        //   int AuditProcedureRecordEvtMsg(String ActionCode, String UserID, String RP, String ObjectID, String ObjectName, String ObjectDescription);
        int AuditProcedureRecordEvtMsgQC(string ActionCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess);
        int AuditProcedureRecordEvtMsg(string EventTypeCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess);
        int AuditPatientCareAssignMsg(string ActionCode, string AccessionNumber, string ReportID, string ReportName, string ObjctID, string ObjectName, string ObjectDescription, bool isSuccess);

        int AuditChargeRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ChargeID, string ChargeDescription, string ObjectDescription, bool isSuccess);
        int AuditSuppilesRecordEvtMsg(string ActionCode, string strEventTypeCode, string ObjectID, string ObjectName, string SupplierID, string SupplierDescription, string ObjectDescription, bool isSuccess);
        #endregion
        #region BillBoard
        DataSet GetAllNotesInDB(string userGuid, string roleName);
        DataSet GetBillBoardDictionaryData();
        #endregion


        DsRole LoadAllRole4Login();

        void UpdatePasswordNewEncry(string userguid, string password);
    }
}