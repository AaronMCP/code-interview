using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Messaging.Objects;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config
{
    public partial class HL7InboundConfig : EntityConfigBase
    {
        internal ProgramContext _contextForTemplate2;

        public const string PublishingSuccessXSLTFileName = "PublishTemplates\\PublishingSuccess.xslt";
        public const string PublishingFailureXSLTFileName = "PublishTemplates\\PublishingFailure.xslt";

        //internal bool XSLTTransform(string sourceXml, ref string targetXml, string xsltFileName, bool enableExtension)
        //{
        //    string xslFile = ConfigHelper.GetFullPath(Program.AppArgument.ConfigFilePath, xsltFileName);
        //    XMLTransformer it = XMLTransformer.CreateFromFileWithCache(xslFile, Program.Log, enableExtension);
        //    return it.TransformString(sourceXml, ref targetXml, enableExtension ? XSLTExtensionTypes.XmlNodeTransformer : XSLTExtensionTypes.None);
        //}

        internal bool XSLTTransform(string sourceXml, ref string targetXml, string xsltFileName, bool enableExtension)
        {
            string xslFile = ConfigHelper.GetFullPath(_contextForTemplate2.AppArgument.ConfigFilePath, xsltFileName);
            XMLTransformer it = XMLTransformer.CreateFromFileWithCache(xslFile, _contextForTemplate2.Log, enableExtension);

            Message sourceMsg = new Message();
            sourceMsg.Body = sourceXml;

            string targetMsgXml = null;
            string sourceMsgXml = sourceMsg.ToXMLString();
            bool res = it.TransformString(sourceMsgXml, ref targetMsgXml, enableExtension ? XSLTExtensionTypes.XmlNodeTransformer : XSLTExtensionTypes.None);

            if (res)
            {
                Message targetMsg = XObjectManager.CreateObject<Message>(targetMsgXml);
                if (targetMsg != null)
                {
                    targetXml = targetMsg.Body;
                }
                else
                {
                    _contextForTemplate2.Log.Write(LogType.Error, "Cannot parse xml string to message object. \r\n" + targetMsgXml);
                    res = false;
                }
            }

            return res;
        }

        internal string GetPublishingSuccessXSLTFileNameWithFullPath()
        {
            return GetFullPath(HL7InboundConfig.PublishingSuccessXSLTFileName);
        }
        internal string GetPublishingFailureXSLTFileNameWithFullPath()
        {
            return GetFullPath(HL7InboundConfig.PublishingFailureXSLTFileName);
        }
    }
}
