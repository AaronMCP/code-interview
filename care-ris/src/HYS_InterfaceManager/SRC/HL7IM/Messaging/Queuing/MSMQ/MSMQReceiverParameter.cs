using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQReceiverParameter : XObject
    {
        private MSMQParameter _msmq = new MSMQParameter();
        public MSMQParameter MSMQ
        {
            get { return _msmq; }
            set { _msmq = value; }
        }

        private int _timeSlice = 3000;  // 3 seconds
        public int TimeSlice
        {
            get { return _timeSlice; }
            set { _timeSlice = value; }
        }

        private bool _enableTransaction = true;
        public bool EnableTransaction
        {
            get { return _enableTransaction; }
            set { _enableTransaction = value; }
        }
    }
}
