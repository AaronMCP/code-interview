using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.Common.Xml;
using HYS.Common.Logging;
using HYS.Messaging.Objects;
using HYS.MessageDevices.MessagePipe.Channels.Common;

namespace HYS.MessageDevices.MessagePipe.Channels.RequestResponse
{
    public class RequestResponseChannelImpl : IChannel
    {
        private ChannelInitializationParameter _param;
        private RequestResponseChannelConfig _config;
        private ProcessingPipeLine _pipeLineReq;
        private ProcessingPipeLine _pipeLineRsp;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                RequestResponseChannelConfig.DEVICE_NAME, logMsg));
        }

        #region IChannel Members

        public bool Initialize(ChannelInitializationParameter parameter)
        {
            if (parameter == null) return false;

            _param = parameter;
            _config = XObjectManager.CreateObject<RequestResponseChannelConfig>(parameter.ConfigXmlString);

            if (_config == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }

            _pipeLineReq = new ProcessingPipeLine(_config.RequestMessageProcessors, _param);
            _pipeLineRsp = new ProcessingPipeLine(_config.ResponseMessageProcessors, _param);
            return _pipeLineReq.Initialize() && _pipeLineRsp.Initialize();
        }

        public ChannelProcessResult ProcessSubscriberMessage(MessagePackage message)
        {
            return ChannelProcessResult.NotMatchEntryCriteria;
        }

        public ChannelProcessResult ProcessResponserMessage(MessagePackage request, out MessagePackage response)
        {
            response = null;

            if (MessageEntryHelper.MatchEntryCriteria(_param, request, _config.EntryControl))
            {
                if (_pipeLineReq.Process(request))
                {
                    string reqXml = request.GetMessageXml();
                    Message reqMsg = XObjectManager.CreateObject<Message>(reqXml);
                    if (reqMsg == null) return ChannelProcessResult.ProcessingError;

                    reqMsg.Header.ID = Guid.NewGuid();
                    reqMsg.Header.Type = _config.RequestMessageTypePair.RequestMessageType;

                    Message rspMsg = null;
                    if (_param.Sender.NotifyMessageRequest(reqMsg, out rspMsg) 
                        && rspMsg != null && rspMsg.Header != null)
                    {
                        if (!_config.RequestMessageTypePair.ResponseMessageType.EqualsTo(rspMsg.Header.Type))
                        {
                            _param.Log.Write(LogType.Error, 
                                "Receiving response message is not the type of :"
                                + _config.RequestMessageTypePair.ResponseMessageType.ToString());
                            return ChannelProcessResult.SendingError;
                        }

                        rspMsg.Header.ID = Guid.NewGuid();
                        rspMsg.Header.Type = _config.ResponseMessageType;

                        string rspXml = rspMsg.ToXMLString();
                        response = new MessagePackage(rspMsg, rspXml);
                        response.Accept(_param.ChannelName);

                        if (_pipeLineRsp.Process(response))
                        {
                            return ChannelProcessResult.Success;
                        }
                        else
                        {
                            return ChannelProcessResult.ProcessingError;
                        }
                    }
                    else
                    {
                        return ChannelProcessResult.SendingError;
                    }
                }
                else
                {
                    return ChannelProcessResult.ProcessingError;
                }
            }
            else
            {
                return ChannelProcessResult.NotMatchEntryCriteria;
            }
        }

        public bool Uninitilize()
        {
            if (_pipeLineReq != null) _pipeLineReq.Uninitialize();
            if (_pipeLineRsp != null) _pipeLineRsp.Uninitialize();
            return true;
        }

        #endregion
    }
}
