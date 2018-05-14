using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetRePrintDAO
    {
        public Object Execute(object param)
        {

              
                try
                {
                    using (RisDAL oKodak = new RisDAL())
                    {
                        string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", param.ToString());
                        string strReportGuid = CommonGlobalSettings.Utilities.GetParameter("ReportGuid", param.ToString());
                        string strSQL = "";
                        if (!string.IsNullOrWhiteSpace(strOrderGuid))
                        {
                            strSQL = string.Format("select IsFilmSent from tregorder where OrderGuid='{0}'", strOrderGuid);
                        }
                        else
                        {
                            strSQL = string.Format("select IsPrint from treport where ReportGuid='{0}'", strReportGuid);
                        }

                        Object obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null || Convert.ToInt32(obj) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }


                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "GetRePrintDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }
               

                return false;
            //KodakDAL oKodak = new KodakDAL();
            //string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

            //Type type = Type.GetType(clsType);
            //IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            //return iRptDAO.Execute(param);
        }

        internal class GetRePrintDAO_ABSTRACT : IReportDAO
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

        internal class GetRePrintDAO_SYBASE : IReportDAO
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

        internal class GetRePrintDAO_MSSQL : IReportDAO
        {
            public Object Execute(object param)
            {
                
                try
                {
                    using (RisDAL oKodak = new RisDAL())
                    {
                        string strOrderGuid = CommonGlobalSettings.Utilities.GetParameter("OrderGuid", param.ToString());
                        string strReportGuid = CommonGlobalSettings.Utilities.GetParameter("ReportGuid", param.ToString());
                        string strSQL = "";
                        if (string.IsNullOrWhiteSpace(strOrderGuid))
                        {
                            strSQL = string.Format("select IsFilmSent from tregorder where OrderGuid='{0}'", strOrderGuid);
                        }
                        else
                        {
                            strSQL = string.Format("select IsPrint from treport where ReportGuid='{0}'", strReportGuid);
                        }

                        Object obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null || Convert.ToInt32(obj) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "GetRePrintDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }
                

                return false
;
            }
        }

    }
}
