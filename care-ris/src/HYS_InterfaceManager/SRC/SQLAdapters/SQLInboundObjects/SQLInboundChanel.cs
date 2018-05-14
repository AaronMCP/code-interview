using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInboundChanel : XObject
    {
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

        private bool _callSPasSQLText = false;
        public bool CallSPAsSQLText
        {
            get { return _callSPasSQLText; }
            set { _callSPasSQLText = value; }
        }

        private ThrPartyDBOperationType _opType = ThrPartyDBOperationType.StorageProcedure;
        public ThrPartyDBOperationType OperationType
        {
            get { return _opType; }
            set { _opType = value; }
        }

        //private InboundRule<SQLInQueryCriteriaItem, SQLInQueryResultItem> _rule = new InboundRule<SQLInQueryCriteriaItem, SQLInQueryResultItem>();
        //public InboundRule<SQLInQueryCriteriaItem, SQLInQueryResultItem> Rule
        //{
        //    get { return _rule; }
        //    set { _rule = value; }
        //}

        private SQLInboundRule _rule = new SQLInboundRule();
        public SQLInboundRule Rule
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
        public bool Modified {
            get { return _modified; }
            set { _modified = value; }
        }

        private string _spStatement = "";
        [XCData(true)]
        public string SPStatement
        {
            get { return _spStatement; }
            set { _spStatement = value; }
        }

        public SQLInboundChanel()
        {

        }
        public SQLInboundChanel(string opName)
        {
            _opName = opName;
        }

        public override string ToString()
        {
            return Rule.RuleName;
        }

        public SQLInboundChanel Clone()
        {
            SQLInboundChanel ch = new SQLInboundChanel();

            ch.ChannelName = this.ChannelName;

            ch.Enable = this.Enable;

            ch.OperationName = this.OperationName;

            ch.Modified = this.Modified;

            ch.OperationType = this.OperationType;

            ch.SPName = this.SPName;

            ch.SPStatement = this.SPStatement;

            //ch.Rule.RuleID = Rule.RuleID; // do not copy RuleID
            ch.Rule.RuleName = Rule.RuleName;
            ch.Rule.CheckProcessFlag = Rule.CheckProcessFlag;
            ch.Rule.AutoUpdateProcessFlag = Rule.AutoUpdateProcessFlag;
            ch.Rule.QueryCriteria.Type = Rule.QueryCriteria.Type;
            ch.Rule.QueryCriteria.SQLStatement = Rule.QueryCriteria.SQLStatement;
            ch.Rule.InputParameterSPStatement = Rule.InputParameterSPStatement;    
            foreach (SQLInQueryCriteriaItem item in Rule.QueryCriteria.MappingList)
            {
                SQLInQueryCriteriaItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (SQLInQueryResultItem item in Rule.QueryResult.MappingList)
            {
                SQLInQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }

    }
}
