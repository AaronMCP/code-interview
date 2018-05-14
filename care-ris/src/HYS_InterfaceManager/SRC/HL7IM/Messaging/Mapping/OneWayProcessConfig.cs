using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Mapping.Replacing;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Common.Logging;
using System.IO;

namespace HYS.IM.Messaging.Mapping
{
    public class OneWayProcessConfig : XObject
    {
        public bool IsEnable()
        {
            return EnableXSLTTransform;// || EnableSchemaValidation || EnableRegexReplacement;
        }

        [XCData(true)]
        public string XSLTFileLocation { get; set; }
        public bool EnableXSLTTransform { get; set; }
        public bool EnableXSLTExtension { get; set; }

        /// <summary>
        /// In order to support including multiple XSLT files in the XSLT script,
        /// we disable the external extension (e.g. calling C# code from XSLT) currently.
        /// </summary>
        /// <param name="sourceMsg"></param>
        /// <param name="targetMsg"></param>
        /// <param name="rootFolderPath"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool TransformMessage(Message sourceMsg, out Message targetMsg, string rootFolderPath, ILog log)
        {
            targetMsg = null;
            if (sourceMsg == null) return false;

            string xslFilePath = XSLTFileLocation;
            if (!Path.IsPathRooted(xslFilePath)) xslFilePath = Path.Combine(rootFolderPath, xslFilePath);
            XMLTransformer t = XMLTransformer.CreateFromFileWithCache(xslFilePath, log, EnableXSLTExtension);
            if (t == null) return false;

            string targetString = null;
            string sourceString = sourceMsg.ToXMLString();
            if (!t.TransformString(sourceString, ref targetString, XSLTExtensionTypes.None)
                || string.IsNullOrEmpty(targetString)) return false;

            targetMsg = XObjectManager.CreateObject<Message>(targetString);
            return targetMsg != null;
        }

        //[XCData(true)]
        //public string SchemaFileLocation { get; set; }
        //public bool EnableSchemaValidation { get; set; }

        //public XCollection<ReplacementRule> RegexReplacementRule { get; set; }
        //public bool EnableRegexReplacement { get; set; }
    }
}
