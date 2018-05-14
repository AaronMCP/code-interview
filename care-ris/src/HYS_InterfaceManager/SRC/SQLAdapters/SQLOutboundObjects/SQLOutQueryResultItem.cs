using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLOutboundAdapterObjects
{
    public class SQLOutQueryResultItem : QueryResultItem
    {
        private ThrPartyDBParamter _paramter = new ThrPartyDBParamter();
        public ThrPartyDBParamter ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public SQLOutQueryResultItem()
        {
        }

        public SQLOutQueryResultItem Clone()
        {
            SQLOutQueryResultItem item = new SQLOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldID = this.ThirdPartyDBPatamter.FieldID;
            item.ThirdPartyDBPatamter.FieldName = this.ThirdPartyDBPatamter.FieldName;
            item.ThirdPartyDBPatamter.FieldType = this.ThirdPartyDBPatamter.FieldType;
            
            item.Translating = this.Translating.Clone();
            item.SourceField = this.SourceField;
            item.TargetField = this.TargetField;
            item.RedundancyFlag = this.RedundancyFlag;
            item.GWDataDBField.Table = this.GWDataDBField.Table;
            item.GWDataDBField.FieldName = this.GWDataDBField.FieldName;

            return item;
        }

        public SQLOutQueryResultItem(string targetField, string fixValue)
            :base(targetField,fixValue)
        {
            
        }

        public SQLOutQueryResultItem(string targetField,GWDataDBField sourceField)
            :base(targetField,sourceField)
        {
            
        }

        public SQLOutQueryResultItem(string targetField, GWDataDBField sourceField, string lutName)
            : base(targetField, sourceField,lutName)
        {

        }

        public SQLOutQueryResultItem(string targetField, GWDataDBField sourceField, bool redundancyFlag)
            : base(targetField, sourceField,redundancyFlag)
        {

        }

        public SQLOutQueryResultItem(string targetField, GWDataDBField sourceField, string lutName, bool redundancyFlag)
            : base(targetField, sourceField,lutName,redundancyFlag)
        {

        }


        public SQLOutQueryResultItem(ThrPartyDBParamter param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
