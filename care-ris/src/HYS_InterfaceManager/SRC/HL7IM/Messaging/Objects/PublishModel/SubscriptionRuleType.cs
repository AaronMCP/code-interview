using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    [Obsolete("Do not use this enum, please use HYS.IM.Messaging.Objects.RoutingModel.RoutingRuleType instead.", true)]
    public enum SubscriptionRuleType
    {
        MessageType,
        ContentBased,
    }
}
