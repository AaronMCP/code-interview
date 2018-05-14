using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public class SubscriptionDescription : XObject
    {
        private XCollection<SubscriptionRule> _rules = new XCollection<SubscriptionRule>();
        public XCollection<SubscriptionRule> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }
    }
}
