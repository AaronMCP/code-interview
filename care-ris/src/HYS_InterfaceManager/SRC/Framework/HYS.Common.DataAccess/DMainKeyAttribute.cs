using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// MainKeyAttribute ��ժҪ˵����
	/// </summary>
	[AttributeUsage( AttributeTargets.Property )]
	public class DMainKeyAttribute : System.Attribute
	{
		private bool _isMainKey;
		public bool IsMainKey
		{
			get{ return _isMainKey; }
		}
		
		
		public DMainKeyAttribute()
		{
			_isMainKey = true;
		}
		public DMainKeyAttribute( bool isMainKey )
		{
			_isMainKey = isMainKey;
		}
	}
}
