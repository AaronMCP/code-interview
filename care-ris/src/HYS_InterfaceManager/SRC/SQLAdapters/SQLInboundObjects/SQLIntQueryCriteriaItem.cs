using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInQueryCriteriaItem : QueryCriteriaItem
    {
        private ThrPartyDBParamter _paramter = new ThrPartyDBParamter();
        public ThrPartyDBParamter ThirdPartyDBPatamter
        {
            get { return _paramter; }
            set { _paramter = value; }
        }

        private bool _isNull;
        public bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }

        private bool _isGetFromStorageProcedure;
        public bool IsGetFromStorageProcedure
        {
            get { return _isGetFromStorageProcedure; }
            set { _isGetFromStorageProcedure = value; }
        }

        public SQLInQueryCriteriaItem()
        {
        }

        public SQLInQueryCriteriaItem Clone()
        {
            SQLInQueryCriteriaItem item = new SQLInQueryCriteriaItem();

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

        public SQLInQueryCriteriaItem(string sourceName, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            SourceField = sourceName;
        }

        public SQLInQueryCriteriaItem(GWDataDBField field, string fixValue)
        {
            Translating.Type = TranslatingType.FixValue;
            Translating.ConstValue = fixValue;
            TargetField = field.GetFullFieldName();
            GWDataDBField = field;
        }

        public SQLInQueryCriteriaItem(string targetName, ThrPartyDBParamter param)
        {
            TargetField = targetName;
            SourceField = param.FieldName;
            _paramter = param;
        }

        public SQLInQueryCriteriaItem(GWDataDBField targetfield, ThrPartyDBParamter param)
        {
            TargetField = targetfield.GetFullFieldName();
            SourceField = param.FieldName;

            GWDataDBField = targetfield;
            _paramter = param;
        }

        public SQLInQueryCriteriaItem(ThrPartyDBParamter param, string sourceName)
        {
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public SQLInQueryCriteriaItem(ThrPartyDBParamter param, GWDataDBField sourceField)
        {
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }

        public SQLInQueryCriteriaItem(ThrPartyDBParamter param, string sourceName, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceName;
            _paramter = param;
        }

        public SQLInQueryCriteriaItem(ThrPartyDBParamter param, GWDataDBField sourceField, bool redundancyFlag)
        {
            RedundancyFlag = redundancyFlag;
            TargetField = param.FieldName;
            SourceField = sourceField.GetFullFieldName();

            GWDataDBField = sourceField;
            _paramter = param;
        }
    }
}
