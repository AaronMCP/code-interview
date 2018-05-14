using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Common.HL7v2.Encoding;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Config;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Controler
{
    public partial class FileReaderControler
    {
        public bool DispatchXMLMessage(string reqXml)
        {
            switch (_publisher.Context.ConfigMgr.Config.InboundMessageDispatching.Model)
            {
                case MessageDispatchModel.Publish: return DispatchXMLMessage_Publish(reqXml);
                case MessageDispatchModel.Custom: return DispatchXMLMessage_Custom(reqXml);
            }
            return false;
        }

        private bool DispatchXMLMessage_Publish(string reqXml)
        {
            bool res = false;
            if (reqXml == null || reqXml.Length < 1) return res;

            Message msg = new Message();
            msg.Header.ID = Guid.NewGuid();
            msg.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            msg.Body = reqXml;

            _publisher.Context.Log.Write("Begin dispatching message to publisher directly.");
            _publisher.Context.Log.Write(LogType.Debug, "XDS message is :" + msg.ToXMLString());
            res = _publisher.NotifyMessagePublish(msg);
            _publisher.Context.Log.Write(string.Format("End dispatching message to publisher directly. Result: {0}", res));      
              
            return res;
        }

        private bool DispatchXMLMessage_Custom(string reqXml)
        {
            bool res = false;
            if (reqXml == null || reqXml.Length < 1) return res;
            _publisher.Context.Log.Write("Begin dispatching message according to message keyword.");

            string reqMsgXml = string.Format("<Message><Header/><Body>{0}</Body></Message>", reqXml);
            string keyword = GetMessageKeyword(reqMsgXml,
                _publisher.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPath,
                _publisher.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaXPathPrefixDefinition);

            if (MatchRegularExpression(keyword, _publisher.Context.ConfigMgr.Config.InboundMessageDispatching.CriteriaPublishValueExpression))
            {
                _publisher.Context.Log.Write(string.Format("Dispatching message with keyword {0} through publisher.", keyword));
                res = DispatchXMLMessage_Publish(reqXml);
            }
            else
            {
                _publisher.Context.Log.Write(LogType.Error, 
                    string.Format("Keyword ({0}) in the following hl7 xml message does not match either publishing nor requesting criteria. \r\n{1}"
                    , keyword, reqMsgXml));
                return res;
            }

            _publisher.Context.Log.Write(string.Format("End dispatching message according to message keyword. Result: {0}", res));
            return res;
        }
    }
}
