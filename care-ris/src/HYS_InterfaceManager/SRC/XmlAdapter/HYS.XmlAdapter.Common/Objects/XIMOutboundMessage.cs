using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMOutboundMessage : XIMMessage
    {
        public XIMOutboundMessage()
        {
            Rule.CheckProcessFlag = true;
        }

        private OutboundRule<QueryCriteriaItem, XIMMappingItem> _rule = new OutboundRule<QueryCriteriaItem, XIMMappingItem>();
        public OutboundRule<QueryCriteriaItem, XIMMappingItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public XIMOutboundMessage Clone()
        {
            XIMOutboundMessage msg = new XIMOutboundMessage();
            msg.HL7EventType = HL7EventType.Clone();
            msg.GWEventType = GWEventType.Clone();
            msg.XSLFileName = XSLFileName;

            //msg.Rule.RuleID = Rule.RuleID;    // do not copy RuleID
            msg.Rule.RuleName = Rule.RuleName;
            msg.Rule.MaxRecordCount = Rule.MaxRecordCount;
            msg.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            msg.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;
            foreach (XIMMappingItem item in Rule.QueryResult.MappingList)
            {
                XIMMappingItem i = item.Clone() as XIMMappingItem;
                msg.Rule.QueryResult.MappingList.Add(i);
            }
            foreach (QueryCriteriaItem item in Rule.QueryCriteria.MappingList)
            {
                QueryCriteriaItem i = new QueryCriteriaItem();
                i.Type = item.Type;
                i.Translating = item.Translating.Clone();
                i.TargetField = item.TargetField;
                i.SourceField = item.SourceField;
                i.Singal = item.Singal;
                i.RedundancyFlag = item.RedundancyFlag;
                i.Operator = item.Operator;
                i.GWDataDBField = item.GWDataDBField.Clone();
                msg.Rule.QueryCriteria.MappingList.Add(i);
            }

            return msg;
        }

        internal override XCollection<XIMMappingItem> MappingList
        {
            get { return Rule.QueryResult.MappingList; }
        }

        internal XCollection<QueryCriteriaItem> QueryCriterias
        {
            get { return Rule.QueryCriteria.MappingList; }
        }
    }
}
