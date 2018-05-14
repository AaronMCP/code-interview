using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DFieldAttribute ��ժҪ˵����
	/// </summary>
	[AttributeUsage( AttributeTargets.Property )]
	public class DFieldAttribute : System.Attribute
	{
		private string _name;
		public string Name
		{
			get{ return _name; }
		}
		private string _type;
		public string Type
		{
			get{ return _type; }
		}

		
		public DFieldAttribute( string name )
		{
			_name = name;
		}
		public DFieldAttribute( string name, string type )
		{
			_name = name;
			_type = type;
		}
	}
}
