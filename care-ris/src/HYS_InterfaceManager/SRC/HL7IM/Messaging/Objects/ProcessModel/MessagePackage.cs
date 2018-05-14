using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Messaging.Objects;

namespace HYS.Messaging.Objects.ProcessModel
{
    public class MessagePackage
    {
        public MessagePackage(Message msg)
        {
            _message = msg;
        }

        public Message _message;
        public Message Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string _xmlString;
        public string XmlString
        {
            get { return _xmlString; }
            set { _xmlString = value; }
        }

        public XmlDocument _xmlDocument;
        public XmlDocument XmlDocument
        {
            get { return _xmlDocument; }
            set { _xmlDocument = value; }
        }

        public XmlTextReader _xmlReader;
        public XmlTextReader XmlReader
        {
            get { return _xmlReader; }
            set { _xmlReader = value; }
        }
    }
}
