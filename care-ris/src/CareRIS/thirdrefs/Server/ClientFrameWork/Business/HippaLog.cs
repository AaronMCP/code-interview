
using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Profile;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class HippaLog : MarshalByRefObject
    {
        #region HIPPA Interface
        public int AuditUserAuthMsg(string EventTypeCode, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditUserAuthMsg(EventTypeCode, false, isSuccess);
        }

        public int AuditUserAuthMsg(string EventTypeCode, bool bHijackLogin, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditUserAuthMsg(EventTypeCode, bHijackLogin, isSuccess);
        }

        public int AuditPatientRecordEvtMsg(string ActionCode, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditPatientRecordEvtMsg(ActionCode, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public int AuditPatientRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditPatientRecordEvtMsg(ActionCode, TypeCode, UserID, UserName, RoleName, NetworkIP, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        public int AuditOrderRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditOrderRecordEvtMsg(ActionCode, AccessionNumber, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public int AuditOrderRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditOrderRecordEvtMsg(ActionCode, TypeCode, UserID, UserName, RoleName, NetworkIP, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        public int AuditEvtMsg(string EventID, string ActionCode, string TypeCode, string EventOutComeIndicator, string UserID, string UserName, string RoleName, string NetworkIP, string UserIsRequestor, string ObjectTypeCode, string ObjectTypeCodeRole, string ObjectIDTypeCode, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditEvtMsg(EventID, ActionCode, TypeCode, EventOutComeIndicator, UserID, UserName, RoleName, NetworkIP, UserIsRequestor, ObjectTypeCode, ObjectTypeCodeRole, ObjectIDTypeCode, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        //   int AuditProcedureRecordEvtMsg(String ActionCode, String UserID, String RP, String ObjectID, String ObjectName, String ObjectDescription);
        public int AuditProcedureRecordEvtMsgQC(string ActionCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditProcedureRecordEvtMsgQC(ActionCode, AccessionNumber, RP, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public int AuditProcedureRecordEvtMsg(string EventTypeCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditProcedureRecordEvtMsg(EventTypeCode, AccessionNumber, RP, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public int AuditPatientCareAssignMsg(string ActionCode, string AccessionNumber, string ReportID, string ReportName, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditPatientCareAssignMsg(ActionCode, AccessionNumber, ReportID, ReportName, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }

        public int AuditChargeRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ChargeID, string ChargeDescription, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditChargeRecordEvtMsg(ActionCode, AccessionNumber, ObjectID, ObjectName, ChargeID, ChargeDescription, ObjectDescription, isSuccess);
        }

        public int AuditSuppilesRecordEvtMsg(string ActionCode, string strEventTypeCode, string ObjectID, string ObjectName, string SupplierID, string SupplierDescription, string ObjectDescription, bool isSuccess)
        {
            return DaoInstanceFactory.GetInstance().AuditSuppilesRecordEvtMsg(ActionCode, strEventTypeCode,ObjectID, ObjectName, SupplierID, SupplierDescription, ObjectDescription, isSuccess);
        }
        #endregion
    }
}
