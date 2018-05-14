using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageType : XObject
    {
        public MessageType()
        {
        }
        public MessageType(string codeSchema, string code)
        {
            CodeSystem = codeSchema;
            Code = code;
        }

        private string _codeSystem = "";
        public string CodeSystem
        {
            get { return _codeSystem; }
            set { _codeSystem = value; }
        }

        private string _code = "";
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _meaning = "";
        [XCData(true)]
        public string Meaning
        {
            get { return _meaning; }
            set { _meaning = value; }
        }

        private MessageSchema _schema;
        /// <summary>
        /// The schema information do not need to be serialized into message xml.
        /// Developer can query metadata repository for message schema according to message type code.
        /// </summary>
        [XNode(false)]
        public MessageSchema Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public static char Spliter = ':';
        public override string ToString()
        {
            if (_codeSystem != null && _codeSystem.Length > 0)
            {
                return _codeSystem + Spliter + _code;
            }
            else
            {
                return _code;
            }
        }
        public bool EqualsTo(MessageType type)
        {
            if (type == null) return false;
            return type._code == _code && type._codeSystem == _codeSystem;
        }
        public static MessageType Parse(string str)
        {
            if (str == null || str.Length < 1) return null;
            string[] slist = str.Split(Spliter);

            MessageType t = new MessageType();
            if (slist.Length > 0) t.CodeSystem = slist[0];
            if (slist.Length > 1) t.Code = slist[1];
            return t;
        }
    }
}
