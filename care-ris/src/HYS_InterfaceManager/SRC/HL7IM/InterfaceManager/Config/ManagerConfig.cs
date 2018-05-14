using System;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;

namespace HYS.HL7IM.Manager.Config
{
    public class ManagerConfig : XObject
    {
        public const string FileName = "HL7GatewayManagerConfig.xml";

        public XCollection < HL7InterfaceConfig> InterfaceList { get;set;}

        public string LoginUser { get; set; }

        public string LoginPassword { get; set; }

        public string AppCaption { get; set; }

        public LogType LogType { get; set; }

        public int GetInterfaceCount(InterfaceType iType)
        {
            int count = 0;
            foreach (HL7InterfaceConfig item in InterfaceList)
            {
                if (item.InterfaceType == iType)
                {
                    count++;
                }
            }

            return count;
        }

        internal static ManagerConfig CreateDefaultConfig()
        {
            ManagerConfig config = new ManagerConfig();

            config.LoginUser = "service";
            config.LoginPassword = "service";
            config.AppCaption = "HL7 Gateway Interface Manager";
            config.LogType = LogType.Error;

            config.InterfaceList = new XCollection<HL7InterfaceConfig>();

            HL7InterfaceConfig sender = new HL7InterfaceConfig();
            sender.InterfaceFolder = "Sender\\HL7GW_SND";
            sender.InstallDate = DateTime.Now;
            sender.InterfaceName = "HL7GW_SND";
            sender.InterfaceType = InterfaceType.Sender;
            sender.InterfaceStatus = InterfaceStatus.Running;

            config.InterfaceList.Add(sender);

            HL7InterfaceConfig receiver = new HL7InterfaceConfig();
            receiver.InterfaceFolder = "Receiver\\HL7GW_RCV";
            receiver.InstallDate = DateTime.Now;
            receiver.InterfaceName = "HL7GW_RCV";
            receiver.InterfaceType = InterfaceType.Receiver;
            receiver.InterfaceStatus = InterfaceStatus.Running;

            config.InterfaceList.Add(receiver);

            return config;
        }

        internal bool CheckInteraceName(string strInterfaceName, InterfaceType InterfaceType)
        {
            foreach (HL7InterfaceConfig item in InterfaceList)
            {
                if (item.InterfaceType == InterfaceType && item.InterfaceName == strInterfaceName)
                {
                    return true;
                }
            }
            return false;
        }

        public void RefreshConfigInfo()
        {
            foreach (HL7InterfaceConfig config in InterfaceList)
            {
                HL7ConfigHelper.LoadConfigInfo(config);
            }
        }
    }

    public class HL7InterfaceConfig :XObject
    {
        [XCData(true)]
        public string InterfaceFolder { get; set; }

        [XCData(true)]
        public InterfaceType InterfaceType { get; set; }

        [XNode(false)]
        public InterfaceStatus InterfaceStatus { get; set; }

        public DateTime InstallDate { get; set; }

        [XNode(false)]
        public string InterfaceName { get; set; }
    }

    public enum InterfaceType
    {
        Receiver,
        Sender
    }

    public enum InterfaceStatus
    {
        Running,
        Stopped,
        Desabled,
        Unkown
    }
}
