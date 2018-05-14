using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Adapters;
//using HYS.IM.EMRMessages;
using HYS.IM.Common.HL7v2.Encoding;
using HYS.IM.Messaging.Registry;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Messaging.Base.Config;
using HYS.Common.Xml;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Controler
{
    /// <summary>
    /// This class contains major/core function (business logic implementation) of this message entity.
    /// 
    /// Note: The HL7OutboundControler need to hold the instance of the SocketClient,
    /// in order to lock the SocketClient.SendData() method against calling from multiple threads.
    /// </summary>
    public class HL7OutboundControler
    {
        private IClient _client;
        private EntityImpl _entity;
        private XmlTransformerBase _transformer;

        public HL7OutboundControler(EntityImpl entity)
        {
            _entity = entity;
            _client = SocketClientFactory.Create(_entity.Context.ConfigMgr.Config.SocketConfig);
            _transformer = XmlTransformerFactory.CreateTransformer(_entity.Context.ConfigMgr.Config.HL7XMLTransformerType, _entity.Context.Log);
        }

        public bool Start()
        {
            return _client.Open();
        }
        public bool Stop()
        {
            return _client.Close();
        }

        public bool SendMLLPNotification(string hl7msg)
        {
            _entity.Context.Log.Write("Begin sending HL7 notification.");
            SocketResult ret = _client.SendData(hl7msg);
            _entity.Context.Log.Write(string.Format("End sending HL7 notification. Result: {0}.", ret.Type.ToString()));
            //SendLogMessage(hl7msg, ret);
            return ret.Type == SocketResultType.Success;
        }
        public bool SendMLLPQuery(string query, out string result)
        {
            _entity.Context.Log.Write("Begin sending HL7 query.");
            SocketResult ret = _client.SendData(query);
            _entity.Context.Log.Write(string.Format("End sending HL7 query. Result: {0}.", ret.Type.ToString()));
            result = ret.ReceivedString;
            //SendLogMessage(query, ret);
            return ret.Type == SocketResultType.Success;
        }
        
        public bool SendHL7v2XMLNotification(string hl7xml)
        {
            bool res = false;
            if (hl7xml == null || hl7xml.Length < 1) return res;

            string hl7text = "";
            _entity.Context.Log.Write("Begin transforming XML to HL7v2.");
            res = _transformer.TransformXmlToHL7v2(hl7xml, out hl7text);
            _entity.Context.Log.Write(string.Format("End transforming XML to HL7v2. Result: {0}.", res));
            
            if (!res)
            {
                _entity.Context.Log.Write(LogType.Error, "XML dump:\r\n" + hl7xml);
                return res;
            }

            res = SendMLLPNotification(hl7text.Replace("\r\n", "\r"));
            return res;
        }
        public bool SendHL7v2XMLQuery(string queryXml, out string resultXml)
        {
            resultXml = "";
            bool res = false;
            if (queryXml == null || queryXml.Length < 1) return res;

            string queryText = "";
            _entity.Context.Log.Write("Begin transforming requesting XML to HL7v2.");
            res = _transformer.TransformXmlToHL7v2(queryXml, out queryText);
            _entity.Context.Log.Write(string.Format("End transforming requesting XML to HL7v2. Result: {0}.", res));
            
            if (!res)
            {
                _entity.Context.Log.Write(LogType.Error, "XML dump:\r\n" + queryXml);
                return res;
            }

            string resultText = "";
            res = SendMLLPQuery(queryText.Replace("\r\n", "\r"), out resultText);
            if (!res) return res;

            _entity.Context.Log.Write("Begin transforming responsing HL7v2 to XML.");
            res = _transformer.TransformHL7v2ToXml(resultText, out resultXml);
            _entity.Context.Log.Write(string.Format("End transforming responsing HL7v2 to XML. Result: {0}.", res));
            return res;
        }

        public bool SendOtherXMLNotification(Message msg)
        {
            bool res = false;
            if (msg == null) return res;

            //string toXml = null;
            //_entity.Context.Log.Write("Begin transforming notification message to transport schema.");
            //string xslFile = ConfigHelper.GetFullPath(_entity.Context.AppArgument.ConfigFilePath, HL7OutboundConfig.XSLTFileName_XDSGatewayMessageToMLLPXMLMessage);
            //XMLTransformer t = XMLTransformer.CreateFromFileWithCache(xslFile, _entity.Context.Log);
            //if (t != null)
            //{
            //    string fromXml = msg.ToXMLString();
            //    res = t.TransformString(fromXml, ref toXml, _entity.Context.ConfigMgr.Config.MessageProcessingXSLTExtensions);
            //}
            //_entity.Context.Log.Write(string.Format("End transforming notification message to transport schema. Result: {0}.", res));
            //if (!res) return res;

            string toXml = msg.Body;
            res = SendMLLPNotification(toXml);
            return res;
        }
        public bool SendOtherXMLQuery(Message reqMsg, out Message rspMsg)
        {
            rspMsg = null;
            bool res = false;
            if (reqMsg == null) return res;

            //string sndXml = null;
            //_entity.Context.Log.Write("Begin transforming request message to transport schema.");
            //string outXsltFile = ConfigHelper.GetFullPath(_entity.Context.AppArgument.ConfigFilePath, HL7OutboundConfig.XSLTFileName_XDSGatewayMessageToMLLPXMLMessage);
            //XMLTransformer ot = XMLTransformer.CreateFromFileWithCache(outXsltFile, _entity.Context.Log);
            //if (ot != null)
            //{
            //    string fromXml = reqMsg.ToXMLString();
            //    res = ot.TransformString(fromXml, ref sndXml, _entity.Context.ConfigMgr.Config.MessageProcessingXSLTExtensions);
            //}
            //_entity.Context.Log.Write(string.Format("End transforming request message to transport schema. Result: {0}.", res));
            //if (!res) return res;

            string sndXml = reqMsg.Body;

            string rcvXml = "";
            res = SendMLLPQuery(sndXml, out rcvXml);
            if (!res) return res;

            //_entity.Context.Log.Write("Begin transforming response message from transport schema.");
            //string inXsltFile = ConfigHelper.GetFullPath(_entity.Context.AppArgument.ConfigFilePath, HL7OutboundConfig.XSLTFileName_MLLPXMLMessageToXDSGatewayMessage);
            //XMLTransformer it = XMLTransformer.CreateFromFileWithCache(inXsltFile, _entity.Context.Log);
            //if (it != null)
            //{
            //    string toXml = null;
            //    res = it.TransformString(rcvXml, ref toXml, _entity.Context.ConfigMgr.Config.MessageProcessingXSLTExtensions);
            //    if (res) rspMsg = XObjectManager.CreateObject<Message>(toXml);
            //}
            //_entity.Context.Log.Write(string.Format("End transforming response message from transport schema. Result: {0}.", res));
            //return rspMsg != null;

            rspMsg = new Message();
            rspMsg.Body = rcvXml;
            return res;
        }
    }
}
