using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;

    namespace Server.ReportDAO
    {


        public class UpdateReportIsModifiedDAO
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

        internal class UpdateReportIsModifiedDAO_ABSTRACT : IReportDAO
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

        internal class UpdateReportIsModifiedDAO_SYBASE : IReportDAO
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

        internal class UpdateReportIsModifiedDAO_MSSQL : IReportDAO
        {
            public object Execute(object param)
            {
                bool isModified = false;
                try
                {
                    Dictionary<string, object> paramMap = param as Dictionary<string, object>;
                    if (paramMap == null || paramMap.Count < 1)
                    {
                        throw (new Exception("No parameter in UpdateReportIsModifiedDAO!"));
                    }

                   
                    string reportGuid = paramMap["reportguid"] as string;
                    using (RisDAL dal = new RisDAL())
                    {
                        DataSet ds = new DataSet();

                        string strLatestSumittedSQL = string.Format("select top 1 reporttext from treportlist where reportguid='{0}' and status = 110 order by operationtime desc", reportGuid);
                        string strLatestApprovedSQL = string.Format("select top 1 reporttext from treportlist where reportguid='{0}' and status = 120 order by operationtime desc", reportGuid);
                        string strUpdateIsModified = "update treport set ismodified = {0} where reportguid ='{1}'";
                        object lastSumittedRptText = Convert.ToString(dal.ExecuteScalar(strLatestSumittedSQL));
                        if (string.IsNullOrEmpty(Convert.ToString(lastSumittedRptText)))//no submitted record
                        {
                            dal.ExecuteNonQuery(string.Format(strUpdateIsModified, 0, reportGuid));
                            return isModified;
                        }
                        else
                        {
                            object lastApprovedRptText = Convert.ToString(dal.ExecuteScalar(strLatestApprovedSQL));
                            if (string.IsNullOrEmpty(Convert.ToString(lastApprovedRptText)))//no approved record
                            {
                                dal.ExecuteNonQuery(string.Format(strUpdateIsModified, 0, reportGuid));
                                return isModified;
                            }
                            else if (lastSumittedRptText.ToString().Equals(lastApprovedRptText.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                dal.ExecuteNonQuery(string.Format(strUpdateIsModified, 0, reportGuid));
                                return isModified;
                            }
                            else
                            {
                                dal.ExecuteNonQuery(string.Format(strUpdateIsModified, 1, reportGuid));
                                return isModified = true;//sumbmited reporttext differ with approved one
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "UpdateReportIsModifiedDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return isModified;
                }
            }
        }

        internal class UpdateReportIsModifiedDAO_ORACLE : IReportDAO
        {
            public object Execute(object param)
            {
                return null;
            }
        }
    }

