using System;

namespace HYS.Common.Xml
{
	/// <summary>
	/// 把字符串类型的属性标记为XML的CDATA
	/// </summary>
	[AttributeUsage( AttributeTargets.Property )]
	public class XCDataAttribute : System.Attribute 
	{
		private bool _enableCData = false;
		public bool EnableCData
		{
			get{ return _enableCData; }
		}
		
		
		public XCDataAttribute()
		{
		}
		public XCDataAttribute( bool enableCData )
		{
			_enableCData = enableCData;
		}
	}
}
