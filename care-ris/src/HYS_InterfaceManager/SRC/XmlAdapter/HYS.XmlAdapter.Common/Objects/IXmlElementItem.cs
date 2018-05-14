using System;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public interface IXmlElementItem
    {
        bool Enable { get; set; }
        XmlElement Element { get; set; }
        GWDataDBField GWDataDBField { get; set; }
        TranslatingRule Translating { get; set; }
        bool RedundancyFlag { get; }
        IXmlElementItem Clone();
    }
}
