using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetLockDAO
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

    internal class GetLockDAO_ABSTRACT : IReportDAO
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

    internal class GetLockDAO_SYBASE : IReportDAO
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

    internal class GetLockDAO_MSSQL : IReportDAO
    {
       // static int iWrittenCount = 0;
        static int _rwfMode = -1;
        const int RWF_COMPLETED_VS_RIS_STATUS = 110;

        public object Execute(object param)
        {
            try
            {
                #region get parameters

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
                string rpguids = "";
                string patientid = "";
                string localname = "";
                string accno = "";
                string reportGuid = "";

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
                    else if(key.ToUpper()=="RPGUIDS")
                    {

                        rpguids = paramMap[key] as string;

                        if (rpguids == null)
                            rpguids = "";
                    }
                    else if (key.ToUpper() == "PATIENTID")
                    {

                        patientid = paramMap[key] as string;

                        if (patientid == null)
                            patientid = "";
                    }
                    else if (key.ToUpper() == "LOCALNAME")
                    {

                        localname = paramMap[key] as string;

                        if (localname == null)
                            localname = "";
                    }
                    else if (key.ToUpper() == "ACCNO")
                    {

                        accno = paramMap[key] as string;

                        if (accno == null)
                            accno = "";
                    }
                    else if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                        {
                            reportGuid = "";
                        }
                    }
                }

                #endregion

                

                DataSet ds = new DataSet();

                //dal.BeginTransaction();

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {
                        DataTable dtLock = null;
                        string strSQL = "";
                        if (!string.IsNullOrEmpty(lockGuid))
                        {
                            strSQL = string.Format("select Owner,OwnerIP,RPGuids,ModuleID from tSync where guid='{0}'", lockGuid);
                            dtLock = dal.ExecuteQuery(strSQL);
                        }

                        if (action.ToUpper() == "ADD")
                        #region ADD
                        {
                            #region only ReportGuid is arrived so should get some info of patient,order and rp
                            if (!string.IsNullOrEmpty(reportGuid))
                            {
                                string strFromRp = string.Format("select procedureguid, orderguid from tregprocedure with(nolock) where reportguid ='{0}'", reportGuid);
                                string strFromOrder = "select accno,patientguid from tregorder with(nolock) where orderguid ='{0}'";
                                string strFromPatient = "select patientid, localname from tregpatient with(nolock) where patientguid='{0}'";
                                //get rp info
                                DataTable dtRp = dal.ExecuteQuery(strFromRp);
                                rpguids = "";
                                foreach (DataRow dr in dtRp.Rows)
                                {
                                    rpguids += Convert.ToString(dr["ProcedureGuid"]) + "&" + owner + "&" + ownerIP + "|";
                                }
                                rpguids = rpguids.TrimEnd(new char[] { '|' });
                                lockGuid = Convert.ToString(dtRp.Rows[0]["OrderGuid"]);
                                //get accno
                                DataTable dtOrder = dal.ExecuteQuery(string.Format(strFromOrder, lockGuid));
                                accno = Convert.ToString(dtOrder.Rows[0]["AccNo"]);
                                //get patient info
                                DataTable dtPatient = dal.ExecuteQuery(string.Format(strFromPatient, dtOrder.Rows[0]["PatientGuid"]));
                                patientid = Convert.ToString(dtPatient.Rows[0]["PatientID"]);
                                localname = Convert.ToString(dtPatient.Rows[0]["LocalName"]);
                                //lockGuid has value now~
                                strSQL = string.Format("select Owner,OwnerIP,RPGuids,ModuleID from tSync where guid='{0}'", lockGuid);
                                dtLock = dal.ExecuteQuery(strSQL);
                            }
                            #endregion

                            //#region RWF

                            //if (_rwfMode == -1)
                            //    _rwfMode = ServerPubFun.GetSystemProfile_Int("RWFmode");

                            //if (_rwfMode == 1)
                            //{
                            //    int rpStatus = CS.GCRIS.RWF.SCU.RwfScu.getRpStatus(lockGuid);

                            //    if (CS.GCRIS.RWF.SCU.RwfScu.needClaim(lockGuid))
                            //    {
                            //        if (!CS.GCRIS.RWF.SCU.RwfScu.claim(lockGuid, owner))
                            //        {
                            //            // failed to check.
                            //            strSQL = "select top 1 '2' syncType, '" + lockGuid + "' guid, 'CSBROKER' owner,"
                            //                + " 'CSBROKER' ownerIP, '0400' moduleID, p.patientID, p.localName, o.AccNo, '' RPGuids"
                            //                + " from tRegPatient p, tRegOrder o "
                            //                + " where p.patientguid=o.patientguid AND o.orderguid='" + lockGuid + "' ";

                            //            dal.ExecuteQuery(strSQL, ds, "tSync");

                            //            return ds;
                            //        }
                            //    }
                            //    else if (100 == rpStatus)
                            //    {
                            //        CS.GCRIS.RWF.SCU.RwfScu.nSetCompleted(lockGuid, owner);
                            //    }
                            //}

                            //#endregion

                            #region add lock in ris

                            if (dtLock == null || dtLock.Rows.Count == 0)
                            {
                                string domain = CommonGlobalSettings.Utilities.GetCurDomain();

                                //Not be locked
                                strSQL = string.Format("insert tSync(syncType, guid, owner, ownerIP, moduleID, patientID, patientName, AccNo,RPGuids,domain) " +
                                    "values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                    lockType, lockGuid, owner, ownerIP, "0400", patientid, localname, accno, rpguids, domain);
                            }
                            else
                            {
                                if (dtLock.Rows[0]["RPGuids"] == null || dtLock.Rows[0]["RPGuids"].ToString().Trim().Length == 0)
                                {
                                    //Lock by other panel
                                    strSQL = string.Format("select syncType, guid, owner, ownerIP, moduleID, patientID, patientName, AccNo,RPGuids from tSync where guid='{0}'", lockGuid);

                                }
                                else
                                {
                                    string strOrgRpGuids = Convert.ToString(dtLock.Rows[0]["RPGuids"]);
                                    string strOrgOwner = Convert.ToString(dtLock.Rows[0]["owner"]);
                                    string strOrgOwnerIP = Convert.ToString(dtLock.Rows[0]["ownerIP"]);
                                    string strModuleID = Convert.ToString(dtLock.Rows[0]["moduleID"]);
                                    char[] sep1 = { '|' };
                                    char[] sep2 = { '&' };
                                    string[] arrItems1 = rpguids.Split(sep1);
                                    bool bLocked = false;
                                    string strLockInfo = "";
                                    foreach (string strSegment in arrItems1)
                                    {

                                        string[] arritems2 = strSegment.Split(sep2);
                                        if (arritems2[0] == null)
                                        {
                                            continue;
                                        }

                                        string[] strOrgItems = strOrgRpGuids.Split(sep1);
                                        foreach (string str in strOrgItems)
                                        {
                                            if (str.Contains(arritems2[0]))
                                            {
                                                bLocked = true;
                                                strLockInfo += str;
                                                strLockInfo += "|";
                                            }
                                        }

                                    }

                                    if (bLocked)
                                    {
                                        strLockInfo = strLockInfo.TrimEnd('|');
                                        //Lock by other by report panel                                   
                                        string[] arrItem3 = strLockInfo.Split(sep1);
                                        if (arrItem3.Length > 0)
                                        {
                                            DataTable dt = new DataTable("tSync");
                                            dt.Columns.Add("Description", typeof(string));
                                            dt.Columns.Add("Owner", typeof(string));
                                            dt.Columns.Add("OwnerIP", typeof(string));
                                            dt.Columns.Add("ModuleID", typeof(string));
                                            foreach (string strSegment in arrItem3)
                                            {
                                                string[] arrItem4 = strSegment.Split(sep2);
                                                string strRPGuid = arrItem4[0];
                                                string strLockUserGuid = arrItem4[1];
                                                string strLockIP = arrItem4[2];
                                                string str = string.Format("select RPDesc AS Description from tRegProcedure where ProcedureGuid='{0}'", strRPGuid);
                                                object obj = dal.ExecuteScalar(str);
                                                if (obj == null)
                                                {
                                                    obj = "";
                                                }
                                                DataRow dr = dt.NewRow();
                                                dr["Description"] = obj.ToString();
                                                dr["Owner"] = strLockUserGuid;
                                                dr["OwnerIP"] = strLockIP;
                                                dr["ModuleID"] = strModuleID;
                                                dt.Rows.Add(dr);

                                            }
                                            ds.Tables.Add(dt);
                                            strSQL = "";
                                        }
                                        else
                                        {
                                            strSQL = string.Format("select syncType, guid, owner, ownerIP, moduleID, patientID, patientName, AccNo,RPGuids from tSync where guid='{0}'", lockGuid);
                                        }

                                    }
                                    else
                                    {
                                        if (strOrgOwner.Contains(owner))
                                        {
                                            strSQL = string.Format("Update tSync set RPGuids='{0}' where guid='{1}'", strOrgRpGuids + "|" + rpguids, lockGuid);
                                        }
                                        else
                                        {

                                            strSQL = string.Format("Update tSync set RPGuids='{0}',owner='{1}',ownerip='{2}' where guid='{3}'", strOrgRpGuids + "|" + rpguids, strOrgOwner + "|" + owner, strOrgOwnerIP + "|" + ownerIP, lockGuid);
                                        }
                                    }

                                }

                            }

                            ServerPubFun.RISLog_Info(0, "GetLockDAO_MSSQL, ADD, owner=" + owner + ", ownerIP=" + ownerIP,
                                    (new System.Diagnostics.StackFrame()).GetFileName(),
                                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                            if (strSQL.Trim().Length > 0)
                            {
                                dal.ExecuteQuery(strSQL, ds, "tSync");
                            }

                            #endregion
                        }
                        #endregion
                        else if (action.ToUpper() == "DELETE" || action.ToUpper() == "DEL")
                        #region Delete
                        {
                            #region RWF

                            if (_rwfMode == -1)
                                _rwfMode = ServerPubFun.GetSystemProfile_Int("RWFmode");
                            /*
                            if (_rwfMode == 1)
                            {
                                int rpStatus = CS.GCRIS.RWF.SCU.RwfScu.getRpStatus(lockGuid);
                                if (RWF_COMPLETED_VS_RIS_STATUS == rpStatus)
                                {
                                    CS.GCRIS.RWF.SCU.RwfScu.spsComplete(lockGuid, owner);
                                }
                                else if (100 == rpStatus)
                                {
                                    CS.GCRIS.RWF.SCU.RwfScu.nCreate(lockGuid, owner);
                                }
                                else if (50 == rpStatus)
                                {
                                    CS.GCRIS.RWF.SCU.RwfScu.spsDefer(lockGuid, owner);
                                }
                                else
                                {
                                    // do nothing
                                }
                            }
                            */
                            #endregion

                            #region delete lock in ris

                            if (dtLock != null && dtLock.Rows.Count > 0)
                            {

                                string strOrgRpGuids = Convert.ToString(dtLock.Rows[0]["RPGuids"]);


                                string strOrgOwner = Convert.ToString(dtLock.Rows[0]["owner"]);
                                string strOrgOwnerIP = Convert.ToString(dtLock.Rows[0]["ownerIP"]);
                                char[] sep = { '|' };
                                string[] arrItems = rpguids.Split(sep);
                                bool bLocked = false;
                                foreach (string strRpGuid in arrItems)
                                {

                                    if (strOrgRpGuids.Contains(strRpGuid + "|"))
                                    {
                                        strOrgRpGuids = strOrgRpGuids.Replace(strRpGuid + "|", "");
                                    }
                                    else if (strOrgRpGuids.Contains(strRpGuid))
                                    {
                                        strOrgRpGuids = strOrgRpGuids.Replace(strRpGuid, "");
                                    }
                                }
                                strOrgRpGuids = strOrgRpGuids.TrimEnd('|');
                                if (strOrgRpGuids.Trim().Length == 0)
                                {
                                    strSQL = string.Format("delete from tSync where guid='{0}'", lockGuid);
                                }
                                else
                                {
                                    string[] arrItems1 = strOrgOwner.Split(sep);
                                    if (arrItems1.Length > 1)
                                    {

                                        foreach (string strOwner in arrItems1)
                                        {
                                            bool bRPGuidContainOwner = false;
                                            string[] arr10 = strOrgRpGuids.Split('|');
                                            foreach (string strRPGuid10 in arr10)
                                            {
                                                string[] arr11 = strRPGuid10.Split('&');
                                                if (owner.ToUpper() == arr11[1].ToUpper())
                                                {
                                                    bRPGuidContainOwner = true;
                                                    break;
                                                }
                                            }

                                            bool bOwnerContain = false;
                                            string[] arr12 = strOrgOwner.Split('|');
                                            foreach (string strOwner10 in arr12)
                                            {
                                                if (owner.ToUpper() == strOwner10.ToUpper())
                                                {
                                                    bOwnerContain = true;
                                                    break;
                                                }
                                            }

                                            if (bOwnerContain && !bRPGuidContainOwner)
                                            {
                                                if (strOrgOwner.Contains(owner + "|"))
                                                {
                                                    strOrgOwner = strOrgOwner.Replace(owner + "|", "");
                                                }
                                                else
                                                {
                                                    //strOrgOwner = strOrgOwner.Replace(owner, "");
                                                    strOrgOwner = "";
                                                    foreach (string strOwner10 in arr12)
                                                    {
                                                        if (owner.ToUpper() != strOwner10.ToUpper())
                                                        {
                                                            strOrgOwner += strOwner10;
                                                            strOrgOwner += "|";
                                                        }

                                                    }
                                                }
                                            }


                                        }
                                        strOrgOwner = strOrgOwner.TrimEnd('|');

                                        string[] arrItems2 = strOrgOwnerIP.Split(sep);
                                        foreach (string strOwnerIP in arrItems2)
                                        {

                                            if (strOrgOwnerIP.Contains(ownerIP + "|") && !strOrgRpGuids.Contains(ownerIP))
                                            {
                                                strOrgOwnerIP = strOrgOwnerIP.Replace(ownerIP + "|", "");
                                            }
                                            else if (strOrgOwnerIP.Contains(ownerIP) && !strOrgRpGuids.Contains(ownerIP))
                                            {
                                                strOrgOwnerIP = strOrgOwnerIP.Replace(ownerIP, "");
                                            }
                                        }
                                        strOrgOwnerIP = strOrgOwnerIP.TrimEnd('|');
                                        strSQL = string.Format("update tSync set owner='{0}',ownerip='{1}',rpguids='{2}' where guid='{3}'", strOrgOwner, strOrgOwnerIP, strOrgRpGuids, lockGuid);
                                    }
                                    else
                                    {
                                        strSQL = string.Format("update tSync set rpguids='{0}' where guid='{1}'", strOrgRpGuids, lockGuid);
                                    }
                                }



                                dal.ExecuteQuery(strSQL, ds, "tSync");
                            }
                            #endregion
                        }
                        #endregion
                        else
                        #region Query
                        {
                            string sql = "select * from tSync, tUser where tSync.Owner=tUser.UserGuid and tSync.SyncType="
                                + lockType + " and tSync.Guid='" + lockGuid + "'";

                            dal.ExecuteQuery(sql, ds, "tSync");
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    //dal.RollbackTransaction();

                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "GetLockDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                    throw (ex);
                }

              
              
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

    internal class GetLockDAO_ORACLE : IReportDAO
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
