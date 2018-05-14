using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInQueryResultItem : QueryResultItem
    {
        private ThrPartyDBParamter _paramter = new ThrPartyDBParamter();
        public ThrPartyDBParamter ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public SQLInQueryResultItem()
        {
        }

        public SQLInQueryResultItem Clone()
        {
            SQLInQueryResultItem item = new SQLInQueryResultItem();

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

        public SQLInQueryResultItem(string targetField, string fixValue)
            :base(targetField,fixValue)
        {
            
        }

        public SQLInQueryResultItem(GWDataDBField trgetField, string sourceField)
            : base(trgetField, sourceField)
        {
            
        }

        public SQLInQueryResultItem(GWDataDBField trgetField, string sourceField, bool redundancyFlag)
            : base(trgetField, sourceField, redundancyFlag)
        {

        }

        public SQLInQueryResultItem(GWDataDBField trgetField, string sourceField, string lutName)
            : base(trgetField, sourceField, lutName)
        {

        }

        public SQLInQueryResultItem(GWDataDBField trgetField, string sourceField, string lutName, bool redundancyFlag)
            : base(trgetField, sourceField, lutName, redundancyFlag)
        {

        }

        public SQLInQueryResultItem(GWDataDBField targetField, ThrPartyDBParamter param)
            : base(targetField, param.FieldName)
        {
            _paramter = param;
        }

        public SQLInQueryResultItem(GWDataDBField targetField, ThrPartyDBParamter param,string lutName)
            :base(targetField,param.FieldName,lutName)
        {
            _paramter = param;
        }

        public SQLInQueryResultItem(GWDataDBField targetField, ThrPartyDBParamter param, bool redundancyFlag)
            : base(targetField, param.FieldName,redundancyFlag)
        {
            _paramter = param;
        }

        public SQLInQueryResultItem(GWDataDBField targetField, ThrPartyDBParamter param, string lutName,bool redundancyFlag)
            : base(targetField, param.FieldName, lutName, redundancyFlag)
        {
            _paramter = param;
        }
    }
}
