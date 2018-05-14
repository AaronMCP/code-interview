using System;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Registry;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Config
{
    public partial class CSBrokerInboundConfig : EntityConfigBase
    {
        public const string DEVICE_NAME = "CSBroker_Inbound";
        public const string CONFIG_FILE_NAME = "CSBrokerInboundConfig.xml";

        public CSBrokerInboundConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "CS Broker Inbound Adapter";
            DeviceName = DEVICE_NAME;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Publisher;
            Direction = DirectionTypes.Inbound;
            PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.GENERIC_NotificationMessageType);
            SubscribeConfig = null;
            ResponseConfig = null;

            // Other Default Configuration
            CSBrokerConnectionString = "Provider=SQLNCLI;Server=(local)\\SQLExpress;Database=GWDataDB;UID=sa;Password=123456;";
            CSBrokerSQLOutboundName = "SQL_OUT";
            TimerInterval = 3000;
            EnaleKanJiReplacement = false;
            KanJiReplacementChar = ".";

            EnableValueReplacement = false;
            _valueReplacement = new XCollection<ValueReplacementRule>();
            _valueReplacement.Add(new ValueReplacementRule()
            {
                FieldName = "ORDER_SCHEDULED_DT",
                MatchExpression = @"\b(?<year>\d{2,4})(?<month>\d{1,2})(?<day>\d{1,2})(?<hour>\d{1,2})(?<minute>\d{1,2})(?<second>\d{1,2}).(?<fractal>\d{1,3})\b",
                ReplaceExpression = @"${year}-${month}-${day} ${hour}:${minute}:${second}"
            });

            MessageDispatchConfig.Model = MessageDispatchModel.Request;
            MessageDispatchConfig.TableName = GWDataDBTable.None.ToString();
            MessageDispatchConfig.FieldName = string.Empty;
        }

        /// <summary>
        /// Get or set connection string of cs broker database.
        /// </summary>
        [XCData(true)]
        public string CSBrokerConnectionString { get; set; }

        /// <summary>
        /// Get or set SQL Server outbound interface name.
        /// </summary>
        [XCData(true)]
        public string CSBrokerSQLOutboundName { get; set; }
        public bool EnaleKanJiReplacement { get; set; }
        public string KanJiReplacementChar { get; set; }

        /// <summary>
        /// Get or set time interval for reading data from database..
        /// </summary>
        [XCData(true)]
        public double TimerInterval { get; set; }

        public bool EnableValueReplacement { get; set; }
        private XCollection<ValueReplacementRule> _valueReplacement;
        public XCollection<ValueReplacementRule> ValueReplacement
        {
            get { return _valueReplacement; }
            set { _valueReplacement = value; }
        }

        private MessageDispatchConfig _dispatchConifg = new MessageDispatchConfig(); 
        public MessageDispatchConfig MessageDispatchConfig
        {
            get { return _dispatchConifg; }
            set { _dispatchConifg = value; }
        }
    }

    public enum MessageDispatchModel
    {
        Publish,        // dispatch message to XDSGW publisher
        Request,        // dispatch message to XDSGW requester
        Custom,         // dispatch message to XDSGW publisher or requester according to message content
    }

    public class MessageDispatchConfig : XObject
    {
        public MessageDispatchModel Model { get; set; }

        [XCData(true)]
        public string TableName { get; set; }
        [XCData(true)]
        public string FieldName { get; set; }
        [XCData(true)]
        public string ValueSubscriber { get; set; }
        [XCData(true)]
        public string ValueResponser { get; set; }
    }
}
