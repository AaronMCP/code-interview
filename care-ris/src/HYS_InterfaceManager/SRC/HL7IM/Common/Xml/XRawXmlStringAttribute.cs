using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.HL7IM.Common.Xml
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class XRawXmlStringAttribute : System.Attribute
    {
        private bool _enableRawXmlString = false;
		public bool EnableRawXmlString
		{
			get{ return _enableRawXmlString; }
		}
		
		public XRawXmlStringAttribute()
		{
		}
        public XRawXmlStringAttribute(bool enableRawXmlString)
		{
			_enableRawXmlString = enableRawXmlString;
		}
    }
}
