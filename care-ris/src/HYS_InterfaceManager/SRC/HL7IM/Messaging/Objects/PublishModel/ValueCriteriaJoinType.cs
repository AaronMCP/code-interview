using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    [Obsolete("Do not use this enum for message routing between entities, use HYS.IM.Messaging.Objects.RoutingModel.ContentCriteria instead", false)]
    public enum ValueCriteriaJoinType
    {
        Unknown,
        And,
        Or,
    }
}
