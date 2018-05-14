using System;
using System.Reflection;
using System.Collections;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DObjectHelper 的摘要说明。
	/// </summary>
	internal class DObjectHelper
	{
		public static bool IsDObject( Type t )
		{
			if( t == null ) return false;
			
			if( t == typeof( DObject ) )
				return true;
			else
				return IsDObject( t.BaseType );
		}
		public static bool IsNumber( PropertyInfo p )
		{
			return ( p != null &&
				( p.PropertyType == typeof( int ) ||
				p.PropertyType == typeof( long ) ||
				p.PropertyType == typeof( float ) ||
				p.PropertyType == typeof( double ) ||
				p.PropertyType == typeof( decimal ) ) );
		}
		public static bool IsMainKey( PropertyInfo p )
		{
			object[] olist = p.GetCustomAttributes( typeof( DMainKeyAttribute ), false );
			if( olist == null || olist.Length < 1 ) return false;
			
			foreach( object o in olist )
			{
				DMainKeyAttribute attr = o as DMainKeyAttribute;
				if( attr != null ) return attr.IsMainKey;
			}

			return false;
		}
		public static bool IsAutoIncrementing( PropertyInfo p )
		{
			if( ! IsNumber(p) ) return false;

			object[] olist = p.GetCustomAttributes( typeof( DAutoIncrementingAttribute ), false );
			if( olist == null || olist.Length < 1 ) return false;
			
			DAutoIncrementingAttribute attr = olist[0] as DAutoIncrementingAttribute;
			if( attr != null ) return attr.IsAutoNumber;

			return false;
		}
		public static PropertyInfo[] GetInsertablePropertyList( PropertyInfo[] plist )
		{
			if( plist == null ) return null;
			ArrayList list = new ArrayList();
			foreach( PropertyInfo p in plist )
			{
				if( !IsAutoIncrementing( p ) ) list.Add( p );
			}
			return list.ToArray( typeof( PropertyInfo ) ) as PropertyInfo[];
		}
		public static string GetFieldName( PropertyInfo p )
		{
			if( p == null ) return "";

			object[] olist = p.GetCustomAttributes( typeof( DFieldAttribute ), false );
			if( olist == null || olist.Length < 1 ) return p.Name;
			
			DFieldAttribute attr = olist[0] as DFieldAttribute;
			if( attr != null ) return attr.Name;

			return p.Name;
		}
		public static string GetFileInfor( PropertyInfo p )
		{
			if( p == null ) return "";

			string name = "";
			string type = "";
			DFieldAttribute attr = null;

			object[] olist = p.GetCustomAttributes( typeof( DFieldAttribute ), false );
			if( olist != null && olist.Length > 0 ) attr = olist[0] as DFieldAttribute;

			if( attr == null )
			{
				name = p.Name + " ";
				type = "nvarchar(50) NULL";
			}
			else
			{
				name = attr.Name + " ";
				type = attr.Type;
			}

			string infor = name + type;
			return infor;
		}
	}
}
