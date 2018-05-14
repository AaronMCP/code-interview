using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using System.Diagnostics;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config
{
    public partial class SOAPServerConfig : EntityConfigBase
    {
        public const string ResponseXDSGWMessageTemplateFolderName = "SampleTemplates";
        public const string ResponseXDSGWMessageTemplateFileName_PublishingSuccess = "PublishTemplates\\PublishingSuccess.xslt";
        public const string ResponseXDSGWMessageTemplateFileName_PublishingFailure = "PublishTemplates\\PublishingFailure.xslt";

        internal string GetResponseXDSGWMessageTemplateFolderFullPath()
        {
            if (Path.IsPathRooted(ResponseXDSGWMessageTemplateFolderName)) return ResponseXDSGWMessageTemplateFolderName;
            return GetFullPath(ResponseXDSGWMessageTemplateFolderName);
        }
        internal string GetResponseXDSGWMessageTemplateFileFullPath_PublishingSuccess()
        {
            if (Path.IsPathRooted(ResponseXDSGWMessageTemplateFileName_PublishingSuccess)) return ResponseXDSGWMessageTemplateFileName_PublishingSuccess;
            return GetFullPath(ResponseXDSGWMessageTemplateFileName_PublishingSuccess);
        }
        internal string GetResponseXDSGWMessageTemplateFileFullPath_PublishingFailure()
        {
            if (Path.IsPathRooted(ResponseXDSGWMessageTemplateFileName_PublishingFailure)) return ResponseXDSGWMessageTemplateFileName_PublishingFailure;
            return GetFullPath(ResponseXDSGWMessageTemplateFileName_PublishingFailure);
        }

        internal void OpenResponseXDSGWMessageTemplateFile_PublishingSuccess()
        {
            string fname = GetResponseXDSGWMessageTemplateFileFullPath_PublishingSuccess();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }
        internal void OpenResponseXDSGWMessageTemplateFile_PublishingFailure()
        {
            string fname = GetResponseXDSGWMessageTemplateFileFullPath_PublishingFailure();
            Process proc = Process.Start("notepad.exe", "\"" + fname + "\"");
            proc.EnableRaisingEvents = false;
        }
    }
}
