using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class AssingReportDAO
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

    internal class AssingReportDAO_ABSTRACT : IReportDAO
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

    internal class AssingReportDAO_SYBASE : IReportDAO
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

    internal class AssingReportDAO_MSSQL : IReportDAO
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

                string orderGuid = "", assignSite = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "ORDERGUID")
                    {
                        orderGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "ASSIGNSITE")
                    {
                        assignSite = inMap[key] as string;
                    }
                }

                if (orderGuid == null || orderGuid.Length < 1 ||
                    assignSite == null || assignSite.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DeleteReportDAO!"));
                }
                string sql = string.Format("update tRegOrder set Assign2Site ='{0}',CurrentSite='{0}',AssignDt=getdate() where orderGuid='{1}'",
                     assignSite, orderGuid);
                string sqlSamestatus = string.Format("select distinct Status from tRegProcedure where orderGuid ='{0}'", orderGuid);

                ServerPubFun.RISLog_Info(0, "AssingReportDAO_MSSQL, SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                using (RisDAL dal = new RisDAL())
                {
                    if (dal.ExecuteQuery(sqlSamestatus).Rows.Count > 1)
                        return "2";

                    dal.ExecuteNonQuery(sql);
                }

                return "1";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "AssingReportDAO_MSSQL=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }

    internal class AssingReportDAO_ORACLE : IReportDAO
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

                string orderGuid = "", assignSite = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "ORDERGUID")
                    {
                        orderGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "ASSIGNSITE")
                    {
                        assignSite = inMap[key] as string;
                    }
                }

                if (orderGuid == null || orderGuid.Length < 1 ||
                    assignSite == null || assignSite.Length < 1)
                {
                    System.Diagnostics.Debug.Assert(false, "Missing Parameter");
                    throw (new Exception("Miss Parameter in DeleteReportDAO!"));
                }
                string sql = string.Format("update tRegOrder set Assign2Siet ={0} where orderGuid={1}",
                    orderGuid, assignSite);


                ServerPubFun.RISLog_Info(0, "AssingReportDAO_MSSQL, SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                using (RisDAL dal = new RisDAL())
                {
                    dal.ExecuteNonQuery(sql);
                }

                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "AssingReportDAO_MSSQL=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "-1";
        }
    }
}