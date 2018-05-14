using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
namespace HYS.SocketAdapter.Configuration
{
   
    public class SocketOutChannel : XObject
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

        public SocketOutChannel Clone()
        {
            SocketOutChannel ch = new SocketOutChannel();

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
            foreach (SocketOutQueryCriterialItem item in Rule.QueryCriteria.MappingList)
            {
                SocketOutQueryCriterialItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (SocketOutQueryResultItem item in Rule.QueryResult.MappingList)
            {
                SocketOutQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }

        private OutboundRule<SocketOutQueryCriterialItem, SocketOutQueryResultItem> _rule = new OutboundRule<SocketOutQueryCriterialItem, SocketOutQueryResultItem>();
        public OutboundRule<SocketOutQueryCriterialItem, SocketOutQueryResultItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public SocketOutChannel()
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
        }
        public SocketOutChannel(string opName)
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
