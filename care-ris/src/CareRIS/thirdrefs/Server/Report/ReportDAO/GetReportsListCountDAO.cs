using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    /// <summary>
    /// GetReportsListCountDAO
    /// </summary>
    public class GetReportsListCountDAO
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            bool bHide0 = ServerPubFun.GetSystemProfile_Bool(ReportCommon.ProfileName.ReportList_HideCount, ReportCommon.ModuleID.Report);
            bool bHide1 = ServerPubFun.GetSystemProfile_Bool(ReportCommon.ProfileName.ReportPrint_HideCount, ReportCommon.ModuleID.Report);
            if (bHide0 && bHide1)
            {
                return -1;
            }

            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }
    }

    /// <summary>
    /// for abstract
    /// </summary>
    internal class GetReportsListCountDAO_ABSTRACT : IReportDAO
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    /// <summary>
    /// for Sybase
    /// </summary>
    internal class GetReportsListCountDAO_SYBASE : IReportDAO
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    /// <summary>
    /// for MS SQLServer
    /// </summary>
    internal class GetReportsListCountDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            int iRet = -1;
            string sql = "";

            try
            {
                #region Parse the parameters

                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    //throw (new Exception("No parameter in GetReportsListCountDAO!"));

                    ServerPubFun.RISLog_Info(0, "No parameter in GetReportsListCountDAO!", "", 0);
                }

                string condition = "";
                bool bOfflineData = false;

                foreach (string key in paramMap.Keys)
                {
                    switch (key.ToUpper())
                    {
                        case "CONDITION":
                            {
                                condition = paramMap[key] as string;

                                if (condition == null)
                                    condition = "";
                            }
                            break;
                        case "OFFLINEDATA":
                            {
                                bOfflineData = (System.Convert.ToString(paramMap[key]) == "1");
                            }
                            break;
                    }
                }

                condition = condition == null ? "" : condition;

                #endregion

                #region Compose SQL sentense

                //sql = "select count(1) "
                //     + " from tRegPatient, tRegOrder, tRegProcedure "
                //     + " left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid "                     
                //     + " , tProcedureCode"
                //     + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid "                     
                //     + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                //     + " and tProcedureCode.ProcedureCode = tRegProcedure.ProcedureCode "
                //     + " and tRegProcedure.status >= " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination);

                //if (condition != null && condition.Length > 0)
                //{
                //    while ((condition = condition.Trim()).Length > 0 && condition.ToUpper().EndsWith("AND"))
                //    {
                //        condition = condition.Substring(0, condition.Length - 3);
                //    }

                //    sql += " and " + condition;
                //}

                #endregion

                #region Execute Query

                using (RisDAL dal = new RisDAL())
                {

                    DataTable dt = new DataTable();

                    //if (0 == iWrittenCount++ % 100)
                    //{
                    //    ServerPubFun.RISLog_Info(0, "GetReportsListCountDAO_MSSQL, SQL=" + sql, "", 0);
                    //}
                    //else
                    //{
                    //    ServerPubFun.RISLog_Info(0, "GetReportsListCountDAO_MSSQL, condition=" + condition + ", iWrittenCount=" + iWrittenCount.ToString(), "", 0);
                    //}

                    //dal.ExecuteQuery(sql, dt);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    iRet = System.Convert.ToInt32(dt.Rows[0][0]);
                    //}

                    dal.Parameters.Clear();
                    dal.Parameters.AddVarChar("@condition", condition, 8000);
                    dal.Parameters.AddVarChar("@offlineCondition", bOfflineData ? "1" : "", 8000);
                    dal.Parameters.AddInt("@TotalCount", iRet, ParameterDirection.Output);

                    DataTable dt1 = new DataTable();
                    dal.ExecuteQuerySP("SP_REPORT_PAGE_COUNT", dt1);
                    dt1.TableName = "ReportList";

                    if (dal.Parameters["@TotalCount"].Value != null)
                    {
                        iRet = Convert.ToInt32(dal.Parameters["@TotalCount"].Value);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportsListCountDAO_MSSQL, MSG=" + ex.Message + ", SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return iRet;
        }
    }

    /// <summary>
    /// for Oracle
    /// </summary>
    internal class GetReportsListCountDAO_ORACLE : IReportDAO
    {
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }
}
