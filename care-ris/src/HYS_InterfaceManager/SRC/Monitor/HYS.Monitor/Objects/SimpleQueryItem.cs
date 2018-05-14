using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Monitor.Objects
{
    public class SimpleQueryItem : XObject
    {
        private bool _enable = false;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private GWDataDBField _fieldName = GWDataDBField.Null;
        public GWDataDBField Fieldname
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _fieldValue = "";
        public string FieldValue
        {
            get { return _fieldValue; }
            set { _fieldValue = value; }
        }

        public SimpleQueryItem()
        {
            _enable = false;
        }

        public SimpleQueryItem(GWDataDBField fieldName, string fieldValue)
        {
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _enable = true;
        }

    }
}
