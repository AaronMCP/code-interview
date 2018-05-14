using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;

namespace Server.ReportDAO
{
   
    public class GetLeaveSoundDAO
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

    internal class GetLeaveSoundDAO_ABSTRACT : IReportDAO
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

    internal class GetLeaveSoundDAO_SYBASE : IReportDAO
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

    internal class GetLeaveSoundDAO_MSSQL : IReportDAO
    {
        

        public object Execute(object param)
        {
            
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetLeaveSoundDAO!"));
                }
                


               

                    string strReportGuid = paramMap["ReportGuid"] as string;


                  
                    DataSet ds = new DataSet();

                   
                        using (RisDAL oKodak = new RisDAL())
                        {
                            string strSQL = string.Format("select A.SoundGuid,A.ReportGuid,A.Path,A.Status,A.LeaveTime,A.Owner,B.LocalName from tLeaveSound A,tUser B where A.Owner=B.UserGuid and ReportGuid='{0}' order  by LeaveTime desc", strReportGuid);
                            DataTable dtLeaveSound = oKodak.ExecuteQuery(strSQL);
                            ds.Tables.Add(dtLeaveSound);
                            return ds;
                        }

                   





            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetLeaveSoundDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return null;
            }

            return null;
        }
    }

    internal class GetLeaveSoundDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            return null;
        }
    }
}
