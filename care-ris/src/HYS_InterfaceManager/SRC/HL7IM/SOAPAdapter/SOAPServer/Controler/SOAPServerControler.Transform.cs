using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using System.IO;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    public partial class SOAPServerControler
    {
        public bool GenerateSOAPMessage(Message msg, out string soapEnvelope)
        {
            soapEnvelope = "";

            if (msg == null || msg.Header == null) return false;
            string msgID = msg.Header.ID.ToString();

            switch (_context.ConfigMgr.Config.OutboundProcessing.Model)
            {
                case MessageProcessModel.Direct:
                    {
                        _context.Log.Write(string.Format("Use the message body as SOAP envelop directly. Message ID: {0}", msgID));
                        soapEnvelope = msg.Body;
                        return true;
                    }
                case MessageProcessModel.Xslt:
                    {
                        string strSoap = "";
                        string strMsg = msg.ToXMLString();

                        _context.Log.Write(string.Format("Begin transforming XDS Gateway message to SOAP message. Message ID: {0}", msgID));

                        XMLTransformer t = XMLTransformer.CreateFromFileWithCache
                            (_context.ConfigMgr.Config.GetXSLTFileFullPath_XDSGatewayMessageToSOAPMessage(),
                            _context.Log);

                        bool res = (t != null &&
                            t.TransformString(strMsg, ref strSoap,
                            _context.ConfigMgr.Config.OutboundProcessing.XSLTExtensions));

                        _context.Log.Write(string.Format("End transforming XDS Gateway message to SOAP message. Message ID: {0}. Result: {1}", msgID, res));

                        soapEnvelope = strSoap;
                        return res;
                    }
            }

            return false;
        }

        public bool GenerateXDSGWMessage(string soapEnvelope, out Message msg)
        {
            string xdsgwMsg;
            return GenerateXDSGWMessage(soapEnvelope, out msg, out xdsgwMsg, true, false);
        }
        public bool GenerateXDSGWMessage(string soapEnvelope, out string xdsgwMsg)
        {
            Message msg;
            return GenerateXDSGWMessage(soapEnvelope, out msg, out xdsgwMsg, false, true);
        }
        public bool GenerateXDSGWMessage(string soapEnvelope, out Message msg, out string xdsgwMsg)
        {
            return GenerateXDSGWMessage(soapEnvelope, out msg, out xdsgwMsg, true, true);
        }
        private bool GenerateXDSGWMessage(string soapEnvelope, out Message msg, out string xdsgwMsg, bool returnMsgObject, bool returnMsgString)
        {
            msg = null;
            xdsgwMsg = "";

            if (soapEnvelope == null || soapEnvelope.Length < 1) return false;

            switch (_context.ConfigMgr.Config.InboundProcessing.Model)
            {
                case MessageProcessModel.Direct:
                    {
                        msg = new Message();
                        msg.Header.ID = Guid.NewGuid();
                        //msg.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;    // do not need to set message type here, set message type when dispatching messages.
                        msg.Body = Message.RemoveXmlHeader(soapEnvelope);

                        if (returnMsgString) xdsgwMsg = msg.ToXMLString();

                        _context.Log.Write(string.Format("Use the SOAP envelope as XDS Gateway message body directly. Message ID: {0}", msg.Header.ID.ToString()));

                        return true;
                    }
                case MessageProcessModel.Xslt:
                    {
                        string strMsg = "";

                        _context.Log.Write("Begin transforming SOAP message to XDS Gateway message. ");

                        XMLTransformer t = XMLTransformer.CreateFromFileWithCache
                            (_context.ConfigMgr.Config.GetXSLTFileFullPath_SOAPMessageToXDSGatewayMessage(),
                            _context.Log);

                        bool res = (t != null &&
                            t.TransformString(soapEnvelope, ref strMsg,
                            _context.ConfigMgr.Config.InboundProcessing.XSLTExtensions));

                        _context.Log.Write(string.Format("End transforming SOAP message to XDS Gateway message. Result: {0}", res));

                        if (!res) return false;

                        if (returnMsgString)
                        {
                            xdsgwMsg = strMsg;
                            return true;
                        }
                        else if (returnMsgObject)
                        {
                            msg = CreateXDSGWMessage(strMsg);
                            return msg != null;
                        }

                        return true;
                    }
            }

            return false;
        }

        public Message CreateXDSGWMessage(string xdsgwMsg)
        {
            Message msg = XObjectManager.CreateObject<Message>(xdsgwMsg);

            if (msg == null)
            {
                _context.Log.Write(LogType.Error, string.Format("Deserialize XDS Gateway message failed. Please check whether the following XML string conforms to XDS Gateway message schema: \r\n{0}", xdsgwMsg));
            }
            else
            {
                msg.Header.ID = Guid.NewGuid();
                //msg.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;    // do not need to set message type here, set message type when dispatching messages.

                _context.Log.Write(string.Format("Deserialize and create XDS Gateway message success. Message ID: {0}", msg.Header.ID.ToString()));
            }

            return msg;
        }
    }
}
