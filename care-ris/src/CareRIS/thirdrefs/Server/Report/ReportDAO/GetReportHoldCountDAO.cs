using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;

    namespace Server.ReportDAO
    {
        public class GetReportHoldCountDAO
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

        internal class GetReportHoldCountDAO_ABSTRACT : IReportDAO
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

        internal class GetReportHoldCountDAO_SYBASE : IReportDAO
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

        internal class GetReportHoldCountDAO_MSSQL : IReportDAO
        {
            public object Execute(object param)
            {
                int count = 0;
                string userGuid = "";
                string reportGuid = "";
                int countType = 0;
                try
                {
                    Dictionary<string, object> paramMap = param as Dictionary<string, object>;
                    if (paramMap == null || paramMap.Count < 1)
                    {
                        throw (new Exception("No parameter in GetReportHoldCountDAO_MSSQL!"));
                    }
                   
                    userGuid = paramMap["UserGuid"] as string;
                    if (paramMap.ContainsKey("ReportGuid"))
                    {
                        reportGuid = paramMap["ReportGuid"] as string;
                    }
                    countType = Convert.ToInt32(paramMap["CountType"]);
                    switch (countType)
                    {
                        case 1:
                            count = getUnWrittenReportCount(userGuid, reportGuid);
                            break;
                        case 2:
                            count = getUnApprovedReportCount(userGuid);
                            break;
                        case 3:
                            count = getUnWrittenUnApprovedReportCount(userGuid,reportGuid);
                            break;
                        default:
                            break;
                    }
                    return count;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return count;
                }
            }

            private int getUnWrittenReportCount(string userGuid,string reportGuid)
            {
                int unWrittenReportCount = 0;
                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        DataTable dtUnwrittenReportNoLock = new DataTable();
                        DataTable dtLockedRp = new DataTable();
                        DataTable dtLockedReportGuids = new DataTable();
                        string lockedSyncRpGuids = "";
                        string lockedRpGuids = "";
                        string lockedReportGuids = "";
                        string sqlCreatedAndCreaterIsSelf = "select count(1) from treport where reportguid = '{0}' and creater ='{1}' and status = 100";
                        string sqlUnwrittenReportNoLock = @"select ReportGuid,Status from treport where status =100 and (creater ='{0}' or (creater <>'{0}' and ReportGuid in('{1}')))";
                        string sqlLockedRp = string.Format("select rpguids from tsync where owner ='{0}' and moduleid='0400'", userGuid);
                        string sqlLockedReportGuids = "select distinct reportguid from tRegProcedure where status =100 and status != 105 and  ReportGuid != null or len(ReportGuid) >0 and ProcedureGuid in ('{0}')";


                        if (!string.IsNullOrEmpty(reportGuid))
                        {
                            int result = Convert.ToInt32(dal.ExecuteScalar(string.Format(sqlCreatedAndCreaterIsSelf, reportGuid,userGuid)));
                            if (result == 1)
                            {
                                return 0;//no limitation to open self 100 status and creater is self
                            }
                        }


                        dal.ExecuteQuery(sqlLockedRp, dtLockedRp);
                        foreach (DataRow dr in dtLockedRp.Rows)
                        {
                            lockedSyncRpGuids = Convert.ToString(dr["RPGuids"]);
                            if (lockedSyncRpGuids.Length > 0)
                            {
                                lockedRpGuids += lockedSyncRpGuids.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0] + ",";
                            }
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            lockedRpGuids = lockedRpGuids.TrimEnd(",".ToCharArray());
                            lockedRpGuids = lockedRpGuids.Replace(",", "','");
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            dal.ExecuteQuery(string.Format(sqlLockedReportGuids, lockedRpGuids), dtLockedReportGuids);
                            foreach (DataRow dr in dtLockedReportGuids.Rows)
                            {
                                lockedReportGuids += Convert.ToString(dr["ReportGuid"]) + ",";
                            }

                            if (lockedReportGuids.Length > 0)
                            {
                                lockedReportGuids = lockedReportGuids.TrimEnd(",".ToCharArray());
                                lockedReportGuids = lockedReportGuids.Replace(",", "','");
                            }
                        }

                        dal.ExecuteQuery(string.Format(sqlUnwrittenReportNoLock, userGuid, lockedReportGuids), dtUnwrittenReportNoLock);

                        unWrittenReportCount = dtUnwrittenReportNoLock.Rows.Count + dtLockedRp.Rows.Count - dtLockedReportGuids.Rows.Count;
                    }
                    return unWrittenReportCount;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return unWrittenReportCount;
                }
            }

            private int getUnApprovedReportCount(string userGuid)
            {
                int unapprovedReportCount = 0;
                try
                {

                    using (RisDAL dal = new RisDAL())
                    {
                        DataTable dtUnApprovedReportNoLock = new DataTable();
                        DataTable dtLockedRp = new DataTable();
                        DataTable dtLockedReportGuids = new DataTable();
                        string lockedSyncRpGuids = "";
                        string lockedRpGuids = "";
                        string sqlLockedRp = string.Format("select rpguids from tsync where owner ='{0}' and moduleid='0400'", userGuid);
                        string sqlLockedReportGuids = "select distinct reportguid from tRegProcedure where status = 110 and ProcedureGuid in ('{0}')";

                        dal.ExecuteQuery(sqlLockedRp, dtLockedRp);
                        foreach (DataRow dr in dtLockedRp.Rows)
                        {
                            lockedSyncRpGuids = Convert.ToString(dr["RPGuids"]);
                            if (lockedSyncRpGuids.Length > 0)
                            {
                                lockedRpGuids += lockedSyncRpGuids.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0] + ",";
                            }
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            lockedRpGuids = lockedRpGuids.TrimEnd(",".ToCharArray());
                            lockedRpGuids = lockedRpGuids.Replace(",", "','");
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            dal.ExecuteQuery(string.Format(sqlLockedReportGuids, lockedRpGuids), dtLockedReportGuids);
                        }

                        unapprovedReportCount = dtLockedReportGuids.Rows.Count;
                    }
                    return unapprovedReportCount;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return unapprovedReportCount;
                }
            }

            private int getUnWrittenUnApprovedReportCount(string userGuid,string reportGuid)
            {
                int unWrittenUnApprovedReportCount = 0;
                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        DataTable dtUnwrittenReportNoLock = new DataTable();
                        DataTable dtLockedRp = new DataTable();
                        DataTable dtLockedReportGuids = new DataTable();
                        string lockedSyncRpGuids = "";
                        string lockedRpGuids = "";
                        string lockedReportGuids = "";
                        string sqlCreatedAndCreaterIsSelf = "select count(1) from treport where reportguid = '{0}' and creater ='{1}' and status = 100";
                        string sqlUnwrittenReportNoLock = @"select ReportGuid,Status from treport where status =100 and (creater ='{0}' or (creater <> '{0}' and ReportGuid in('{1}')))";
                        string sqlLockedRp = string.Format("select rpguids from tsync where owner ='{0}' and moduleid='0400'", userGuid);
                        string sqlLockedReportGuids = "select distinct reportguid from tRegProcedure where (status =100 or status =105 or status =120)  and ProcedureGuid in ('{0}')";

                        if (!string.IsNullOrEmpty(reportGuid))
                        {
                            int result = Convert.ToInt32(dal.ExecuteScalar(string.Format(sqlCreatedAndCreaterIsSelf, reportGuid, userGuid)));
                            if (result == 1)
                            {
                                return 0;//no limitation to open self 100 status and creater is self
                            }
                        }

                        dal.ExecuteQuery(sqlLockedRp, dtLockedRp);
                        foreach (DataRow dr in dtLockedRp.Rows)
                        {
                            lockedSyncRpGuids = Convert.ToString(dr["RPGuids"]);
                            if (lockedSyncRpGuids.Length > 0)
                            {
                                lockedRpGuids += lockedSyncRpGuids.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0] + ",";
                            }
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            lockedRpGuids = lockedRpGuids.TrimEnd(",".ToCharArray());
                            lockedRpGuids = lockedRpGuids.Replace(",", "','");
                        }

                        if (lockedRpGuids.Length > 0)
                        {
                            dal.ExecuteQuery(string.Format(sqlLockedReportGuids, lockedRpGuids), dtLockedReportGuids);
                            foreach (DataRow dr in dtLockedReportGuids.Rows)
                            {
                                lockedReportGuids += Convert.ToString(dr["ReportGuid"]) + ",";
                            }

                            if (lockedReportGuids.Length > 0)
                            {
                                lockedReportGuids = lockedReportGuids.TrimEnd(",".ToCharArray());
                                lockedReportGuids = lockedReportGuids.Replace(",", "','");
                            }
                        }
                        dal.ExecuteQuery(string.Format(sqlUnwrittenReportNoLock, userGuid, lockedReportGuids), dtUnwrittenReportNoLock);
                        //unwritten(created by self) and not locked + locked all reports - locked(status = 100,105,120)
                        unWrittenUnApprovedReportCount = dtUnwrittenReportNoLock.Rows.Count + dtLockedRp.Rows.Count - dtLockedReportGuids.Rows.Count;
                    }
                    return unWrittenUnApprovedReportCount;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return unWrittenUnApprovedReportCount;
                }
            }
        }

        internal class GetReportHoldCountDAO_ORACLE : IReportDAO
        {
            public object Execute(object param)
            {
                return null;
            }
        }
    }

