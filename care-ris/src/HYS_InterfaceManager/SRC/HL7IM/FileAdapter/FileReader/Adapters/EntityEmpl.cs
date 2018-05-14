using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Controler;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Adapters
{
    [MessageEntityEntry("File Reader", InteractionTypes.Publisher,
     "Read HL7v2 or XML message from text file, transform to XDSGW messagse and publish out.")]
    public class EntityImpl : IMessageEntity, IPublisher, IMessagePublisher
    {
        private ProgramContext _context = new ProgramContext();
        public ProgramContext Context { get { return _context; } }
        private FileReaderControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return _controler.StartTimer(); ;
        }

        public bool Stop()
        {
            return _controler.StopTimer();
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            _context.PreLoading(arg);
            _controler = new FileReaderControler(this);
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

        #region IPublisher Members

        public event MessagePublishHandler OnMessagePublish;

        public bool NotifyMessagePublish(Message message)
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
