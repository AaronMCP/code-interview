using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{
    public class ComposingRule : XObject
    {
        private bool _enable;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to perform the composing.")]
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private XCollection<ComposingRuleItem> _fields = new XCollection<ComposingRuleItem>();
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Perform the composing in these fields.")]
        public XCollection<ComposingRuleItem> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public ComposingRuleItem GetComposingRule(GWDataDBField field)
        {
            if (field == null) return null;
            foreach (ComposingRuleItem f in Fields)
            {
                if (f.Table == field.Table &&
                    f.FieldName == field.FieldName) return f;
            }
            return null;
        }
    }
}
