using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data;

namespace Server.ReportDAO.Impl
{
    public class ReferralDAO_ABSTRACT : IReferral
    {
        public DataSet getMemo(string patientId, string accNo)
        {
            

            try
            {
                string szWhere = "";

                if (!string.IsNullOrWhiteSpace(patientId))
                {
                    szWhere += " AND PatientID=@patientID";
                }

                if (!string.IsNullOrWhiteSpace(accNo))
                {
                    szWhere += " AND (AccNo=@accNo OR ACCNO=@accNo+'^^^'+SourceDomain)";
                }

                if (string.IsNullOrWhiteSpace(szWhere))
                {
                    szWhere = " AND 1=2";
                }

                string sql = " SELECT TOP 200 * FROM [tReferralLog]"
                    + " WHERE referralID IN (SELECT ReferralID FROM [tReferralList] WHERE 1=1 " + szWhere + ")"
                    + " ORDER BY [OperateDt]";
                using (RisDAL oKodak = new RisDAL())
                {
                    using (SqlConnection conn = new SqlConnection(oKodak.ConnectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = sql;
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@patientID", patientId);
                        cmd.Parameters.AddWithValue("@accNo", accNo);

                        ServerPubFun.RISLog_Info(0, "getMemo, " + cmd.CommandText + ", PID=" + patientId + ", ACCNO=" + accNo, "", 0);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);



                        return ds;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
           

            return null;
        }
    }

    public class ReferralDAO_MSSQL : ReferralDAO_ABSTRACT
    {

    }

    public class ReferralDAO_ORACLE : ReferralDAO_ABSTRACT
    {

    }

    public class ReferralDAO_SYBASE : ReferralDAO_ABSTRACT
    {

    }
}
