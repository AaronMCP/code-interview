using System;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DAutoIncrementingAttribute 的摘要说明。
	/// </summary>
	[AttributeUsage( AttributeTargets.Property )]
	public class DAutoIncrementingAttribute : System.Attribute
	{
		private bool _isAutoNumber;
		public bool IsAutoNumber
		{
			get{ return _isAutoNumber; }
		}
		
		
		public DAutoIncrementingAttribute()
		{
			_isAutoNumber = true;
		}
		public DAutoIncrementingAttribute( bool isAutoNumber )
		{
			_isAutoNumber = isAutoNumber;
		}
	}
}
