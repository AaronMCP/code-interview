using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Config;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using System.IO;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler
{
    public partial class SOAPClientControler
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
                        return true;
                    }
            }

            return false;
        }
        public bool GenerateXDSGWMessage(string soapEnvelope, out Message msg)
        {
            msg = null;

            if (soapEnvelope == null || soapEnvelope.Length < 1) return false;

            switch (_context.ConfigMgr.Config.InboundProcessing.Model)
            {
                case MessageProcessModel.Direct:
                    {
                        msg = new Message();
                        msg.Header.ID = Guid.NewGuid();
                        msg.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                        msg.Body = Message.RemoveXmlHeader(soapEnvelope);

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

                        msg = XObjectManager.CreateObject<Message>(strMsg);

                        if (msg == null)
                        {
                            _context.Log.Write(LogType.Error, string.Format("Deserialize XDS Gateway message failed. Please check whether the following XML string conforms to XDS Gateway message schema: \r\n{0}", strMsg));
                            return false;
                        }
                        else
                        {
                            msg.Header.ID = Guid.NewGuid();
                            msg.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;

                            _context.Log.Write(string.Format("Deserialize and create XDS Gateway message success. Message ID: {0}", msg.Header.ID.ToString()));
                            return true;
                        }
                    }
            }

            return false;
        }
    }
}
