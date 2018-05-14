using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class OutboundRuleManager<TC, TR> : RuleManagerBase<TC, TR>
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
        public OutboundRuleManager()
        {
            _Rule = new OutboundRule<TC, TR>();
            _ruleType = typeof(OutboundRule<TC, TR>);
            _FileName = "OutboundRule.xml";
        }

        public OutboundRule<TC, TR> Rule
        {
            get { return _Rule as OutboundRule<TC, TR>; }
            set { _Rule = value; }
        }
    }
}
