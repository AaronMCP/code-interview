using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        private static MessageType _dicomXML_QueryRequestMessageType;
        public static MessageType DICOMXML_QueryRequestMessageType
        {
            get
            {
                if (_dicomXML_QueryRequestMessageType == null)
                {
                    _dicomXML_QueryRequestMessageType = new MessageType();
                    _dicomXML_QueryRequestMessageType.Code = "QUERY_REQUEST";
                    _dicomXML_QueryRequestMessageType.CodeSystem = "DICOMXML";
                    _dicomXML_QueryRequestMessageType.Meaning = "DICOM query request message in XML schema.";
                }
                return _dicomXML_QueryRequestMessageType;
            }
        }

        private static MessageType _dicomXML_QueryResultMessageType;
        public static MessageType DICOMXML_QueryResultMessageType
        {
            get
            {
                if (_dicomXML_QueryResultMessageType == null)
                {
                    _dicomXML_QueryResultMessageType = new MessageType();
                    _dicomXML_QueryResultMessageType.Code = "QUERY_RESULT";
                    _dicomXML_QueryResultMessageType.CodeSystem = "DICOMXML";
                    _dicomXML_QueryResultMessageType.Meaning = "DICOM query response message in XML schema.";
                }
                return _dicomXML_QueryResultMessageType;
            }
        }
    }
}
