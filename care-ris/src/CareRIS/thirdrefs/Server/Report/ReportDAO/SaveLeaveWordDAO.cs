using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;
using CommonGlobalSettings.Utility;
using CommonGlobalSettings;

namespace Server.ReportDAO
{
 

    public class SaveLeaveWordDAO
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

    internal class SaveLeaveWordDAO_ABSTRACT : IReportDAO
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

    internal class SaveLeaveWordDAO_SYBASE : IReportDAO
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

    internal class SaveLeaveWordDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            
            try
            {
                BaseDataSetModel bdsm = param as BaseDataSetModel;

                if (bdsm == null || bdsm.DataSetParameter == null)
                {
                    throw (new Exception("No parameter in SaveLeaveWordDAO!"));
                }



                //KodakDAL dal = new KodakDAL();
                {

                    DataSet ds = bdsm.DataSetParameter;
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string strReportGuid = Convert.ToString(ds.Tables[0].Rows[0]["ReportGuid"]);
                        string strLeaveWord = Convert.ToString(ds.Tables[0].Rows[0]["LeaveWord"]);
                        string strSQL = string.Format("update tReport set isLeaveWord=1, Comments= @Comments where ReportGuid='{0}'", strReportGuid);
                        using (var dal = new RisDAL())
                        {
                            dal.Parameters.Add("@Comments", strLeaveWord);
                            dal.ExecuteNonQuery(strSQL);
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "SaveLeaveWordDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return false;
            }
           

            return true;
        }
    }

    internal class SaveLeaveWordDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            return null;
        }
    }
}
