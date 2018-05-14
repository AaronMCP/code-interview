using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Xml
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class XNodeAttribute : System.Attribute
    {
        private bool _visibleInSerialization;
        public bool VisibleInSerialization
        {
            get { return _visibleInSerialization; }
        }

        public XNodeAttribute()
        {
            _visibleInSerialization = true;
        }
        public XNodeAttribute(bool visibleInSerialization)
        {
            _visibleInSerialization = visibleInSerialization;
        }
    }
}
