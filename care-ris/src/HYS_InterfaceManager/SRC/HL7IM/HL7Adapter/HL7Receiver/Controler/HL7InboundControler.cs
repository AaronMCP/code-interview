using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Adapters;
using HYS.IM.Messaging.Objects;
//using HYS.IM.EMRMessages;
using HYS.IM.Common.HL7v2.Encoding;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config;
using System.IO;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Mapping.Transforming;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler
{
    /// <summary>
    /// This class contains major/core function (business logic implementation) of this message entity.
    /// </summary>
    public partial class HL7InboundControler
    {
        private IServer _server;
        private EntityImpl _entity;
        private XmlTransformerBase _transformer;

        private bool ProcessHL7Message(string receiveData, ref string sendData)
        {
            try
            {
                _entity.Context.Log.Write("Begin processing HL7v2 text message");

                string hl7msgType = HL7MessageParser.GetField(receiveData, "MSH", 9);
                _entity.Context.Log.Write("Dispatching message according to message type: " + hl7msgType);

                if (hl7msgType.IndexOf("QBP") >= 0)
                {
                    Message req = new Message();
                    //req.Header.Type = MessageRegistry.HL7V2_QueryRequestMessageType;
                    req.Header.Type = MessageRegistry.GENERIC_RequestMessageType;
                    HYS.IM.Messaging.Registry.HL7MessageHelper.SetHL7V2PayLoad(req, receiveData);

                    Message rsp;
                    _entity.Context.Log.Write("Begin sending request to responser.");
                    bool ret = _entity.NotifyMessageRequest(req, out rsp);
                    _entity.Context.Log.Write("End sending request to responser. Result: " + ret.ToString());
                    _entity.Context.ConfigMgr.Config.DumpMessage(req, receiveData, false, ret);

                    if (ret)
                    {
                        sendData = HYS.IM.Messaging.Registry.HL7MessageHelper.GetHL7V2PayLoad(rsp);
                        return true;
                    }
                    else
                    {
                        sendData = "";
                        return false;
                    }
                }
                else
                {
                    Message notify = new Message();
                    //notify.Header.Type = MessageRegistry.HL7V2_NotificationMessageType;
                    notify.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
                    HYS.IM.Messaging.Registry.HL7MessageHelper.SetHL7V2PayLoad(notify, receiveData);

                    _entity.Context.Log.Write("Begin sending notification to subscriber.");
                    bool ret = _entity.NotifyMessagePublish(notify);
                    _entity.Context.Log.Write("End sending notification to subscriber. Result: " + ret.ToString());
                    _entity.Context.ConfigMgr.Config.DumpMessage(notify, receiveData, false, ret);

                    //if (ret)
                    //{
                    //    sendData = HL7MessageParser.FormatResponseMessage(receiveData,
                    //        _entity.Context.ConfigMgr.Config.ReadHL7AckAATemplate());
                    //}
                    //else
                    //{
                    //    sendData = HL7MessageParser.FormatResponseMessage(receiveData,
                    //        _entity.Context.ConfigMgr.Config.ReadHL7AckAETemplate());
                    //}

                    string xsltFileName = ret ? HL7InboundConfig.PublishingSuccessXSLTFileName : HL7InboundConfig.PublishingFailureXSLTFileName;
                    string reqXml = null;
                    string rspXml = null;
                    sendData = "";
                    ret = false;

                    if (_transformer.TransformHL7v2ToXml(receiveData, out reqXml))
                    {
                        if (_entity.Context.ConfigMgr.Config.XSLTTransform(reqXml, ref rspXml, xsltFileName,
                            _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions)
                            && (!string.IsNullOrEmpty(rspXml)))
                        {
                            if (_transformer.TransformXmlToHL7v2(rspXml, out sendData)
                                && (!string.IsNullOrEmpty(sendData)))
                            {
                                sendData = sendData.Replace("\r\n", "\r");
                                ret = true;
                            }
                            else
                            {
                                _entity.Context.Log.Write(LogType.Error, "Transform outgoing XML to outgoing HL7v2 text failed.\r\n" + rspXml);
                            }
                        }
                        else
                        {
                            _entity.Context.Log.Write(LogType.Error, "Transform incoming XML to outgoing XML failed.\r\n" + reqXml);
                        }
                    }
                    else
                    {
                        _entity.Context.Log.Write(LogType.Error, "Transform incoming HL7v2 text to incoming XML failed.");
                    }

                    _entity.Context.Log.Write("End processing HL7v2 text message. Result: " + ret.ToString());
                    return ret;
                }
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
                return false;
            }
        }
        private bool ProcessHL7XMLMessage(string receiveData, ref string sendData)
        {
            try
            {
                _entity.Context.Log.Write(string.Format("Begin processing HL7v2 text message with XML in thread ID: {0}",
                    System.Threading.Thread.CurrentThread.ManagedThreadId));

                bool res = false;
                string reqXml = "";
                _entity.Context.Log.Write("Begin transforming incoming HL7v2 to XML");
                res = _transformer.TransformHL7v2ToXml(receiveData, out reqXml);
                _entity.Context.Log.Write(string.Format("End transforming incoming HL7v2 to XML. Result: {0}", res));
                
                if (!res)
                {
                    _entity.Context.ConfigMgr.Config.DumpMessage(null, receiveData, false, false);
                    return res;
                }

                res = DispatchHL7XMLMessage(receiveData, reqXml, ref sendData);
                if (res == false || string.IsNullOrEmpty(sendData)) //sendData == null || sendData.Length < 1)
                {
                    sendData = "";
                    //sendData = HL7MessageParser.FormatResponseMessage(receiveData,
                    //        _entity.Context.ConfigMgr.Config.ReadHL7AckAETemplate());
                }

                _entity.Context.Log.Write(string.Format("End processing HL7v2 text message with XML in thread ID: {1}. Result: {0}.",
                    res, System.Threading.Thread.CurrentThread.ManagedThreadId));
                return res;
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
                return false;
            }
        }
        private bool ProcessOtherXMLMessage(string receiveData, ref string sendData)
        {
            try
            {
                _entity.Context.Log.Write(string.Format("Begin processing MLLP XML message in thread ID: {0}",
                   System.Threading.Thread.CurrentThread.ManagedThreadId));

                //bool res = false;
                //string incomingXdsgwXml = null;
                //_entity.Context.Log.Write("Begin transforming incoming MLLP XML to XDSGW message");
                //string incomingXslFile = ConfigHelper.GetFullPath(_entity.Context.AppArgument.ConfigFilePath, HL7InboundConfig.XSLTFileName_MLLPXMLMessageToXDSGatewayMessage);
                //XMLTransformer it = XMLTransformer.CreateFromFileWithCache(incomingXslFile, _entity.Context.Log);
                //if (it != null) res = it.TransformString(receiveData, ref incomingXdsgwXml, _entity.Context.ConfigMgr.Config.MessageProcessingXSLTExtensions);
                //_entity.Context.Log.Write(string.Format("End transforming incoming MLLP XML to XDSGW message. Result: {0}", res));
                //if (!res) return res;

                bool res = false;
                res = DispatchOtherXMLMessage(receiveData, ref sendData);
                if (res == false || sendData == null || sendData.Length < 1)
                {
                    res = _entity.Context.ConfigMgr.Config.XSLTTransform(receiveData, ref sendData, HL7InboundConfig.PublishingFailureXSLTFileName,
                            _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions);
                }

                _entity.Context.Log.Write(string.Format("End processing MLLP XML message in thread ID: {1}. Result: {0}.",
                    res, System.Threading.Thread.CurrentThread.ManagedThreadId));
                return res;
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
                return false;
            }
        }
        private bool _server_OnRequest(string receiveData, ref string sendData)
        {
            bool res = false;

            string incomingMessage = receiveData;
            if (_entity.Context.ConfigMgr.Config.MessagePreprocessing.Enable)
                incomingMessage = _entity.Context.ConfigMgr.Config.MessagePreprocessing.Replace(incomingMessage);

            switch (_entity.Context.ConfigMgr.Config.MessageProcessingType)
            {
                case MessageProcessType.HL7v2Text:
                    res = ProcessHL7Message(incomingMessage, ref sendData);
                    break;
                case MessageProcessType.HL7v2XML:
                    res = ProcessHL7XMLMessage(incomingMessage, ref sendData);
                    break;
                case MessageProcessType.OtherXML:
                    res = ProcessOtherXMLMessage(XmlHelper.EatXmlDeclaration(incomingMessage), ref sendData);
                    break;
            }

            //if (_entity.Context.ConfigMgr.Config.EnableHL7XMLTransform)
            //    res = ProcessHL7XMLMessage(receiveData, ref sendData);
            //else
            //    res = ProcessHL7Message(receiveData, ref sendData);

            return res;
        }

        public HL7InboundControler(EntityImpl entity)
        {
            _entity = entity;
            _transformer = XmlTransformerFactory.CreateTransformer(_entity.Context.ConfigMgr.Config.HL7XMLTransformerType, _entity.Context.Log);
        }
        public bool Start()
        {
            if (_server != null && _server.IsListening)
            {
                _entity.Context.Log.Write(LogType.Error, "Socket server has already started.");
                return false;
            }
            else
            {
                _server = SocketServer.Create(_entity.Context.ConfigMgr.Config.SocketConfig);
                _server.OnRequest += new RequestEventHandler(_server_OnRequest);
                return _server.Start();
            }
        }
        public bool Stop()
        {
            if (_server == null || !_server.IsListening)
            {
                _entity.Context.Log.Write(LogType.Error, "Socket server is not running.");
                return false;
            }
            else
            {
                return _server.Stop();
            }
        }
    }
}
