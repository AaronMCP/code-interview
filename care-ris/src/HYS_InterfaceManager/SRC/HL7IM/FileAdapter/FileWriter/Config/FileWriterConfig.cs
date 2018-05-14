using System;
using HYS.IM.Common.HL7v2.Xml;
using HYS.Common.Xml;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Config
{
    public class FileWriterConfig : EntityConfigBase
    {
        public const string DEVICE_NAME = "FILE_WRITER";
        public const string CONFIG_FILE_NAME = "FileWriterConfig.xml";

        public FileWriterConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "File Writer";
            DeviceName = DEVICE_NAME;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Subscriber;
            Direction = DirectionTypes.Outbound;
            ResponseConfig = null;
            RequestConfig = null;
            PublishConfig = null;

            // Other Default Configuration
            OutputFileFolder = "C:\\Output";
            MessageProcessingType = MessageProcessType.HL7v2XML;
            FileExtension = ".INI";
            CodePageName = "utf-8";
            OrganizationMode = FileOrganizationMode.Day;
            HL7XMLTransformerType = NHL7ToolkitTransformer.DEVICE_NAME;
        }

        [XCData(true)]
        public string OutputFileFolder { get; set; }
        [XCData(true)]
        public string FileExtension { get; set; }
        [XCData(true)]
        public FileOrganizationMode OrganizationMode { get; set; }
        [XCData(true)]
        public string CodePageName { get; set; }

        public MessageProcessType MessageProcessingType { get; set; }

        [XCData(true)]
        public string HL7XMLTransformerType { get; set; }
    }

    public enum FileOrganizationMode
    {
        /// <summary>
        /// Output file into output folder directly.
        /// </summary>
        None,
        /// <summary>
        /// Organizing file by years.
        /// </summary>
        Year,
        /// <summary>
        /// Organizing file by quarters.
        /// </summary>
        Quarter,
        /// <summary>
        /// Organizing file by months.
        /// </summary>
        Month,
        /// <summary>
        /// Organizing file by weeks.
        /// </summary>
        Week,
        /// <summary>
        /// Organizing file by days.
        /// </summary>
        Day,
        /// <summary>
        /// Organizing file by hours.
        /// </summary>
        Hour
    }

    public enum MessageProcessType
    {
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains raw HL7v2 text in it.
        /// </summary>
        HL7v2Text = 0,
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains HL7v2 XML in it,
        /// and could be transform to HL7v2 text by XmlTransformer in Common.HL7v2 namespace.
        /// </summary>
        HL7v2XML = 1,
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains other type of XML in it,
        /// including HL7v3 XML and non-standard XML.
        /// </summary>
        OtherXML = 2,
    }
}
