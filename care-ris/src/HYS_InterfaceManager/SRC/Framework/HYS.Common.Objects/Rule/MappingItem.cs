using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class MappingItem : XObject
    {
        private string _sourceField = "";
        public string SourceField
        {
            get { return _sourceField; }
            set { _sourceField = value; }
        }

        private string _targetField = "";
        public string TargetField
        {
            get { return _targetField; }
            set { _targetField = value; }
        }

        private bool _redundancyFlag;
        public bool RedundancyFlag
        {
            get { return _redundancyFlag; }
            set { _redundancyFlag = value; }
        }

        private TranslatingRule _translating = new TranslatingRule();
        public TranslatingRule Translating
        {
            get { return _translating; }
            set { _translating = value; }
        }

        private GWDataDBField _gwDataDBField = new GWDataDBField();
        public GWDataDBField GWDataDBField
        {
            get { return _gwDataDBField; }
            set { _gwDataDBField = value; }
        }

        public MappingItem()
        {
        }

        public MappingItem(string targetfield, string fixValue)
        {
            _targetField = targetfield;
            _translating.ConstValue = fixValue;
            _translating.Type = TranslatingType.FixValue;
        }
        
        public MappingItem(string targetfield, GWDataDBField sourcefield)
        {
            _targetField = targetfield;
            _sourceField = sourcefield.GetFullFieldName();
            _gwDataDBField = sourcefield;
        }

        public MappingItem(string targetfield, GWDataDBField sourcefield, string lutName)
            : this(targetfield,sourcefield)
        {
            _translating.LutName = lutName;
            _translating.Type = TranslatingType.LookUpTable;
        }

        public MappingItem(string targetfield, GWDataDBField sourcefield, bool redundancyFlag)
            : this(targetfield,sourcefield)
        {
            _redundancyFlag = redundancyFlag;
        }

        public MappingItem(string targetfield, GWDataDBField sourcefield, string lutName, bool redundancyFlag)
            : this(targetfield,sourcefield,lutName)
        {
            _redundancyFlag = redundancyFlag;
        }

        public MappingItem(GWDataDBField targetfield, string sourcefield)
        {
            _sourceField = sourcefield;
            _targetField = targetfield.GetFullFieldName();
            _gwDataDBField = targetfield;
        }

        public MappingItem(GWDataDBField targetfield, string sourcefield, string lutName) 
            : this(targetfield,sourcefield)
        {
            _translating.LutName = lutName;
            _translating.Type = TranslatingType.LookUpTable;
        }

        public MappingItem(GWDataDBField targetfield, string sourcefield, bool redundancyFlag)
            : this(targetfield, sourcefield)
        {
            _redundancyFlag = redundancyFlag;
        }

        public MappingItem(GWDataDBField targetfield, string sourcefield, string lutName, bool redundancyFlag)
            : this(targetfield, sourcefield, lutName)
        {
            _redundancyFlag = redundancyFlag;
        }
    }
}
