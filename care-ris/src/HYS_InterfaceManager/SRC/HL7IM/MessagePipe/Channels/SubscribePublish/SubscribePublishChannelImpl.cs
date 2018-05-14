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

namespace HYS.MessageDevices.MessagePipe.Channels.SubscribePublish
{
    public class SubscribePublishChannelImpl : IChannel
    {
        private SubscribePublishChannelConfig _config;
        private ChannelInitializationParameter _param;
        private ProcessingPipeLine _pipeLine;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                SubscribePublishChannelConfig.DEVICE_NAME, logMsg));
        }

        #region IChannel Members

        public bool Initialize(ChannelInitializationParameter parameter)
        {
            if (parameter == null) return false;

            _param = parameter;
            _config = XObjectManager.CreateObject<SubscribePublishChannelConfig>(parameter.ConfigXmlString);

            if (_config == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }

            _pipeLine = new ProcessingPipeLine(_config.Processors, _param);
            return _pipeLine.Initialize();
        }

        public ChannelProcessResult ProcessSubscriberMessage(MessagePackage message)
        {
            if (MessageEntryHelper.MatchEntryCriteria(_param, message, _config.EntryControl))
            {
                if (_pipeLine.Process(message))
                {
                    string msgXml = message.GetMessageXml();
                    Message msg = XObjectManager.CreateObject<Message>(msgXml);
                    if (msg == null) return ChannelProcessResult.ProcessingError;
                    
                    msg.Header.ID = Guid.NewGuid();
                    msg.Header.Type = _config.PublishMessageType;

                    if (_param.Sender.NotifyMessagePublish(msg))
                    {
                        return ChannelProcessResult.Success;
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

        public ChannelProcessResult ProcessResponserMessage(MessagePackage request, out MessagePackage response)
        {
            response = null;
            return ChannelProcessResult.NotMatchEntryCriteria;
        }

        public bool Uninitilize()
        {
            if (_pipeLine != null) _pipeLine.Uninitialize();
            return true;
        }

        #endregion
    }
}
