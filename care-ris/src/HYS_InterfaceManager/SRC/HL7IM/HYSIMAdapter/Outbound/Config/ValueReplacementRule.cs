using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Mapping.Replacing;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Config
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
