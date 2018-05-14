using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace HYS.Adapter.Monitor.Utility
{
    public class DataAccess
    {
        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool TestDBConnection()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Program.Log.Write(e);
                return false;
            }
        }

        private OleDbConnection _dbConnection = null;
        public void CloseDBConnection()
        {
            if (_dbConnection != null) _dbConnection.Close();
        }

        public DataSet ExecuteQuery(string sql)
        {
            try
            {
                if (sql == null || sql.Length < 1) return null;

                _dbConnection = new OleDbConnection(ConnectionString);
                OleDbDataAdapter ad = new OleDbDataAdapter(sql, _dbConnection);

                DataSet ds = new DataSet();
                ad.Fill(ds);
                //If ds.Tables.Count = 0, how to do?
                return ds;
            }
            catch (Exception e)
            {
                Program.Log.Write(e);
                return null;
            }
        }

        public bool ExecuteNoneQuery(string sql) {
            _dbConnection = new OleDbConnection(ConnectionString);
            OleDbTransaction transaction = null;
            OleDbCommand cmd = null;
            
            try
            {
                if (sql == null || sql.Length < 1) return false;

                _dbConnection.Open();

                transaction = _dbConnection.BeginTransaction();

                cmd = new OleDbCommand(sql, _dbConnection, transaction);
                cmd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                Program.Log.Write(e);
                transaction.Rollback();
                return false;
            }
        }
    }
}
