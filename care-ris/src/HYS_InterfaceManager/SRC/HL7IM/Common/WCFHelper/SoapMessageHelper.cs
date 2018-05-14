using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;

namespace HYS.IM.Common.WCFHelper
{
    public class SoapMessageHelper
    {
        private static EnvelopeVersion GetEnvelopVersion(SoapEnvelopeVersion soapVersion)
        {
            switch (soapVersion)
            {
                case SoapEnvelopeVersion.Soap11: return EnvelopeVersion.Soap11;
                case SoapEnvelopeVersion.Soap12: return EnvelopeVersion.Soap12;
                case SoapEnvelopeVersion.None: return EnvelopeVersion.None;
                default: return null;
            }
        }
        private static AddressingVersion GetAddressingVersion(WSAddressingVersion wsaddressVersion)
        {
            switch (wsaddressVersion)
            {
                case WSAddressingVersion.WSAddressing10: return AddressingVersion.WSAddressing10;
                case WSAddressingVersion.WSAddressingAugust2004: return AddressingVersion.WSAddressingAugust2004;
                case WSAddressingVersion.None: return AddressingVersion.None;
                default: return null;
            }
        }
        private static SoapEnvelopeVersion GetSoapEnvelopeVersion(EnvelopeVersion version)
        {
            if (version == EnvelopeVersion.Soap11) return SoapEnvelopeVersion.Soap11;
            else if (version == EnvelopeVersion.Soap12) return SoapEnvelopeVersion.Soap12;
            else return SoapEnvelopeVersion.None;
        }
        private static WSAddressingVersion GetWSAddressingVersion(AddressingVersion version)
        {
            if (version == AddressingVersion.WSAddressing10) return WSAddressingVersion.WSAddressing10;
            else if (version == AddressingVersion.WSAddressingAugust2004) return WSAddressingVersion.WSAddressingAugust2004;
            else return WSAddressingVersion.None;
        }


        public static Message CreateWCFMessage(SoapMessageBase message, string action)
        {
            if (message == null) return null;
            return CreateWCFMessage(message.SoapEnvelopeVersion,
                                    message.WSAddressingVersion,
                                    action,
                                    message.SoapEnvelopeBodyContent);
        }

        public static Message CreateWCFMessage(SoapEnvelopeVersion soapVersion,
                                            WSAddressingVersion wsaddressVersion,
                                            string action,
                                            string body)
        {
            EnvelopeVersion eVersion = GetEnvelopVersion(soapVersion);
            AddressingVersion aVersion = GetAddressingVersion(wsaddressVersion);
            MessageVersion mVersion = MessageVersion.CreateVersion(eVersion, aVersion);
            //Note: the XmlReader should not be dispose during the message lifecyle
            Message msg = Message.CreateMessage(mVersion, action, XmlReader.Create(new StringReader(body)));
            return msg;
        }

        public static Message CreateWCFMessageWithEnvelope(SoapEnvelopeVersion soapVersion,
                                            WSAddressingVersion wsaddressVersion,
                                            string action,
                                            string envelope)
        {
            EnvelopeVersion eVersion = GetEnvelopVersion(soapVersion);
            AddressingVersion aVersion = GetAddressingVersion(wsaddressVersion);
            MessageVersion mVersion = MessageVersion.CreateVersion(eVersion, aVersion);
            //Note: the XmlReader should not be dispose during the message lifecyle
            Message msg = Message.CreateMessage(XmlReader.Create(new StringReader(envelope)), 99999999, mVersion);
            msg.Headers.Action = action;
            return msg;
        }

        public static Message CreateEmptyWCFMessage(SoapEnvelopeVersion soapVersion,
                                            WSAddressingVersion wsaddressVersion,
                                            string action)
        {
            EnvelopeVersion eVersion = GetEnvelopVersion(soapVersion);
            AddressingVersion aVersion = GetAddressingVersion(wsaddressVersion);
            MessageVersion mVersion = MessageVersion.CreateVersion(eVersion, aVersion);
            Message msg = Message.CreateMessage(mVersion, action);
            return msg;
        }

        public static void ParseWCFMessage(SoapMessageBase message, Message wcfMsg)
        {
            if (message == null || wcfMsg == null || wcfMsg.Version == null) return;
            message.SoapEnvelopeVersion = GetSoapEnvelopeVersion(wcfMsg.Version.Envelope);
            message.WSAddressingVersion = GetWSAddressingVersion(wcfMsg.Version.Addressing);

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter xw = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8))
                {
                    wcfMsg.WriteBodyContents(xw);   //it can automatically handle xml namespace, such as moving the necessary namespace declaration from envelope to body content.
                    xw.Close();
                }
                string str = Encoding.UTF8.GetString(ms.GetBuffer());
                ms.Close();

                message.SoapEnvelopeBodyContent = str.TrimEnd('\0');
            }
        }

        public static string DumpWCFMessage(Message wcfMsg)
        {
            string str = string.Empty;
            if (wcfMsg == null) return str;

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter xw = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8))
                {
                    wcfMsg.WriteMessage(xw);
                    xw.Close();
                }
                str = Encoding.UTF8.GetString(ms.GetBuffer());
                ms.Close();
            }

            //Sometimes there may be planty of \0 charater at the end of the string dumped from a incoming message,
            return str.TrimEnd('\0');
        }
    }
}
