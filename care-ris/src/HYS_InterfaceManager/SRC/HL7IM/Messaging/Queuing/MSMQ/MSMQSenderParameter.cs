using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQSenderParameter : XObject
    {
        private MSMQParameter _msmq = new MSMQParameter();
        public MSMQParameter MSMQ
        {
            get { return _msmq; }
            set { _msmq = value; }
        }
    }
}
