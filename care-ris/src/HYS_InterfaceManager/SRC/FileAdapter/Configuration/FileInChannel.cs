using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.Configuration
{
    public class FileInChannel :XObject
    {

        public FileInChannel()
        {

        }
        public FileInChannel(string opName)
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

        public FileInChannel Clone()
        {
            FileInChannel ch = new FileInChannel();

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
            foreach (FileInQueryCriteriaItem item in Rule.QueryCriteria.MappingList)
            {
                FileInQueryCriteriaItem i = item.Clone() ;
                ch.Rule.QueryCriteria.MappingList.Add(i);                
            }
            foreach (FileInQueryResultItem item in Rule.QueryResult.MappingList)
            {
                FileInQueryResultItem i = item.Clone() ;
                ch.Rule.QueryResult.MappingList.Add(i);
            }

            return ch;
        }

        private InboundRule<FileInQueryCriteriaItem, FileInQueryResultItem> _rule = new InboundRule<FileInQueryCriteriaItem, FileInQueryResultItem>();
        public InboundRule<FileInQueryCriteriaItem, FileInQueryResultItem> Rule
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
