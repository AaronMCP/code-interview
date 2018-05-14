using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using System.Text;


namespace HYS.FileAdapter.Configuration
{

    public class FileInQueryResultItem : QueryResultItem
    {
        private ThrPartyDBParamterExIn _paramter = new ThrPartyDBParamterExIn();
        public ThrPartyDBParamterExIn ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        public FileInQueryResultItem()
        {
        }

        public FileInQueryResultItem Clone()
        {
            FileInQueryResultItem item = new FileInQueryResultItem();
                        
            item.ThirdPartyDBPatamter = this.ThirdPartyDBPatamter.Clone();

            item.Translating = this.Translating.Clone();
            item.SourceField = this.SourceField;
            item.TargetField = this.TargetField;
            item.RedundancyFlag = this.RedundancyFlag;
            item.GWDataDBField.Table = this.GWDataDBField.Table;
            item.GWDataDBField.FieldName = this.GWDataDBField.FieldName;
                      
            return item;
        }

        public FileInQueryResultItem(string sourceName, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            SourceField = sourceName;
        }

        public FileInQueryResultItem(GWDataDBField field, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = field.GetFullFieldName();
            GWDataDBField = field;
        }

        public FileInQueryResultItem(string targetName, ThrPartyDBParamterExIn param)
        {
            TargetField = targetName;
            SourceField = param.FieldName;
            _paramter = param;
        }

        public FileInQueryResultItem(GWDataDBField targetfield, ThrPartyDBParamterExIn param)
        {
            TargetField = targetfield.GetFullFieldName();
            SourceField = param.FieldName;

            GWDataDBField = targetfield;
            _paramter = param;
        }

        public FileInQueryResultItem(ThrPartyDBParamterExIn param, string sourceName)
        {
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public FileInQueryResultItem(ThrPartyDBParamterExIn param, GWDataDBField sourceField)
        {
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }

        public FileInQueryResultItem(ThrPartyDBParamterExIn param, string sourceName, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public FileInQueryResultItem(ThrPartyDBParamterExIn param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
