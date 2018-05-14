using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    public class MessageTypePair : XObject
    {
        public MessageTypePair()
        {
        }
        public MessageTypePair(MessageType reqType, MessageType rspType)
        {
            RequestMessageType = reqType;
            ResponseMessageType = rspType;
        }

        private MessageType _requestMessageType = new MessageType();
        public MessageType RequestMessageType
        {
            get { return _requestMessageType; }
            set { _requestMessageType = value; }
        }

        private MessageType _responseMessageType = new MessageType();
        public MessageType ResponseMessageType
        {
            get { return _responseMessageType; }
            set { _responseMessageType = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public override string ToString()
        {
            return RequestMessageType.ToString() + " / " + ResponseMessageType.ToString();
        }
        public bool EqualsTo(MessageTypePair pair)
        {
            if (pair == null) return false;
            return pair._requestMessageType.EqualsTo(_requestMessageType)
                && pair._responseMessageType.EqualsTo(_responseMessageType);
        }
    }
}
