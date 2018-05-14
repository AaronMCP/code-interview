using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;

namespace HYS.Common.Objects.Config
{
    public class Chinese2PinyinRuleItem : GWDataDBField
    {
        private ChineseCodeConvertType _convertType;
        public ChineseCodeConvertType ConvertType
        {
            get { return _convertType; }
            set { _convertType = value; }
        }

        private PinyinType _type;
        public PinyinType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Chinese2PinyinRuleItem()
        {
        }
        public Chinese2PinyinRuleItem(GWDataDBField field)
            : base(field.FieldName, field.Table, field.IsAuto)
        {
        }
        public Chinese2PinyinRuleItem(string field, GWDataDBTable table, bool isAuto)
            : base(field, table, isAuto)
        {
        }

        public new Chinese2PinyinRuleItem Clone()
        {
            Chinese2PinyinRuleItem item = new Chinese2PinyinRuleItem(FieldName, Table, IsAuto);
            item.ConvertType = ConvertType;
            item.Type = Type;
            return item;
        }
    }
}
