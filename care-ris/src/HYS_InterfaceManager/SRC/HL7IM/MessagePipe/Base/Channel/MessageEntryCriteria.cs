using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public class MessageEntryCriteria : XObject
    {
        [XCData(true)]
        public string XPath { get; set; }
        [XCData(true)]
        public string XPathPrefixDefinition { get; set; }
        [XCData(true)]
        public string RegularExpression { get; set; }
    }
}
