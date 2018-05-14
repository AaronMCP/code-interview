using System;
using System.Collections.Generic;
using System.Text;
using Server.Business.ClientFramework;
namespace Server.Utilities.HippaLogTool
{
    public class HippaLogTool
    {
        private HippaLogTool()
        { }
        private static HippaLog hippaservice = new HippaLog();
        public static int AuditUserAuthMsg(string EventTypeCode, bool isSuccess)
        {
            return hippaservice.AuditUserAuthMsg(EventTypeCode, isSuccess);
        }
        public static int AuditPatientRecordEvtMsg(string ActionCode, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditPatientRecordEvtMsg(ActionCode, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public static int AuditPatientRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditPatientRecordEvtMsg(ActionCode, TypeCode, UserID, UserName, RoleName, NetworkIP, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        public static int AuditOrderRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditOrderRecordEvtMsg(ActionCode, AccessionNumber, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public static int AuditOrderRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditOrderRecordEvtMsg(ActionCode, TypeCode, UserID, UserName, RoleName, NetworkIP, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        public static int AuditEvtMsg(string EventID, string ActionCode, string TypeCode, string EventOutComeIndicator, string UserID, string UserName, string RoleName, string NetworkIP, string UserIsRequestor, string ObjectTypeCode, string ObjectTypeCodeRole, string ObjectIDTypeCode, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditEvtMsg(EventID, ActionCode, TypeCode, EventOutComeIndicator, UserID, UserName, RoleName, NetworkIP, UserIsRequestor, ObjectTypeCode, ObjectTypeCodeRole, ObjectIDTypeCode, ObjectID, ObjectName, ObjectDetail, ObjectDescription, isSuccess);
        }
        //   int AuditProcedureRecordEvtMsg(String ActionCode, String UserID, String RP, String ObjectID, String ObjectName, String ObjectDescription);
        public static int AuditProcedureRecordEvtMsgQC(string ActionCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditProcedureRecordEvtMsgQC(ActionCode, AccessionNumber, RP, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public static int AuditProcedureRecordEvtMsg(string EventTypeCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditProcedureRecordEvtMsg(EventTypeCode, AccessionNumber, RP, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public static int AuditPatientCareAssignMsg(string ActionCode, string AccessionNumber, string ReportID, string ReportName, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditPatientCareAssignMsg(ActionCode, AccessionNumber, ReportID, ReportName, ObjectID, ObjectName, ObjectDescription, isSuccess);
        }
        public static int AuditChargeRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ChargeID, string ChargeDescription, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditChargeRecordEvtMsg(ActionCode, AccessionNumber, ObjectID, ObjectName, ChargeID, ChargeDescription, ObjectDescription, isSuccess);
        }

        public static int AuditSuppliesRecordEvtMsg(string ActionCode, string strEventTypeCode, string ObjectID, string ObjectName, string SuppliesID, string SuppliesDescription, string ObjectDescription, bool isSuccess)
        {
            return hippaservice.AuditSuppilesRecordEvtMsg(ActionCode, strEventTypeCode,ObjectID, ObjectName, SuppliesID, SuppliesDescription, ObjectDescription, isSuccess);
        }

    }
}
