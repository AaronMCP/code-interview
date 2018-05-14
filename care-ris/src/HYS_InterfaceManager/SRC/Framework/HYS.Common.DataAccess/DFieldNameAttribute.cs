using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DFieldNameAttribute ��ժҪ˵����
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
