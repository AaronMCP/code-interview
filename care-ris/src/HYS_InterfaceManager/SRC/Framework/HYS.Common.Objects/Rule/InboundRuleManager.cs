using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class InboundRuleManager<TC, TR> : RuleManagerBase<TC, TR>
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
        public InboundRuleManager()
        {
            _Rule = new InboundRule<TC, TR>();
            _ruleType = typeof(InboundRule<TC, TR>);
            _FileName = "InboundRule.xml";
        }

        public InboundRule<TC, TR> Rule
        {
            get { return _Rule as InboundRule<TC, TR>; }
            set { _Rule = value; }
        }
    }
}
