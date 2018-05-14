using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Controler;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Adapters
{
    [MessageEntityEntry("File Writer", InteractionTypes.Subscriber,
     "Receive XDSGW message containing HL7 v2 sdk XML/HL7 v2 text/other xml, and then write them to file.")]
    public class EntityImpl : IMessageEntity, ISubscriber
    {
        private ProgramContext _context = new ProgramContext();
        public ProgramContext Context { get { return _context; } }

        FileWriterControler _controler = null;

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
            _controler = new FileWriterControler(_context);
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

                bool res = _controler.ProcessSubscribedMessage(message);
                _context.Log.Write(string.Format("End processing subscribed message. Message ID: {0}. Result: {1}", id, res));
                _context.Log.Write("");

                if (!res) LPCException.RaiseLPCException(route, "File Writer process error.maybe need republish it.");
            }
            else
            {
                _context.Log.Write(LogType.Error, "Received publishing message failed or receive a unwanted publishing message.");
            }
        }

        #endregion
    }
}
