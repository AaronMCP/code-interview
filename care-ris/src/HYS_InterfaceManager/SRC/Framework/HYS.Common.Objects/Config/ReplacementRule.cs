using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{
    public class ReplacementRule : XObject
    {
        private bool _enable;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to perform the replacement.")]
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private XCollection<ReplacementRuleItem> _fields = new XCollection<ReplacementRuleItem>();
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Perform the replacement in these fields.")]
        public XCollection<ReplacementRuleItem> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public ReplacementRuleItem GetReplacementRule(GWDataDBField field)
        {
            if (field == null) return null;
            foreach (ReplacementRuleItem f in Fields)
            {
                if (f.Table == field.Table &&
                    f.FieldName == field.FieldName) return f;
            }
            return null;
        }
    }
}
