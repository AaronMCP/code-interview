using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using System.IO;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Adapters;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    public partial class SOAPServerControler
    {
        /// <summary>
        /// Receive IncomingMessageXml (string), return OutgoingMessage (object)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="dspModel"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DispatchXDSGWMessage(SOAPReceiverSession session, MessageDispatchModel dspModel, EntityImpl entity)
        {
            if (session == null || 
                session.Status != SOAPReceiverSessionStatus.IncomingMessageXmlGenerated) return false;

            switch (dspModel)
            {
                case MessageDispatchModel.Publish: return DispatchXDSGWMessage_Publish(session, entity);
                case MessageDispatchModel.Request: return DispatchXDSGWMessage_Request(session, entity);
                case MessageDispatchModel.Custom: return DispatchXDSGWMessage_Custom(session, entity);
                case MessageDispatchModel.Test: return DispatchXDSGWMessage_Test(session);
            }

            return false;
        }

        private bool DispatchXDSGWMessage_Publish(SOAPReceiverSession session, EntityImpl entity)
        {
            bool res = false;
            if (entity == null) return res;
            _context.Log.Write("Begin dispatching message to publisher directly.");

            Message reqMsg = CreateXDSGWMessage(session.IncomingMessageXml);
            reqMsg.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            session.IncomingMessage = reqMsg;

            string baseStringToGenerateResponseMsg = null;
            if (_context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXDSGWMessageBasedOnIncomingSoapEnvelopeDirectly)
            {
                baseStringToGenerateResponseMsg = session.IncomingSOAPEnvelope;
            }
            else
            {
                baseStringToGenerateResponseMsg = session.IncomingMessageXml;
            }

            string xslFileName = entity.NotifyMessagePublish(reqMsg) ? 
                SOAPServerConfig.ResponseXDSGWMessageTemplateFileName_PublishingSuccess :
                SOAPServerConfig.ResponseXDSGWMessageTemplateFileName_PublishingFailure;

            string rspMsgString = GetPublishResponseMessageContent(xslFileName, baseStringToGenerateResponseMsg);

            Message rspMsg = CreateXDSGWMessage(rspMsgString);
            if (rspMsg != null)
            {
                session.OutgoingMessage = rspMsg;
                res = true;
            }

            // When publish fail, instead of creating SOAP failure envelope (by fixed XML),
            // it will generate an normal responsing SOAP envelope indicating the error by XSLT
            // (this XSLT could be based on incoming XDSGW Message or original SOAP envelope).
            // If generation error, it will create SOAP failure envelope.

            _context.Log.Write(string.Format("End dispatching message to publisher directly. Result: {0}", res));
            return res;
        }
        private bool DispatchXDSGWMessage_Request(SOAPReceiverSession session, EntityImpl entity)
        {
            bool res = false;
            if (entity == null) return res;
            _context.Log.Write("Begin dispatching message to requester directly.");

            Message reqMsg = CreateXDSGWMessage(session.IncomingMessageXml);
            reqMsg.Header.Type = MessageRegistry.GENERIC_RequestMessageType;
            session.IncomingMessage = reqMsg;

            Message rspMsg = null;
            if (entity.NotifyMessageRequest(reqMsg, out rspMsg))
            {
                // request success
                
                session.OutgoingMessage = rspMsg;
                res = true;
            }
            else
            {
                // request failure

                string baseStringToGenerateResponseMsg = null;
                if (_context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXDSGWMessageBasedOnIncomingSoapEnvelopeDirectly)
                {
                    baseStringToGenerateResponseMsg = session.IncomingSOAPEnvelope;
                }
                else
                {
                    baseStringToGenerateResponseMsg = session.IncomingMessageXml;
                }

                string xslFileName = SOAPServerConfig.ResponseXDSGWMessageTemplateFileName_PublishingFailure;
                string rspMsgString = GetPublishResponseMessageContent(xslFileName, baseStringToGenerateResponseMsg);
                rspMsg = CreateXDSGWMessage(rspMsgString);
                if (rspMsg != null)
                {
                    session.OutgoingMessage = rspMsg;
                    res = true;
                }

                // When request fail, instead of creating SOAP failure envelope (by fixed XML),
                // it will generate an normal responsing SOAP envelope indicating the error by XSLT
                // (this XSLT could be based on incoming XDSGW Message or original SOAP envelope).
                // If generation error, it will create SOAP failure envelope.
            }

            _context.Log.Write(string.Format("End dispatching message to requester directly. Result: {0}", res));
            return res;
        }
        private bool DispatchXDSGWMessage_Custom(SOAPReceiverSession session, EntityImpl entity)
        {
            bool res = false;
            if (entity == null) return res;
            _context.Log.Write("Begin dispatching message according to message keyword.");

            string reqMsgString = session.IncomingMessageXml;
            string keyword = GetMessageKeyword(reqMsgString,
                _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPath,
                _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPathPrefixDefinition);
            session.IncomingMessageDispatchingKey = keyword;

            if (MatchRegularExpression(keyword, _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaPublishValueExpression))
            {
                _context.Log.Write(string.Format("Dispatching message with keyword {0} through publisher.", keyword));
                res = DispatchXDSGWMessage_Publish(session, entity);
            }
            else if (MatchRegularExpression(keyword, _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaRequestValueExpression))
            {
                _context.Log.Write(string.Format("Dispatching message with keyword {0} through requester.", keyword));
                res = DispatchXDSGWMessage_Request(session, entity);
            }
            else
            {
                //res = DispatchXDSGWMessage_Test(session);
                return false;

                // When dispatch fail,
                // it will create SOAP failure envelope with fixed XML only (stead of by using XSLT),
            }

            _context.Log.Write(string.Format("End dispatching message according to message keyword. Result: {0}", res));
            return res;
        }
        private bool DispatchXDSGWMessage_Test(SOAPReceiverSession session)
        {
            bool res = false;
            _context.Log.Write("Begin generating response message by using the sample files.");

            string key = "";
            string reqMsgString = session.IncomingMessageXml;
            if (session.Status != SOAPReceiverSessionStatus.IncomingMessageDispatchingKeyGenerated)
            {
                key = GetMessageKeyword(reqMsgString,
                    _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPath,
                    _context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPathPrefixDefinition);
                session.IncomingMessageDispatchingKey = key;
            }

            string baseStringToGenerateResponseMsg = null;
            if (_context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXDSGWMessageBasedOnIncomingSoapEnvelopeDirectly)
            {
                baseStringToGenerateResponseMsg = session.IncomingSOAPEnvelope;
            }
            else
            {
                baseStringToGenerateResponseMsg = reqMsgString;
            }

            string rspMsgString = GetSampleResponseMessageContent(key, baseStringToGenerateResponseMsg);
            Message rspMsg = CreateXDSGWMessage(rspMsgString);

            if (rspMsg != null)
            {
                session.OutgoingMessage = rspMsg;
                res = true;
            }

            _context.Log.Write(string.Format("End generating response message by using the sample files. Result: {0}", res));
            return res;
        }
    }
}
