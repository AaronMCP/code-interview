using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler;
//using HYS.IM.EMRMessages;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Adapters
{
    /// <summary>
    /// This class is the entry point of the NT Service Host.
    /// This class is the communication endpoint to interact with other message entities in XDS Gateway.
    /// </summary>
    [MessageEntityEntry("HL7 Inbound Adapter", InteractionTypes.Publisher | InteractionTypes.Requester,
     "Receive HL7 v2 messages and publish or send request to other XDS Gateway message entities.")]
    public class EntityImpl : IMessageEntity, IPublisher, IRequester
    {
        private ProgramContext _context = new ProgramContext();
        internal ProgramContext Context { get { return _context; } }

        private HL7InboundControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return _controler.Start();
        }

        public bool Stop()
        {
            return _controler.Stop();
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            _context.PreLoading(arg);
            _controler = new HL7InboundControler(this);
            return true;
        }

        public EntityConfigBase GetConfiguration()
        {
            return _context.ConfigMgr.Config;
        }

        public bool Uninitalize()
        {
            _context.BeforeExit();
            return true;
        }

        #endregion

        #region IRequester Members

        public event MessageRequestHandler OnMessageRequest;

        internal bool NotifyMessageRequest(Message request, out Message response)
        {
            response = new Message();

            try
            {
                if (request == null || request.Header == null) return false;

                if (OnMessageRequest == null ||
                    _context.ConfigMgr.Config.RequestConfig.Channels.Count < 1)
                {
                    _context.Log.Write(LogType.Error, "Cannot find binded responser entity to receive the requesting message.");
                    return false;
                }
                else
                {
                    //PullChannelConfig route = _context.ConfigMgr.Config.RequestConfig.FindTheFirstMatchedChannel(request);
                    //if (route == null)
                    //{
                    //    _context.Log.Write(LogType.Error, string.Format("Cannot find requesting channel to send the request message type: {0}.", request.Header.Type));
                    //    return false;
                    //}

                    //_context.Log.Write(string.Format("Sending request({0}:{1}) to entity({2})", request.Header.Type, request.Header.ID.ToString(), route.ReceiverEntityID));
                    //bool result = OnMessageRequest(route, request, out response);
                    //_context.Log.Write(string.Format("Received response({0}) from entity({1}), result: {2}", response != null && response.Header != null ? response.Header.Type.ToString() + ":" + response.Header.ID.ToString() : "NULL", route.ReceiverEntityID, result.ToString()));

                    _context.Log.Write(string.Format("Sending request({0}:{1}) to entity({2})", request.Header.Type, request.Header.ID.ToString(), "auto"));
                    bool result = OnMessageRequest(null, request, out response);
                    _context.Log.Write(string.Format("Received response({0}) from entity({1}), result: {2}", response != null && response.Header != null ? response.Header.Type.ToString() + ":" + response.Header.ID.ToString() : "NULL", "auto", result.ToString()));

                    if (result &&
                        response != null &&
                        response.Header != null)
                    {
                        return true;
                    }
                    else
                    {
                        _context.Log.Write(LogType.Error, "Received responsing message failed or receive a unwanted responsing message.");
                        return false;
                    }
                }
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
                return false;
            }
        }

        #endregion

        #region IPublisher Members

        public event MessagePublishHandler OnMessagePublish;

        internal bool NotifyMessagePublish(Message message)
        {
            try
            {
                if (message == null ||
                    message.Header == null ||
                    OnMessagePublish == null) return false;

                bool result = OnMessagePublish(message);

                if (!result) _context.Log.Write(LogType.Error, string.Format("Send publishing message failed. Message Type: {0}", message.Header.Type));
                return result;
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
                return false;
            }
        }

        #endregion
    }
}
