using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using System.Timers;
using System.IO;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Config;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.HL7v2.Encoding;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Adapters;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Controler
{
    public partial class FileReaderControler
    {
        private IMessagePublisher _publisher;
        private Timer _readerTimer = null;
        private XmlTransformerBase _transformer = null;
        private const string FOLDER_SUCCESS_NAME = "SUCCESS";
        private const string FOLDER_FIALED_NAME = "FAILURE";
        private const string FOLDER_ACK_NAME = "ACK";
        private string _fialedFolder = string.Empty;
        private string _ackFodler = string.Empty;
        private string _successFolder = string.Empty;

        public FileReaderControler(IMessagePublisher publisher)
        {
            _publisher = publisher;

            _transformer = XmlTransformerFactory.CreateTransformer(_publisher.Context.ConfigMgr.Config.HL7XMLTransformerType, _publisher.Context.Log);
            _fialedFolder = Path.Combine(_publisher.Context.ConfigMgr.Config.FileOutboundFolder, FOLDER_FIALED_NAME);
            _successFolder = Path.Combine(_publisher.Context.ConfigMgr.Config.FileOutboundFolder, FOLDER_SUCCESS_NAME);
            _ackFodler = Path.Combine(_publisher.Context.ConfigMgr.Config.FileOutboundFolder, FOLDER_ACK_NAME);

            _readerTimer = new Timer(_publisher.Context.ConfigMgr.Config.TimerInterval);
            _readerTimer.Elapsed += new ElapsedEventHandler(_readerTimer_Elapsed);
        }

        void _readerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _readerTimer.Stop();
                ProcessReaderWork();
            }
            catch (Exception ex)
            {
                _publisher.Context.Log.Write(ex);
            }
            finally
            {
                _readerTimer.Start();
            }
        }

        private void ProcessReaderWork()
        {
            if (!Directory.Exists(_publisher.Context.ConfigMgr.Config.FileInboundFolder))
            {
                _publisher.Context.Log.Write(LogType.Error, "File input folder don't exists.");
                return;
            }
            string[] fileList = Directory.GetFiles(_publisher.Context.ConfigMgr.Config.FileInboundFolder
                , "*" + _publisher.Context.ConfigMgr.Config.FileExtension);
            string fileName = string.Empty;
            foreach (string file in fileList)
            {
                fileName = Path.GetFileName(file);
                _publisher.Context.Log.Write(string.Format("Begin processing file '{0}'.", fileName));
                if (ProcessFileMessage(file))
                {
                    _publisher.Context.Log.Write(string.Format("Processing file '{0}' success.", fileName));
                }
                else
                {
                    _publisher.Context.Log.Write(LogType.Error, string.Format("Processing file '{0}' failed.", fileName));
                }
                _publisher.Context.Log.Write(string.Format("End processing file '{0}'.", fileName));
            }
        }

        private bool ProcessFileMessage(string filePath)
        {
            bool res = false;
            string sendData = string.Empty;

            switch (_publisher.Context.ConfigMgr.Config.MessageProcessingType)
            {
                case MessageProcessType.HL7v2Text:
                    res = ProcessHL7Message(filePath, ref sendData);
                    break;
                case MessageProcessType.HL7v2XML:
                    res = ProcessHL7XMLMessage(filePath, ref sendData);
                    break;
                case MessageProcessType.OtherXML:
                    res = ProcessOtherXMLMessage(filePath, ref sendData);
                    break;
            }

            ProccessSourceFile(filePath, res, sendData);
            return res;
        }

        private void ProccessSourceFile(string filePath, bool result, string response)
        {
            string fileName = Path.GetFileName(filePath);
            FileHelper.WriteFile(Path.Combine(_ackFodler, fileName + ".ACK"), response, _publisher.Context.ConfigMgr.Config.EncodeName);
            _publisher.Context.Log.Write("Write ack file " + fileName + ".ACK");

            if (result)
            {
                if (FileDisposeType.Delete == _publisher.Context.ConfigMgr.Config.SourceFileDisposeType)
                {
                    File.Delete(filePath);
                    _publisher.Context.Log.Write("Deleted file " + fileName);
                }
                if (FileDisposeType.Move == _publisher.Context.ConfigMgr.Config.SourceFileDisposeType)
                {
                    FileHelper.MoveFile(filePath, Path.Combine(_successFolder, fileName));
                    _publisher.Context.Log.Write(string.Format("Moved file {0}  to {1}", fileName, _successFolder));
                }
            }
            else
            {
                FileHelper.MoveFile(filePath, Path.Combine(_fialedFolder, fileName));
                _publisher.Context.Log.Write(string.Format("Moved file {0}  to {1}", fileName, _fialedFolder));
            }
        }

        private bool ReadFileContent(string filePath, ref string fileContent)
        {
            fileContent = File.ReadAllText(filePath, Encoding.GetEncoding(_publisher.Context.ConfigMgr.Config.EncodeName));
            if (string.IsNullOrEmpty(fileContent))
            {
                _publisher.Context.Log.Write("File is empty.");
                return false;
            }
            _publisher.Context.Log.Write(LogType.Debug, "File content is :\r\n" + fileContent);
            return true;
        }

        private bool ProcessHL7Message(string filePath, ref string sendData)
        {
            string fileContent = string.Empty;
            if (!ReadFileContent(filePath, ref fileContent))
            {
                return false;
            }
            Message notify = new Message();
            //notify.Header.Type = MessageRegistry.HL7V2_NotificationMessageType;
            notify.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            HYS.IM.Messaging.Registry.HL7MessageHelper.SetHL7V2PayLoad(notify, fileContent);

            _publisher.Context.Log.Write("Begin sending notification to subscriber.");
            bool ret = _publisher.NotifyMessagePublish(notify);
            _publisher.Context.Log.Write("End sending notification to subscriber. Result: " + ret.ToString());

            if (ret)
            {
                sendData = HL7MessageParser.FormatResponseMessage(fileContent,
                    _publisher.Context.ConfigMgr.Config.ReadHL7AckAATemplate());
            }
            else
            {
                sendData = HL7MessageParser.FormatResponseMessage(fileContent,
                    _publisher.Context.ConfigMgr.Config.ReadHL7AckAETemplate());
            }

            _publisher.Context.Log.Write("End processing HL7v2 text message");
            return ret;
        }

        private bool ProcessHL7XMLMessage(string filePath, ref string sendData)
        {
            string fileContent = string.Empty;
            if (!ReadFileContent(filePath, ref fileContent))
            {
                return false;
            }
            string fileName = Path.GetFileName(filePath);
            string strXml = fileContent;
            if (_transformer.TransformHL7v2ToXml(fileContent, out strXml))
            {
                _publisher.Context.Log.Write(LogType.Debug, "Transformed HL7 xml content is : \r\n" + strXml);
            }
            else
            {
                _publisher.Context.Log.Write(LogType.Error, "Transforming HL7 to xml error.");

                FileHelper.MoveFile(filePath, Path.Combine(_fialedFolder, fileName));
                _publisher.Context.Log.Write(string.Format("Moved file {0}  to {1}", fileName, _fialedFolder));
                return false;
            }

            bool res = DispatchXMLMessage(strXml);
            if (res)
            {
                sendData = HL7MessageParser.FormatResponseMessage(fileContent,
                                _publisher.Context.ConfigMgr.Config.ReadHL7AckAATemplate());
            }
            else
            {
                sendData = HL7MessageParser.FormatResponseMessage(fileContent,
                                _publisher.Context.ConfigMgr.Config.ReadHL7AckAETemplate());
            }
            return res;
        }

        private bool ProcessOtherXMLMessage(string filePath, ref string sendData)
        {
            string fileContent = string.Empty;
            if (!ReadFileContent(filePath, ref fileContent))
            {
                return false;
            }

            bool res = DispatchXMLMessage(XmlHelper.EatXmlDeclaration(fileContent));
            if (res)
            {
                res = _publisher.Context.ConfigMgr.Config.XSLTTransform(fileContent, ref sendData, FileReaderConfig.PublishingSuccessXSLTFileName);
            }
            else
            {
                res = _publisher.Context.ConfigMgr.Config.XSLTTransform(fileContent, ref sendData, FileReaderConfig.PublishingFailureXSLTFileName);
            }

            return res;
        }

        public bool StartTimer()
        {
            _readerTimer.Start();
            return true;
        }

        public bool StopTimer()
        {
            _readerTimer.Stop();
            return true;
        }
    }

    public interface IMessagePublisher
    {
        bool NotifyMessagePublish(Message message);

        ProgramContext Context { get;}
    }
}
