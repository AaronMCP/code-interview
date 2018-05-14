using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using MSG = HYS.IM.Messaging.Objects;
using System.Data.OleDb;
using HYS.IM.Messaging.Base.Config;
using HYS.Common.Xml;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Config;
using System.Text.RegularExpressions;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Adapters;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Controler
{
    public class FileWriterControler
    {
        private ProgramContext _context;
        private XmlTransformerBase _transformer;

        public FileWriterControler(ProgramContext context)
        {
            _context = context;
            _transformer = XmlTransformerFactory.CreateTransformer(_context.ConfigMgr.Config.HL7XMLTransformerType, _context.Log);
        }

        public bool ProcessSubscribedMessage(MSG.Message msg)
        {
            if (msg == null || msg.Header == null) return false;
            string msgId = msg.Header.ID.ToString();

            bool res = false;
            _context.Log.Write(string.Format("Begin processing message. ID: {0}", msgId));

            if (_context.ConfigMgr.Config.MessageProcessingType == MessageProcessType.HL7v2Text)
            {
                res = WriteHL7v2TextMessage(msg);
            }
            else if (_context.ConfigMgr.Config.MessageProcessingType == MessageProcessType.HL7v2XML)
            {
                res = WriteHL7v2XmlLMessage(msg);
            }
            else if (_context.ConfigMgr.Config.MessageProcessingType == MessageProcessType.OtherXML)
            {
                res = WriteOtherXmlMessage(msg);
            }
            else
            {
                _context.Log.Write(LogType.Error, "Message process type config error.");
            }

            if (!res) DumpErrorMessage(msg, msgId);
            _context.Log.Write(string.Format("Finish processing message. ID: {0}, Result: {1}.\r\n", msgId, res));
            return res;
        }

        private bool WriteHL7v2TextMessage(HYS.IM.Messaging.Objects.Message msg)
        {
            try
            {
                string payload = HL7MessageHelper.GetHL7V2PayLoad(msg);
                string filePath = Path.Combine(_context.ConfigMgr.Config.OutputFileFolder, PathHelper.GetRelativePathByMode(_context.ConfigMgr.Config.OrganizationMode));
                filePath = Path.Combine(filePath, msg.Header.ID.ToString() + _context.ConfigMgr.Config.FileExtension);

                FileHelper.WriteFile(filePath, payload, _context.ConfigMgr.Config.CodePageName);
                _context.Log.Write(string.Format("Write message to file {0}", filePath));
                return true;
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
                return false;
            }
        }

        private bool WriteHL7v2XmlLMessage(HYS.IM.Messaging.Objects.Message msg)
        {
            try
            {
                string hl7Msg = string.Empty;
                if (_transformer.TransformXmlToHL7v2(msg.Body, out hl7Msg))
                {
                    _context.Log.Write("Transform xml to hl7 v2 text success.");
                }
                else
                {
                    _context.Log.Write("Transform xml to hl7 v2 text failure.");
                    return false;
                }

                string relativePath = PathHelper.GetRelativePathByMode(_context.ConfigMgr.Config.OrganizationMode);
                string filePath = _context.ConfigMgr.Config.OutputFileFolder;
                if (!string.IsNullOrEmpty(relativePath))
                {
                    filePath = Path.Combine(filePath, relativePath);
                }
                filePath = Path.Combine(filePath, msg.Header.ID.ToString() + _context.ConfigMgr.Config.FileExtension);

                FileHelper.WriteFile(filePath, hl7Msg, _context.ConfigMgr.Config.CodePageName);
                _context.Log.Write(string.Format("Write message to file {0} success.", filePath));
                return true;
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
                return false;
            }
        }

        private bool WriteOtherXmlMessage(HYS.IM.Messaging.Objects.Message msg)
        {
            try
            {
                StringBuilder xmlContent = new StringBuilder();
                xmlContent.AppendLine(string.Format(XmlHelper.XML_DELARATION_FORMAT, _context.ConfigMgr.Config.CodePageName))
                    .Append(msg.Body);

                string filePath = Path.Combine(_context.ConfigMgr.Config.OutputFileFolder, PathHelper.GetRelativePathByMode(_context.ConfigMgr.Config.OrganizationMode));
                filePath = Path.Combine(filePath, msg.Header.ID.ToString() + _context.ConfigMgr.Config.FileExtension);

                FileHelper.WriteFile(filePath, xmlContent.ToString(), _context.ConfigMgr.Config.CodePageName);
                _context.Log.Write(string.Format("Write message to file {0} success.", filePath));
                return true;
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
                return false;
            }
        }

        private void DumpErrorMessage(MSG.Message msg, string msgID)
        {
            DumpErrorMessage(msg.ToXMLString(), msgID);
        }
        private void DumpErrorMessage(string msgXml, string msgFileName)
        {
            try
            {
                string pathName = Path.Combine(Application.StartupPath, "FileWriterErrorMessage");
                string fileName = Path.Combine(pathName, string.Format("{0}.xml", msgFileName));
                _context.Log.Write(LogType.Information, "Dumping message into following file: " + fileName);
                if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);
                File.WriteAllText(fileName, msgXml, Encoding.UTF8);
            }
            catch (Exception err)
            {
                _context.Log.Write(err);
            }
        }
    }
}
