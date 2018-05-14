using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public enum QueryCriteriaOperator
    {
        Equal,
        NotEqual,
        LargerThan,
        EqualLargerThan,
        SmallerThan,
        EqualSmallerThan,
        Like,
        
        // 20100617 
        // to support special MWL query criteria in Hong Kong
        // where "equal+or" solution (the replacement of "in" in former versions) is too complex for the engineers of hospital (not only the engineers of CSH),
        // who would like to configure CS Broker when business rule changed.
        In,
    }
}
