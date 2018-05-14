using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.ComponentModel;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DataAccessHelper 的摘要说明。
	/// </summary>
	[Obsolete("Please use class Common.DataAccess.DataBase instead.", true)]
    public class DataAccessHelper
	{
		public DataAccessHelper()
		{
		}

        public static bool TestDBConnection(string dbcn)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(dbcn);
                conn.Open();
                conn.Close();

                return true;
            }
            catch (Exception e)
            {
                _lastError = e;
                return false;
            }
        }

		private static Exception _lastError = null;
		private static void NotifyException( string sql, Exception err )
		{
			if( OnError != null ) OnError( ConnectionString, sql, err );
		}
		public static event DataAccessExceptionHanlder OnError;
		public static Exception LastError
		{
			get{ return _lastError; }
		}
		
		private static string _connectionString;
		public static string ConnectionString
		{
			get{ return _connectionString; }
			set{ _connectionString = value; }
		}

		private static OleDbConnection _dbConnection = null;
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void CloseDBConnection()
		{
			if( _dbConnection != null ) _dbConnection.Close();
		}

        [MethodImpl(MethodImplOptions.Synchronized)]
		public static OleDbDataReader DoQuery(string sql)
		{
			try
			{
				if( sql==null || sql.Length<1 ) return null;
				
				OleDbConnection conn = new OleDbConnection(ConnectionString);
				conn.Open();

				OleDbCommand cmd = new OleDbCommand(sql,conn);
				OleDbDataReader dbData = cmd.ExecuteReader();

				_dbConnection = conn;
				return dbData;
			}
			catch( Exception e )
			{
				NotifyException( sql, e );
				return null;
			}
		}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DataSet ExecuteQuery(string sql)
        {
            try
            {
                if (sql == null || sql.Length < 1) return null;

                _dbConnection = new OleDbConnection(ConnectionString);
                OleDbDataAdapter ad = new OleDbDataAdapter(sql, _dbConnection);

                DataSet ds = new DataSet();
                ad.Fill(ds);

                return ds;
            }
            catch (Exception e)
            {
                //throw e;
                NotifyException(sql, e);
                return null;
            }
        }
		
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static DataView GetDataView(string tableName)
		{
			if( tableName == null || tableName.Length < 1 ) return null;
			string sql = "SELECT * FROM " + tableName;
		
			try
			{
				_dbConnection = new OleDbConnection( ConnectionString );
				OleDbDataAdapter ad = new OleDbDataAdapter( sql, _dbConnection );
			
				DataSet ds = new DataSet();
				ad.Fill( ds, tableName );
                //ds.WriteXml("dataset_demo.xml",XmlWriteMode.IgnoreSchema);

				return ds.Tables[tableName].DefaultView;
			}
			catch( Exception e )
			{
				//throw e;
				NotifyException( sql, e );
				return null;
			}
		}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string[] GetTableNames()
        {
            try
            {
                List<string> list = new List<string>();

                OleDbConnection conn = new OleDbConnection(ConnectionString);
                conn.Open();

                string[] restrictions = new string[4];
                restrictions[3] = "TABLE";

                DataTable table = conn.GetSchema("Tables", restrictions);
                
                if (table != null)
                {
                    foreach (DataRow r in table.Rows)
                    {
                        string str = r["TABLE_NAME"] as string;
                        if (str != null) list.Add(str);
                    }
                }

                _dbConnection = conn;
                return list.ToArray();
                //return table;
            }
            catch (Exception e)
            {
                NotifyException("GetSchema(\"Tables\")", e);
                return null;
            }
        }
	}

	public delegate void DataAccessExceptionHanlder( string cnn, string sql, Exception err );
}
