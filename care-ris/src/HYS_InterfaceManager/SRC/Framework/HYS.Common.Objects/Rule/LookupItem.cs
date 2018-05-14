using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class LookupItem : XObject
    {
        private string _sourceValue = "";
        public string SourceValue
        {
            get { return _sourceValue; }
            set { _sourceValue = value; }
        }

        private string _targetValue = "";
        public string TargetValue
        {
            get { return _targetValue; }
            set { _targetValue = value; }
        }

        public LookupItem()
        {
        }
        public LookupItem(string source, string target)
        {
            _sourceValue = source;
            _targetValue = target;
        }
    }
}
