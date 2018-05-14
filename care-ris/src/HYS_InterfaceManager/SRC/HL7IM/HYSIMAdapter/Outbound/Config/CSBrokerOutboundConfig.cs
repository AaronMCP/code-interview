using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;
using HYS.Common.Xml;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Config
{
    public class CSBrokerOutboundConfig : EntityConfigBase
    {
        public const string DEVICE_NAME = "CSBROKER_OUTBOUND";
        public const string CONFIG_FILE_NAME = "CSBrokerOutboundConfig.xml";

        public CSBrokerOutboundConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "CS Broker Outbound Adapter";
            DeviceName = DEVICE_NAME;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Subscriber;
            Direction = DirectionTypes.Outbound;
            ResponseConfig = null;
            RequestConfig = null;
            PublishConfig = null;

            // Other Default Configuration
            
            CSBrokerOLEDBConnectionString = "Provider=SQLNCLI;Server=(local)\\SQLExpress;Database=GWDataDB;UID=sa;Password=123456;";
            CSBrokerPassiveSQLInboundInterfaceName = "";
            EnableXMLTransform = false;
            EnaleKanJiReplacement = false;
            KanJiReplacementChar = ".";
            XSLTFilePath = "";
            EnableValueReplacement = false;
            _valueReplacement = new XCollection<ValueReplacementRule>();
            _valueReplacement.Add(new ValueReplacementRule()
            {
                FieldName = "ORDER_SCHEDULED_DT",
                MatchExpression = @"\b(?<year>\d{2,4})(?<month>\d{1,2})(?<day>\d{1,2})(?<hour>\d{1,2})(?<minute>\d{1,2})(?<second>\d{1,2}).(?<fractal>\d{1,3})\b",
                ReplaceExpression = @"${year}-${month}-${day} ${hour}:${minute}:${second}"
            });
        }

        [XCData(true)]
        public string CSBrokerOLEDBConnectionString { get; set; }
        [XCData(true)]
        public string CSBrokerPassiveSQLInboundInterfaceName { get; set; }

        public bool EnableXMLTransform { get; set; }
        public bool EnaleKanJiReplacement { get; set; }
        public string KanJiReplacementChar { get; set; }

        [XCData(true)]
        public string XSLTFilePath { get; set; }

        public bool EnableValueReplacement { get; set; }
        private XCollection<ValueReplacementRule> _valueReplacement;
        public XCollection<ValueReplacementRule> ValueReplacement
        {
            get { return _valueReplacement; }
            set { _valueReplacement = value; }
        }


      
    

    }
}
