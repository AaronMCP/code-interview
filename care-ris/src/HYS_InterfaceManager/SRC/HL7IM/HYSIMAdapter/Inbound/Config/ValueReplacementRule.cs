using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Mapping.Replacing;
using HYS.Common.Xml;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Config
{
    public class ValueReplacementRule : ReplacementRule
    {
        [XCData(true)]
        public string FieldName { get; set; }
        //[XCData(true)]
        //public string MatchExpression { get; set; }
        //[XCData(true)]
        //public string ReplaceExpression { get; set; }
    }
}
