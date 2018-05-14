using System;

namespace HYS.Common.Xml
{
	/// <summary>
	/// ���ַ������͵����Ա��ΪXML��CDATA
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
