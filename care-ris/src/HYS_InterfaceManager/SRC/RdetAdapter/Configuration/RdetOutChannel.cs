using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
namespace HYS.RdetAdapter.Configuration
{
   
    public class RdetOutChannel : XObject
    {
        private string _channelName = "";
        public string ChannelName
        {
            get { return _channelName; }
            set { _channelName = value; }
        }

        private bool _enable = false;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private string _opName = "";
        public string OperationName
        {
            get { return _opName; }
            set { _opName = value; }
        }

        public RdetOutChannel Clone()
        {
            RdetOutChannel ch = new RdetOutChannel();

            ch.ChannelName = this.ChannelName;

            ch.Enable = this.Enable;

            ch.OperationName = this.OperationName;

            //ch.Rule.RuleID = Rule.RuleID;     //do not copy RuleID
            ch.Rule.RuleName = Rule.RuleName;
            ch.Rule.MaxRecordCount = Rule.MaxRecordCount;
            ch.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            ch.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;

            ch.Rule.QueryCriteria.Type = Rule.QueryCriteria.Type;
            ch.Rule.QueryCriteria.SQLStatement = Rule.QueryCriteria.SQLStatement;

            //ch.Rule.QueryCriteriaRuleType = this.Rule.QueryCriteriaRuleType;           
            foreach (RdetOutQueryCriterialItem item in Rule.QueryCriteria.MappingList)
            {
                RdetOutQueryCriterialItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (RdetOutQueryResultItem item in Rule.QueryResult.MappingList)
            {
                RdetOutQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }

        private OutboundRule<RdetOutQueryCriterialItem, RdetOutQueryResultItem> _rule = new OutboundRule<RdetOutQueryCriterialItem, RdetOutQueryResultItem>();
        public OutboundRule<RdetOutQueryCriterialItem, RdetOutQueryResultItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public RdetOutChannel()
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
        }
        public RdetOutChannel(string opName)
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
            _opName = opName;
        }

        public override string ToString()
        {
            return Rule.RuleName;
        }
    }

}
