using System;
using System.Text;
using System.Messaging;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQSender : PushSenderBase
    {
        private MSMQSenderParameter _parameter;
        public MSMQSenderParameter Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public MSMQSender(PushChannelConfig config, ILog log)
            : base(config, log)
        {
            _parameter = config.MSMQConfig.SenderParameter;
        }

        private MessageQueue queue;

        public override bool Initialize()
        {
            if (Parameter == null) return false;

            try
            {
                string msmqPath = Parameter.MSMQ.Path;
                if (MessageQueue.Exists(msmqPath))
                {
                    queue = new MessageQueue(msmqPath);
                }
                else
                {
                    queue = MessageQueue.Create(msmqPath, true);
                    queue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
                }

                _log.Write("MSMQSender initialize succeeded. " + msmqPath);
            }
            catch (Exception err)
            {
                _log.Write(LogType.Error, "MSMQSender initialize failed.");
                _log.Write(err);
            }

            return true;
        }
        public override bool Unintialize()
        {
            try
            {
                queue.Close();
                queue.Dispose();

                _log.Write("MSMQSender uninitialize succeeded. ");
            }
            catch (Exception err)
            {
                _log.Write(LogType.Error, "MSMQSender uninitialize failed.");
                _log.Write(err);
            }

            return true;
        }

        public override bool SendMessage(HYS.IM.Messaging.Objects.Message message)
        {
            if (message == null || message.Header == null)
            {
                _log.Write(LogType.Error, "MSMQSender cannot send NULL message or message without header.");
                return false;
            }

            string msgIDInfo = "MSMQSender send message id=" + message.Header.ID.ToString();
            _log.Write(LogType.Debug, msgIDInfo + " serializing.");
            string xmlString = message.ToXMLString();

            lock (queue)    // 20130204 Send() is not thread safe according to msdn.
            {
                using (MessageQueueTransaction trans = new MessageQueueTransaction())
                {
                    try
                    {
                        _log.Write(LogType.Debug, msgIDInfo + " begin.");

                        trans.Begin();
                        queue.Send(xmlString, MSMQHelper.GetMessageLabel(message), trans);
                        trans.Commit();

                        _log.Write(LogType.Debug, msgIDInfo + " end.");
                        return true;
                    }
                    catch (Exception err)
                    {
                        trans.Abort();
                        //throw err;
                        _log.Write(LogType.Error, "MSMQSender send message failed.");
                        _log.Write(err);
                        return false;
                    }
                }
            }
        }
    }
}
