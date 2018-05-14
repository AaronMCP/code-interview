using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class TranslatingRule : XObject
    {
        private string _lutName = "";
        public string LutName
        {
            get { return _lutName; }
            set { _lutName = value; }
        }
        
        private string _constValue = "";
        [XCData(true)]
        public string ConstValue
        {
            get { return _constValue; }
            set { _constValue = value; }
        }

        private TranslatingType _type = TranslatingType.None;
        public TranslatingType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public override string ToString()
        {
            switch( Type )
            {
                default:
                case TranslatingType.None:
                    return "[" + Type.ToString() + "]";
                case TranslatingType.LookUpTable :
                case TranslatingType.LookUpTableReverse :
                    return "[" + Type.ToString() + "]" + LutName;
                case TranslatingType.FixValue :
                case TranslatingType.DefaultValue:
                    return "[" + Type.ToString() + "]" + ConstValue;
            }
        }

        public TranslatingRule Clone()
        {
            TranslatingRule rule = new TranslatingRule();
            rule.ConstValue = ConstValue;
            rule.LutName = LutName;
            rule.Type = Type;
            return rule;
        }
    }
}
