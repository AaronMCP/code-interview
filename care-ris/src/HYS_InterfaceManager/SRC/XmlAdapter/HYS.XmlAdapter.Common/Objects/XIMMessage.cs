using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public abstract class XIMMessage : XObject
    {
        private HL7EventType _hl7EventType = HL7EventType.Empty.Clone();
        public HL7EventType HL7EventType
        {
            get { return _hl7EventType; }
            set { _hl7EventType = value; }
        }

        private GWEventType _gwEventType = GWEventType.Empty.Clone();
        public GWEventType GWEventType
        {
            get { return _gwEventType; }
            set { _gwEventType = value; }
        }

        private string _xslFileName = "";
        public string XSLFileName
        {
            get { return _xslFileName; }
            set { _xslFileName = value; }
        }

        internal abstract XCollection<XIMMappingItem> MappingList
        {
            get;
        }
    }
}
