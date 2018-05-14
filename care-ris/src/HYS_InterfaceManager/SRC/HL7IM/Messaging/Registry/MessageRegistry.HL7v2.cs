using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        #region message types definition for generic HL7v2 XML communication

        private static MessageType _hl7V2XML_NotificationMessageType;
        public static MessageType HL7V2XML_NotificationMessageType
        {
            get
            {
                if (_hl7V2XML_NotificationMessageType == null)
                {
                    _hl7V2XML_NotificationMessageType = new MessageType();
                    _hl7V2XML_NotificationMessageType.Code = "NOTIFICATION";
                    _hl7V2XML_NotificationMessageType.CodeSystem = "HL7V2XML";
                    _hl7V2XML_NotificationMessageType.Meaning = "HL7 notification message in HL7v2 XML schema.";
                }
                return _hl7V2XML_NotificationMessageType;
            }
        }

        private static MessageType _hl7V2XML_QueryRequestMessageType;
        public static MessageType HL7V2XML_QueryRequestMessageType
        {
            get
            {
                if (_hl7V2XML_QueryRequestMessageType == null)
                {
                    _hl7V2XML_QueryRequestMessageType = new MessageType();
                    _hl7V2XML_QueryRequestMessageType.Code = "QUERY_REQUEST";
                    _hl7V2XML_QueryRequestMessageType.CodeSystem = "HL7V2XML";
                    _hl7V2XML_QueryRequestMessageType.Meaning = "HL7 query request message in HL7v2 XML schema.";
                }
                return _hl7V2XML_QueryRequestMessageType;
            }
        }

        private static MessageType _hl7V2XML_QueryResultMessageType;
        public static MessageType HL7V2XML_QueryResultMessageType
        {
            get
            {
                if (_hl7V2XML_QueryResultMessageType == null)
                {
                    _hl7V2XML_QueryResultMessageType = new MessageType();
                    _hl7V2XML_QueryResultMessageType.Code = "QUERY_RESULT";
                    _hl7V2XML_QueryResultMessageType.CodeSystem = "HL7V2XML";
                    _hl7V2XML_QueryResultMessageType.Meaning = "HL7 query response message in HL7v2 XML schema.";
                }
                return _hl7V2XML_QueryResultMessageType;
            }
        }

        #endregion
    }
}
