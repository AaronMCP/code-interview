using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Logging;
//using HYS.Common.DataAccess;
using HYS.Messaging.Objects;
using HYS.Messaging.Objects.Entity;
using HYS.Messaging.Objects.PublishModel;
using HYS.Messaging.Objects.RequestModel;
using HYS.Messaging.Base.Config;
using HYS.Messaging.Queuing.MSMQ;
using HYS.Messaging.Queuing.LPC;
using HYS.Messaging.Queuing;
using HYS.Messaging.Mapping;
using HYS.Messaging.Objects.RoutingModel;

namespace HYS.Messaging.Test
{
    static class Program
    {
        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        public const string AppName = "Messaging.Test";
        public const string AppConfigFileName = NTServiceHostConfig.NTServiceHostConfigFileName;    // "NTServiceHost.xml";;
        
        public static LogControler Log;
        public static ConfigManager<NTServiceHostConfig> ConfigMgt;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Directory.Delete(@"D:\temp\a"); //throw exception if folder not empty
            //return;

            //SortedDictionary<int, string> dic = new SortedDictionary<int, string>();
            //dic.Add(-2, "a");
            //dic.Add(-1, "b");
            //dic.Add(-3, "c");
            //StringBuilder sb = new StringBuilder();
            //foreach (KeyValuePair<int, string> p in dic)
            //{
            //    sb.AppendLine(p.Key.ToString() + "_" + p.Value);
            //}
            //MessageBox.Show(sb.ToString());
            //return;

            //List<string> alist = new List<string>();
            //alist.Capacity = 3;
            //alist.Add("a");
            //alist.Add("b");
            //alist.Add("c");
            //alist.Add("d");
            //alist.Add("e");
            ////alist.Add("f");
            //MessageBox.Show(alist.Count.ToString() + "//" + alist.Capacity.ToString());
            //return;

            //string path = Path.GetDirectoryName(@"c:\..\..\aaa");
            //string path = Path.GetDirectoryName(Path.GetDirectoryName(@"c:\a"));
            //MessageBox.Show("'" + path + "'");
            //return;

            //string folder = Path.GetFileName(@"C:\asd\aa");
            //MessageBox.Show(folder);
            //return;

            //Test_Path();
            //return;

            //XObjectManager.OnError += new XObjectExceptionHandler(XObjectManager_OnError);

            //Test_Body();
            //return;

            Log = new LogControler(AppName);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            ConfigMgt = new ConfigManager<NTServiceHostConfig>(AppConfigFileName);
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);

                MessageBox.Show("Cannot load configuration file.");
                return;
            }

            //Test_Header();
            //Test_Message();
            //Test_SubscribeCriteria();
            
            //Test_EntityContract();
            //Test_EntityConfig();

            //Test_ConfigMgt();
            //return;


            //AppDomain.CurrentDomain.AppendPrivatePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\DemoFileAdapter\bin\Debug");
            
            //string str = AppDomain.CurrentDomain.RelativeSearchPath;
            //str = AppDomain.CurrentDomain.DynamicDirectory;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());

            Log.WriteAppExit(AppName);
        }

        private static void Test_Path()
        {
            string str = null;

            //MessageBox.Show(Path.Combine("C:\\a\\", "a"));
            //MessageBox.Show(Path.Combine("C:\\a\\", "\\a"));
            //return;

            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a\");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"..\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"C:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a\b\c\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\NewFolder\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            str = ConfigHelper.GetRelativePath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\NewFolder\a\b\a.txt");
            MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));

            //str = DirectPath2IndirectPath(@"a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"..\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"C:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\Debug\a\b\c\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\Test\bin\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\NewFolder\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
            //str = DirectPath2IndirectPath(@"D:\ClearCase\10095177_GCGateway_view_dev_mobile\gcgateway\XDS-Gateway\SRC\Messaging\NewFolder\a\b\a.txt");
            //MessageBox.Show(str + "\r\n\r\n" + Path.GetFullPath(str));
        }
        private static string DirectPath2IndirectPath(string directFilePath)
        {
            if (!Path.IsPathRooted(directFilePath)) return directFilePath;

            // AppPath = C:\A\B
            string fromPath = Application.StartupPath.ToLowerInvariant();

            string toPath = Path.GetDirectoryName(directFilePath);
            string toFile = Path.GetFileName(directFilePath);

            if (toPath.ToLowerInvariant().Contains(fromPath))   // directFilePath = C:\A\B\D\f.exe or C:\A\B\f.exe
            {
                string path = toPath.Substring(fromPath.Length);
                if (path.StartsWith("\\")) path = path.TrimStart('\\');
                else if (path.StartsWith("/")) path = path.TrimStart('/');
                path = Path.Combine(path, toFile);
                return path;
            }
            else // directFilePath = C:\A\D\f.exe or C:\A\f.exe or D:\A\f.exe
            {
                char[] fList = fromPath.Replace('/','\\').ToCharArray();
                char[] tList = toPath.ToLowerInvariant().Replace('/', '\\').ToCharArray();

                int flen = fList.Length;
                int tlen = tList.Length;
                int len = flen > tlen ? flen : tlen;

                int i = 0;
                for (; i < len; i++)
                {
                    char cf = i < flen ? fList[i] : '\0';
                    char ct = i < tlen ? tList[i] : '\0';
                    if (cf != ct) break;
                }

                if (i < 1) return directFilePath;   // D:\A\f.exe

                StringBuilder sb = new StringBuilder();
                
                if (i < flen)
                {
                    string diffFromPath = fromPath.Substring(i).TrimStart('\\');
                    string[] pathDepth = diffFromPath.Split('\\');
                    for (int j = 0; j < pathDepth.Length; j++) sb.Append("..\\");
                }

                if (i < tlen)
                {
                    string diffToPath = toPath.Substring(i);
                    sb.Append(diffToPath);
                }

                string path = sb.ToString();
                path = Path.Combine(path, toFile);
                return path;
            }
        }

        private static void XObjectManager_OnError(object source, Exception error)
        {
            MessageBox.Show(source + "\r\n\r\n" + error);
        }

        private static void Test_Header()
        {
            MessageHeader h = new MessageHeader();
            h.ID = Guid.NewGuid();

            string xml = h.ToXMLString();
            MessageBox.Show(xml);

            MessageHeader h1 = XObjectManager.CreateObject<MessageHeader>(xml);
            MessageBox.Show(h1.ToXMLString());
        }
        private static void Test_Message()
        {
            HYS.Messaging.Objects.Message msg = new HYS.Messaging.Objects.Message();

            msg.Reference = new MessageReference();
            //msg.Reference.FileList = new XCollection<MessageReferenceFile>("File");
            //msg.Reference.FileList = new XCollection<MessageReferenceFile>();
            msg.Reference.FileList.Add(new ReferenceFile());
            msg.Reference.FileList.Add(new ReferenceFile());

            msg.Reference.FileList[0].Name = "name test";
            msg.Reference.FileList[1].Location ="location test";

            msg.Body = "aaaaa";
            //msg.Body = "aa<aaa";

            string xml = msg.ToXMLString();
            MessageBox.Show(xml);

            HYS.Messaging.Objects.Message msg1 = XObjectManager.CreateObject<HYS.Messaging.Objects.Message>(xml);
            MessageBox.Show(msg1.ToXMLString());

            xml = msg.Reference.FileList.ToXMLString();
            MessageBox.Show(xml);

            //XCollection<MessageReferenceFile> list = XObjectManager.CreateObject<XCollection<MessageReferenceFile>>(xml);
            //MessageBox.Show(list.ToXMLString());

            ReferenceFileCollection list = XObjectManager.CreateObject<ReferenceFileCollection>(xml);
            MessageBox.Show(list.ToXMLString());
        }
        private static void Test_SubscribeCriteria()
        {
            SubscriptionRule sc = new SubscriptionRule();
            sc.Type = RoutingRuleType.MessageType;

            string xml = sc.ToXMLString();
            MessageBox.Show(xml);

            SubscriptionRule sc1 = XObjectManager.CreateObject<SubscriptionRule>(xml);
            MessageBox.Show(sc1.ToXMLString());
        }

        private static void Test_Body()
        {
            //string xmlstring = "<a><b>hello</b></a>";
            string xmlstring = XMLHeader + "<a><b>hello</b></a>";
            //XmlTextReader r = new XmlTextReader(new StringReader(xmlstring));
            //if(r.Read()) xmlstring = r.ReadOuterXml();

            HYS.Messaging.Objects.Message msg = new HYS.Messaging.Objects.Message();
            msg.Body = HYS.Messaging.Objects.Message.RemoveXmlHeader(xmlstring);

            string str = msg.ToXMLString();
            MessageBox.Show(str);

            HYS.Messaging.Objects.Message newmsg = XObjectManager.CreateObject<HYS.Messaging.Objects.Message>(str);
            MessageBox.Show(newmsg.ToXMLString());
        }

        private static MessageType GetUploadMessageFromMetaDataRepository()
        {
            MessageSchema sch1 = new MessageSchema();
            sch1.ID = Guid.NewGuid();
            sch1.Name = "Department upload request schema for Renji EMR Integration";
            sch1.Location = "Renji_IntegrationUpload.xsd";

            MessageType mt1 = new MessageType();
            mt1.Code = "DEPT_UPLOAD";
            mt1.CodeSystem = "RENJI";
            mt1.Schema = sch1;

            return mt1;
        }
        private static MessageType GetErrorMessageFromMetaDataRepository()
        {
            MessageSchema sch2 = new MessageSchema();
            sch2.ID = Guid.NewGuid();
            sch2.Name = "Department upload error response schema for Renji EMR Integration";
            sch2.Location = "Renji_IntegrationError.xsd";

            MessageType mt2 = new MessageType();
            mt2.Code = "DEPT_UPLOAD_ERR";
            mt2.CodeSystem = "RENJI";
            mt2.Schema = sch2;

            return mt2;
        }
        //private static LPCSenderParameter GetMessageBoxLPCChannelFromResourceRegisty()
        //{
        //    LPCSenderParameter p = new LPCSenderParameter();
        //    p.AssemblyName = "MessageBox";
        //    p.ClassName = "HYS.MessageBox.MessageBoxAgent";
        //    p.Location = "HYS.MessageBox.dll";
        //    p.AssemblyDatabase.ConnectionString = "database connection string goes here";
        //    p.AssemblyConfigLocation = @"../MsgBox1/";
        //    return p;
        //}
        private static MSMQReceiverParameter GetMSMQPRCChannelFromResourceRegisty()
        {
            MSMQReceiverParameter q = new MSMQReceiverParameter();
            q.MSMQ.Path = "127.0.0.1/ErrorQueue";
            return q;
        }

        public class FileInboundChannel : XObject //: ProcessConfig
        {
            private string _inputFolderPath = @"C:\input";
            public string InputFolderPath
            {
                get { return _inputFolderPath; }
                set { _inputFolderPath = value; }
            }

            private string _validationSuccessFolderPath = @"C:\input\ok";
            public string ValidationSuccessFolderPath
            {
                get { return _validationSuccessFolderPath; }
                set { _validationSuccessFolderPath = value; }
            }

            private string _validationErrorFolderPath = @"C:\input\error";
            public string ValidationErrorFolderPath
            {
                get { return _validationErrorFolderPath; }
                set { _validationErrorFolderPath = value; }
            }

            private int _timerInterval = 10000; //10s
            public int TimerInterval
            {
                get { return _timerInterval; }
                set { _timerInterval = value; }
            }
        }
        public class FileOutboundChannel : XObject //: ProcessConfig
        {
            private string _submittingErrorFolderPath = @"C:\output";
            public string SubmittingErrorFolderPath
            {
                get { return _submittingErrorFolderPath; }
                set { _submittingErrorFolderPath = value; }
            }
        }

        public class FileInterfaceContract : EntityContractBase
        {
            private XCollection<FileInboundChannel> _fileInboundConfig = new XCollection<FileInboundChannel>();
            public XCollection<FileInboundChannel> FileInboundConfig
            {
                get { return _fileInboundConfig; }
                set { _fileInboundConfig = value; }
            }

            private XCollection<FileOutboundChannel> _fileOutboundConfig = new XCollection<FileOutboundChannel>();
            public XCollection<FileOutboundChannel> FileOutboundConfig
            {
                get { return _fileOutboundConfig; }
                set { _fileOutboundConfig = value; }
            }
        }
        private static void Test_EntityContract()
        {
            //FileInterfaceContract c = new FileInterfaceContract();
            
            //c.DeviceID = Guid.NewGuid();
            //c.Name = "File Inbound Interface";
            //c.Direction = DirectionTypes.Inbound | DirectionTypes.Outbound;
            //c.Description = "File Inbound Interface for Renji EMR Integration\r\n"
            //+ "1. read 3rd party files, transform data and publish file-submitting message to integration platform\r\n"
            //+ "2. subscribe file-submitting-error message from integration platform, transform data and write file to 3rd party";
            //c.Interaction = InteractionTypes.Publisher | InteractionTypes.Subscriber;

            //c.FileInboundConfig.Add(new FileInboundChannel());
            //c.FileOutboundConfig.Add(new FileOutboundChannel());

            //c.Publication = new PublicationDescription();
            //c.Publication.MessageTypeList = new XCollection<MessageType>();
            //c.Publication.MessageTypeList.Add(GetUploadMessageFromMetaDataRepository());

            //c.Subscription = new SubscriptionRule();
            //c.Subscription.Type = SubscriptionRuleType.MessageType;
            //c.Subscription.MessageTypeList = new XCollection<MessageType>();
            //c.Subscription.MessageTypeList.Add(GetErrorMessageFromMetaDataRepository());

            //c.Subscription.ContentCriteriaList = null;
            //c.ResponseDescription = null;
            //c.RequestDescription = null;

            //using (StreamWriter sw = File.CreateText("FileInbound_DeviceDir.xml"))
            //{
            //    sw.WriteLine(XMLHeader);
            //    sw.Write(c.ToXMLString());
            //}
        }
        
        /// <summary>
        /// 1. read 3rd party files, transform data and publish file-submitting message to integration platform
        /// 2. subscribe file-submitting-error message from integration platform, transform data and write file to 3rd party
        /// </summary>
        public class FileInterfaceConfiguration : EntityConfigBase
        {
            private XCollection<FileInboundChannel> _fileInboundConfig = new XCollection<FileInboundChannel>();
            public XCollection<FileInboundChannel> FileInboundConfig
            {
                get { return _fileInboundConfig; }
                set { _fileInboundConfig = value; }
            }

            private XCollection<FileOutboundChannel> _fileOutboundConfig = new XCollection<FileOutboundChannel>();
            public XCollection<FileOutboundChannel> FileOutboundConfig
            {
                get { return _fileOutboundConfig; }
                set { _fileOutboundConfig = value; }
            }
        }
        private static void Test_EntityConfig()
        {
            //FileInterfaceConfiguration c = new FileInterfaceConfiguration();

            //c.EntityID = Guid.NewGuid();
            //c.Name = "File Inbound Interface";
            //c.Direction = DirectionTypes.Inbound | DirectionTypes.Outbound;
            //c.Description = "File Inbound Interface for Renji EMR Integration\r\n"
            //+ "1. read 3rd party files, transform data and publish file-submitting message to message box\r\n"
            //+ "2. subscribe file-submitting-error message from SOAP outbound interface, transform data and write file to 3rd party";
            //c.Interaction = InteractionTypes.Publisher | InteractionTypes.Subscriber;

            //c.FileInboundConfig.Add(new FileInboundChannel());
            //c.FileOutboundConfig.Add(new FileOutboundChannel());

            //PublishSenderChannel sendChannel = new PublishSenderChannel();
            //sendChannel.Subscription = new SubscriptionRule();
            //sendChannel.Subscription.Type = SubscriptionRuleType.MessageType;
            //sendChannel.Subscription.MessageTypeList = new XCollection<MessageType>();
            //sendChannel.Subscription.MessageTypeList.Add(GetUploadMessageFromMetaDataRepository());

            //sendChannel.ProtocolType = ProtocolType.LPC;
            //sendChannel.LPCParameter = GetMessageBoxLPCChannelFromResourceRegisty();
            //sendChannel.MSMQParameter = null;

            ////sendChannel.ProcessConfig.ProcessOrder.Add(sendChannel.ProcessConfig.XSDValidater.Info);
            ////sendChannel.ProcessConfig.ProcessOrder.Add(sendChannel.ProcessConfig.XSLTransformer.Info);

            ////c.PublishConfig = new EntityPublishConfig<FileInboundChannel>();
            //c.PublishConfig.Publication = new PublicationDescription();
            //c.PublishConfig.Publication.MessageTypeList = new XCollection<MessageType>();
            //c.PublishConfig.Publication.MessageTypeList.Add(GetUploadMessageFromMetaDataRepository());
            //c.PublishConfig.Channels.Add(sendChannel);

            ////PublishReceiverChannel receiveChannel = new PublishReceiverChannel();
            ////receiveChannel.Subscription = new SubscriptionRule();
            ////receiveChannel.Subscription.Type = SubscriptionRuleType.MessageType;
            ////receiveChannel.Subscription.MessageTypeList = new XCollection<MessageType>();
            ////receiveChannel.Subscription.MessageTypeList.Add(GetErrorMessageFromMetaDataRepository());

            ////receiveChannel.ProtocolType = ProtocolType.MSMQ;
            ////receiveChannel.MSMQParameter = GetMSMQPRCChannelFromResourceRegisty();
            ////receiveChannel.LPCParameter = null;

            //c.SubscribeConfig = new SubscribeConfig();
            ////c.SubscribeConfig.Channels.Add(receiveChannel);

            //c.ResponseConfig = null;
            //c.RequestConfig = null;

            //using (StreamWriter sw = File.CreateText("FileInbound_InterfaceDir.xml"))
            //{
            //    sw.WriteLine(XMLHeader);
            //    sw.Write(c.ToXMLString());
            //}
        }

        public static void Test_ConfigMgt()
        {
            ConfigManager<NTServiceHostConfig> mgt = new ConfigManager<NTServiceHostConfig>("NTServiceHostConfig.xml");
            mgt.Load();

            MessageBox.Show(mgt.Config.Entities.Count.ToString());
        }
    }
}
