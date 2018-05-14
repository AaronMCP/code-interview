using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections;
using System.Reflection;
using System.Drawing;
using System.Text;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// 
	/// </summary>
	public class DObject
	{
        public DObject()
        {
        }

        private DObjectManager _dataManager;
        public DObjectManager GetDataManager()
        {
            return _dataManager;
        }
        internal void SetDataManager(DObjectManager dataManager)
        {
            _dataManager = dataManager;
        }

		private PropertyInfo[] _plist;
		private PropertyInfo[] PropertyList
		{
			get
			{
				if( _plist == null ) _plist = this.GetType().GetProperties();
				return _plist;
			}
		}
		private PropertyInfo GetPropertyName( string name )
		{
			PropertyInfo[] plist = PropertyList;
			foreach( PropertyInfo p in plist ) if( p.Name == name ) return p;
			return null;
		}

		private PropertyInfo MainKey
		{
			get
			{
				foreach( PropertyInfo p in PropertyList )
				{
					if( DObjectHelper.IsMainKey( p ) ) return p;
				}
				return null;
			}
		}
		
		
		private void SetProperty( PropertyInfo p, object val )
		{
			if( p == null ) return;

			if( val == null )
			{
				this.GetType().InvokeMember( p.Name, 
					BindingFlags.DeclaredOnly | BindingFlags.Public | 
					BindingFlags.Instance | BindingFlags.SetProperty,
					null, this, new object[]{ null } );
			}
			else
			{
				string str = val.ToString();

				if( p.PropertyType == typeof( string ) )
				{
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ str } );
					return;
				}

				if( p.PropertyType == typeof( DateTime ) )
				{
					DateTime v = DateTime.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( decimal ) )
				{
					decimal v = decimal.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( int ) )
				{
					int v = int.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( long ) )
				{
					long v = long.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( float ) )
				{
					float v = float.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( double ) )
				{
					double v = double.Parse( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( Color ) )
				{
					ColorConverter cc = new ColorConverter();
					Color v = (Color) cc.ConvertFromInvariantString( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( Font ) )
				{
					FontConverter cc = new FontConverter();
					Font v = (Font) cc.ConvertFromInvariantString( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( Point ) )
				{
					PointConverter cc = new PointConverter();
					Point v = (Point) cc.ConvertFromInvariantString( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( Size ) )
				{
					SizeConverter cc = new SizeConverter();
					Size v = (Size) cc.ConvertFromInvariantString( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}

				if( p.PropertyType == typeof( Rectangle ) )
				{
					RectangleConverter cc = new RectangleConverter();
					Rectangle v = (Rectangle) cc.ConvertFromInvariantString( str );
					this.GetType().InvokeMember( p.Name, 
						BindingFlags.DeclaredOnly | BindingFlags.Public | 
						BindingFlags.Instance | BindingFlags.SetProperty,
						null, this, new object[]{ v } );
					return;
				}
			}
		}
		
		private string GetProperty( PropertyInfo p )
		{
			if( p == null ) return "";

			object o = this.GetType().InvokeMember( p.Name, 
				BindingFlags.DeclaredOnly |	BindingFlags.Public | 
				BindingFlags.Instance | BindingFlags.GetProperty,
				null, this, new object[]{} );

			if( o == null ) return "";

			if( p.PropertyType == typeof( string ) )
			{
				return "'" + o.ToString() + "'";
			}

			if( p.PropertyType == typeof( DateTime ) )
			{
				return "#" + o.ToString() + "#";
			}

			if( p.PropertyType == typeof( int ) ||
				p.PropertyType == typeof( long ) ||
				p.PropertyType == typeof( float ) ||
				p.PropertyType == typeof( double ) ||
				p.PropertyType == typeof( decimal ) )
			{
				return o.ToString();
			}

//			if( p.PropertyType == typeof( Color ) ||
//				p.PropertyType == typeof( Font ) ||
//				p.PropertyType == typeof( Point ) ||
//				p.PropertyType == typeof( Size ) ||
//				p.PropertyType == typeof( Rectangle ) )
//			{
//				return "'" + o.ToString() + "'";
//			}

			return "'" + o.ToString() + "'";
		}

		
		internal bool LoadData( DataRow dbData )
		{
			if( dbData == null ) return false;
			
			PropertyInfo[] plist = PropertyList;
			if( plist == null ) return false;

			foreach( PropertyInfo p in plist )
			{
				string fname = DObjectHelper.GetFieldName( p );
				SetProperty( p, dbData[fname] );
			}

			return true;
		}
		internal bool LoadData( DbDataRecord dbData )
		{
			if( dbData == null ) return false;
			
			PropertyInfo[] plist = PropertyList;
			if( plist == null ) return false;

			foreach( PropertyInfo p in plist )
			{
				string fname = DObjectHelper.GetFieldName( p );
				SetProperty( p, dbData[fname] );
			}

			return true;
		}
		internal string GetCreateTableSql( string tableName )
		{
			PropertyInfo[] plist = PropertyList;
			if( plist == null ) return "";

			StringBuilder sb = new StringBuilder();
			sb.Append( "CREATE TABLE " );
			sb.Append( tableName + " (" );

			foreach( PropertyInfo p in plist )
			{
				string finfo = DObjectHelper.GetFileInfor( p );
				sb.Append( finfo + "," );
			}

			string str = sb.ToString().TrimEnd(',');
			str = str + ")";
			return str;
		}
		internal string GetSelectAllSql( string tableName )
		{
			PropertyInfo[] plist = PropertyList;
			if( plist == null ) return "";

			StringBuilder sb = new StringBuilder();
			sb.Append( "SELECT " );

			foreach( PropertyInfo p in plist )
			{
				string fname = DObjectHelper.GetFieldName( p );
				sb.Append( fname + "," );
			}

			string str = sb.ToString().TrimEnd(',');
			str = str + " FROM " + tableName;
			return str;
		}
		internal string GetSelectSql( string tableName )
		{
			StringBuilder sb = new StringBuilder();
			sb.Append( " WHERE " );
			
//			foreach( PropertyInfo p in PropertyList )
//			{
//				string str = GetProperty(p);
//				if( str != null && str.Length > 0 )
//					sb.Append( p.Name + "=" + GetProperty(p) + "," );
//			}

			PropertyInfo key = MainKey;
			if( key == null ) return "";
			string fname = DObjectHelper.GetFieldName( key );
			sb.Append( fname + "=" + GetProperty(key) );

			string sql = sb.ToString();
			sql = sql.TrimEnd( ',' ).Replace( ",", " AND " );
			sql = GetSelectAllSql( tableName ) + sql;
			
			return sql;
		}

		internal string GetInsertSql( string tableName )
		{
			PropertyInfo[] plist = DObjectHelper.GetInsertablePropertyList( PropertyList );
			if( plist == null ) return "";

			StringBuilder sb = new StringBuilder();
			sb.Append( "INSERT INTO " );
			sb.Append( tableName );
			sb.Append( " (");

			foreach( PropertyInfo p in plist )
			{
				string fname = DObjectHelper.GetFieldName( p );
				sb.Append( fname + "," );
			}

			string str = sb.ToString().TrimEnd(',');
			str = str + ") VALUES (";

			sb = new StringBuilder();
			sb.Append( str );

			foreach( PropertyInfo p in plist )
			{
				sb.Append( GetProperty( p ) + "," );
			}

			str = sb.ToString().TrimEnd(',');
			str = str + ")";

			return str;
		}
		internal string GetDeleteSql( string tableName )
		{
			PropertyInfo key = MainKey;
			if( key == null ) return "";
			string val = GetProperty( key );
			string fname = DObjectHelper.GetFieldName( key );
			string sql = "DELETE FROM " + tableName + " WHERE " + fname + "=" + val;
			return sql;
		}
		internal string GetUpdateSql( string tableName )
		{
			PropertyInfo[] plist = DObjectHelper.GetInsertablePropertyList( PropertyList );
			if( plist == null ) return "";
			
			PropertyInfo key = MainKey;
			if( key == null ) return "";
			string val = GetProperty( key );
			
			StringBuilder sb = new StringBuilder();
			sb.Append( "UPDATE " );
			sb.Append( tableName );
			sb.Append( " SET " );

			foreach( PropertyInfo p in plist )
			{
				string fname = DObjectHelper.GetFieldName( p );
				sb.Append( fname + "=" + GetProperty(p) + "," );
			}

			string str = sb.ToString();
			string kname = DObjectHelper.GetFieldName( key );
			string ostr = str.TrimEnd( ',' ) + " WHERE " + kname + "=" + val;

			return ostr;
		}
		internal string GetMaxAutoIDSql( string tableName )
		{
			PropertyInfo key = MainKey;
			if( key == null || ! DObjectHelper.IsAutoIncrementing( key ) ) return "";

            string kname = DObjectHelper.GetFieldName(key);
            string sql = "SELECT MAX(" + kname + ") FROM " + tableName;
			return sql;
		}
	}
}

