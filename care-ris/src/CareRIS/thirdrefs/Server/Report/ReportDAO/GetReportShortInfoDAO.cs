using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportShortInfoDAO
    {
        public object Execute(object param)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }
    }

    internal class GetReportShortInfoDAO_ABSTRACT : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetReportShortInfoDAO_SYBASE : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetReportShortInfoDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportShortInfoDAO!"));
                }

                using (RisDAL dal = new RisDAL())
                {

                    string reportGuid = "";
                    string rpGuid = "";
                    string accNO = "";

                    foreach (string key in paramMap.Keys)
                    {
                        if (key.ToUpper() == "REPORTGUID")
                        {
                            reportGuid = paramMap[key] as string;

                            if (reportGuid == null)
                                reportGuid = "";
                        }
                        else if (key.ToUpper() == "PROCEDUREGUID")
                        {
                            rpGuid = paramMap[key] as string;

                            if (rpGuid == null)
                                rpGuid = "";
                        }
                        else if (key.ToUpper() == "ACCNO")
                        {
                            accNO = paramMap[key] as string;

                            if (accNO == null)
                                accNO = "";
                        }
                    }

                    if ((reportGuid == null || reportGuid.Length < 1) &&
                        (rpGuid == null || rpGuid.Length < 1) &&
                        (accNO == null || accNO.Length < 1))
                    {
                        throw (new Exception("NULL parameter in GetReportShortInfoDAO!"));
                    }

                    string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                        "select " + GetReturnCol() + " from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock) \r\n"
                            + " left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                            + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                            + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                            ;

                    if (reportGuid.Length > 0)
                    {
                        sql += " and tReport.reportGuid = '" + reportGuid + "'";
                    }

                    if (rpGuid.Length > 0)
                    {
                        sql += " and tRegProcedure.ProcedureGuid = '" + rpGuid + "'";
                    }

                    if (accNO.Length > 0)
                    {
                        sql += " and tRegOrder.AccNO = '" + accNO + "'";
                    }

                    DataSet ds = new DataSet();

                    dal.ExecuteQuery(sql, ds, "ReportShortInfo");

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportShortInfoDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        private string GetReturnCol()
        {
            return "tRegPatient.PatientID as " + ReportCommon.ReportCommon.FIELDNAME_PATIENTID + ", \r\n"
                + " tRegPatient.LocalName as " + ReportCommon.ReportCommon.FIELDNAME_LOCALNAME + ", \r\n"
                + " tRegOrder.AccNo as " + ReportCommon.ReportCommon.FIELDNAME_ACCNO + ", \r\n"
                + " tRegOrder.OrderGuid as " + ReportCommon.ReportCommon.FIELDNAME_ORDERGUID + ", \r\n"
                + " tRegOrder.PatientType as " + ReportCommon.ReportCommon.FIELDNAME_tRegOrder__PatientType + ", \r\n"
                + " tRegOrder.ApplyDept as " + ReportCommon.ReportCommon.FIELDNAME_tRegOrder__ApplyDept + ", \r\n"
                + " tRegProcedure.ProcedureGuid as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDUREGUID + ", \r\n"
                + " tRegProcedure.ProcedureCode as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDURECODE + ", \r\n"
                + " tRegProcedure.status as " + ReportCommon.ReportCommon.FIELDNAME_RPSTATUS + ", \r\n"
                + " tRegProcedure.Modality as " + ReportCommon.ReportCommon.FIELDNAME_MODALITY + ", \r\n"
                + " tRegProcedure.IsExistImage as " + ReportCommon.ReportCommon.FIELDNAME_ISEXISTIMAGE + ", \r\n"
                + " tReport.ReportGuid as " + ReportCommon.ReportCommon.FIELDNAME_REPORTGUID + ", \r\n"
                + " tReport.reportName as " + ReportCommon.ReportCommon.FIELDNAME_REPORTNAME + ", \r\n"
                + " tReport.Creater as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_CREATER + ", \r\n"
                + " tReport.CreateDt as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_CREATEDT + ", \r\n"
                + " tReport.Submitter as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTER + ", \r\n"
                + " tReport.SubmitDt as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITDT + ", \r\n"
                + " tReport.FirstApprover as " + ReportCommon.ReportCommon.FIELDNAME_FIRSTAPPROVER + ", \r\n"
                + " tReport.FirstApproveDt as " + ReportCommon.ReportCommon.FIELDNAME_FIRSTAPPROVEDATE + ", \r\n"
                + " tReport.RejectToObject as " + ReportCommon.ReportCommon.FIELDNAME_REJECTTOOBJECT + ", \r\n"
                + " tReport.Rejecter as " + ReportCommon.ReportCommon.FIELDNAME_REPORTREJECTER + ", \r\n"
                + " tReport.RejectDt as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_REJECTDT + ", \r\n"
                + " tReport.IsPrint as " + ReportCommon.ReportCommon.FIELDNAME_REPORT_ISPRINT + ", \r\n"
                + " tRegProcedure.RPDesc as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDURECODEDESC + ", \r\n"
                + " tRegProcedure.ModalityType as " + ReportCommon.ReportCommon.FIELDNAME_MODALITYTYPE + " \r\n";
        }
    }

    internal class GetReportShortInfoDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportShortInfoDAO!"));
                }

                using (RisDAL dal = new RisDAL())
                {

                    string reportGuid = "";
                    string rpGuid = "";
                    string accNO = "";

                    foreach (string key in paramMap.Keys)
                    {
                        if (key.ToUpper() == "REPORTGUID")
                        {
                            reportGuid = paramMap[key] as string;

                            if (reportGuid == null)
                                reportGuid = "";
                        }
                        else if (key.ToUpper() == "PROCEDUREGUID")
                        {
                            rpGuid = paramMap[key] as string;

                            if (rpGuid == null)
                                rpGuid = "";
                        }
                        else if (key.ToUpper() == "ACCNO")
                        {
                            accNO = paramMap[key] as string;

                            if (accNO == null)
                                accNO = "";
                        }
                    }

                    if ((reportGuid == null || reportGuid.Length < 1) &&
                        (rpGuid == null || rpGuid.Length < 1) &&
                        (accNO == null || accNO.Length < 1))
                    {
                        throw (new Exception("NULL parameter in GetReportShortInfoDAO!"));
                    }

                    string sql = "select " + GetReturnCol() + " from tRegPatient, tRegOrder, tProcedureCode, tRegProcedure "
                            + " left join tReport on tRegProcedure.reportGuid = tReport.reportGuid "
                            + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid "
                            + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                            + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode ";

                    if (reportGuid.Length > 0)
                    {
                        sql += " and tReport.reportGuid = '" + reportGuid + "'";
                    }

                    if (rpGuid.Length > 0)
                    {
                        sql += " and tRegProcedure.ProcedureGuid = '" + rpGuid + "'";
                    }

                    if (accNO.Length > 0)
                    {
                        sql += " and tRegOrder.AccNO = '" + accNO + "'";
                    }

                    DataSet ds = new DataSet();

                    dal.ExecuteQuery(sql, ds, "ReportShortInfo");

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportShortInfoDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        private string GetReturnCol()
        {
            return "tRegPatient.PatientID as " + ReportCommon.ReportCommon.FIELDNAME_PATIENTID + ", \r\n"
                + " tRegPatient.LocalName as " + ReportCommon.ReportCommon.FIELDNAME_LOCALNAME + ", \r\n"
                + " tRegOrder.AccNo as " + ReportCommon.ReportCommon.FIELDNAME_ACCNO + ", \r\n"
                + " tRegProcedure.ProcedureGuid as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDUREGUID + ", \r\n"
                + " tRegProcedure.ProcedureCode as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDURECODE + ", \r\n"
                + " tRegProcedure.status as " + ReportCommon.ReportCommon.FIELDNAME_RPSTATUS + ", \r\n"
                + " tRegProcedure.Modality as " + ReportCommon.ReportCommon.FIELDNAME_MODALITY + ", \r\n"
                + " tReport.ReportGuid as " + ReportCommon.ReportCommon.FIELDNAME_REPORTGUID + ", \r\n"
                + " tReport.reportName as " + ReportCommon.ReportCommon.FIELDNAME_REPORTNAME + ", \r\n"
                + " tProcedureCode.Description as " + ReportCommon.ReportCommon.FIELDNAME_PROCEDURECODEDESC + ", \r\n"
                + " tProcedureCode.ModalityType as " + ReportCommon.ReportCommon.FIELDNAME_MODALITYTYPE + " \r\n";
        }
    }
}
