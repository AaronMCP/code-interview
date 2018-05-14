using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.RdetAdapter.Configuration
{
    

    public class RdetOutQueryResultItem : QueryResultItem
    {
        private ThrPartyDBParamter _paramter = new ThrPartyDBParamter();
        public ThrPartyDBParamter ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public RdetOutQueryResultItem()
        {
        }

        public RdetOutQueryResultItem Clone()
        {
            RdetOutQueryResultItem item = new RdetOutQueryResultItem();

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

        public RdetOutQueryResultItem(string gcDataDBFieldName, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = gcDataDBFieldName;
        }

        public RdetOutQueryResultItem(GWDataDBField field, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = field.GetFullFieldName();
            GWDataDBField = field;
        }

        public RdetOutQueryResultItem(string targetName, ThrPartyDBParamter param)
        {
            TargetField = targetName;
            SourceField = param.FieldName;
            _paramter = param;
        }

        public RdetOutQueryResultItem(GWDataDBField targetfield, ThrPartyDBParamter param)
        {
            TargetField = targetfield.GetFullFieldName();
            SourceField = param.FieldName;

            GWDataDBField = targetfield;
            _paramter = param;
        }

        public RdetOutQueryResultItem(ThrPartyDBParamter param, string sourceName)
        {
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public RdetOutQueryResultItem(ThrPartyDBParamter param, GWDataDBField sourceField)
        {
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }

        public RdetOutQueryResultItem(ThrPartyDBParamter param, string sourceName, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public RdetOutQueryResultItem(ThrPartyDBParamter param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
