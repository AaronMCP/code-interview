using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Mapping.Transforming;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Config
{
    public partial class FileReaderConfig : EntityConfigBase
    {
        public const string PublishingSuccessXSLTFileName = "PublishTemplates\\PublishingSuccess.xslt";
        public const string PublishingFailureXSLTFileName = "PublishTemplates\\PublishingFailure.xslt";

        internal bool XSLTTransform(string sourceXml, ref string targetXml, string xsltFileName)
        {
            string xslFile = ConfigHelper.GetFullPath(_contextForTemplate.AppArgument.ConfigFilePath, xsltFileName);
            XMLTransformer it = XMLTransformer.CreateFromFileWithCache(xslFile, _contextForTemplate.Log);
            return it.TransformString(sourceXml, ref targetXml, XSLTExtensionTypes.None);
        }

        internal string GetSuccessXSLTFilePath()
        {
            return ConfigHelper.GetFullPath(_contextForTemplate.AppArgument.ConfigFilePath, PublishingSuccessXSLTFileName);
        }

        internal string GetFailureXSLTFilePath()
        {
            return ConfigHelper.GetFullPath(_contextForTemplate.AppArgument.ConfigFilePath, PublishingFailureXSLTFileName);
        }
    }
}
