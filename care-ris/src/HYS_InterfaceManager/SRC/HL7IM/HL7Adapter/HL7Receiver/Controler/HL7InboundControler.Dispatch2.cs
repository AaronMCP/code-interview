using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler
{
    public partial class HL7InboundControler
    {
        public bool DispatchOtherXMLMessage(string reqXml, ref string rspXml)
        {
            switch (_entity.Context.ConfigMgr.Config.InboundMessageDispatching.Model)
            {
                case MessageDispatchModel.Publish: return DispatchOtherXMLMessage_Publish(reqXml, ref rspXml);
                case MessageDispatchModel.Request: return DispatchOtherXMLMessage_Request(reqXml, ref rspXml);
                case MessageDispatchModel.Custom: return DispatchOtherXMLMessage_Custom(reqXml, ref rspXml);
            }
            return false;
        }

        private bool DispatchOtherXMLMessage_Publish(string reqXml, ref string rspXml)
        {
            bool res = false;
            if (reqXml == null || reqXml.Length < 1) return res;

            Message msg = new Message();
            msg.Header.ID = Guid.NewGuid();
            //msg.Header.Type = MessageRegistry.HL7V2XML_NotificationMessageType;
            msg.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            msg.Body = reqXml;

            _entity.Context.Log.Write("Begin dispatching message to publisher directly.");
            res = _entity.NotifyMessagePublish(msg);
            _entity.Context.Log.Write(string.Format("End dispatching message to publisher directly. Result: {0}", res));
            _entity.Context.ConfigMgr.Config.DumpMessage(msg, reqXml, true, res);
            //if (!res) return res;

            _entity.Context.Log.Write("Begin generating acknowledgement using tamplate.");
            if (res) _entity.Context.ConfigMgr.Config.XSLTTransform(reqXml, ref rspXml, HL7InboundConfig.PublishingSuccessXSLTFileName,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions);
            else _entity.Context.ConfigMgr.Config.XSLTTransform(reqXml, ref rspXml, HL7InboundConfig.PublishingFailureXSLTFileName,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions);
            _entity.Context.Log.Write(string.Format("End generating acknowledgement using tamplate. Result length: {0}", (rspXml == null) ? "(null)" : rspXml.Length.ToString()));

            return res;
        }
        private bool DispatchOtherXMLMessage_Request(string reqXml, ref string rspXml)
        {
            bool res = false;
            if (reqXml == null || reqXml.Length < 1) return res;

            Message reqMsg = new Message();
            reqMsg.Header.ID = Guid.NewGuid();
            //reqMsg.Header.Type = MessageRegistry.HL7V2XML_QueryRequestMessageType;
            reqMsg.Header.Type = MessageRegistry.GENERIC_RequestMessageType;
            reqMsg.Body = reqXml;

            Message rspMsg = null;
            _entity.Context.Log.Write("Begin dispatching message to requester directly.");
            res = _entity.NotifyMessageRequest(reqMsg, out rspMsg) && (rspMsg != null);
            _entity.Context.Log.Write(string.Format("End dispatching message to requester directly. Result: {0}", res));
            _entity.Context.ConfigMgr.Config.DumpMessage(reqMsg, reqXml, true, res);

            if (res) rspXml = rspMsg.Body;
            return res;
        }
        private bool DispatchOtherXMLMessage_Custom(string reqXml, ref string rspXml)
        {
            bool res = false;
            if (reqXml == null || reqXml.Length < 1) return res;
            _entity.Context.Log.Write("Begin dispatching message according to message keyword.");

            string reqMsgXml = string.Format("<Message><Header/><Body>{0}</Body></Message>", reqXml);
            string keyword = GetMessageKeyword(reqMsgXml,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPath,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPathPrefixDefinition);

            if (MatchRegularExpression(keyword, _entity.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaPublishValueExpression))
            {
                _entity.Context.Log.Write(string.Format("Dispatching message with keyword {0} through publisher.", keyword));
                res = DispatchOtherXMLMessage_Publish(reqXml, ref rspXml);
            }
            else if (MatchRegularExpression(keyword, _entity.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaRequestValueExpression))
            {
                _entity.Context.Log.Write(string.Format("Dispatching message with keyword {0} through requester.", keyword));
                res = DispatchOtherXMLMessage_Request(reqXml, ref rspXml);
            }
            else
            {
                _entity.Context.Log.Write(LogType.Error,
                    string.Format("Keyword ({0}) in the following hl7 xml message does not match either publishing nor requesting criteria. \r\n{1}"
                    , keyword, reqMsgXml));
                return res;
            }

            _entity.Context.Log.Write(string.Format("End dispatching message according to message keyword. Result: {0}", res));
            return res;
        }
    }
}
