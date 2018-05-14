using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Controler;
//using HYS.IM.EMRMessages;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Adapters
{
    /// <summary>
    /// This class is the entry point of the NT Service Host.
    /// This class is the communication endpoint to interact with other message entities in XDS Gateway.
    /// </summary>
    [MessageEntityEntry("HL7 Outbound Adapter", InteractionTypes.Subscriber,
     "Subscribe HL7 v2 messages and send them to HL7 v2 listener with MLLP.")]
    public class EntityImpl : IMessageEntity, ISubscriber, IResponser   //, IPublisher
    {
        private ProgramContext _context = new ProgramContext();
        internal ProgramContext Context { get { return _context; } }

        private HL7OutboundControler _controler;

        #region IMessageEntity Members

        public bool Start()
        {
            return _controler.Start();
        }

        public bool Stop()
        {
            _controler.Stop();
            return true;
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            _context.PreLoading(arg);
            _controler = new HL7OutboundControler(this);
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
            bool sndResult = false;

            try
            {
                if (message != null &&
                    message.Header != null)
                {
                    switch (_context.ConfigMgr.Config.MessageProcessingType)
                    {
                        case MessageProcessType.HL7v2Text:
                            string payload = HL7MessageHelper.GetHL7V2PayLoad(message);
                            sndResult = _controler.SendMLLPNotification(payload);
                            break;
                        case MessageProcessType.HL7v2XML:
                            string hl7xml = message.Body;
                            sndResult = _controler.SendHL7v2XMLNotification(hl7xml);
                            break;
                        default:
                        case MessageProcessType.OtherXML:
                            sndResult = _controler.SendOtherXMLNotification(message);
                            break;
                    }

                    ////if (MessageRegistry.HL7V2_NotificationMessageType.EqualsTo(message.Header.Type))
                    //if (!_context.Context.ConfigMgr.Config.EnableHL7XMLTransform)
                    //{
                    //    string payload = HL7MessageHelper.GetHL7V2PayLoad(message);
                    //    sndResult = _controler.SendNotification(payload);
                    //}
                    ////else if (MessageRegistry.HL7V2XML_NotificationMessageType.EqualsTo(message.Header.Type))
                    //else
                    //{
                    //    string hl7xml = message.Body;
                    //    sndResult = _controler.SendXMLNotification(hl7xml);
                    //}
                    ////else
                    ////{
                    ////    _context.Log.Write(LogType.Error, "Received a unwanted publishing message.");
                    ////}
                }
                else
                {
                    _context.Log.Write(LogType.Error, "Received publishing message failed.");
                }
            }
            catch (Exception e)
            {
                _context.Log.Write(e);
            }

            if (!sndResult) LPCException.RaiseLPCException(route, "Send HL7 message failed, maybe retry is needed.");
        }

        #endregion

        #region IResponser Members

        public bool ProcessMessage(IPullRoute route, Message request, out Message response)
        {
            response = new Message();

            try
            {
                if (request != null &&
                    request.Header != null)
                {
                    string req;
                    string rsp;

                    switch (_context.ConfigMgr.Config.MessageProcessingType)
                    {
                        case MessageProcessType.HL7v2Text:
                            {
                                req = HL7MessageHelper.GetHL7V2PayLoad(request);
                                if (_controler.SendMLLPQuery(req, out rsp))
                                {
                                    response.Header.ID = Guid.NewGuid();
                                    response.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                                    HL7MessageHelper.SetHL7V2PayLoad(response, rsp);
                                    return true;
                                }
                                else
                                {
                                    _context.Log.Write(LogType.Error, "Processing requsting HL7 message failed or sending HL7 message to remote HL7 listener failed.");
                                    return false;
                                }
                            }
                        case MessageProcessType.HL7v2XML:
                            {
                                req = request.Body;
                                if (_controler.SendHL7v2XMLQuery(req, out rsp))
                                {
                                    response.Header.ID = Guid.NewGuid();
                                    response.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                                    response.Body = rsp;
                                    return true;
                                }
                                else
                                {
                                    _context.Log.Write(LogType.Error, "Processing requsting XML message (transform XML to/from HL7v2 text) failed or sending HL7 message to remote HL7 listener failed.");
                                    return false;
                                }
                            }
                        default:
                        case MessageProcessType.OtherXML:
                            {
                                if (_controler.SendOtherXMLQuery(request, out response))
                                {
                                    response.Header.ID = Guid.NewGuid();
                                    response.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                                    return true;
                                }
                                else
                                {
                                    _context.Log.Write(LogType.Error, "Processing requsting XML message (transform XML to/from transport schema) failed or sending HL7 message to remote HL7 listener failed.");
                                    return false;
                                }
                            }
                    }

                    ////if (MessageRegistry.HL7V2_QueryRequestMessageType.EqualsTo(request.Header.Type))
                    //if(!_context.Context.ConfigMgr.Config.EnableHL7XMLTransform)
                    //{
                    //    req = HL7MessageHelper.GetHL7V2PayLoad(request);
                    //    if (_controler.SendQuery(req, out rsp))
                    //    {
                    //        response.Header.ID = Guid.NewGuid();
                    //        //response.Header.Type = MessageRegistry.HL7V2_QueryResultMessageType;
                    //        response.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                    //        HL7MessageHelper.SetHL7V2PayLoad(response, rsp);
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        _context.Log.Write(LogType.Error, "Processing requsting HL7 message failed or sending HL7 message to remote HL7 listener failed.");
                    //        return false;
                    //    }
                    //}
                    ////else if (MessageRegistry.HL7V2XML_QueryRequestMessageType.EqualsTo(request.Header.Type))
                    //else
                    //{
                    //    req = request.Body;
                    //    if (_controler.SendXMLQuery(req, out rsp))
                    //    {
                    //        response.Header.ID = Guid.NewGuid();
                    //        //response.Header.Type = MessageRegistry.HL7V2XML_QueryResultMessageType;
                    //        response.Header.Type = MessageRegistry.GENERIC_ResponseMessageType;
                    //        response.Body = rsp;
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        _context.Log.Write(LogType.Error, "Processing requsting XML message failed or sending HL7 message to remote HL7 listener failed.");
                    //        return false;
                    //    }
                    //}
                    ////else
                    ////{
                    ////    _context.Log.Write(LogType.Error, "Received a unwanted publishing message.");
                    ////    return false;
                    ////}
                }
                else
                {
                    _context.Log.Write(LogType.Error, "Received requsting message failed.");
                    return false;
                }
            }
            catch (Exception e)
            {
                _context.Log.Write(e);
                return false;
            }
        }

        #endregion
    }
}