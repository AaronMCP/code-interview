using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class DeleteReportDAO
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

    internal class DeleteReportDAO_ABSTRACT : IReportDAO
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

    internal class DeleteReportDAO_SYBASE : IReportDAO
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

    internal class DeleteReportDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in DeleteReportDAO!"));
                }

                string reportGuid = "", curUserGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERID")
                    {
                        curUserGuid = inMap[key] as string;
                    }
                }

                if (reportGuid == null || reportGuid.Length < 1 ||
                    curUserGuid == null || curUserGuid.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DeleteReportDAO!"));
                }
                  string sql = " begin tran \r\n"
               + " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination).ToString()
               + " ,ReportGuid=null  where reportGuid = '" + reportGuid + "'  \r\n"
               + " insert into treportdelpool select * from treport where reportguid='"+reportGuid+"' \r\n"               
               + " update treportdelpool set deleter='"+curUserGuid+"',deletedt='"+DateTime.Now.ToString()+"' where reportguid='"+reportGuid+"' \r\n"
               + " delete from tReportlist where reportGuid = '" + reportGuid + "' \r\n"               
               + " delete from tReport where reportGuid = '" + reportGuid + "' \r\n"
                  #region Kevin For SR
               + " delete from tReportContent where ReportId = '" + reportGuid + "'\r\n"
                  #endregion
               + " commit";


                ServerPubFun.RISLog_Info(0, "DeleteReportDAO_MSSQL, SQL=" + sql, 
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                // Delete
                tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);



                using (RisDAL dal = new RisDAL())
                {

                    DataTable dt = new DataTable();

                    dal.ExecuteQuery(sql, dt);
                }
                ServerPubFun.OnReportDelete(rptInfo, ReportCommon.RP_Status.Examination);

                //return (dt != null && dt.Rows.Count > 0);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "DeleteReportDAO_MSSQL=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }

    internal class DeleteReportDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in DeleteReportDAO!"));
                }

                string reportGuid = "", curUserGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERID")
                    {
                        curUserGuid = inMap[key] as string;
                    }
                }

                if (reportGuid == null || reportGuid.Length < 1 ||
                    curUserGuid == null || curUserGuid.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DeleteReportDAO!"));
                }
                
                string sql = " begin "
                 + " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination).ToString()
                 + " where reportGuid = '" + reportGuid + "'; "
                 + " delete from tReportlist where reportGuid = '" + reportGuid + "';"                 
                 + " delete from tReport  where reportGuid = '" + reportGuid + "'; "                 
                 + " commit; end;";



                

                // Delete
                tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);

                ServerPubFun.OnReportDelete(rptInfo, ReportCommon.RP_Status.Examination);

                using (RisDAL dal = new RisDAL())
                {

                    dal.ExecuteNonQuery(sql);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "DeleteReportDAO_ORACLE=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }
}
