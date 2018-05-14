using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCNotifier : PushSenderBase
    {
        private LPCChannelConfig _parameter;
        public LPCChannelConfig Parameter
        {
            get{ return _parameter ; }
            set{_parameter = value; }
        }

        public LPCNotifier(PushChannelConfig config, ILog log)
            : base(config, log)
        {
            _parameter = config.LPCConfig;
        }

        //private ISubscriber _subscriber;
        private LPCNotificationReceiver _receiver;

        public override bool Initialize()
        {
            //if (EntityDictionary.Entities.ContainsKey(_parameter.ReceiverEntityID))
            //{
            //    _subscriber = EntityDictionary.Entities[_parameter.ReceiverEntityID] as ISubscriber;
            //    if (_subscriber != null) _log.Write("LPCNotifier connect with subscriber successfully. " + _parameter.ReceiverEntityID.ToString());
            //    else _log.Write(LogType.Error, "LPCNotifier connect with subscriber failed.");
            //}
            _receiver = LPCReceiverDictionary.GetPushReceiver(Channel.ID);
            if (_receiver != null)
            {
                //_log.Write("LPCNotifier connect with subscriber successfully. " + _parameter.ReceiverEntityID.ToString());
                _log.Write("LPCNotifier connect with subscriber successfully. Push route ID: " + Channel.ID);
            }
            else
            {
                //_log.Write(LogType.Warning, "LPCNotifier cannot find subscriber. " + _parameter.ReceiverEntityID.ToString());
                _log.Write(LogType.Warning, "LPCNotifier cannot find subscriber. Push route ID: " + Channel.ID + ". Entity ID: " + _parameter.ReceiverEntityID.ToString());
            }
            //return _subscriber != null;
            return _receiver != null;
        }

        public override bool Unintialize()
        {
            _log.Write("LPCNotifier uninitialize. ");
            //_subscriber = null;
            _receiver = null;
            return true;
        }

        public override bool SendMessage(HYS.IM.Messaging.Objects.Message message)
        {
            //if (_subscriber != null)
            if (_receiver != null)
            {
                bool result;
                string id = message.Header.ID.ToString();
                _log.Write("LPCNotifier start sending a message to subscriber. Subscriber ID: " + _parameter.ReceiverEntityID + " Message ID=" + id);
                try
                {
                    //_subscriber.ReceiveMessage(Channel, message);
                    _receiver.ReceiveMessage(message);
                    result = true;
                }
                catch (LPCException err)
                {
                    _log.Write(err);
                    result = false;
                }
                _log.Write("LPCNotifier finish sending a message to subscriber. Subscriber ID: " + _parameter.ReceiverEntityID + " Message ID=" + id);
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
