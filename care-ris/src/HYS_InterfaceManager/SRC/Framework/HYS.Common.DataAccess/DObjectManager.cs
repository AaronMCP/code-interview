using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Windows.Forms;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// 
	/// </summary>
	public class DObjectManager
	{
		protected Type _objectType;
		public Type ObjectType
		{
			get{ return _objectType; }
		}
		protected string _tableName;
		public string TableName
		{
			get{ return _tableName; }
		}
        private DataBase _database;
        public DataBase DataBase
        {
            get { return _database; }
        }

		private DObjectManager( string tableName, Type objectType )
		{
			_tableName = tableName;
			_objectType = objectType;

			if( _tableName == null || _tableName.Length < 1 || 
				_objectType == null || ! DObjectHelper.IsDObject( _objectType ) )
				throw new ArgumentNullException("DObjectManager构造函数的参数不能为空，而且传入的objectType参数必须是DObject类型或DObject的派生类型。");
		}
        public DObjectManager(DataBase db, string tableName, Type objectType) : this(tableName,objectType)
        {
            _database = db;
        }

		private static Exception _lastError = null;
		internal void NotifyException( object o, Exception err )
		{
			_lastError = err;
			if( OnError != null ) OnError( o, err );
		}
		public static event DataManagerExceptionHanlder OnError;
		public static Exception LastError
		{
			get{ return _lastError; }
		}
		

		public bool Create()
		{
			DObject obj = ObjectType.Assembly.CreateInstance( ObjectType.ToString() ) as DObject;
			if( obj == null ) return false;

			string sql = obj.GetCreateTableSql( TableName );

			OleDbDataReader dbData = _database.DoQuery( sql );
			bool res = dbData != null;
			_database.CloseDBConnection();

			return res;
		}
		public bool Drop()
		{
			string sql = "DROP TABLE " + TableName;

			OleDbDataReader dbData = _database.DoQuery( sql );
			bool res = dbData != null;
			_database.CloseDBConnection();

			return res;
		}


		public int GetMaxID()
		{
			int id = -1;

			DObject obj = ObjectType.Assembly.CreateInstance( ObjectType.ToString() ) as DObject;
			if( obj == null ) return id;

			string sql = obj.GetMaxAutoIDSql( TableName );

			OleDbDataReader dbData = _database.DoQuery( sql );
			if( dbData != null )
			{
				foreach( DbDataRecord record in dbData )
				{
					if( record[0] == DBNull.Value )
					{
                        id = 0; // 1;
					}
					else
					{
                        id = int.Parse(record[0].ToString());   // +1;
					}
					break;
				}
			}
			_database.CloseDBConnection();

			return id;
		}
		public bool Insert( DObject obj )
		{
			if( obj == null ) return false;
			string sql = obj.GetInsertSql( TableName );

//			OleDbDataReader dbData = DataAccessHelper.DoQuery( sql );
//			bool res = dbData != null;
//			DataAccessHelper.CloseDBConnection();
//			return res;

			return Execute( sql );
		}

		public bool Delete( DObject obj )
		{
			if( obj == null ) return false;
			string sql = obj.GetDeleteSql( TableName );

//			OleDbDataReader dbData = DataAccessHelper.DoQuery( sql );
//			bool res = dbData != null;
//			DataAccessHelper.CloseDBConnection();
//			return res;

			return Execute( sql );
		}

		public bool Update( DObject obj )
		{
			if( obj == null ) return false;
			string sql = obj.GetUpdateSql( TableName );

//			OleDbDataReader dbData = DataAccessHelper.DoQuery( sql );
//			bool res = dbData != null;
//			DataAccessHelper.CloseDBConnection();
//			return res;

			return Execute( sql );
		}

		public DObjectCollection SelectAll()
		{
			return Select( null, null );
		}

		public DObjectCollection Select( DOrder order )
		{
			return Select( null, order );
		}
		public DObjectCollection Select( DObject criteria )
		{
			return Select( criteria, null );
		}
		
		public DObjectCollection Select( DObject criteria, DOrder order )
		{
			string sql = "";
			if( criteria == null )
			{
				sql = "SELECT * FROM " + TableName;
			}
			else
			{
				sql = criteria.GetSelectSql( TableName );
			}

			if( order != null )
			{
				sql += order.ToSqlString();
			}

			return Select( sql );
		}

        public DObjectCollection Select(string sql)
		{
			DObjectCollection list = null;

			OleDbDataReader dbData = _database.DoQuery( sql );
			if( dbData != null )
			{
				list = new DObjectCollection();
				foreach( DbDataRecord record in dbData )
				{
					DObject obj = ObjectType.Assembly.CreateInstance( ObjectType.ToString() ) as DObject;
					if( obj == null || obj.LoadData( record ) ) list.Add( obj );
                    obj.SetDataManager(this);
				}
			}
			_database.CloseDBConnection();

			return list;
		}

		protected bool Execute( string sql )
		{
			OleDbDataReader dbData = _database.DoQuery( sql );
			bool res = dbData != null;
			_database.CloseDBConnection();
			return res;
		}


		private DataGrid _dtGrid;
		public void AttachDataGrid( DataGrid dtGrid )
		{
			_dtGrid = dtGrid;
		}
		public DObject GetSelectedObject()
		{
			if( _dtGrid == null ) return null;
			int index = _dtGrid.CurrentRowIndex;
			DataView view = _dtGrid.DataSource as DataView;
			if( view == null ) return null;
			DataRow row = view.Table.Rows[index];

			DObject obj = ObjectType.Assembly.CreateInstance( ObjectType.ToString() ) as DObject;
			if( obj == null || obj.LoadData( row ) ) return obj;
			return null;
		}
		public bool RefreshDataGrid()
		{
			if( _dtGrid == null ) return false;
			_dtGrid.DataSource = _database.GetDataView( TableName );
			_database.CloseDBConnection();
            return _dtGrid.DataSource != null;
		}
	}

	public delegate void DataManagerExceptionHanlder( object o, Exception err );
}
