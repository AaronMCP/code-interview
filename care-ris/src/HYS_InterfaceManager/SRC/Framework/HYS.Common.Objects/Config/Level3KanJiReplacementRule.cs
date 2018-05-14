using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{

    public class Level3KanJiReplacementRule : XObject
    {
        private bool _enable;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to translate Level three KanJi to special charactor.")]
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private XCollection<Level3KanJiReplacementRuleItem> _fields = new XCollection<Level3KanJiReplacementRuleItem>();
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Translate Level three KanJi to special charactor in these fields.")]
        public XCollection<Level3KanJiReplacementRuleItem> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public Level3KanJiReplacementRuleItem GetLevel3KanJiReplacementRule(GWDataDBField field)
        {
            if (field == null) return null;
            foreach (Level3KanJiReplacementRuleItem f in Fields)
            {
                if (f.Table == field.Table &&
                    f.FieldName == field.FieldName) return f;
            }
            return null;
        }
    }
   
}
