using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageHistory : XObject
    {
        private Guid _entityID = Guid.Empty;
        public Guid EntityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        private MessageState _state;
        public MessageState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _retryTimes;
        public int RetryTimes
        {
            get { return _retryTimes; }
            set { _retryTimes = value; }
        }
    }
}
