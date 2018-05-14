using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Objects;
using System.Xml;
using System.Xml.XPath;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Adapters
{
    [MessageEntityEntry("SOAP Server Adapter", InteractionTypes.Publisher | InteractionTypes.Requester,
     "Recieve SOAP message and package them into XDSGW message and publish out, or receive SOAP request and pass the request to other message entity in XDSGW.")]
    public class EntityImpl : IMessageEntity, IPublisher, IRequester
    {
        private ProgramContext _context = new ProgramContext();
        internal ProgramContext Context { get { return _context; } }

        private SOAPReceiver _receiver;
        private SOAPServerControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return _receiver.Start(_context.ConfigMgr.Config.SOAPServiceURI);
        }

        public bool Stop()
        {
            _receiver.Stop();
            return true;
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            _context.PreLoading(arg);
            _controler = new SOAPServerControler(_context);
            _receiver = new SOAPReceiver(_context.ConfigMgr.Config, _context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(), _context.Log);
            _receiver.OnMessageReceived += delegate(SOAPReceiverSession session)
            {
                bool res = _controler.ProcessSoapSession(session, _context.ConfigMgr.Config.InboundMessageDispatching.Model, this);
                
                //string Keywords = GetKeywords(session.IncomingMessage.Body.ToString());
                //SendLogMessage(string.Format("Process SOAP message {0}",res),session.IncomingMessage.Body.ToString(),Keywords);
                return res;
            };
            return true;
        }

        //public string GetKeywords(string Msg)
        //{
        //    if (Msg.Equals(""))
        //    {
        //        return "null";
        //    }

        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(Msg);

        //        XPathNavigator nav = doc.CreateNavigator();

        //        XPathNodeIterator itr = nav.Select(@"/HL7/PID/PatientID_InternalID");
        //        int i = 0;
        //        while (itr.MoveNext())
        //        {
        //            XPathNavigator clone = itr.Current.Clone();
        //            itr.Current.MoveToAttribute("ID", "");
        //            if (i > 0)
        //            {
        //                sb.Append(",");
        //            }
        //            sb.Append(itr.Current.Value);

        //            XPathNodeIterator itr1 = clone.Select("AssigningAuthority/@NameSpaceID");
        //            while (itr1.MoveNext())
        //            {
        //                if (!itr1.Current.Value.Equals(""))
        //                    sb.Append("," + itr1.Current.Value);
        //            }
        //            i++;
        //        }
        //    }catch(Exception err)
        //    {
        //    }

        //    return sb.ToString();

        //}

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

        //private void SendLogMessage(string logStr, string Memo,string Keywords)
        //{
        //    string[] Key = new string[1];
        //    Key[0] = Keywords;
        //    NotifyMessagePublish(
        //        DataTrackingLogHelper.CreateDataTrackingLogMessage(
        //        Program.ConfigMgr.Config.Name,
        //        logStr,Memo,Key));
        //}

        #endregion

        #region IRequester Members

        public event MessageRequestHandler OnMessageRequest;

        internal bool NotifyMessageRequest(Message request, out Message response)
        {
            response = new Message();

            try
            {
                if (request == null ||
                    request.Header == null ||
                    OnMessageRequest == null) return false;

                if (_context.ConfigMgr.Config.RequestConfig.Channels.Count < 1)
                {
                    _context.Log.Write(LogType.Error, "Cannot find binded responser entity to receive the requesting message.");
                    return false;
                }
                else
                {
                    //PullChannelConfig route = Program.ConfigMgr.Config.RequestConfig.FindTheFirstMatchedChannel(request);
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
    }
}
