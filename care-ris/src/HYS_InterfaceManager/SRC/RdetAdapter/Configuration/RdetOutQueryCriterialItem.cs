using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.RdetAdapter.Configuration
{
    
    public class RdetOutQueryCriterialItem : QueryCriteriaItem
    {
        private ThrPartyDBParamter _paramter = new ThrPartyDBParamter();
        public ThrPartyDBParamter ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public RdetOutQueryCriterialItem()
        {
        }

        public RdetOutQueryCriterialItem Clone()
        {
            RdetOutQueryCriterialItem item = new RdetOutQueryCriterialItem();
            item.ThirdPartyDBPatamter.FieldID = this.ThirdPartyDBPatamter.FieldID;
            item.ThirdPartyDBPatamter.FieldName = this.ThirdPartyDBPatamter.FieldName;
            item.ThirdPartyDBPatamter.FieldType = this.ThirdPartyDBPatamter.FieldType;            

            item.Type = this.Type;
            item.Translating = this.Translating.Clone();

            item.SourceField = this.SourceField;
            item.TargetField = this.TargetField;

            item.Singal = this.Singal;

            item.Operator = this.Operator;

            item.RedundancyFlag = this.RedundancyFlag;

            item.GWDataDBField.Table = this.GWDataDBField.Table;
            item.GWDataDBField.FieldName = this.GWDataDBField.FieldName;
            //item.GWDataDBField.IsAuto = this.GWDataDBField.IsAuto;

            return item;
        }

        public RdetOutQueryCriterialItem(string sourceName, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            SourceField = sourceName;
        }

        public RdetOutQueryCriterialItem(GWDataDBField field, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = field.GetFullFieldName();
            GWDataDBField = field;
        }

        public RdetOutQueryCriterialItem(string targetName, ThrPartyDBParamter param)
        {
            TargetField = targetName;
            SourceField = param.FieldName;
            _paramter = param;
        }

        public RdetOutQueryCriterialItem(GWDataDBField targetfield, ThrPartyDBParamter param)
        {
            TargetField = targetfield.GetFullFieldName();
            SourceField = param.FieldName;

            GWDataDBField = targetfield;
            _paramter = param;
        }

        public RdetOutQueryCriterialItem(ThrPartyDBParamter param, string sourceName)
        {
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public RdetOutQueryCriterialItem(ThrPartyDBParamter param, GWDataDBField sourceField)
        {
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }

        public RdetOutQueryCriterialItem(ThrPartyDBParamter param, string sourceName, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public RdetOutQueryCriterialItem(ThrPartyDBParamter param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
