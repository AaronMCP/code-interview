using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DFieldNameAttribute 的摘要说明。
	/// </summary>
	[AttributeUsage( AttributeTargets.Property )]
	public class DFieldNameAttribute : System.Attribute
	{
		private string _fieldName;
		public string FieldName
		{
			get{ return _fieldName; }
		}
		
		public DFieldNameAttribute( string fieldName )
		{
			_fieldName = fieldName;
		}
	}
}
