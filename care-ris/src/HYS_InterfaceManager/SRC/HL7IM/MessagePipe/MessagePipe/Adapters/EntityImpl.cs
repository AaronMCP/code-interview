using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Base;
using HYS.MessageDevices.MessagePipe.Controler;
using HYS.Messaging.Objects.PublishModel;
using HYS.Messaging.Objects.RequestModel;
using HYS.Messaging.Base.Config;
using HYS.Messaging.Objects;
using HYS.Messaging.Queuing;
using HYS.Common.Logging;
using HYS.Messaging.Queuing.LPC;
using HYS.MessageDevices.MessagePipe.Base;

namespace HYS.MessageDevices.MessagePipe.Adapters
{
    /// <summary>
    /// This class is the entry point of the NT Service Host.
    /// This class is the communication endpoint to interact with other message entities in XDS Gateway.
    /// </summary>
    [MessageEntityEntry("Message Processing Pipeline", InteractionTypes.Publisher | InteractionTypes.Subscriber | InteractionTypes.Requester | InteractionTypes.Responser,
     "This is a common configurable message processing engine for XDS Gateway, it performs XML message validation/tranformation/split/merge/filter/enhance/etc.")]
    public class EntityImpl : IMessageEntity, IPublisher, IRequester, ISubscriber, IResponser, ISender
    {
        private MessagePipeControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            Program.PreLoading(arg);
            _controler = new MessagePipeControler(this, Program.Log);
            return _controler.InitializeChannels();
        }

        public EntityConfigBase GetConfiguration()
        {
            return Program.ConfigMgr.Config;
        }

        public bool Uninitalize()
        {
            _controler.UninitializeChannels();
            Program.BeforeExit();
            return true;
        }

        #endregion

        #region IPublisher Members

        public event MessagePublishHandler OnMessagePublish;

        public bool NotifyMessagePublish(Message message)
        {
            try
            {
                if (message == null ||
                    message.Header == null ||
                    OnMessagePublish == null) return false;

                string desc = string.Format("message {0}:{1}", message.Header.Type, message.Header.ID.ToString());
                Program.Log.Write(string.Format("Begin publishing {0}", desc));
                bool result = OnMessagePublish(message);
                Program.Log.Write(string.Format("End publishing {0}. Result: {1}", desc, result));

                if (!result) Program.Log.Write(LogType.Error, "Send publishing message failed.");
                return result;
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                return false;
            }
        }

        #endregion

        #region IRequester Members

        public event MessageRequestHandler OnMessageRequest;

        public bool NotifyMessageRequest(Message request, out Message response)
        {
            response = new Message();

            try
            {
                if (request == null ||
                    request.Header == null ||
                    OnMessageRequest == null) return false;

                if (Program.ConfigMgr.Config.RequestConfig.Channels.Count < 1)
                {
                    Program.Log.Write(LogType.Error, "Cannot find binded responser entity to receive the requesting message.");
                    return false;
                }
                else
                {
                    // Support sending request to the first responser entity only.
                    //PullChannelConfig route = Program.ConfigMgr.Config.RequestConfig.Channels[0];
                    PullChannelConfig route = Program.ConfigMgr.Config.RequestConfig.FindTheFirstMatchedChannel(request);
                    if (route == null)
                    {
                        Program.Log.Write(LogType.Error, string.Format("Cannot find requesting channel to send the request message type: {0}.", request.Header.Type));
                        return false;
                    }

                    Program.Log.Write(string.Format("Sending request({0}:{1}) to entity({2})", request.Header.Type, request.Header.ID.ToString(), route.ReceiverEntityID));
                    bool result = OnMessageRequest(route, request, out response);
                    Program.Log.Write(string.Format("Received response({0}) from entity({1}), result: {2}", response != null && response.Header != null ? response.Header.Type.ToString() + ":" + response.Header.ID.ToString() : "NULL", route.ReceiverEntityID, result.ToString()));

                    if (result &&
                        response != null &&
                        response.Header != null)
                    {
                        return true;
                    }
                    else
                    {
                        Program.Log.Write(LogType.Error, "Received responsing message failed or receive a unwanted responsing message.");
                        return false;
                    }
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                return false;
            }
        }

        #endregion

        #region ISubscriber Members

        public void ReceiveMessage(IPushRoute route, Message message)
        {
            bool sndResult = false;

            try
            {
                if (message != null &&
                    message.Header != null)
                {
                    string desc = string.Format("processing message type: {0} id: {1} in thread id: {2}", 
                        message.Header.Type, message.Header.ID.ToString(), System.Threading.Thread.CurrentThread.ManagedThreadId);

                    Program.Log.Write(string.Format("Subscriber begin {0}", desc));
                    sndResult = _controler.DispatchSubscriberMessage(message);
                    Program.Log.Write(string.Format("Subscriber finish {0}. Result: {1}", desc, sndResult));
                    Program.Log.Write("");
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Received publishing message failed or receive a unwanted publishing message.");
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }

            if (!sndResult) LPCException.RaiseLPCException(route, "Dispatching and processing message success but sending message failed, maybe retry is needed.");
        }

        #endregion

        #region IResponser Members

        public bool ProcessMessage(IPullRoute route, Message request, out Message response)
        {
            response = new Message();

            try
            {
                if (request != null &&
                    request.Header != null)
                {
                    string desc = string.Format("processing message type: {0} id: {1} in thread id: {2}",
                        request.Header.Type, request.Header.ID.ToString(), System.Threading.Thread.CurrentThread.ManagedThreadId);

                    Program.Log.Write(string.Format("Responser begin {0}", desc));
                    bool res = _controler.DispatchResponserMessage(request, out response);
                    Program.Log.Write(string.Format("Responser finish {0}. Result: {1}, {2}", desc, res,
                        (response != null && response.Header != null) ? response.Header.Type + ":" + response.Header.ID.ToString() : null));
                    Program.Log.Write("");

                    return res;
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Received requsting message failed or receive a unwanted requsting message.");
                    return false;
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                return false;
            }
        }

        #endregion
    }
}
