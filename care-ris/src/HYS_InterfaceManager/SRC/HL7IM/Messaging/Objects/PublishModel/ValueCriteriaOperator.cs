using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    [Obsolete("Do not use this enum for message routing between entities, use HYS.IM.Messaging.Objects.RoutingModel.ContentCriteria instead", false)]
    public enum ValueCriteriaOperator
    {
        Unknown,
        String_EqualPattern,
        String_NotEqualPattern,
        String_LargerThanPattern,
        String_EqualLargerThanPattern,
        String_SmallerThanPattern,
        String_EqualSmallerThanPattern,
        Number_EqualPattern,
        Number_NotEqualPattern,
        Number_LargerThanPattern,
        Number_EqualLargerThanPattern,
        Number_SmallerThanPattern,
        Number_EqualSmallerThanPattern,
        RegularExpression_MatchPattern,
    }
}
