using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{

    public class GetSignImageDAO
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

    internal class GetSignImageDAO_ABSTRACT : IReportDAO
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

    internal class GetSignImageDAO_SYBASE : IReportDAO
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

    internal class GetSignImageDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetLockDAO!"));
                }

                string userGuid = "";
                string reportGuid = "";
                string SignActions = "";
                string certsn = "";
                bool IsGetDigitalSignImage = false;

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "USERGUID")
                    {
                        userGuid = paramMap[key] as string;

                        if (userGuid == null)
                            userGuid = "";
                    }
                    if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }

                    if (key.ToUpper() == "ISGETDIGITALSIGNIMAGE")
                    {
                        IsGetDigitalSignImage = Convert.ToBoolean(paramMap[key]);
                    }

                    if (key.ToUpper() == "SIGNACTIONS")
                    {
                        SignActions = Convert.ToString(paramMap[key]);
                    }

                }

                

                DataSet ds = new DataSet();

                //dal.BeginTransaction();

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        if (reportGuid.Trim().Length > 0)
                        {
                            string strSQL = string.Format("SELECT Creater,Submitter,FirstApprover,SecondApprover FROM tReport WHERE REPORTGUID = '{0}'", reportGuid);
                            DataTable dt = dal.ExecuteQuery(strSQL);


                            if (dt == null || dt.Rows.Count == 0)
                            {
                                return null;
                            }

                            string strCreater = "", strFirstApprover = "", strSubmitter = "", strSecondApprover = "";
                            if (dt.Rows[0]["Creater"] != null)
                            {
                                strCreater = Convert.ToString(dt.Rows[0]["Creater"]);
                            }

                            if (dt.Rows[0]["FirstApprover"] != null)
                            {
                                strFirstApprover = Convert.ToString(dt.Rows[0]["FirstApprover"]);
                            }

                            if (dt.Rows[0]["Submitter"] != null)
                            {
                                strSubmitter = Convert.ToString(dt.Rows[0]["Submitter"]);
                            }

                            if (dt.Rows[0]["SecondApprover"] != null)
                            {
                                strSecondApprover = Convert.ToString(dt.Rows[0]["SecondApprover"]);
                            }

                            if (!IsGetDigitalSignImage)
                            {
                                if (strCreater.Length > 0)
                                {

                                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strCreater);
                                    DataTable dtCreater = dal.ExecuteQuery(strSQL);
                                    dtCreater.TableName = "CreaterSignImage";
                                    ds.Tables.Add(dtCreater);
                                }

                                if (strFirstApprover.Length > 0)
                                {
                                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strFirstApprover);
                                    DataTable dtFirstApprover = dal.ExecuteQuery(strSQL);
                                    dtFirstApprover.TableName = "FirstApproverSignImage";
                                    ds.Tables.Add(dtFirstApprover);

                                }

                                if (strSubmitter.Length > 0)
                                {
                                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strSubmitter);
                                    DataTable dtSubmitter = dal.ExecuteQuery(strSQL);
                                    dtSubmitter.TableName = "SubmitterSignImage";
                                    ds.Tables.Add(dtSubmitter);
                                }

                                if (strSecondApprover.Length > 0)
                                {
                                    strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", strSecondApprover);
                                    DataTable dtSecondApprover = dal.ExecuteQuery(strSQL);
                                    dtSecondApprover.TableName = "SecondApproverSignImage";
                                    ds.Tables.Add(dtSecondApprover);
                                }
                            }
                            else
                            {
                                #region CA sign pic
                                string[] signActionsArr = SignActions.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                if (signActionsArr.Length > 0)
                                {
                                    bool isSubmitAction, isApproveAction, isSecondApproveAction;
                                    isSubmitAction = isApproveAction = isSecondApproveAction = false;
                                    foreach (string act in signActionsArr)
                                    {
                                        if (act.Equals("submitreport", StringComparison.OrdinalIgnoreCase))
                                        {
                                            isSubmitAction = true;
                                        }
                                        else if (act.Equals("approvereport", StringComparison.OrdinalIgnoreCase))
                                        {
                                            isApproveAction = true;
                                        }
                                        else if (act.Equals("secondapprovereport", StringComparison.OrdinalIgnoreCase))
                                        {
                                            isSecondApproveAction = true;
                                        }
                                    }
                                    if (isSubmitAction)
                                    {
                                        strSQL = string.Format("SELECT signpic FROM tUserCerts WHERE certsn = (select top 1 certsn  from tsignedhistory where reportguid ='{0}' and action='submitreport' order by createdt desc)", reportGuid);
                                        dal.ExecuteQuery(strSQL, ds, "SubmitterSignImage");
                                    }

                                    if (isApproveAction)
                                    {
                                        strSQL = string.Format("SELECT signpic FROM tUserCerts WHERE certsn = (select top 1 certsn  from tsignedhistory where reportguid ='{0}' and action='ApproveReport' order by createdt desc)", reportGuid);
                                        dal.ExecuteQuery(strSQL, ds, "FirstApproverSignImage");
                                    }

                                    if (isSecondApproveAction)
                                    {
                                        strSQL = string.Format("SELECT signpic FROM tUserCerts WHERE certsn = (select top 1 certsn  from tsignedhistory where reportguid ='{0}' and action='SecondApproveReport' order by createdt desc)", reportGuid);
                                        dal.ExecuteQuery(strSQL, ds, "SecondApproverSignImage");
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {

                            string strSQL = string.Format("SELECT SignImage FROM tUser WHERE userguid = '{0}'", userGuid);

                            dal.ExecuteQuery(strSQL, ds, "SignImage");

                        }
                    }
                }
                catch (Exception ex)
                {
                    //dal.RollbackTransaction();

                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    throw (ex);
                }

                //dal.CommitTransaction();

               

                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetLockDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    internal class GetSignImageDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetLockDAO!"));
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

                ServerPubFun.RISLog_Error(0, "GetLockDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
