using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;
using HYS.Common.DataAccess;

namespace HYS.Common.DataAccess
{
    public class AdapterDataBase : DataBase
    {
        public AdapterDataBase(string connstr)
            : base(connstr)
        {

        }
        public OleDbCommand GetCommand()
        {
            return new OleDbCommand();
        }

        public OleDbParameter GetParameter()
        {
            return new OleDbParameter();
        }

        public bool ExecCommand(IDbCommand cmd)
        {
            if (cmd is OleDbCommand)
            {
                //OleDbConnection conn = new OleDbConnection("Provider=SQLOLEDB; Server=CN-SH-D0406210; Database=GWDataDB; UID=10095177; Trusted_Connection=Yes;");

                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }

                
                
            }
            return false;
        }

        public bool ExecCommand(IDbCommand cmd, DataSet dsRet)
        {
            if (cmd is OleDbCommand)
            {

                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    OleDbDataAdapter da = new OleDbDataAdapter((OleDbCommand)cmd);
                    conn.Open();
                    cmd.Connection = conn;
                    da.Fill(dsRet);
                    conn.Close();
                    return true;
                }
                
            }
            return false;
        }
    }
}
