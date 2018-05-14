using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Config
{
    public class HL7GatewayConfigConfig : XObject
    {
        private readonly char[] MessageEntitySeperator = new char[] { ',' };
        public const string ConfigFileName = "HL7GatewayConfig.xml";

        public HL7GatewayConfigConfig()
        {
            // default configuration
            HL7GatewayInterfaceName = "HL7 Receiver Interface";
            IntegrationSolutionPath = "../Bin";
            MessageEntityGroup1 = "SOAP_IN,HL7_IN,FILE_IN";
            MessageEntityGroup2 = "SOAP_OUT,CSB_OUT";
        }

        [XCData(true)]
        public string HL7GatewayInterfaceName { get; set; }
        [XCData(true)]
        public string IntegrationSolutionPath { get; set; }
        [XCData(true)]
        public string MessageEntityGroup1 { get; set; }
        [XCData(true)]
        public string MessageEntityGroup2 { get; set; }

        public string[] GetMessageEntityGroup1()
        {
            if(string.IsNullOrEmpty(MessageEntityGroup1)) return new string[] {};
            return MessageEntityGroup1.Split(MessageEntitySeperator, 3);
        }
        public string[] GetMessageEntityGroup2()
        {
            if (string.IsNullOrEmpty(MessageEntityGroup2)) return new string[] { };
            return MessageEntityGroup2.Split(MessageEntitySeperator, 2);
        }

        private XCollection <MessageEntityConfigConfig> _messageEntityConfigConfigs = new XCollection<MessageEntityConfigConfig>();
        public XCollection<MessageEntityConfigConfig> MessageEntityConfigConfigs
        {
            get { return _messageEntityConfigConfigs; }
            set { _messageEntityConfigConfigs = value; }
        }

        public MessageEntityConfigConfig GetMessageEntityConfigConfig(string entityName)
        {
            MessageEntityConfigConfig cfg = null;
            if (string.IsNullOrEmpty(entityName)) return cfg;
            lock (_messageEntityConfigConfigs)
            {
                foreach (MessageEntityConfigConfig c in _messageEntityConfigConfigs)
                {
                    if (c.EntityName == entityName)
                    {
                        cfg = c;
                        break;
                    }
                }
                if (cfg == null)
                {
                    cfg = new MessageEntityConfigConfig();
                    cfg.EntityName = entityName;
                    _messageEntityConfigConfigs.Add(cfg);
                }
            }
            return cfg;
        }

        public bool FlipDiagram { get; set; }
    }
}
