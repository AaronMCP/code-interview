using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIM
    {
        public static XmlElement RIM_ITEM = new XmlElement("ITEM[]", typeof(XIMItem));
    }
}
