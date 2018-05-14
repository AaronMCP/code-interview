using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.Messaging.Objects;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Config;
using HYS.MessageDevices.MessagePipe.Channels;
using HYS.Common.Xml;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Controler
{
    /// <summary>
    /// This class contains major/core function (business logic implementation) of this message entity.
    /// </summary>
    public class MessagePipeControler : IDispatcher
    {
        private ILog _log;
        private ISender _msgSender;
        private List<ChannelControler> _chnList;

        public MessagePipeControler(ISender sender, ILog log)
        {
            _log = log;
            _msgSender = sender;
        }

        internal bool InitializeChannels()
        {
            try
            {
                _chnList = new List<ChannelControler>();
                foreach (ChannelInstance ci in Program.ConfigMgr.Config.Channels)
                {
                    if (!ci.Enable) continue;

                    _log.Write(string.Format("Begin initializing channel {0} as {1}", ci.Name, ci.DeviceName));

                    IChannel chn = ChannelFactory.CreateChannel(ci.DeviceName, Program.Log);
                    bool res = (chn != null &&
                        chn.Initialize(new ChannelInitializationParameter
                           (ci.Name, 
                           Program.AppArgument.ConfigFilePath, 
                           ci.Setting, 
                           _msgSender,
                           Program.Log)));

                    _log.Write(string.Format("End initializing channel {0} as {1}, result: {2}", ci.Name, ci.DeviceName, res));
                    _log.Write("");

                    if (res)
                    {
                        _chnList.Add(new ChannelControler(chn, ci));
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        internal void UninitializeChannels()
        {
            try
            {
                foreach (ChannelControler chn in _chnList)
                {
                    bool res = chn.ChannelImpl.Uninitilize();
                    _log.Write(string.Format("Uninitialize channel {0} as {1}, result: {2}", chn.ChannelConfig.Name, chn.ChannelConfig.DeviceName, res));
                }
                _chnList.Clear();
            }
            catch (Exception err)
            {
                _log.Write(err);
            }
        }

        #region IDispatcher Members

        public bool DispatchSubscriberMessage(Message message)
        {
            if (_chnList == null || message == null) return true;

            _log.Write("Serializing message to XML string.");
            string msgXml = message.ToXMLString();
            MessagePackage p = new MessagePackage(message, msgXml);
            _log.Write("Begin dispatching message to channels.");

            bool sendSuccess = true;
            bool hasMatchedChannel = false;
            foreach (ChannelControler chn in _chnList)
            {
                _log.Write("Begin trying channel: " + chn.ToString());
                ChannelProcessResult r = chn.ChannelImpl.ProcessSubscriberMessage(p);
                _log.Write(string.Format("End trying channel: {0}, Result: {1}", chn, r));

                bool continues = false;
                switch (r)
                {
                    case ChannelProcessResult.NotMatchEntryCriteria:
                        {
                            continues = true;
                            break;
                        }
                    case ChannelProcessResult.ProcessingError:
                        {
                            hasMatchedChannel = true;
                            continues = chn.ChannelConfig.PassOnOriginalMessageToNextChannelIfProcessingError;
                            break;
                        }
                    case ChannelProcessResult.SendingError:
                        {
                            hasMatchedChannel = true;
                            sendSuccess = false;
                            break;
                        }
                    case ChannelProcessResult.Success:
                        {
                            hasMatchedChannel = true;
                            break;
                        }
                }

                if (!sendSuccess) break;
                if (continues) continue;
                else break;
            }

            if (!hasMatchedChannel) sendSuccess = false;
            _log.Write(string.Format("End dispatching message to channels. Sending Result: {0}", sendSuccess));
            return sendSuccess;
        }

        public bool DispatchResponserMessage(Message request, out Message response)
        {
            response = null;
            if (_chnList == null || request == null) return false;

            _log.Write("Serializing requesting message to XML string.");
            string reqMsgXml = request.ToXMLString();
            MessagePackage reqPackage = new MessagePackage(request, reqMsgXml);
            _log.Write("Begin dispatching requesting message to channels.");

            bool procesSuccess = false;
            foreach (ChannelControler chn in _chnList)
            {
                MessagePackage rspPackage = null;
                _log.Write("Begin trying channel: " + chn.ToString());
                ChannelProcessResult r = chn.ChannelImpl.ProcessResponserMessage(reqPackage, out rspPackage);
                _log.Write(string.Format("End trying channel: {0}, Result: {1}", chn, r));

                bool continues = false;
                switch (r)
                {
                    case ChannelProcessResult.NotMatchEntryCriteria:
                        {
                            continues = true;
                            break;
                        }
                    case ChannelProcessResult.ProcessingError:
                        {
                            continues = chn.ChannelConfig.PassOnOriginalMessageToNextChannelIfProcessingError;
                            break;
                        }
                    case ChannelProcessResult.SendingError:
                        {
                            break;
                        }
                    case ChannelProcessResult.Success:
                        {
                            procesSuccess = (rspPackage != null);
                            break;
                        }
                }

                if (procesSuccess)
                {
                    _log.Write("Deserializing XML string to responsing message.");
                    string rspMsgXml = rspPackage.GetMessageXml();
                    response = XObjectManager.CreateObject<Message>(rspMsgXml);
                    procesSuccess = (response != null);
                    break;
                }

                if (continues) continue;
                else break;
            }

            _log.Write(string.Format("End dispatching requesting message to channels. Processing Result: {0}", procesSuccess));
            return procesSuccess;
        }

        #endregion
    }
}
