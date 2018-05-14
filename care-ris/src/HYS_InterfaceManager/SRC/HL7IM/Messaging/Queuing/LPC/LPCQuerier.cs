using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCQuerier : PullSenderBase
    {
        private LPCChannelConfig _parameter;
        public LPCChannelConfig Parameter
        {
            get{ return _parameter ; }
            set{_parameter = value; }
        }

        public LPCQuerier(PullChannelConfig config, ILog log)
            : base(config, log)
        {
            _parameter = config.LPCConfig;
        }

        //private IResponser _responser;
        private LPCQueryReceiver _receiver;

        public override bool Initialize()
        {
            //if (EntityDictionary.Entities.ContainsKey(_parameter.ReceiverEntityID))
            //{
            //    _responser = EntityDictionary.Entities[_parameter.ReceiverEntityID] as IResponser;
            //    if(_responser!= null )_log.Write("LPCQuerier connect with responser successfully. " + _parameter.ReceiverEntityID.ToString());
            //    else _log.Write(LogType.Error, "LPCQuerier connect with responser failed.");
            //}
            _receiver = LPCReceiverDictionary.GetPullReceiver(Channel.ID);
            if (_receiver != null)
            {
                //_log.Write("LPCQuerier connect with responser successfully. " + _parameter.ReceiverEntityID.ToString());
                _log.Write("LPCQuerier connect with responser successfully. Pull route ID: " + Channel.ID);
            }
            else
            {
                //_log.Write(LogType.Warning, "LPCQuerier cannot find responser. " + _parameter.ReceiverEntityID.ToString());
                _log.Write(LogType.Warning, "LPCQuerier cannot find responser. Pull route ID: " + Channel.ID + ". Entity ID: " + _parameter.ReceiverEntityID.ToString());
            }
            //return _responser != null;
            return _receiver != null;
        }

        public override bool Unintialize()
        {
            _log.Write("LPCQuerier uninitialize. ");
            //_responser = null;
            _receiver = null;
            return true;
        }

        public override bool SendMessage(Message request, out Message response)
        {
            response = null;
            //if (_responser != null)
            if(_receiver != null)
            {
                bool res;
                string reqid = request.Header.ID.ToString();
                _log.Write("LPCQuerier start sending a message to responser. reqID=" + reqid);
                //bool res = _responser.ProcessMessage(Channel, request, out response);
                try
                {
                    res = _receiver.MessageReceive(request, out response);
                }
                catch (Exception err)
                {
                    _log.Write(err);
                    res = false;
                }
                string resid = response != null ? response.Header.ID.ToString() : "(null)";
                _log.Write("LPCQuerier finish sending a message to responser. reqID=" + reqid + " resID=" + resid); ;
                return res;
            }
            else
            {
                return false;
            }
        }
    }
}
