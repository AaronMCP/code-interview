using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Common.Logging;
using System.Xml;
using System.Xml.XPath;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Adapters
{
    [MessageEntityEntry("SOAP Client Adapter", InteractionTypes.Subscriber | InteractionTypes.Responser,
      "Subscribe messages and send them to SOAP server, or receive requesting message and pass the request to SOAP server and receive the response and return.")]
    public class EntityImpl : IMessageEntity, ISubscriber, IResponser   //, IPublisher
    {
        private ProgramContext _context = new ProgramContext();
        internal ProgramContext Context { get { return _context; } }

        private SOAPClientControler _controler;

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
            _controler = new SOAPClientControler(_context);
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
            if (message != null &&
                message.Header != null) // &&
            //MessageRegistry.RHISPIX_NotificationMessageType.EqualsTo(message.Header.Type))    // do not need validation so strict
            {
                string id = message.Header.ID.ToString();
                _context.Log.Write(string.Format("Begin processing subscribed message. Message ID: {0}", id));

                SOAPClientControlerProcessStatus s = _controler.ProcessSubscribedMessage(message);

                _context.Log.Write(string.Format("End processing subscribed message. Message ID: {0}. Result: {1}", id, s));
                _context.Log.Write("");

                //SendLogMessage(string.Format("Receive SOAP response {0}",s),message.Body.ToString(),"null");
                if (s == SOAPClientControlerProcessStatus.SendingSOAPMessageError)
                    LPCException.RaiseLPCException(route, "Sending SOAP message error, maybe retry is needed.");

                if (s == SOAPClientControlerProcessStatus.RecevingFailureSOAPResponse)
                    LPCException.RaiseLPCException(route, "Sending SOAP message success, and recieve a failure response.");
            }
            else
            {
                _context.Log.Write(LogType.Error, "Received publishing message failed or receive a unwanted publishing message.");
            }
        }

        #endregion

        #region IResponser Members

        public bool ProcessMessage(IPullRoute route, Message request, out Message response)
        {
            response = null;
            if (request != null &&
                request.Header != null)// &&
                //MessageRegistry.RHISPIX_RequestMessageType.EqualsTo(request.Header.Type))     // do not need validation so strict
            {

                string id = request.Header.ID.ToString();
                _context.Log.Write(string.Format("Begin processing requesting message. Request Message ID: {0}", id));

                SOAPClientControlerProcessStatus s = _controler.ProcessRequestingMessage(request, out response);

                _context.Log.Write(
                    string.Format("End processing requesting message. Request Message ID: {0}. Response Message ID: {1}. Result: {2}",
                    id, (response != null && response.Header != null) ? response.Header.ID.ToString() : "(null)", s));
                _context.Log.Write("");

                //string Keywords = GetKeywords(request.Body.ToString());
                //SendLogMessage(string.Format("Send SOAP message {0}", s),request.Body.ToString(),Keywords);
                return s == SOAPClientControlerProcessStatus.Success;
            }
            else
            {
                _context.Log.Write(LogType.Error, "Received requsting message failed or receive a unwanted requsting message.");
                return false;
            }
        }

        public string GetKeywords(string Msg)
        {
            if (Msg.Equals(""))
            {
                return "null";
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Msg);

                XPathNavigator nav = doc.CreateNavigator();

                XPathNodeIterator itr = nav.Select(@"//HL7/PID/PatientID_InternalID");
                int i = 0;
                while (itr.MoveNext())
                {
                    XPathNavigator clone = itr.Current.Clone();
                    itr.Current.MoveToAttribute("ID", "");
                    if (i > 0)
                        sb.Append(",");
                    sb.Append(itr.Current.Value);

                    XPathNodeIterator itr1 = clone.Select("AssigningAuthority/@NameSpaceID");
                    while (itr1.MoveNext())
                    {
                        if (!itr1.Current.Value.Equals(""))
                            sb.Append("," + itr1.Current.Value);
                    }
                    i++;
                }
            }
            catch (Exception err)
            {
                _context.Log.Write(LogType.Error, "Get keywords error, "+err.Message);
            }

            return sb.ToString();

        }
        #endregion

        //#region IPublisher Members

        //public event MessagePublishHandler OnMessagePublish;

        //internal bool NotifyMessagePublish(Message message)
        //{
        //    try
        //    {
        //        if (message == null ||
        //            message.Header == null ||
        //            OnMessagePublish == null) return false;

        //        bool result = OnMessagePublish(message);

        //        if (!result) _context.Log.Write(LogType.Error, "Send publishing message failed.");
        //        return result;
        //    }
        //    catch (Exception err)
        //    {
        //        _context.Log.Write(err);
        //        return false;
        //    }
        //}

        ////private void SendLogMessage(string logStr,string Memo,string Keywords)
        ////{
        ////    string[] Key= new string[1];
        ////    Key[0] = Keywords;

        ////    NotifyMessagePublish(
        ////        DataTrackingLogHelper.CreateDataTrackingLogMessage(
        ////        _context.ConfigMgr.Config.Name,
        ////        logStr,
        ////        Memo,
        ////        Key));
        ////}

        //#endregion
    }
}
