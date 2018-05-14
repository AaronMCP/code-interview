using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;


namespace Server.ReportDAO
{


    public class UpdateLeaveSoundDAO
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

    internal class UpdateLeaveSoundDAO_ABSTRACT : IReportDAO
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

    internal class UpdateLeaveSoundDAO_SYBASE : IReportDAO
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

    internal class UpdateLeaveSoundDAO_MSSQL : IReportDAO
    {


        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in UpdateLeaveSoundDAO!"));
                }
                


                try
                {
                    using (RisDAL oKodak = new RisDAL())
                    {
                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(oKodak.ConnectionString))
                        {

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                            conn.Open();
                            cmd.CommandTimeout = 0;
                            cmd.Connection = conn;
                            string strSoundGuid = paramMap["SoundGuid"] as string;




                            DataSet ds = new DataSet();

                            try
                            {
                                string strSQL = string.Format("update tLeaveSound set status=1 where SoundGuid='{0}'", strSoundGuid);
                                DataTable dtLeaveSound = oKodak.ExecuteQuery(strSQL);
                                ds.Tables.Add(dtLeaveSound);

                            }
                            catch (Exception ex)
                            {


                                System.Diagnostics.Debug.Assert(false, ex.Message);

                                ServerPubFun.RISLog_Error(0, "UpdateLeaveSoundDAO=" + ex.Message,
                                    (new System.Diagnostics.StackFrame()).GetFileName(),
                                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                                throw (ex);
                            }

                        }
                    }



                }
                catch (Exception ex)
                {
                    //dal.RollbackTransaction();

                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "UpdateLeaveSoundDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                    throw (ex);
                }



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "UpdateLeaveSoundDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return false;
            }

            return true;
        }
    }

    internal class UpdateLeaveSoundDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            return null;
        }
    }
}
