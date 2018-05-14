using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;
using Server.Utilities.LogFacility;
using ServerCommon = Server.DAO.Common;

namespace Server.ReportDAO
{
    /// <summary>
    /// structure of ReportInfo
    /// </summary>
    public struct tagReportInfo
    {
        public int status;
        public string dept;
        public string orderGuid;
        public string reportGuid;
        public string reportName;
        public string AccNO;
        public string patientID;
        public string patientName;
        public string patientLocalName;
        public string patientAlias;
        public string patientAddress;
        public string patientPhone;
        public string patientMarriage;
        public string patientType;
        public string patientDemo;
        public string remotePID;
        public string gender;
        public string MedicareNo;
        public string ReferenceNo;
        public string modality;
        public string modalityType;
        public string operationStep;
        public string reportCreater;
        public string reportSubmitter;
        public string reportApprover;
        public DateTime birthday;
        public DateTime reportCreateDt;
        public DateTime reportApproveDt;
        public DateTime reportSubmitDt;
        public DateTime reportRejectDt;
        public DateTime registerDt;
        public DateTime examineDt;
        public string wys;
        public string wyg;
        public string wysText;
        public string wygText;
        public string reportCreater_LocalName;
        public string reportSubmitter_LocalName;
        public string reportApprover_LocalName;
        public string inHospitalRegion;
        public string visitNo;
        public string bedNo;
        public string isVIP;
        public string inHospitalNo;
        public string patientComment;
        public string remoteAccNo;
        public string applyDoctor;
        public string visitComments;
        public string observation;
        public string orderComments;
        public string duration;
        public string technician;
        public string technician__LocalName;
        public string bodypart;
        public string procedureCode;
        public string procedureDesc;
        public string procedureComments;
        public string isCharge;
        public string techinfo;
        public string charge;
        public string hisid;
        public string cardno;

    }

    /// <summary>
    /// pub functions at server
    /// </summary>
    public class ServerPubFun
    {
        private static LogManagerForServer _log = null;
        private static Dictionary<string, int> _mapFieldWidth = null;
        private static int iWrittenCount = 0;
        private static int _iIsolationLevel = -1;

        /// <summary>
        /// Error log
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="errDesc"></param>
        /// <param name="errFileName"></param>
        /// <param name="errLineNo"></param>
        public static void RISLog_Error(long errCode, string errDesc, string errFileName, long errLineNo)
        {
            if (_log == null)
                _log = new LogManagerForServer(ReportCommon.ProfileName._REPORTLOGLEVEL_FORSERVER, "0400");

            if (_log != null)
            {
                _log.Error(
                    System.Convert.ToInt32(ReportCommon.ModuleID.Report),
                    "Report",
                    errCode,
                    errDesc,
                    "",
                    errFileName,
                    errLineNo);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);

                
            }
        }

        /// <summary>
        /// Debug log
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="errDesc"></param>
        /// <param name="errFileName"></param>
        /// <param name="errLineNo"></param>
        public static void RISLog_Info(long errCode, string errDesc, string errFileName, long errLineNo)
        {
            if (_log == null)
                _log = new LogManagerForServer(ReportCommon.ProfileName._REPORTLOGLEVEL_FORSERVER, "0400");

            if (_log != null)
            {
                _log.Info(
                    System.Convert.ToInt32(ReportCommon.ModuleID.Report),
                    "Report",
                    errCode,
                    errDesc,
                    "",
                    errFileName,
                    errLineNo);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);

                
            }
        }

        /// <summary>
        /// Get Report Info by reportGuid
        /// </summary>
        /// <param name="reportGuid"></param>
        /// <returns></returns>
        public static tagReportInfo GetReportInfo(string reportGuid)
        {

            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                  "  select "
                + "  tRegPatient.patientID, tRegPatient.localName, tRegPatient.EnglishName, tRegPatient.address, tRegPatient.telephone,"
                + "  tRegPatient.isVip, tRegPatient.Comments tRegPatient__Comments, tRegPatient.remotePID tRegPatient__remotePID,"
                + "  tRegPatient.Gender as tRegPatient__Gender, tRegPatient.Birthday as tRegPatient__Birthday,"
                + "  tRegPatient.ReferenceNo as tRegPatient__ReferenceNo,tRegPatient.MedicareNo as tRegPatient__MedicareNo,"
                + "  tRegOrder.inhospitalNo, tRegOrder.clinicNo, tRegOrder.PatientType, tRegOrder.Observation, "
                + "  tRegOrder.inHospitalRegion, tRegOrder.BedNo, tRegOrder.comments tRegOrder__comments,"
                + "  tRegOrder.OrderGuid as tRegOrder__OrderGuid, tRegOrder.AccNo, tRegOrder.applyDept as tRegOrder__applyDept,"
                + "  tRegOrder.applyDoctor as tRegOrder__applyDoctor, tRegOrder.VisitComment tRegOrder__VisitComment,"
                + "  tRegOrder.remoteAccNo,tRegOrder.HisID as tRegOrder__HisID,tRegOrder.CardNo as tRegOrder__CardNo,"
                + "  tRegProcedure.ProcedureGuid as tRegProcedure__ProcedureGuid, tRegProcedure.status as tRegProcedure__status, "
                + "  tRegProcedure.charge, tRegProcedure.ModalityType, tRegProcedure.Modality, "
                + "  tRegProcedure.Registrar, tRegProcedure.RegisterDt, tRegProcedure.Technician, "
                + "  tRegProcedure.OperationStep, tRegProcedure.ExamineDt, tRegProcedure.comments tRegProcedure__comments, "
                + "  tRegProcedure.isCharge, tRegProcedure.ProcedureCode tRegProcedure__ProcedureCode, "
                + "  tRegProcedure.RPDesc tProcedureCode__Description, tRegProcedure.bodypart tProcedureCode__bodypart, "
                + "  tReport.reportGuid, "
                + "  tReport.ReportName, tReport.techInfo, tReport.wysText, tReport.wygText, "
                + "  tReport.WYS, tReport.WYG, "
                + "  tReport.creater as tReport__creater, tReport.createDt as tReport__createDt,"
                + "  tReport.submitter as tReport__submitter, tReport.submitDt as tReport__submitDt,"
                + "  tReport.firstApprover as tReport__firstApprover, tReport.firstApproveDt as tReport__firstApproveDt,"
                + "  tReport.rejectDt as tReport__rejectDt"
                + "  from tRegPatient with (nolock),"
                + "  tRegOrder with (nolock), tRegProcedure with (nolock) "
                + "  left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid "
                + "  where tRegPatient.PatientGuid = tRegOrder.PatientGuid "
                + "  and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                + " and tReport.reportGuid = '" + reportGuid.Trim() + "'";

            if (0 == iWrittenCount++ % 100)
            {
                ServerPubFun.RISLog_Info(0, "GetReportInfo, SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            else
            {
                ServerPubFun.RISLog_Info(0, "GetReportInfo, reportGuid=" + reportGuid,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            DataTable dt = new DataTable("ReportInfo");

            using (RisDAL dal = new RisDAL())
            {

                tagReportInfo ri = new tagReportInfo();

                dal.ExecuteQuery(sql, dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    ri.status = ReportCommon.Converter.toInt(dr["tRegProcedure__status"]);
                    ri.dept = dr["tRegOrder__applyDept"] as string;
                    ri.orderGuid = dr["tRegOrder__OrderGuid"] as string;
                    ri.reportGuid = dr["reportGuid"] as string;
                    ri.reportName = dr["reportName"] as string;
                    ri.AccNO = dr["AccNO"] as string;
                    ri.patientID = dr["PatientID"] as string;
                    ri.patientName = dr["EnglishName"] as string;
                    ri.patientLocalName = dr["LocalName"] as string;
                    //ri.patientAlias = dr["Alias"] as string;
                    ri.patientAddress = dr["Address"] as string;
                    ri.patientPhone = dr["TelePhone"] as string;
                    //ri.patientMarriage = dr["Marriage"] as string;
                    ri.gender = dr["tRegPatient__Gender"] as string;
                    ri.ReferenceNo = dr["tRegPatient__ReferenceNo"] as string;
                    ri.MedicareNo = dr["tRegPatient__MedicareNo"] as string;
                    ri.patientType = dr["PatientType"] as string;
                    ri.remotePID = dr["tRegPatient__remotePID"] as string;
                    ri.modality = dr["Modality"] as string;
                    ri.modalityType = dr["modalityType"] as string;
                    ri.operationStep = dr["operationStep"] as string;
                    ri.reportCreater = dr["tReport__creater"] as string;
                    ri.reportSubmitter = dr["tReport__submitter"] as string;
                    ri.reportApprover = dr["tReport__firstApprover"] as string;
                    ri.reportCreateDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__createDt"]);
                    ri.reportSubmitDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__submitDt"]);
                    ri.reportApproveDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__firstApproveDt"]);
                    ri.reportRejectDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__rejectDt"]);
                    ri.birthday = ReportCommon.ReportCommon.GetDateTime(dr["tRegPatient__birthday"]);
                    ri.registerDt = ReportCommon.ReportCommon.GetDateTime(dr["registerDt"]);
                    ri.examineDt = ReportCommon.ReportCommon.GetDateTime(dr["examineDt"]);
                    ri.inHospitalRegion = dr["inHospitalRegion"] as string;
                    ri.visitNo = dr["clinicNo"] as string;
                    ri.bedNo = dr["bedNo"] as string;
                    ri.isVIP = ReportCommon.Converter.toInt(dr["isVip"]).ToString();
                    ri.inHospitalNo = dr["inhospitalNo"] as string;
                    ri.patientComment = dr["tRegPatient__Comments"] as string;
                    ri.remoteAccNo = dr["remoteAccNo"] as string;
                    ri.applyDoctor = dr["tRegOrder__applyDoctor"] as string;
                    ri.hisid = dr["tRegOrder__hisid"] as string;
                    ri.cardno = dr["tRegOrder__cardno"] as string;
                    ri.visitComments = dr["tRegOrder__VisitComment"] as string;
                    ri.observation = dr["Observation"] as string;
                    ri.orderComments = dr["tRegOrder__comments"] as string;
                    ri.technician = dr["technician"] as string;
                    ri.procedureComments = dr["remoteAccNo"] as string;
                    ri.isCharge = ReportCommon.Converter.toInt(dr["isCharge"]).ToString();

                    string tmp = "";
                    double dChange = 0;
                    foreach (DataRow tmpDr in dt.Rows)
                    {
                        tmp = tmpDr["tProcedureCode__bodypart"] as string;
                        if (tmp != null)
                        {
                            ri.bodypart += tmp + ", ";
                        }

                        tmp = tmpDr["tRegProcedure__ProcedureCode"] as string;
                        if (tmp != null)
                        {
                            ri.procedureCode += tmp + ", ";
                        }

                        tmp = tmpDr["tProcedureCode__Description"] as string;
                        if (tmp != null)
                        {
                            ri.procedureDesc += tmp + ", ";
                        }

                        double dTmp = System.Convert.ToDouble(tmpDr["charge"]);
                        dChange += dTmp;
                    }
                    ri.bodypart = ri.bodypart.Trim(", ".ToCharArray());
                    ri.procedureCode = ri.procedureCode.Trim(", ".ToCharArray());
                    ri.procedureDesc = ri.procedureDesc.Trim(", ".ToCharArray());
                    ri.charge = dChange.ToString();

                    ri.reportCreater_LocalName = ServerPubFun.GetLocalName(ri.reportCreater);
                    ri.reportSubmitter_LocalName = ServerPubFun.GetLocalName(ri.reportSubmitter);
                    ri.reportApprover_LocalName = ServerPubFun.GetLocalName(ri.reportApprover);
                    ri.technician__LocalName = ServerPubFun.GetLocalName(ri.technician);

                    ri.duration = System.Convert.ToString(ri.examineDt - ri.registerDt);

                    Byte[] buff = dr["wys"] as Byte[];
                    if (buff == null)
                        ri.wys = "";
                    else
                        ri.wys = ReportCommon.Converter.GetStringFromBytes(buff);

                    buff = dr["wyg"] as Byte[];
                    if (buff == null)
                        ri.wyg = "";
                    else
                        ri.wyg = ReportCommon.Converter.GetStringFromBytes(buff);

                    // buff = dr["wysText"] as Byte[];

                    ri.wysText = dr["wysText"] as string;

                    //    ri.wysText = ReportCommon.Converter.GetStringFromBytes(buff);

                    // buff = dr["wygText"] as Byte[];
                    //if (buff == null)
                    ri.wygText = dr["wygText"] as string;
                    //else
                    //  ri.wygText = ReportCommon.Converter.GetStringFromBytes(buff);

                    ri.techinfo = System.Convert.ToString(dr["techinfo"]);
                }
                return ri;
            }

           
        }

        /// <summary>
        /// convert Single quote to double Single quotes for SQL sentence
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string getSQLString(string src)
        {
            if (!string.IsNullOrEmpty(src))
            {
                return src.Replace("'", "''");
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Report Info by reportGuid
        /// </summary>
        /// <param name="reportGuid"></param>
        /// <returns></returns>
        public static int GetReportOldStatus(string reportGuid)
        {

            string sql = " select top 1 tRegProcedure.status from tRegProcedure "
                + "  where reportGuid = '" + reportGuid.Trim() + "'";

            using (RisDAL dal = new RisDAL())
            {

                int nStatus = -1;
                Object obj = dal.ExecuteScalar(sql);

                if (obj != null)
                {
                    nStatus = Convert.ToInt32(obj);
                }
                return nStatus;
            }

           
        }

        /// <summary>
        /// Get Report Info by rp guid
        /// </summary>
        /// <param name="rpGuid"></param>
        /// <returns></returns>
        public static tagReportInfo GetReportInfo2(string rpGuid)
        {
            int idx = rpGuid.IndexOf(',');
            string rp = rpGuid;
            if (idx > 0 && idx < rpGuid.Length)
            {
                rp = rpGuid.Substring(0, idx);
            }
            rp = rp.Trim("' ".ToCharArray());


            string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                    " select "
                + "  tRegPatient.patientID, tRegPatient.localName, tRegPatient.EnglishName, tRegPatient.address, tRegPatient.telephone,"
                + "  tRegPatient.isVip, tRegPatient.Comments tRegPatient__Comments, tRegPatient.remotePID tRegPatient__remotePID,"
                + "  tRegPatient.Gender as tRegPatient__Gender, tRegPatient.Birthday as tRegPatient__Birthday,"
                + "  tRegOrder.inhospitalNo, tRegOrder.clinicNo, tRegOrder.PatientType, tRegOrder.Observation, "
                + "  tRegOrder.inHospitalRegion, tRegOrder.BedNo, tRegOrder.comments tRegOrder__orderComment,"
                + "  tRegOrder.OrderGuid as tRegOrder__OrderGuid, tRegOrder.AccNo, tRegOrder.applyDept as tRegOrder__applyDept,"
                + "  tRegOrder.applyDoctor as tRegOrder__applyDoctor, tRegOrder.VisitComment tRegOrder__VisitComment,"
                + "  tRegOrder.remoteAccNo,"
                + "  tRegProcedure.ProcedureGuid as tRegProcedure__ProcedureGuid, tRegProcedure.status as tRegProcedure__status, "
                + "  tRegProcedure.charge, tRegProcedure.ModalityType, tRegProcedure.Modality, "
                + "  tRegProcedure.Registrar, tRegProcedure.RegisterDt, tRegProcedure.Technician, "
                + "  tRegProcedure.OperationStep, tRegProcedure.ExamineDt, tRegProcedure.comments tRegProcedure__comments, "
                + "  tRegProcedure.isCharge, tRegProcedure.ProcedureCode tRegProcedure__ProcedureCode, "
                + "  tRegProcedure.RPDesc tProcedureCode__Description, tRegProcedure.bodypart tProcedureCode__bodypart, "
                + "  tReport.reportGuid, "
                + "  tReport.ReportName, tReport.techInfo, tReport.wysText, tReport.wygText, "
                + "  tReport.WYS, tReport.WYG, "
                + "  tReport.creater as tReport__creater, tReport.createDt as tReport__createDt,"
                + "  tReport.submitter as tReport__submitter, tReport.submitDt as tReport__submitDt,"
                + "  tReport.firstApprover as tReport__firstApprover, tReport.firstApproveDt as tReport__firstApproveDt,"
                + "  tReport.rejectDt as tReport__rejectDt"
                + "  from tRegPatient with (nolock), "
                + "  tRegOrder with (nolock), tRegProcedure with (nolock) "
                + "  left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid "
                + "  where tRegPatient.PatientGuid = tRegOrder.PatientGuid "
                + "  and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                + " and tRegProcedure.procedureGuid = '" + rp + "'";

            DataTable dt = new DataTable("ReportInfo");

            

            tagReportInfo ri = new tagReportInfo();

            if (0 == iWrittenCount++ % 100)
            {
                ServerPubFun.RISLog_Info(0, "GetReportInfo2, SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            else
            {
                ServerPubFun.RISLog_Info(0, "GetReportInfo2, rpGuid=" + rpGuid,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            using (RisDAL dal = new RisDAL())
            {
                dal.ExecuteQuery(sql, dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    ri.status = ReportCommon.Converter.toInt(dr["tRegProcedure__status"]);
                    ri.dept = dr["tRegOrder__applyDept"] as string;
                    ri.orderGuid = dr["tRegOrder__OrderGuid"] as string;
                    ri.reportGuid = dr["reportGuid"] as string;
                    ri.reportName = dr["reportName"] as string;
                    ri.AccNO = dr["AccNO"] as string;
                    ri.patientID = dr["PatientID"] as string;
                    ri.patientName = dr["EnglishName"] as string;
                    ri.patientLocalName = dr["LocalName"] as string;
                    // ri.patientAlias = dr["Alias"] as string;
                    ri.patientAddress = dr["Address"] as string;
                    ri.patientPhone = dr["TelePhone"] as string;
                    // ri.patientMarriage = dr["Marriage"] as string;
                    ri.gender = dr["tRegPatient__Gender"] as string;
                    ri.patientType = dr["PatientType"] as string;
                    ri.remotePID = dr["tRegPatient__remotePID"] as string;
                    ri.modality = dr["Modality"] as string;
                    ri.modalityType = dr["modalityType"] as string;
                    ri.operationStep = dr["operationStep"] as string;
                    ri.reportCreater = dr["tReport__creater"] as string;
                    ri.reportSubmitter = dr["tReport__submitter"] as string;
                    ri.reportApprover = dr["tReport__firstApprover"] as string;
                    ri.reportCreateDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__createDt"]);
                    ri.reportSubmitDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__submitDt"]);
                    ri.reportApproveDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__firstApproveDt"]);
                    ri.reportRejectDt = ReportCommon.ReportCommon.GetDateTime(dr["tReport__rejectDt"]);
                    ri.birthday = ReportCommon.ReportCommon.GetDateTime(dr["tRegPatient__birthday"]);
                    ri.registerDt = ReportCommon.ReportCommon.GetDateTime(dr["registerDt"]);
                    ri.examineDt = ReportCommon.ReportCommon.GetDateTime(dr["examineDt"]);
                    ri.inHospitalRegion = dr["inHospitalRegion"] as string;
                    ri.visitNo = dr["clinicNo"] as string;
                    ri.bedNo = dr["bedNo"] as string;
                    ri.isVIP = ReportCommon.Converter.toInt(dr["isVip"]).ToString();
                    ri.inHospitalNo = dr["inhospitalNo"] as string;
                    ri.patientComment = dr["tRegPatient__Comments"] as string;
                    ri.remoteAccNo = dr["remoteAccNo"] as string;

                    ri.applyDoctor = dr["tRegOrder__applyDoctor"] as string;
                    ri.visitComments = dr["tRegOrder__visitComment"] as string;
                    ri.observation = dr["Observation"] as string;
                    ri.orderComments = dr["tRegOrder__orderComment"] as string;
                    ri.technician = dr["technician"] as string;
                    ri.procedureComments = dr["remoteAccNo"] as string;
                    ri.isCharge = ReportCommon.Converter.toInt(dr["isCharge"]).ToString();

                    string tmp = "";
                    double dChange = 0;
                    foreach (DataRow tmpDr in dt.Rows)
                    {
                        tmp = tmpDr["tProcedureCode__bodypart"] as string;
                        if (tmp != null)
                        {
                            ri.bodypart += tmp + ", ";
                        }

                        tmp = tmpDr["tRegProcedure__ProcedureCode"] as string;
                        if (tmp != null)
                        {
                            ri.procedureCode += tmp + ", ";
                        }

                        tmp = tmpDr["tProcedureCode__Description"] as string;
                        if (tmp != null)
                        {
                            ri.procedureDesc += tmp + ", ";
                        }

                        double dTmp = System.Convert.ToDouble(tmpDr["charge"]);
                        dChange += dTmp;
                    }
                    ri.bodypart = ri.bodypart.Trim(", ".ToCharArray());
                    ri.procedureCode = ri.procedureCode.Trim(", ".ToCharArray());
                    ri.procedureDesc = ri.procedureDesc.Trim(", ".ToCharArray());
                    ri.charge = dChange.ToString();

                    ri.reportCreater_LocalName = ServerPubFun.GetLocalName(ri.reportCreater);
                    ri.reportSubmitter_LocalName = ServerPubFun.GetLocalName(ri.reportSubmitter);
                    ri.reportApprover_LocalName = ServerPubFun.GetLocalName(ri.reportApprover);
                    ri.technician__LocalName = ServerPubFun.GetLocalName(ri.technician);

                    ri.duration = System.Convert.ToString(ri.examineDt - ri.registerDt);

                    Byte[] buff = dr["wys"] as Byte[];
                    if (buff == null)
                        ri.wys = "";
                    else
                        ri.wys = ReportCommon.Converter.GetStringFromBytes(buff);

                    buff = dr["wyg"] as Byte[];
                    if (buff == null)
                        ri.wyg = "";
                    else
                        ri.wyg = ReportCommon.Converter.GetStringFromBytes(buff);

                    //buff = dr["wysText"] as Byte[];
                    //if (buff == null)
                    ri.wysText = dr["wysText"] as string;
                    //else
                    //  ri.wysText = ReportCommon.Converter.GetStringFromBytes(buff);

                    // buff = dr["wygText"] as Byte[];
                    //if (buff == null)
                    ri.wygText = dr["wygText"] as string;
                    //else
                    //  ri.wygText = ReportCommon.Converter.GetStringFromBytes(buff);

                    ri.techinfo = System.Convert.ToString(dr["techinfo"]);
                }

            }

            return ri;
        }

        /// <summary>
        /// OnReportDelete
        /// </summary>
        /// <param name="rptInfo"></param>
        /// <param name="nextStatus"></param>
        public static void OnReportDelete(tagReportInfo rptInfo, ReportCommon.RP_Status nextStatus)
        {
            try
            {
                // Need to send gateway
                if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                    return;

                //
                // Gateway
                using (RisDAL dal = new RisDAL())
                {
                    string guid = Guid.NewGuid().ToString();

                    if (dal.DriverClassName.ToUpper() == "ORACLE")
                    #region Oracle
                    {
                        string sql = " insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '" + (nextStatus == ReportCommon.RP_Status.Repeatshot ? "32" : "33") + "', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID
                        + "', '" + (nextStatus == ReportCommon.RP_Status.Repeatshot ? "17" : "16") + "'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '"
                        + ((rptInfo.reportGuid == null || rptInfo.reportGuid == "") ? " " : rptInfo.reportGuid) + "', '"
                        + rptInfo.AccNO + "'," + " '" + rptInfo.patientID + "', '204', '"
                        + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportApproveDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                        sql = "begin " + sql + " commit; end;";

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                    }
                    #endregion
                    else if (dal.DriverClassName.ToUpper() == "MSSQL")
                    #region MSSQL
                    {
                        string sql = SaveReportDAO_MSSQL.MakeSQL4GateWay(rptInfo, guid, nextStatus == ReportCommon.RP_Status.Repeatshot ? 32 : 33, 16, 204);

                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                        {
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.Connection = conn;

                            try
                            {
                                //
                                // begin transaction
                                //cmd.Transaction = conn.BeginTransaction();

                                ServerPubFun.RISLog_Info(0, "OnReportDelete=" + sql,
                                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();


                            }
                            catch (Exception ex)
                            {
                                //cmd.Transaction.Rollback();

                                System.Diagnostics.Debug.Assert(false, ex.Message);

                                RISLog_Error(0, "OnReportDelete, MSG=" + ex.Message + ", SQL=" + sql,
                                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                   (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            }
                        }
                    }
                    #endregion
                    else
                    {
                        System.Diagnostics.Debug.Assert(false, "Invalid DB setting!");
                    }

                    // Hippa
                    Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                        CommonGlobalSettings.HippaName.ActionCode.Delete,
                        
                        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                }
            }
            catch
            {
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                   CommonGlobalSettings.HippaName.ActionCode.Delete,
                   rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
            }
            
        }

        /// <summary>
        /// GetSystemProfile_String
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSystemProfile_String(string name)
        {
            if (name == null || (name = name.Trim()).Length < 1)
                return "";

            
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "Select value From tSystemProfile Where Name = '" + name + "'";


                using (RisDAL oKodak = new RisDAL())
                {
                    dt = oKodak.ExecuteQuery(strSQL);
                }
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                {
                    return dt.Rows[0][0] as string;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false);

                RISLog_Error(0, "GetSystemProfile_String, MSG=" + ex.Message,
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
               
            }

            return "";
        }

        public static string GetSystemProfile_String(string name, ReportCommon.ModuleID moduleId)
        {
            return ServerCommon.DaoInstanceFactory.GetInstance().GetSystemProfileValue
                (name, ReportCommon.ReportCommon.GetMoudleIDString(moduleId));
        }

        /// <summary>
        /// GetSystemProfile_Int
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetSystemProfile_Int(string name)
        {
            try
            {
                string ret = GetSystemProfile_String(name);

                if (ret != null && (ret = ret.Trim()).Length > 0)
                {
                    return System.Convert.ToInt32(ret);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            return 0;
        }

        public static int GetSystemProfile_Int(string name, ReportCommon.ModuleID moduleId)
        {
            try
            {
                string ret = GetSystemProfile_String(name, moduleId);

                if (ret != null && (ret = ret.Trim()).Length > 0)
                {
                    return System.Convert.ToInt32(ret);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            return 0;
        }
        /// <summary>
        /// GetSystemProfile_Bool
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool GetSystemProfile_Bool(string name)
        {
            try
            {
                string ret = GetSystemProfile_String(name);

                if (ret != null && (ret = ret.Trim().ToUpper()).Length > 0)
                {
                    //return System.Convert.ToBoolean(ret);
                    return ret == "1" || ret == "Y" || ret == "TRUE";
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            return false;
        }

        public static bool GetSystemProfile_Bool(string name, ReportCommon.ModuleID moduleId)
        {
            try
            {
                string ret = GetSystemProfile_String(name, moduleId);

                if (ret != null && (ret = ret.Trim().ToUpper()).Length > 0)
                {
                    //return System.Convert.ToBoolean(ret);
                    return ret == "1" || ret == "Y" || ret == "TRUE";
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            return false;
        }
        /// <summary>
        /// Get Width of table column
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int GetColumnWidth(string tableName, string fieldName)
        {
            if (tableName == null || (tableName = tableName.Trim()).Length < 1 ||
                fieldName == null || (fieldName = fieldName.Trim()).Length < 1)
                return 0;

            if (_mapFieldWidth == null)
            {
                _mapFieldWidth = new Dictionary<string, int>();
            }

            if (_mapFieldWidth == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return 0;
            }

            string key = tableName.ToUpper().Trim() + "," + fieldName.ToUpper().Trim();

            if (_mapFieldWidth.ContainsKey(key))
            {
                return _mapFieldWidth[key];
            }

            
            DataTable dt = new DataTable();

            try
            {
                using (RisDAL oKodak = new RisDAL())
                {
                    string sql = "select syscolumns.length as collength from sysobjects, syscolumns"
                        + " where syscolumns.id=sysobjects.id and sysobjects.xtype='U'"
                        + " and sysobjects.name='" + tableName + "' and syscolumns.name='" + fieldName + "'";

                    if (oKodak.DriverClassName.ToUpper() == "ORACLE")
                    {
                        sql = "select data_length as collength from user_tab_cols"
                            + " where table_name = '" + tableName.ToUpper() + "' and column_name = '" + fieldName.ToUpper() + "'";
                    }

                    dt = oKodak.ExecuteQuery(sql);

                    if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                    {
                        _mapFieldWidth.Add(key, System.Convert.ToInt32(dt.Rows[0][0]));

                        return _mapFieldWidth[key];
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false);

                RISLog_Error(0, "GetColumnWidth, MSG=" + ex.Message,
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                
            }

            return 0;
        }

        /// <summary>
        /// GetLocalName
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public static string GetLocalName(string userGuid)
        {
            try
            {
                if (userGuid != null && (userGuid = userGuid.Trim()).Length > 0)
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        DataTable dt = new DataTable();
                        string sql = "";

                        //multi userguid
                        if (userGuid.Length > Guid.NewGuid().ToString().Length)
                        {
                            string tmp = "'" + userGuid.Replace(",", "','") + "'";
                            sql = "select UserGuid,LocalName from tUser where UserGuid in(" + tmp + ")";
                            dal.ExecuteQuery(sql, dt);
                            if (null != dt && dt.Rows.Count > 0)
                            {
                                tmp = "";
                                foreach (string str in userGuid.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                                {
                                    DataRow[] drs = dt.Select("UserGuid='" + str + "'");
                                    if (drs.Length > 0)
                                    {
                                        tmp += drs[0]["LocalName"].ToString() + ",";
                                    }
                                    else
                                    {
                                        tmp += str + ",";
                                    }
                                }
                                tmp = tmp.TrimEnd(",".ToCharArray());
                                return tmp;
                            }
                            return "";
                        }
                        else
                        {
                            sql = "select rolename, tUser.userGuid, LoginName, localName, englishname "
                                + " from tUser, tRole2User"
                                + " where tUser.UserGuid = tRole2User.UserGuid"
                                + " and tUser.UserGuid = '" + userGuid + "'";

                            dal.ExecuteQuery(sql, dt);

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                string retName = dt.Rows[0]["LocalName"].ToString();


                                return retName;
                            }
                        }
                        if (dal != null)
                        {
                            dal.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetLocalName=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "";
        }

        /// <summary>
        /// DeclareNoCase
        /// </summary>
        public static void DeclareOracleNoCase()
        {
            try
            {
                string sql0 = " alter session set nls_comp=ANSI";
                string sql1 = " alter session set nls_sort=GENERIC_BASELETTER";

                using (RisDAL dal = new RisDAL())
                {

                    if (dal.DriverClassName.ToUpper() == "ORACLE")
                    {
                        dal.ExecuteNonQuery(sql0);
                        dal.ExecuteNonQuery(sql1);
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// GetIsolationLevel
        /// 0 -- ReadUncommitted
        /// 1 -- ReadCommitted
        /// 2 -- Snapshot
        /// 3 -- RepeatableRead
        /// 4 -- Serializable
        /// 5 -- Chaos
        /// 6 -- Unspecified
        /// </summary>
        /// <returns></returns>
        public static IsolationLevel GetIsolationLevel()
        {
            try
            {
                if (_iIsolationLevel < 0)
                {
                    _iIsolationLevel = ServerPubFun.GetSystemProfile_Int(ReportCommon.ProfileName.IsolationLevel);
                }

                if (_iIsolationLevel == 0)
                {
                    return IsolationLevel.ReadUncommitted;
                }
                else if (_iIsolationLevel == 1)
                {
                    return IsolationLevel.ReadCommitted;
                }
                else if (_iIsolationLevel == 2)
                {
                    return IsolationLevel.Snapshot;
                }
                else if (_iIsolationLevel == 3)
                {
                    return IsolationLevel.RepeatableRead;
                }
                else if (_iIsolationLevel == 4)
                {
                    return IsolationLevel.Serializable;
                }
                else if (_iIsolationLevel == 5)
                {
                    return IsolationLevel.Chaos;
                }
                else if (_iIsolationLevel == 6)
                {
                    return IsolationLevel.Unspecified;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetIsolationLevel=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return IsolationLevel.ReadUncommitted;
        }
    }
}
