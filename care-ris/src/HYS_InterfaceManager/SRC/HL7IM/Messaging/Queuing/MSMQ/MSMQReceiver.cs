using System;
using System.Text;
using System.Messaging;
using System.Threading;
using System.Collections.Generic;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public delegate void ErrorHandler(Exception err);

    public class MSMQReceiver : PushReceiverBase
    {
        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        private MSMQReceiverParameter _parameter;
        public MSMQReceiverParameter Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public MSMQReceiver(PushChannelConfig config, ILog log)
            : base(config, log)
        {
            _parameter = config.MSMQConfig.ReceiverParameter;
        }

        private Thread thread;
        private ManualResetEvent stopevent;
        private void NotifyMessage(Message msg)
        {
            _log.Write(LogType.Debug, "MSMQReceiver begin to deserializing message.");

            HYS.IM.Messaging.Objects.Message m = MSMQHelper.GetMessage(msg);

            if (m == null || m.Header == null)
            {
                _log.Write(LogType.Error, "MSMQReceiver receive and abandon a NULL message or message without header.");
                return;
            }
            else
            {
                _log.Write(LogType.Debug, "MSMQReceiver receive message id=" + m.Header.ID.ToString());
            }

            NotifyMessageReceived(m);
        }
        private void ListenTransactional()
        {
            MessageQueue queue = null;

            try
            {
                queue = new MessageQueue(_parameter.MSMQ.Path);
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                TimeSpan tspan = TimeSpan.FromMilliseconds(_parameter.TimeSlice);
                _log.Write("MSMQReceiver transactional listener thread start. time slice=" + _parameter.TimeSlice.ToString());

                while (_isRunning)
                {
                    try
                    {
                        using (MessageQueueTransaction tran = new MessageQueueTransaction())
                        {
                            try
                            {
                                tran.Begin();
                                Message msg = queue.Receive(tspan, tran);
                                NotifyMessage(msg);
                                tran.Commit();

                                _log.Write(LogType.Debug, "MSMQReceiver commit receiving message.");
                            }
                            catch (MSMQReceiveCancelException cancelErr)
                            {
                                tran.Abort();
                                _log.Write(LogType.Warning, "MSMQReceiver cancel receiving message from MSMQ because of " + cancelErr.Message);
                            }
                        }
                    }
                    catch (MessageQueueException err)
                    {
                        if (err.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                        {
                            if (!_isRunning) break;
                        }
                        else
                        {
                            _log.Write(LogType.Error, "MSMQReceiver transactional receive message failed.");
                            _log.Write(err);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (OnError != null) OnError(e);
                _log.Write(LogType.Error, "MSMQReceiver transactional listener thread failed.");
                _log.Write(e);
            }
            finally
            {
                if (queue != null)
                {
                    queue.Close();
                    queue.Dispose();
                }

                if (stopevent != null) stopevent.Set();
            }

            if (OnStop != null) OnStop(null, EventArgs.Empty);

            _log.Write("MSMQReceiver transactional listener thread stop.");
        }
        private void Listen()
        {
            MessageQueue queue = null;

            try
            {
                queue = new MessageQueue(_parameter.MSMQ.Path);
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                TimeSpan tspan = TimeSpan.FromMilliseconds(_parameter.TimeSlice);
                _log.Write("MSMQReceiver listener thread start. time slice=" + _parameter.TimeSlice.ToString());

                while (_isRunning)
                {
                    try
                    {
                        // Note 20130206: 
                        // Here do not use MessageQueueTransaction to receive message,
                        // Good: easy to by pass bad-formated message, prevent bad-formated message block the queue.
                        // Bad: when outbound adapter process message failed, the message will be lost.
                        // Improvement: use MessageQueueTransaction, validate format before notify out.

                        Message msg = queue.Receive(tspan);
                        NotifyMessage(msg);
                    }
                    catch (MessageQueueException err)
                    {
                        if (err.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                        {
                            if (!_isRunning) break;
                        }
                        else
                        {
                            // handle err message to avoid message lost
                            _log.Write(LogType.Error, "MSMQReceiver receive message failed.");
                            _log.Write(err);
                            //throw err;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (OnError != null) OnError(e);
                _log.Write(LogType.Error, "MSMQReceiver listener thread failed.");
                _log.Write(e);
                //throw e;
            }
            finally
            {
                if (queue != null)
                {
                    queue.Close();
                    queue.Dispose();
                }

                if (stopevent != null) stopevent.Set();
            }

            if (OnStop != null) OnStop(null, EventArgs.Empty);

            _log.Write("MSMQReceiver listener thread stop.");
        }

        public event ErrorHandler OnError;
        public event EventHandler OnStop;

        public override bool Start()
        {
            _log.Write("MSMQReceiver try to start listener");

            lock (this)
            {
                try
                {
                    if (_isRunning) return false;

                    _isRunning = true;

                    _log.Write("MSMQReceiver listener starting");

                    thread = _parameter.EnableTransaction ?
                        new Thread(new ThreadStart(ListenTransactional)) :
                        new Thread(new ThreadStart(Listen));
                    thread.Start();

                    return true;
                }
                catch (Exception e)
                {
                    if (OnError != null) OnError(e);
                    _log.Write(LogType.Error, "MSMQReceiver start listener failed.");
                    _log.Write(e);
                    return false;
                }
            }
        }
        public override bool Stop()
        {
            _log.Write("MSMQReceiver try to stop listener");

            lock (this)
            {
                try
                {
                    if (!_isRunning) return false;

                    _isRunning = false;

                    int waitSecond = _parameter.TimeSlice * 2;

                    _log.Write("MSMQReceiver listener stoping. wait second=" + waitSecond.ToString());

                    stopevent = new ManualResetEvent(false);
                    stopevent.WaitOne(TimeSpan.FromSeconds(waitSecond), true);

                    _log.Write("MSMQReceiver listener stopped.");

                    return true;
                }
                catch (Exception e)
                {
                    if (OnError != null) OnError(e);
                    _log.Write(LogType.Error, "MSMQReceiver stop listener failed.");
                    _log.Write(e);
                    return false;
                }
            }
        }

        #region IPushReceiver Members

        public override bool Initialize()
        {
            if (Parameter == null) return false;

            try
            {
                string msmqPath = Parameter.MSMQ.Path;
                if (!MessageQueue.Exists(msmqPath))
                {
                    MessageQueue queue = MessageQueue.Create(msmqPath, true);
                    queue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
                }

                _log.Write("MSMQReceiver initialize succeeded. " + msmqPath);
            }
            catch (Exception err)
            {
                _log.Write(LogType.Error, "MSMQReceiver initialize failed.");
                _log.Write(err);
            }

            return true;
        }

        public override bool Unintialize()
        {
            _log.Write("MSMQReceiver uninitialize succeeded. ");
            return true;
        }

        #endregion
    }

    //public delegate void ErrorHandler(Exception err);
}
