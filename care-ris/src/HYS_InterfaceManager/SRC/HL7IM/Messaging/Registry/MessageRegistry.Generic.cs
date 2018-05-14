using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        private static MessageType _generic_NotificationMessageType;
        public static MessageType GENERIC_NotificationMessageType
        {
            get
            {
                if (_generic_NotificationMessageType == null)
                {
                    _generic_NotificationMessageType = new MessageType();
                    _generic_NotificationMessageType.Code = "NOTIFICATION";
                    _generic_NotificationMessageType.CodeSystem = "GENERIC";
                    _generic_NotificationMessageType.Meaning = "Generic notification message (request message without meaningful response) without particular schema specification.";
                }
                return _generic_NotificationMessageType;
            }
        }

        private static MessageType _generic_RequestMessageType;
        public static MessageType GENERIC_RequestMessageType
        {
            get
            {
                if (_generic_RequestMessageType == null)
                {
                    _generic_RequestMessageType = new MessageType();
                    _generic_RequestMessageType.Code = "REQUEST";
                    _generic_RequestMessageType.CodeSystem = "GENERIC";
                    _generic_RequestMessageType.Meaning = "Generic request message without particular schema specification.";
                }
                return _generic_RequestMessageType;
            }
        }

        private static MessageType _generic_ResponseMessageType;
        public static MessageType GENERIC_ResponseMessageType
        {
            get
            {
                if (_generic_ResponseMessageType == null)
                {
                    _generic_ResponseMessageType = new MessageType();
                    _generic_ResponseMessageType.Code = "RESPONSE";
                    _generic_ResponseMessageType.CodeSystem = "GENERIC";
                    _generic_ResponseMessageType.Meaning = "Generic response message without particular schema specification.";
                }
                return _generic_ResponseMessageType;
            }
        }
    }
}
