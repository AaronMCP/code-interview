using System;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMMappingItem : QueryResultItem, IXmlElementItem
    {
        private bool _enable;
        private XmlElement _element = new XmlElement();

        public XIMMappingItem()
        {
        }
        public XIMMappingItem(XmlElement element)
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

        public IXmlElementItem Clone()
        {
            XIMMappingItem item = new XIMMappingItem();
            item.GWDataDBField = GWDataDBField.Clone();
            item.Translating = Translating.Clone();
            item.RedundancyFlag = RedundancyFlag;
            item.SourceField = SourceField;
            item.TargetField = TargetField;
            item.Element = Element.Clone();
            item.Enable = Enable;
            return item;
        }

        #endregion
    }
}
