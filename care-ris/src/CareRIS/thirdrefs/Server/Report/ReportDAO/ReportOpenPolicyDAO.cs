using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;

namespace Server.ReportDAO
{

    public class ReportOpenPolicyDAO
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

        public object Execute(object param, ref string strObjectGuids)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

                Type type = Type.GetType(clsType);
                IReportNewDAO iRptDAO = Activator.CreateInstance(type) as IReportNewDAO;
                return iRptDAO.Execute(param, ref strObjectGuids);
            }
        }
    }

    internal class ReportOpenPolicyDAO_ABSTRACT : IReportDAO, IReportNewDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return null;
        }
        public object Execute(object param, ref string strObjectGuids)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportNewDAO iRptDAO = Activator.CreateInstance(type) as IReportNewDAO;
            return iRptDAO.Execute(param, ref strObjectGuids);
        }
    }



    /// <summary>
    /// return 0--正常  1--已写过报告 2--已删除 3--不是同一个ORDER 4--图像未到达 
    /// </summary>
    internal class ReportOpenPolicyDAO_MSSQL : IReportDAO, IReportNewDAO
    {
        static int iWrittenLog = 0;
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return null;
        }

        public object Execute(object param, ref string strObjectGuids)
        {
            try
            {
                #region Parse the parameters

                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in ReportOpenPolicyDAO!"));
                }

                string RPGuids = "";
                bool bAssociated = false;
                bool bOfflineData = false;

                foreach (string key in inMap.Keys)
                {
                    switch (key.ToUpper())
                    {
                        case "RPGUIDS":
                            {
                                RPGuids = inMap[key] as string;
                            }
                            break;
                        case "ASSOCIATED":
                            {
                                bAssociated = (System.Convert.ToString(inMap[key]) == "1");
                            }
                            break;
                        case "OFFLINEDATA":
                            {
                                bOfflineData = (System.Convert.ToString(inMap[key]) == "1");
                            }
                            break;
                    }
                }

                #endregion

                string strSQL = "";

                bool bExistImage = true;
                using (RisDAL dal = new RisDAL())
                {

                    if (bAssociated)
                    {
                        string strTemp = "";
                        strSQL = string.Format(
                            "   SELECT OrderGuid,status,reportGuid FROM tregprocedure where procedureguid='{0}' ", RPGuids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);

                        if (bOfflineData)
                            strSQL += string.Format(" UNION "
                               + " SELECT OrderGuid,status,reportGuid FROM RISArchive..tregprocedure where procedureguid='{0}' ", RPGuids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);

                        DataTable dt1 = dal.ExecuteQuery(strSQL);
                        if (dt1 == null || dt1.Rows.Count < 1)
                        {
                            // RP not exists
                            return 2;
                        }

                        if (Convert.ToInt32(dt1.Rows[0]["status"]) != 50)
                        {
                            strObjectGuids = Convert.ToString(dt1.Rows[0]["ReportGuid"]);

                            if (string.IsNullOrEmpty(strObjectGuids))
                                return 2;
                            else
                                return 1;
                        }


                        strSQL = string.Format(
                            "   SELECT ProcedureGuid,IsExistImage  FROM   tregprocedure where status=50 and OrderGuid='{0}' "
                            , Convert.ToString(dt1.Rows[0]["OrderGuid"]));

                        if (bOfflineData)
                            strSQL += string.Format(" UNION "
                            + " SELECT ProcedureGuid,IsExistImage  FROM   RISArchive..tregprocedure where status=50 and OrderGuid='{0}' "
                            , Convert.ToString(dt1.Rows[0]["OrderGuid"]));

                        dt1 = dal.ExecuteQuery(strSQL);


                        foreach (DataRow dr in dt1.Rows)
                        {
                            string strRPGuid = Convert.ToString(dr["ProcedureGuid"]);
                            strTemp += strRPGuid;
                            strTemp += ",";
                            if (dr["IsExistImage"] is DBNull || dr["IsExistImage"] == null || Convert.ToInt32(dr["IsExistImage"]) == 0)
                            {
                                bExistImage = false;
                            }
                        }
                        strObjectGuids = strTemp.TrimEnd(',');
                        return bExistImage ? 0 : 4;

                    }

                    //
                    if (RPGuids.Split(',').Length > 1)
                    {
                        RPGuids = RPGuids.Trim(", ".ToCharArray());
                        RPGuids = RPGuids.Replace(",", "','");
                        RPGuids = "'" + RPGuids + "'";
                        strSQL = string.Format(
                            "  SELECT A.OrderGuid,A.Status,A.IsExistImage,A.ReportGuid from tRegProcedure A  where A.ProcedureGuid in({0})"
                            , RPGuids);

                        if (bOfflineData)
                            strSQL += string.Format(" UNION "
                           + " SELECT A.OrderGuid,A.Status,A.IsExistImage,A.ReportGuid from RISArchive..tRegProcedure A  where A.ProcedureGuid in({0})"
                            , RPGuids);

                    }
                    else
                    {
                        strSQL = string.Format(
                            "  SELECT A.OrderGuid,A.Status,A.IsExistImage,A.ReportGuid from tRegProcedure A where A.ProcedureGuid ='{0}'"
                            , RPGuids);

                        if (bOfflineData)
                            strSQL += string.Format(" UNION "
                           + " SELECT A.OrderGuid,A.Status,A.IsExistImage,A.ReportGuid from RISArchive..tRegProcedure A where A.ProcedureGuid ='{0}'"
                            , RPGuids);
                    }

                    DataTable dt2 = dal.ExecuteQuery(strSQL);

                    if (dt2 == null || dt2.Rows.Count == 0)
                    {
                        // RP NOT exists
                        return 2;
                    }


                    bool bSameOrder = true;

                    string strExpression = "status>50";
                    DataRow[] drFound = dt2.Select(strExpression);
                    if (drFound.Length != 0)
                    {

                        foreach (DataRow dr in drFound)
                        {
                            if (dr["ReportGuid"] != null && !(dr["ReportGuid"] is DBNull))
                                strObjectGuids += Convert.ToString(dr["ReportGuid"]);
                            strObjectGuids += ",";
                        }
                        strObjectGuids.TrimEnd(',');
                        return 1;
                    }
                    else
                    {
                        strObjectGuids = RPGuids;
                    }

                    strExpression = "IsExistImage=0";
                    drFound = dt2.Select(strExpression);
                    if (drFound.Length > 0)
                    {
                        bExistImage = false;
                    }

                    if (dt2.Rows.Count > 1)
                    {
                        List<string> listOrderGuid = new List<string>();
                        foreach (DataRow dr in dt2.Rows)
                        {
                            string strOrderGuid = Convert.ToString(dr["OrderGuid"]);
                            if (listOrderGuid.Count == 0)
                            {
                                listOrderGuid.Add(strOrderGuid);
                                continue;
                            }
                            if (!listOrderGuid.Contains(strOrderGuid))
                            {
                                bSameOrder = false;
                                break;
                            }

                        }
                    }


                    if (!bSameOrder)
                    {
                        return 3;
                    }

                    if (!bExistImage)
                    {
                        return 4;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "ReportOpenPolicyDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return 0;
        }
    }

}
