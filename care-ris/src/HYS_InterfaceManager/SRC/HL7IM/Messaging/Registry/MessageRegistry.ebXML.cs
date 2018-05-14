using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        #region message types definition for IHE XDS Integration Profile

        private static MessageType _xdsa_DocumentSubmitMessageType;
        public static MessageType XDSa_DocumentSubmitMessageType
        {
            get
            {
                if (_xdsa_DocumentSubmitMessageType == null)
                {
                    _xdsa_DocumentSubmitMessageType = new MessageType();
                    _xdsa_DocumentSubmitMessageType.Code = "DOC_SUBMIT";
                    _xdsa_DocumentSubmitMessageType.CodeSystem = "XDSA";
                    _xdsa_DocumentSubmitMessageType.Meaning = "Submitting document metadata in IHE ebXML v2.1 schema.";
                }
                return _xdsa_DocumentSubmitMessageType;
            }
        }

        private static MessageType _xdsa_DocumentRegisterMessageType;
        public static MessageType XDSa_DocumentRegisterMessageType
        {
            get
            {
                if (_xdsa_DocumentRegisterMessageType == null)
                {
                    _xdsa_DocumentRegisterMessageType = new MessageType();
                    _xdsa_DocumentRegisterMessageType.Code = "DOC_REGISTER";
                    _xdsa_DocumentRegisterMessageType.CodeSystem = "XDSA";
                    _xdsa_DocumentRegisterMessageType.Meaning = "Document registration request in IHE ebXML v2.1 schema";

                }
                return _xdsa_DocumentRegisterMessageType;
            }
        }

        private static MessageType _xdsa_DocumentRegisterResponseMessageType;
        public static MessageType XDSa_DocumentRegisterResponseMessageType
        {
            get
            {
                if (_xdsa_DocumentRegisterResponseMessageType == null)
                {
                    _xdsa_DocumentRegisterResponseMessageType = new MessageType();
                    _xdsa_DocumentRegisterResponseMessageType.Code = "DOC_REGISTER_RSP";
                    _xdsa_DocumentRegisterResponseMessageType.CodeSystem = "XDSA";
                    _xdsa_DocumentRegisterResponseMessageType.Meaning = "Document registration response in IHE ebXML v2.1 schema";
                }
                return _xdsa_DocumentRegisterResponseMessageType;
            }
        }

        private static MessageType _xdsb_DocumentSubmitMessageType;
        public static MessageType XDSb_DocumentSubmitMessageType
        {
            get
            {
                if (_xdsb_DocumentSubmitMessageType == null)
                {
                    _xdsb_DocumentSubmitMessageType = new MessageType();
                    _xdsb_DocumentSubmitMessageType.Code = "DOC_SUBMIT";
                    _xdsb_DocumentSubmitMessageType.CodeSystem = "XDSB";
                    _xdsb_DocumentSubmitMessageType.Meaning = "Submitting document metadata in IHE ebXML v3.0 schema.";
                }
                return _xdsb_DocumentSubmitMessageType;
            }
        }

        private static MessageType _xdsb_DocumentRegisterMessageType;
        public static MessageType XDSb_DocumentRegisterMessageType
        {
            get
            {
                if (_xdsb_DocumentRegisterMessageType == null)
                {
                    _xdsb_DocumentRegisterMessageType = new MessageType();
                    _xdsb_DocumentRegisterMessageType.Code = "DOC_REGISTER";
                    _xdsb_DocumentRegisterMessageType.CodeSystem = "XDSB";
                    _xdsb_DocumentRegisterMessageType.Meaning = "Document registration request in IHE ebXML v3.0 schema";
                }
                return _xdsb_DocumentRegisterMessageType;
            }
        }

        private static MessageType _xdsb_DocumentRegisterResponseMessageType;
        public static MessageType XDSb_DocumentRegisterResponseMessageType
        {
            get
            {
                if (_xdsb_DocumentRegisterResponseMessageType == null)
                {
                    _xdsb_DocumentRegisterResponseMessageType = new MessageType();
                    _xdsb_DocumentRegisterResponseMessageType.Code = "DOC_REGISTER_RSP";
                    _xdsb_DocumentRegisterResponseMessageType.CodeSystem = "XDSB";
                    _xdsb_DocumentRegisterResponseMessageType.Meaning = "Document registration response in IHE ebXML v3.0 schema";
                }
                return _xdsb_DocumentRegisterResponseMessageType;
            }
        }

        #endregion
    }
}
