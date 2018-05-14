using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.Configuration
{
    

    public class FileOutQueryResultItem : QueryResultItem
    {
        private ThrPartyDBParamterExOut _paramter = new ThrPartyDBParamterExOut();
        public ThrPartyDBParamterExOut ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public FileOutQueryResultItem()
        {
        }

        public FileOutQueryResultItem Clone()
        {
            FileOutQueryResultItem item = new FileOutQueryResultItem();

            item.ThirdPartyDBPatamter = this.ThirdPartyDBPatamter.Clone();

            item.Translating = this.Translating.Clone();
            item.SourceField = this.SourceField;
            item.TargetField = this.TargetField;
            item.RedundancyFlag = this.RedundancyFlag;
            item.GWDataDBField.Table = this.GWDataDBField.Table;
            item.GWDataDBField.FieldName = this.GWDataDBField.FieldName;

            return item;
        }

        public FileOutQueryResultItem(string gcDataDBFieldName, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = gcDataDBFieldName;
        }

        public FileOutQueryResultItem(GWDataDBField field, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = field.GetFullFieldName();
            GWDataDBField = field;
        }

        public FileOutQueryResultItem(string targetName, ThrPartyDBParamterExOut param)
        {
            TargetField = targetName;
            SourceField = param.FieldName;
            _paramter = param;
        }

        public FileOutQueryResultItem(GWDataDBField targetfield, ThrPartyDBParamterExOut param)
        {
            TargetField = targetfield.GetFullFieldName();
            SourceField = param.FieldName;

            GWDataDBField = targetfield;
            _paramter = param;
        }

        public FileOutQueryResultItem(ThrPartyDBParamterExOut param, string sourceName)
        {
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public FileOutQueryResultItem(ThrPartyDBParamterExOut param, GWDataDBField sourceField)
        {
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }

        public FileOutQueryResultItem(ThrPartyDBParamterExOut param, string sourceName, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public FileOutQueryResultItem(ThrPartyDBParamterExOut param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
