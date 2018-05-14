using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DOrder 的摘要说明。
	/// </summary>
	public class DOrder
	{
		public readonly string Field;
		public readonly DOrderType Type = DOrderType.Ascending;
		public DOrder( string field, DOrderType type )
		{
			Field = field;
			Type = type;
		}
		public string ToSqlString()
		{
			if( Field == null || Field.Length < 1 ) return "";
			string sql =  " ORDER BY " + Field;
			
			switch( Type )
			{
				case DOrderType.Ascending : sql += " ASC"; break;
				case DOrderType.Descending : sql += " DESC"; break;
			}

			return sql;
		}
	}

	public enum DOrderType
	{
		Ascending,
		Descending,
	}
}
