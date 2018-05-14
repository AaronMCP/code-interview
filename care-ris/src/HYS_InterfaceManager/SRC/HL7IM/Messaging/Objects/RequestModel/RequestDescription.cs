using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    public class RequestDescription : XObject
    {
        private XCollection<RequestRule> _rules = new XCollection<RequestRule>();
        public XCollection<RequestRule> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }
    }
}
