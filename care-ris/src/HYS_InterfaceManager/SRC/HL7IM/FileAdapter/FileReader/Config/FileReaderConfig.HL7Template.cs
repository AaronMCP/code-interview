using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Config
{
    public partial class FileReaderConfig : EntityConfigBase
    {
        internal ProgramContext _contextForTemplate;
        /// <summary>
        /// Note:
        /// 
        /// These template files are used for generating outgoing ACK when HL7InboundAdapter run as an message entity,
        /// especially when incoming HL7 message is dispatched through the publisher to other message entity.
        /// (when incoming HL7 message is dispatched through the requester, the outgoing ACK should be generated
        /// by the response message receive from other message entity through the requester).
        /// 
        /// The tempalte files in "SampleTemplate" folder are only used for unit testing.
        /// They are loaded into FormServer.cs GUI and be used to process different incoming HL7 message,
        /// according to the HL7 event type (e.g. A01, Q22).
        /// 
        /// </summary>
        public const string HL7AckAATemplateFileName = "PublishTemplates\\HL7AckAATemplate.txt";
        public const string HL7AckAETemplateFileName = "PublishTemplates\\HL7AckAETemplate.txt";

        private string GetFullPath(string relativePath)
        {
            string fullPath = ConfigHelper.GetFullPath(Path.Combine(_contextForTemplate.AppArgument.ConfigFilePath, relativePath));
            return fullPath;
        }

        internal string GetHL7AckAATemplateFileNameWithFullPath()
        {
            return GetFullPath(FileReaderConfig.HL7AckAATemplateFileName);
        }
        internal void WriteHL7AckAATemplate(string content)
        {
            try
            {
                string fn = GetHL7AckAATemplateFileNameWithFullPath();
                using (StreamWriter sw = File.CreateText(fn))
                {
                    sw.Write(content);
                }
            }
            catch (Exception e)
            {
                _contextForTemplate.Log.Write(e);
            }
        }
        internal string ReadHL7AckAATemplate()
        {
            try
            {
                if (_hl7AckAATempalte == null)
                {
                    string fn = GetHL7AckAATemplateFileNameWithFullPath();
                    using (StreamReader sr = File.OpenText(fn))
                    {
                        _hl7AckAATempalte = sr.ReadToEnd();
                    }
                }
                return _hl7AckAATempalte;
            }
            catch (Exception e)
            {
                _contextForTemplate.Log.Write(e);
                return "";
            }
        }
        private string _hl7AckAATempalte;

        internal string GetHL7AckAETemplateFileNameWithFullPath()
        {
            return GetFullPath(FileReaderConfig.HL7AckAETemplateFileName);
        }
        internal void WriteHL7AckAETemplate(string content)
        {
            try
            {
                string fn = GetHL7AckAETemplateFileNameWithFullPath();
                using (StreamWriter sw = File.CreateText(fn))
                {
                    sw.Write(content);
                }
            }
            catch (Exception e)
            {
                _contextForTemplate.Log.Write(e);
            }
        }
        internal string ReadHL7AckAETemplate()
        {
            try
            {
                if (_hl7AckAETempalte == null)
                {
                    string fn = GetHL7AckAETemplateFileNameWithFullPath();
                    using (StreamReader sr = File.OpenText(fn))
                    {
                        _hl7AckAETempalte = sr.ReadToEnd();
                    }
                }
                return _hl7AckAETempalte;
            }
            catch (Exception e)
            {
                _contextForTemplate.Log.Write(e);
                return "";
            }
        }
        private string _hl7AckAETempalte;
    }
}
