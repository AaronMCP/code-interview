using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLOutboundAdapterObjects
{
    public class SQLOutboundChanel : XObject
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
        [XCData(true)]
        public string OperationName
        {
            get { return _opName; }
            set { _opName = value; }
        }

        private ThrPartyDBOperationType _opType = ThrPartyDBOperationType.StorageProcedure;
        public ThrPartyDBOperationType OperationType
        {
            get { return _opType; }
            set { _opType = value; }
        }

        private OutboundRule<SQLOutQueryCriteriaItem, SQLOutQueryResultItem> _rule = new OutboundRule<SQLOutQueryCriteriaItem, SQLOutQueryResultItem>();
        public OutboundRule<SQLOutQueryCriteriaItem, SQLOutQueryResultItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        private string _spName = "";
        public string SPName
        {
            get { return _spName; }
            set { _spName = value; }
        }

        private bool _modified = false;
        public bool Modified
        {
            get { return _modified; }
            set { _modified = value; }
        }

        private bool _ignoreDBException = true;
        public bool IgnoreDBException
        {
            get { return _ignoreDBException; }
            set { _ignoreDBException = value; }
        }

        private string _spStatement = "";
        [XCData(true)]
        public string SPStatement
        {
            get { return _spStatement; }
            set { _spStatement = value; }
        }

        public SQLOutboundChanel()
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
        }

        public SQLOutboundChanel(string opName)
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
            _opName = opName;
        }

        public override string ToString()
        {
            return Rule.RuleName;
        }

        public SQLOutboundChanel Clone()
        {
            SQLOutboundChanel ch = new SQLOutboundChanel();

            ch.ChannelName = this.ChannelName;

            ch.Enable = this.Enable;

            ch.OperationName = this.OperationName;

            ch.Modified = this.Modified;

            ch.OperationType = this.OperationType;

            ch.SPName = this.SPName;

            ch.SPStatement = this.SPStatement;


            //ch.Rule.RuleID = Rule.RuleID; // do not copy RuleID
            ch.Rule.RuleName = Rule.RuleName;
            ch.Rule.MaxRecordCount = Rule.MaxRecordCount;
            ch.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            ch.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;

            ch.Rule.QueryCriteria.Type = Rule.QueryCriteria.Type;
            ch.Rule.QueryCriteria.SQLStatement = Rule.QueryCriteria.SQLStatement;

            //ch.Rule.QueryCriteriaRuleType = this.Rule.QueryCriteriaRuleType;           
            foreach (SQLOutQueryCriteriaItem  item in Rule.QueryCriteria.MappingList)
            {
                SQLOutQueryCriteriaItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (SQLOutQueryResultItem item in Rule.QueryResult.MappingList)
            {
                SQLOutQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }
                        

            return ch;
        }
    }
}
