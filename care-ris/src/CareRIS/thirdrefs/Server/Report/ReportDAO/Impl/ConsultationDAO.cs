using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data.SqlClient;

namespace Server.ReportDAO.Impl
{
    public class ConsultationDAO_ABSTRACT : IConsultation
    {
        public bool save(ReportCommon.Consultation consultation)
        {
            

            try
            {
                string sql = " if exists(select 1 from tConsultation where cstGuid=@cstGuid)"
                    + " update tConsultation set cstUserGuid=@cstUserGuid,cstSite=@cstSite,cstOrderGuid=@cstOrderGuid,cstStatus=@cstStatus"
                    + " where cstGuid=@cstGuid"
                    + " else "
                    + " insert tConsultation(cstGuid,cstUserGuid,cstSite,cstOrderGuid,cstStatus,cstApplyTime,Domain) "
                    + " VALUES(@cstGuid,@cstUserGuid,@cstSite,@cstOrderGuid,@cstStatus,getdate(),(select top 1 value from tSystemProfile where Name='domain'))"
                    ;
                using (RisDAL oKodak = new RisDAL())
                {
                    using (SqlConnection conn = new SqlConnection(oKodak.ConnectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = sql;
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@cstGuid", consultation.cstGuid);
                        cmd.Parameters.AddWithValue("@cstUserGuid", consultation.cstUserGuid);
                        cmd.Parameters.AddWithValue("@cstSite", consultation.cstSite);
                        cmd.Parameters.AddWithValue("@cstOrderGuid", consultation.cstOrderGuid);
                        cmd.Parameters.AddWithValue("@cstStatus", consultation.cstStatus);

                        cmd.ExecuteNonQuery();

                    }
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            

            return false;
        }
    }

    public class ConsultationDAO_MSSQL : ConsultationDAO_ABSTRACT
    {

    }

    public class ConsultationDAO_ORACLE : ConsultationDAO_ABSTRACT
    {

    }

    public class ConsultationDAO_SYBASE : ConsultationDAO_ABSTRACT
    {

    }
}
