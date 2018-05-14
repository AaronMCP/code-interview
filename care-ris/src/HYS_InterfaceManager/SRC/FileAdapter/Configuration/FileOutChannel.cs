using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
namespace HYS.FileAdapter.Configuration
{
   
    public class FileOutChannel : XObject
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

        public FileOutChannel Clone()
        {
            FileOutChannel ch = new FileOutChannel();

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
            foreach (FileOutQueryCriterialItem item in Rule.QueryCriteria.MappingList)
            {
                FileOutQueryCriterialItem i = item.Clone();
                ch.Rule.QueryCriteria.MappingList.Add(i);
            }
            foreach (FileOutQueryResultItem item in Rule.QueryResult.MappingList)
            {
                FileOutQueryResultItem i = item.Clone();
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }


        private OutboundRule<FileOutQueryCriterialItem, FileOutQueryResultItem> _rule = new OutboundRule<FileOutQueryCriterialItem, FileOutQueryResultItem>();
        public OutboundRule<FileOutQueryCriterialItem, FileOutQueryResultItem> Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }

        public FileOutChannel()
        {
            Rule.CheckProcessFlag = true;
            Rule.AutoUpdateProcessFlag = false;
        }
        public FileOutChannel(string opName)
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
