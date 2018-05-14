using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;
using CommonGlobalSettings.Utility;

namespace Server.ReportDAO
{
 

    public class SaveLeaveSoundDAO
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

    internal class SaveLeaveSoundDAO_ABSTRACT : IReportDAO
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

    internal class SaveLeaveSoundDAO_SYBASE : IReportDAO
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

    internal class SaveLeaveSoundDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in SaveLeaveSoundDAO!"));
                }



                using (RisDAL dal = new RisDAL())
                {



                    DataSet ds = paramMap["DataSet"] as DataSet;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string strSoundGuid = Guid.NewGuid().ToString();
                        string strReportGuid = Convert.ToString(dr["ReportGuid"]);
                        string strPath = Convert.ToString(dr["Path"]);
                        int nStatus = 0;
                        string strLeaveTime = GlobalCommon.ToLongDateTime(DateTime.Now);
                        string strOwner = Convert.ToString(dr["Owner"]);
                        string strDomain = Convert.ToString(dr["Domain"]);

                        string strSQL = string.Format("insert into tLeaveSound(SoundGuid,ReportGuid,Path,Status,LeaveTime,Owner,Domain) values('{0}','{1}','{2}',{3},'{4}','{5}','{6}')",
                            strSoundGuid, strReportGuid, strPath, nStatus, strLeaveTime, strOwner, strDomain);
                        dal.ExecuteNonQuery(strSQL);
                        strSQL = string.Format("update tReport set IsLeaveSound=1 where ReportGuid='{0}'", strReportGuid);
                        dal.ExecuteNonQuery(strSQL);



                    }

                }
               



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "SaveLeaveSoundDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return false;
            }
          
            return true;
        }
    }

    internal class SaveLeaveSoundDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            return null;
        }
    }
}
