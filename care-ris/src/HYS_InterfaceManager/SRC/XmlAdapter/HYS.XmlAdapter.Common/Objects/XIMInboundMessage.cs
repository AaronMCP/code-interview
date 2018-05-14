using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMInboundMessage : XIMMessage
    {
        private InboundRule<QueryCriteriaItem, XIMMappingItem> _rule = new InboundRule<QueryCriteriaItem, XIMMappingItem>();
        public InboundRule<QueryCriteriaItem, XIMMappingItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public XIMInboundMessage Clone()
        {
            XIMInboundMessage msg = new XIMInboundMessage();
            msg.HL7EventType = HL7EventType.Clone();
            msg.GWEventType = GWEventType.Clone();
            msg.XSLFileName = XSLFileName;

            //msg.Rule.RuleID = Rule.RuleID;        // do not copy RuleID
            msg.Rule.RuleName = Rule.RuleName;
            msg.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            msg.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;
            foreach (XIMMappingItem item in Rule.QueryResult.MappingList)
            {
                XIMMappingItem i = item.Clone() as XIMMappingItem;
                msg.Rule.QueryResult.MappingList.Add(i);
            }

            return msg;
        }

        internal override XCollection<XIMMappingItem> MappingList
        {
            get { return Rule.QueryResult.MappingList; }
        }
    }
}
