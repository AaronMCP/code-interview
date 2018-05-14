using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    /// <summary>
    /// GetExamNumberDAO
    /// </summary>
    public class GetExamNumberDAO
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
    internal class GetExamNumberDAO_ABSTRACT : IReportDAO
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
    internal class GetExamNumberDAO_SYBASE : IReportDAO
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
    internal class GetExamNumberDAO_MSSQL : IReportDAO
    {

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Execute(object param)
        {
            int iRet = 0;
            string sql = "";

            try
            {
                #region Parse the parameters

                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    ServerPubFun.RISLog_Info(0, "No parameter in GeExamNumberDAODAO!", "", 0);
                }

                string patientid = "";
                string orderguid = "";
                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "PATIENTID")
                    {
                        patientid = paramMap[key] as string;
                    }
                    if (key.ToUpper() == "ORDERGUID")
                    {
                        orderguid = paramMap[key] as string;
                    }
                }
                patientid = patientid == null ? "" : patientid;
                orderguid = orderguid == null ? "" : orderguid;
                #endregion


                #region Execute Query
                using (RisDAL dal = new RisDAL())
                {
                    DataTable dt = new DataTable();
                    string sp = @"SP_GetPatientExamNo";
                    dal.Parameters.AddVarChar("@PatientID", patientid, patientid.Length * 2);
                    dal.Parameters.AddVarChar("@OrderGuid", orderguid, orderguid.Length * 2);
                    object obj = dal.ExecuteScalarSP(sp);
                    if (obj != null || obj != DBNull.Value)
                    {
                        return Convert.ToInt32(obj);
                    }
                }


                #endregion
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GeExamNumberDAO_MSSQL, MSG=" + ex.Message + ", SQL=" + sql,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return 1;//default is 1
            }

            return 1;//default is 1
        }
    }

    /// <summary>
    /// for Oracle
    /// </summary>
    internal class GeExamNumberDAO_ORACLE : IReportDAO
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
