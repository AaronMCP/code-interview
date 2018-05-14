using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.RoutingModel
{
    public interface IRoutingRule
    {
        RoutingRuleType Type { get; }
        ContentCriteria ContentCriteria { get; }
        XCollection<MessageType> MessageTypeList { get; }
    }
}
