using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class DisqualifyImageDAO
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

    internal class DisqualifyImageDAO_ABSTRACT : IReportDAO
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

    internal class DisqualifyImageDAO_SYBASE : IReportDAO
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

    internal class DisqualifyImageDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in DisqualifyImageDAO!"));
                }

                string rpGuids = "", reportGuid = "", curUserGuid = "", reason = "";
                string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
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
                    else if (key.ToUpper() == "PROCEDUREGUID")
                    {
                        rpGuids = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "REASON")
                    {
                        reason = inMap[key] as string;
                    }
                }

                if ((reportGuid == null || reportGuid.Length < 1) &&
                    (rpGuids == null || rpGuids.Length < 1))
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DisqualifyImageDAO!"));
                }

                if (curUserGuid == null || curUserGuid.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DisqualifyImageDAO!"));
                }

                if (reportGuid == null)
                    reportGuid = "";

                reportGuid = reportGuid.Trim(" ,".ToCharArray());

                if (rpGuids == null)
                    rpGuids = "";

                rpGuids = rpGuids.Trim(" ,".ToCharArray());

                reason = reason == null ? "" : reason.Length >= 512 ? reason.Substring(0, 512) : reason;

                string sql = " begin tran \r\n";

                using (RisDAL dal = new RisDAL())
                {

                    if (reportGuid != null && reportGuid.Length > 0)
                    {
                        string sqlGetRPs = "select procedureGuid from tRegProcedure where ReportGuid='" + reportGuid + "'";

                        DataTable dtTmp = dal.ExecuteQuery(sqlGetRPs);
                        if (dtTmp != null)
                        {
                            foreach (DataRow dr in dtTmp.Rows)
                            {
                                string tmpRP = System.Convert.ToString(dr[0]);
                                if (string.IsNullOrEmpty(tmpRP))
                                    continue;

                                sql += " if exists(select 1 from tReShot where procedureGuid ='" + tmpRP + "') \r\n"
                                    + " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "',"
                                    + " rejectDt = getdate(),domain='" + strDomain + "' "
                                    + " where procedureGuid = '" + tmpRP + "' \r\n"
                                    + " else "
                                    + " insert tReShot(ProcedureGuid, reason, rejectDoctor, rejectDt, domain) "
                                    + " values ('" + tmpRP + "', '" + reason + "', '" + curUserGuid + "', getdate(), '" + strDomain + "') \r\n"
                                    ;
                            }
                        }

                        //sql += " delete from tReShot where procedureGuid in (select procedureGuid from tRegProcedure where ReportGuid='" + reportGuid + "') \r\n"
                        //    + " insert tReShot(ProcedureGuid) select procedureGuid from tRegProcedure "
                        //    + " where reportGuid = '" + reportGuid + "' \r\n"
                        //    + " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "', rejectDt = getdate(),domain='" + strDomain + "' "
                        //    + " where (rejectDoctor is null or rejectDoctor = '')"
                        //    + " and procedureGuid in (select procedureGuid from tRegProcedure"
                        //    + "  where reportGuid = '" + reportGuid + "') \r\n";

                        sql += ""
                            + " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Repeatshot).ToString() + ",reportguid=null where  "
                            + "  reportGuid = '" + reportGuid + "' \r\n"
                            + " delete from tReportlist where reportGuid = '" + reportGuid + "' \r\n"
                            + " delete from tReport where reportGuid = '" + reportGuid + "' \r\n";
                    }
                    else if (rpGuids != null && rpGuids.Length > 0)
                    {
                        string[] arrRPs = rpGuids.Split(",;".ToCharArray());

                        foreach (string tmpRP in arrRPs)
                        {
                            if (string.IsNullOrEmpty(tmpRP))
                                continue;

                            sql += " if exists(select 1 from tReShot where procedureGuid ='" + tmpRP + "') \r\n"
                                + " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "',"
                                + " rejectDt = getdate(),domain='" + strDomain + "' "
                                + " where procedureGuid = '" + tmpRP + "' \r\n"
                                + " else "
                                + " insert tReShot(ProcedureGuid, reason, rejectDoctor, rejectDt, domain) "
                                + " values ('" + tmpRP + "', '" + reason + "', '" + curUserGuid + "', getdate(), '" + strDomain + "') \r\n"
                                ;
                        }

                        if (!rpGuids.Contains("'"))
                        {
                            rpGuids = rpGuids.Replace(",", "','");
                            rpGuids = rpGuids.Replace(";", "','");
                            rpGuids = "'" + rpGuids + "'";
                        }

                        //sql += " delete from tReShot where procedureGuid in (" + rpGuids + ") \r\n"
                        //+ " insert tReShot(ProcedureGuid) select procedureGuid from tRegProcedure"
                        //+ " where procedureGuid in (" + rpGuids + ") \r\n"
                        //+ " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "', rejectDt = getdate(),domain='" + strDomain + "' "
                        //+ " where (rejectDoctor is null or rejectDoctor = '') and procedureGuid in (" + rpGuids + ") \r\n";

                        sql += " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Repeatshot).ToString()
                            + " where procedureGuid in (" + rpGuids + ") \r\n";
                    }

                    sql += " commit";

                    ServerPubFun.RISLog_Info(0, "DisqualifyImageDAO_MSSQL, SQL=" + sql,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                    // Delete
                    tagReportInfo rptInfo = ServerPubFun.GetReportInfo2(rpGuids); //ServerPubFun.GetReportInfo(reportGuid);

                    ServerPubFun.OnReportDelete(rptInfo, ReportCommon.RP_Status.Repeatshot);



                    dal.ExecuteNonQuery(sql);

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "DisqualifyImageDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }

    internal class DisqualifyImageDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in DisqualifyImageDAO!"));
                }

                string rpGuids = "", reportGuid = "", curUserGuid = "", reason = "";
                string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
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
                    else if (key.ToUpper() == "PROCEDUREGUID")
                    {
                        rpGuids = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "REASON")
                    {
                        reason = inMap[key] as string;
                    }
                }

                if ((reportGuid == null || reportGuid.Length < 1) &&
                    (rpGuids == null || rpGuids.Length < 1))
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DisqualifyImageDAO!"));
                }

                if (curUserGuid == null || curUserGuid.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DisqualifyImageDAO!"));
                }

                if (reportGuid == null)
                    reportGuid = "";

                reportGuid = reportGuid.Trim(" ,".ToCharArray());

                if (rpGuids == null)
                    rpGuids = "";

                rpGuids = rpGuids.Trim(" ,".ToCharArray());

                reason = reason == null ? "" : reason.Length >= 512 ? reason.Substring(0, 512) : reason;

                string sql = " begin ";

                if (reportGuid != null && reportGuid.Length > 0)
                {


                    sql += " delete from tReShot where procedureGuid in (select procedureGuid from tRegProcedure "
                         + " where reportGuid = '" + reportGuid + "'; "
                         + " insert into tReShot(ProcedureGuid) select procedureGuid from tRegProcedure"
                         + " where reportGuid = '" + reportGuid + "'; "
                         + " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "', rejectDt = sysdate,domain='" + strDomain + "' "
                         + " where (rejectDoctor is null or rejectDoctor = '')"
                         + " and procedureGuid in (select procedureGuid from tRegProcedure "
                         + "  where reportGuid = '" + reportGuid + "'); ";

                    sql += " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Repeatshot).ToString() + " where "
                        + "  reportGuid = '" + reportGuid + "'; "
                        + " delete from tReportlist where reportGuid = '" + reportGuid + "';"
                        + "  delete from tReport where reportGuid = '" + reportGuid + "'; ";



                }
                else if (rpGuids != null && rpGuids.Length > 0)
                {
                    if (!rpGuids.Contains("'"))
                    {
                        rpGuids = rpGuids.Replace(",", "','");
                        rpGuids = rpGuids.Replace(";", "','");
                        rpGuids = "'" + rpGuids + "'";
                    }


                    sql += " delete from tReShot where procedureGuid in (" + rpGuids + "); "
                          + " insert into tReShot(ProcedureGuid) select procedureGuid from tRegProcedure"
                          + " where procedureGuid in (" + rpGuids + "); "
                          + " update tReShot set reason='" + reason + "', rejectDoctor='" + curUserGuid + "', rejectDt = sysdate,domain='" + strDomain + "' "
                          + " where (rejectDoctor is null or rejectDoctor = '') and procedureGuid in (" + rpGuids + "); ";


                    sql += " update tRegProcedure set status = " + System.Convert.ToInt32(ReportCommon.RP_Status.Repeatshot).ToString() + " where procedureGuid in (" + rpGuids + "); ";



                }

                sql += " commit; end;";

                

                // Delete
                tagReportInfo rptInfo = ServerPubFun.GetReportInfo2(rpGuids); //ServerPubFun.GetReportInfo(reportGuid);

                ServerPubFun.OnReportDelete(rptInfo, ReportCommon.RP_Status.Repeatshot);

                using (RisDAL dal = new RisDAL())
                {

                    dal.ExecuteNonQuery(sql);

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "DisqualifyImageDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }
}
