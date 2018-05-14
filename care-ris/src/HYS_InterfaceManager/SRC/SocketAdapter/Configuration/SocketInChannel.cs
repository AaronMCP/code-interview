using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SocketAdapter.Configuration
{
    public class SocketInChannel :XObject
    {

        public SocketInChannel()
        {

        }
        public SocketInChannel(string opName)
        {
            _opName = opName;
        }

        private string _channelName = "";
        public string ChannelName {
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

        public SocketInChannel Clone()
        {
            SocketInChannel ch = new SocketInChannel();

            ch.ChannelName = this.ChannelName;

            ch.Enable = this.Enable;

            ch.OperationName = this.OperationName;

            //ch.Rule.RuleID = Rule.RuleID;     //do not copy RuleID
            ch.Rule.RuleName = Rule.RuleName;
            ch.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            ch.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;

            ch.Rule.QueryCriteria.Type = Rule.QueryCriteria.Type;
            ch.Rule.QueryCriteria.SQLStatement = Rule.QueryCriteria.SQLStatement;

            //ch.Rule.QueryCriteriaRuleType = this.Rule.QueryCriteriaRuleType;           
            foreach (SocketInQueryCriteriaItem  item in Rule.QueryCriteria.MappingList)
            {
                SocketInQueryCriteriaItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (SocketInQueryResultItem  item in Rule.QueryResult.MappingList)
            {
                SocketInQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }

        private InboundRule<SocketInQueryCriteriaItem, SocketInQueryResultItem> _rule = new InboundRule<SocketInQueryCriteriaItem, SocketInQueryResultItem>();
        public InboundRule<SocketInQueryCriteriaItem, SocketInQueryResultItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        

        public override string ToString()
        {
            return Rule.RuleName;
        }
    }
}
