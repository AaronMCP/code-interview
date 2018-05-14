using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Config
{
    public class Chinese2PinyinRule : XObject
    {
        private bool _enable;
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Whether to translate Chinese charactor to Pinyin.")]
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private XCollection<Chinese2PinyinRuleItem> _fields = new XCollection<Chinese2PinyinRuleItem>();
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Translate Chinese charactor to Pinyin in these fields.")]
        public XCollection<Chinese2PinyinRuleItem> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public Chinese2PinyinRuleItem GetChinese2PinyinRule(GWDataDBField field)
        {
            if (field == null) return null;
            foreach (Chinese2PinyinRuleItem f in Fields)
            {
                if (f.Table == field.Table &&
                    f.FieldName == field.FieldName) return f;
            }
            return null;
        }
    }
}
