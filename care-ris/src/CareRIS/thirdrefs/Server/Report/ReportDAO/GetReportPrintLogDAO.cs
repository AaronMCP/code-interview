using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportPrintLogDAO
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

    internal class GetReportPrintLogDAO_ABSTRACT : IReportDAO
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

    internal class GetReportPrintLogDAO_SYBASE : IReportDAO
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

    internal class GetReportPrintLogDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportHistoryDAO!"));
                }

                string patientID = "", reportGuid = "",strType="";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "PATIENTGUID" || key.ToUpper() == "PATIENTID")
                    {
                        patientID = paramMap[key] as string;

                        if (patientID == null)
                            patientID = "";
                    }
                    else if (key.ToUpper() == "REPORTGUID" || key.ToUpper() == "REPORTID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        strType = paramMap[key] as string;

                        if (strType == null)
                            strType = "";
                    }
                }

                string sql = "select distinct tRegPatient.PatientID, tRegPatient.LocalName, tRegOrder.AccNO,"
                    + " tRegOrder.createDt, tReport.reportName, tReportPrintLog.*,tPrintTemplate.TemplateName"
                    + " from tRegPatient, tRegOrder, tRegProcedure, "
                    + " tReport, tReportPrintLog"
                    + " left join tPrintTemplate"
                    + " on tReportPrintLog.PrintTemplateGuid = tPrintTemplate.TemplateGuid"
                    + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"                    
                    + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                    + " and tRegProcedure.reportGuid = tReport.reportGuid"                    
                    + " and tReport.reportGuid = tReportPrintLog.reportGuid";

                if (patientID != null && patientID.Length > 0)
                {
                    sql += " and tRegPatient.PatientID='" + patientID + "'";
                }

                if (reportGuid != null && reportGuid.Length > 0)
                {
                    sql += " and tReport.reportGuid='" + reportGuid + "'";
                }

                if (strType != null && strType.Length > 0)
                {
                    sql += " and tReportPrintLog.Type='" + strType + "'";
                }


                sql += " order by tRegOrder.createDt desc, tReportPrintLog.PrintDt desc";

                

                DataSet ds = new DataSet();
                using (RisDAL dal = new RisDAL())
                {

                    dal.ExecuteQuery(sql, ds, "ReportPrintLog");

                    Dictionary<string, object> outMap = new Dictionary<string, object>();

                    outMap.Add("DataSet", ds);
                    return outMap;
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportPrintLogDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    internal class GetReportPrintLogDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportHistoryDAO!"));
                }

                string patientID = "", reportGuid = "",strType="";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "PATIENTGUID" || key.ToUpper() == "PATIENTID")
                    {
                        patientID = paramMap[key] as string;

                        if (patientID == null)
                            patientID = "";
                    }
                    else if (key.ToUpper() == "REPORTGUID" || key.ToUpper() == "REPORTID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        strType = paramMap[key] as string;

                        if (strType == null)
                            strType = "";
                    }
                }

                string sql = "select distinct tRegPatient.PatientID, tRegPatient.LocalName, tRegOrder.AccNO,"
                    + " tRegOrder.createDt, tReport.reportName, tReportPrintLog.*"
                    + " from tRegPatient, tRegOrder, tRegProcedure,"
                    + " tReport, tReportPrintLog"
                    + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"                    
                    + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                    + " and tRegProcedure.reportGuid = tReport.reportGuid"                   
                    + " and tReport.reportGuid = tReportPrintLog.reportGuid";

                if (patientID != null && patientID.Length > 0)
                {
                    sql += " and tRegPatient.PatientID='" + patientID + "'";
                }

                if (reportGuid != null && reportGuid.Length > 0)
                {
                    sql += " and tReport.reportGuid='" + reportGuid + "'";
                }
                if (strType != null && strType.Length > 0)
                {
                    sql += " and tReportPrintLog.Type='" + strType + "'";
                }


                sql += " order by tRegOrder.createDt desc";

                

                DataSet ds = new DataSet();
                using (RisDAL dal = new RisDAL())
                {

                    dal.ExecuteQuery(sql, ds, "ReportPrintLog");

                    Dictionary<string, object> outMap = new Dictionary<string, object>();

                    outMap.Add("DataSet", ds);

                    return outMap;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportPrintLogDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
