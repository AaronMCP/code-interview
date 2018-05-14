using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{

    public class GetDrawerSignDAO
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

    internal class GetDrawerSignDAO_ABSTRACT : IReportDAO
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

    internal class GetDrawerSignDAO_SYBASE : IReportDAO
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

    internal class GetDrawerSignDAO_MSSQL : IReportDAO
    {
      

        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetDrawerSignDAO!"));
                }

                          

               
                DataSet ds = new DataSet();

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        string strReportGuids = paramMap["ReportGuid"] as string;
                        string[] arrItem = strReportGuids.Split('|');

                        DataTable dtDrawSign = null;
                        strReportGuids = "";
                        foreach (string str in arrItem)
                        {
                            string strSQL = string.Format("select ReportGuid,IsDraw,DrawerSign,TakeFilmDept,"
                                + " TakeFilmRegion,TakeFilmComment "
                                //+ " ReportTextApprovedSign, ReportTextSubmittedSign,"
                                //+ " CombinedForCertification, SignCombinedForCertification "
                                + " from tReport where ReportGuid ='{0}'", str);
                            DataTable dt = dal.ExecuteQuery(strSQL);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dtDrawSign == null)
                                {
                                    dtDrawSign = dt.Clone();
                                }
                                dtDrawSign.Rows.Add(dt.Rows[0].ItemArray);
                            }
                        }
                        ds.Tables.Add(dtDrawSign);
                    }
                                       
                }
                catch (Exception ex)
                {
                    

                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "GetDrawerSignDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                    throw (ex);
                }               


                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetDrawerSignDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    internal class GetDrawerSignDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetDrawerSignDAO!"));
                }

                string action = "";
                string lockType = "";
                string lockGuid = "";
                string owner = "";
                string ownerIP = "";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "LOCKTYPE")
                    {
                        lockType = paramMap[key] as string;

                        if (lockType == null)
                            lockType = "";
                    }
                    else if (key.ToUpper() == "LOCKGUID")
                    {
                        lockGuid = paramMap[key] as string;

                        if (lockGuid == null)
                            lockGuid = "";
                    }
                    else if (key.ToUpper() == "ACTION")
                    {
                        action = paramMap[key] as string;

                        if (action == null)
                            action = "";
                    }
                    else if (key.ToUpper() == "OWNER")
                    {
                        owner = paramMap[key] as string;

                        if (owner == null)
                            owner = "";
                    }
                    else if (key.ToUpper() == "OWNERIP")
                    {
                        ownerIP = paramMap[key] as string;

                        if (ownerIP == null)
                            ownerIP = "";
                    }
                }

               

                DataSet ds = new DataSet();

                //dal.BeginTransaction();

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        if (action.ToUpper() == "ADD")
                        {
                            string sql0 = "merge into tSync D"
                                + " using (select " + lockType + " syncType, '" + lockGuid + "' guid, '"
                                + owner + "' owner, '" + ownerIP + "' ownerIP from dual) S"
                                + " on (s.synctype = d.syncType and s.guid = d.guid)"
                                + " when not matched then insert (d.syncType, d.guid, d.owner, d.ownerIP)"
                                + " values(s.synctype, s.guid, s.owner, s.ownerip)";
                            string sql1 = " select * from tSync, tUser where tSync.Owner = tUser.UserGuid"
                                + " and tSync.SyncType=" + lockType + " and tSync.Guid='" + lockGuid + "' for update";
                            string sql2 = " commit";



                            dal.ExecuteNonQuery(sql0);
                            dal.ExecuteQuery(sql1, ds, "tSync");
                            dal.ExecuteNonQuery(sql2);
                        }
                        else if (action.ToUpper() == "DELETE" || action.ToUpper() == "DEL")
                        {
                            string sql0 = "delete from tSync"
                                + " where syncType=" + lockType + " and guid='" + lockGuid + "'";
                            string sql1 = " select * from tSync, tUser where tSync.Owner = tUser.UserGuid"
                                + " and tSync.SyncType=" + lockType + " and tSync.Guid='" + lockGuid + "' ";
                            string sql2 = " commit";

                            dal.ExecuteNonQuery(sql0);
                            dal.ExecuteQuery(sql1, ds, "tSync");
                            dal.ExecuteNonQuery(sql2);
                        }
                        else
                        {
                            string sql = "select * from tSync, tUser where tSync.Owner=tUser.UserGuid and tSync.SyncType="
                                + lockType + " and tSync.Guid='" + lockGuid + "'";

                            dal.ExecuteQuery(sql, ds, "tSync");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //dal.RollbackTransaction();

                    throw (ex);
                }

                //dal.CommitTransaction();

               

                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetDrawerSignDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
