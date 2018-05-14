using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{
    public class ReplacementRuleItem : GWDataDBField
    {
        private RegularExpressionItem _regularExpression = new RegularExpressionItem();
        [Browsable(false)]
        public RegularExpressionItem RegularExpression
        {
            get { return _regularExpression; }
            set { _regularExpression = value; }
        }

        public ReplacementRuleItem()
        {
        }
        public ReplacementRuleItem(GWDataDBField field)
            : base(field.FieldName, field.Table, field.IsAuto)
        {
        }
        public ReplacementRuleItem(string field, GWDataDBTable table, bool isAuto)
            : base(field, table, isAuto)
        {
        }

        public new ReplacementRuleItem Clone()
        {
            ReplacementRuleItem item = new ReplacementRuleItem(FieldName,Table,IsAuto);
            item.RegularExpression = RegularExpression.Clone();
            return item;
        }
    }
}
