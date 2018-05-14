using System;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XmlElementItem : IXmlElementItem
    {
        private bool _enable;
        private bool _redundancyFlag = false;
        private XmlElement _element = new XmlElement();
        private GWDataDBField _field = GWDataDBField.Null;
        private TranslatingRule _translating = new TranslatingRule();
        
        public XmlElementItem()
        {
        }
        public XmlElementItem(XmlElement element)
        {
            _element = element.Clone();
        }

        #region IXmlElementItem Members

        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
        
        public XmlElement Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
            }
        }

        public GWDataDBField GWDataDBField
        {
            get
            {
                return _field;
            }
            set
            {
                _field = value;
            }
        }

        public TranslatingRule Translating
        {
            get
            {
                return _translating;
            }
            set
            {
                _translating = value;
            }
        }

        public IXmlElementItem Clone()
        {
            XmlElementItem item = new XmlElementItem();
            item._translating = _translating.Clone();
            item._element = _element.Clone();
            item._field = _field.Clone();
            return item;
        }

        public bool RedundancyFlag
        {
            get { return _redundancyFlag; }
        }

        #endregion
    }
}
