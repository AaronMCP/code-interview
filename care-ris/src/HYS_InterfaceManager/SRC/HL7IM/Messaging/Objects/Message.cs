using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class Message : XObject
    {
        private MessageHeader _header = new MessageHeader();
        public MessageHeader Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private string _body = "";
        //[XCData(true)]    
        /// <summary>
        /// CDATA cannot be nesting, in order to complain with XML document with CDATA,
        /// Body should not be defined as CDATA, but body content should be well formatted XML
        /// </summary>
        [XRawXmlString(true)]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private MessageReference _reference = new MessageReference();
        public MessageReference Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        private object _context;    //MessageContext
        [XNode(false)]
        public object Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private const string XmlHeaderEnd = "?>";
        private const string XmlHeaderStart = "<?";
        public static string RemoveXmlHeader(string str)
        {
            if (str.StartsWith(XmlHeaderStart))
            {
                int index = str.IndexOf(XmlHeaderEnd);
                return str.Substring(index + XmlHeaderEnd.Length);
            }
            else
            {
                return str;
            }
        }

        // the following optimization is to:
        // - avoid XML serialization/deserialization in LPC communication
        // - faciliate serializing/deserializing body content of the message 

        [XNode(false)]
        public XObject BodyObject { get; set; }
        public string BodyObjectTypeName { get; set; }
        public void SetBodyObject(XObject obj)
        {
            if (obj == null) return;
            BodyObject = obj;
        }
        public void SetBodyObject(XObject obj, Type serializeAccordingToType)
        {
            if (obj == null) return;
            BodyObject = obj;
            if (serializeAccordingToType == null) return;
            BodyObjectTypeName = serializeAccordingToType.AssemblyQualifiedName;
        }
        public XObject GetBodyObject()
        {
            if (BodyObject != null) return BodyObject;

            if (BodyObjectTypeName == null) return null;
            Type t = Type.GetType(BodyObjectTypeName, false, false);
            if (t == null) return null;

            return BodyObject = XObjectManager.CreateObject(Body, t) as XObject;
        }

        protected override object GetValueEx(string name)
        {
            if (name != "Body") return base.GetValueEx(name);

            if (BodyObject != null)
            {
                if (string.IsNullOrEmpty(BodyObjectTypeName))
                {
                    BodyObjectTypeName = BodyObject.GetType().AssemblyQualifiedName;
                    return BodyObject.ToXMLString();
                }
                else
                {
                    // to support SetBodyObject(XObject obj, Type serializeAccordingToType)
                    // e.g. SetBodyObject(T_Demography, T_Doc_Meta)

                    Type t = Type.GetType(BodyObjectTypeName, false, false);
                    return BodyObject.ToXMLString(t);
                }
            }

            return Body;
        }
    }
}
