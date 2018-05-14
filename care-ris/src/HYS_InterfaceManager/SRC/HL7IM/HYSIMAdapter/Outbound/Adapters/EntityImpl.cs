using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Controler;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Config;
using System.IO;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Adapters
{
    [MessageEntityEntry("CS Broker Outbound Adapter", InteractionTypes.Subscriber,
     "Receive XDSGW message containing CS Broker DataSet XML, and insert into CS Broker database by calling storage procedure of passive SQL Inbound Interface on CS Broker.")]
    public class EntityImpl : IMessageEntity, ISubscriber
    {
        private ProgramContext _context = new ProgramContext();
        internal ProgramContext Context { get { return _context; } }

        private CSBrokerOutboundControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            _context.PreLoading(arg);
            _controler = new CSBrokerOutboundControler(this);
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

        #region ISubscriber Members

        public void ReceiveMessage(IPushRoute route, Message message)
        {
            if (message != null && message.Header != null)
            {
                string id = message.Header.ID.ToString();
                _context.Log.Write(string.Format("Begin processing subscribed message. Message ID: {0}", id));

                bool res = _controler.ProcessSubscribedMessage(route, message);
                _context.Log.Write(string.Format("End processing subscribed message. Message ID: {0}. Result: {1}", id, res));
                _context.Log.Write("");

                if (!res) LPCException.RaiseLPCException(route, "Call storage procedure of CS Broker SQL Inbound Interface error.");
            }
            else
            {
                _context.Log.Write(LogType.Error, "Received publishing message failed or receive a unwanted publishing message.");
            }
        }

        #endregion
    }
}
