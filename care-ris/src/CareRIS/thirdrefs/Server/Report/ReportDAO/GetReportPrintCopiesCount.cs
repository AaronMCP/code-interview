using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportPrintCopiesCountDAO
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

    internal class GetReportPrintCopiesCountDAO_ABSTRACT : IReportDAO
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

    internal class GetReportPrintCopiesCountDAO_SYBASE : IReportDAO
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

    internal class GetReportPrintCopiesCountDAO_MSSQL : IReportDAO
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

                    if (key.ToUpper() == "REPORTGUID" || key.ToUpper() == "REPORTID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                }
                using (RisDAL dal = new RisDAL())
                {
                    string sql = "select PrintCopies from tReport where ReportGuid ='" + reportGuid + "'";
                    DataTable dt = dal.ExecuteQuery(sql);

                    if (dt != null && dt.Rows.Count == 1)
                    {
                        int count = Convert.ToInt32(dt.Rows[0][0]);
                        return count = (count >= 0 ? count : 0);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportPrintCopiesCountDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return 0;
        }
    }

    internal class GetReportPrintCopiesCountDAO_ORACLE : IReportDAO
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

                string patientID = "", reportGuid = "", strType = "";

                foreach (string key in paramMap.Keys)
                {

                    if (key.ToUpper() == "REPORTGUID" || key.ToUpper() == "REPORTID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                }
                using (RisDAL dal = new RisDAL())
                {
                    string sql = "select PrintCopies from tReport where ReportGuid ='" + reportGuid + "'";
                    DataTable dt = dal.ExecuteQuery(sql);

                    if (dt != null && dt.Rows.Count == 1)
                    {
                        int count = Convert.ToInt32(dt.Rows[0][0]);
                        return count = (count >= 0 ? count : 0);
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportPrintCopiesCountDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return 0;
        }
    }
}
