using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Adapters;
using HYS.IM.Messaging.Objects;
//using HYS.IM.EMRMessages;
using HYS.IM.Common.HL7v2.Encoding;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler
{
    public partial class HL7InboundControler
    {
        public bool DispatchHL7XMLMessage(string reqHL7, string reqXml, ref string rspHL7)
        {
            switch (_entity.Context.ConfigMgr.Config.InboundMessageDispatching.Model)
            {
                case MessageDispatchModel.Publish: return DispatchHL7XMLMessage_Publish(reqHL7, reqXml, ref rspHL7);
                case MessageDispatchModel.Request: return DispatchHL7XMLMessage_Request(reqHL7, reqXml, ref rspHL7);
                case MessageDispatchModel.Custom: return DispatchHL7XMLMessage_Custom(reqHL7, reqXml, ref rspHL7);
            }
            return false;
        }

        private bool DispatchHL7XMLMessage_Publish(string reqHL7, string reqXml, ref string rspHL7)
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
            _entity.Context.ConfigMgr.Config.DumpMessage(msg, reqHL7, false, res);
            //if (!res) return res;

            //_entity.Context.Log.Write("Begin generating acknowledgement using tamplate.");
            //if (res) rspHL7 = HL7MessageParser.FormatResponseMessage(reqHL7, _entity.Context.ConfigMgr.Config.ReadHL7AckAATemplate());
            //else rspHL7 = HL7MessageParser.FormatResponseMessage(reqHL7, _entity.Context.ConfigMgr.Config.ReadHL7AckAETemplate());
            //_entity.Context.Log.Write(string.Format("End generating acknowledgement using tamplate. Result length: {0}", (rspHL7 == null) ? "(null)" : rspHL7.Length.ToString()));

            string rspXml = null;
            _entity.Context.Log.Write("Begin generating XML acknowledgement using tamplate.");
            if (res) res = _entity.Context.ConfigMgr.Config.XSLTTransform(reqXml, ref rspXml, HL7InboundConfig.PublishingSuccessXSLTFileName,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions);
            else res = _entity.Context.ConfigMgr.Config.XSLTTransform(reqXml, ref rspXml, HL7InboundConfig.PublishingFailureXSLTFileName,
                _entity.Context.ConfigMgr.Config.InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions);
            if (!res) _entity.Context.Log.Write(LogType.Error, "Transform incoming XML to outgoing XML failed.\r\n" + reqXml);
            _entity.Context.Log.Write(string.Format("End generating XML acknowledgement using tamplate. Result: {0} Result length: {1}", res, (rspXml == null) ? "(null)" : rspXml.Length.ToString()));

            if (res && (!string.IsNullOrEmpty(rspXml)))
            {
                _entity.Context.Log.Write("Begin transforming XML acknowledgement to text.");
                res = _transformer.TransformXmlToHL7v2(rspXml, out rspHL7);
                if (res && (!string.IsNullOrEmpty(rspHL7))) rspHL7 = rspHL7.Replace("\r\n", "\r");
                else _entity.Context.Log.Write(LogType.Error, "Transform outgoing XML to outgoing HL7v2 text failed.\r\n" + rspXml);
                _entity.Context.Log.Write(string.Format("End transforming XML acknowledgement to text. Result: {0} Result length: {1}", res, (rspHL7 == null) ? "(null)" : rspHL7.Length.ToString()));
            }

            return res;
        }
        private bool DispatchHL7XMLMessage_Request(string reqHL7, string reqXml, ref string rspHL7)
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
            res = _entity.NotifyMessageRequest(reqMsg, out rspMsg);
            _entity.Context.Log.Write(string.Format("End dispatching message to requester directly. Result: {0}", res));
            _entity.Context.ConfigMgr.Config.DumpMessage(reqMsg, reqHL7, false, res);
            if (!res) return res;

            string rsphl7 = "";
            _entity.Context.Log.Write("Begin transforming incoming XML to HL7v2");
            res = _transformer.TransformXmlToHL7v2(rspMsg.Body, out rsphl7);
            _entity.Context.Log.Write(string.Format("End transforming incoming XML to HL7v2. Result: {0}", res));

            if (!res)
            {
                _entity.Context.Log.Write(LogType.Error, "XML dump:\r\n" + rspMsg.Body);
                return res;
            }

            rspHL7 = rsphl7;

            return res;
        }
        private bool DispatchHL7XMLMessage_Custom(string reqHL7, string reqXml, ref string rspHL7)
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
                res = DispatchHL7XMLMessage_Publish(reqHL7, reqXml, ref rspHL7);
            }
            else if (MatchRegularExpression(keyword, _entity.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaRequestValueExpression))
            {
                _entity.Context.Log.Write(string.Format("Dispatching message with keyword {0} through requester.", keyword));
                res = DispatchHL7XMLMessage_Request(reqHL7, reqXml, ref rspHL7);
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
