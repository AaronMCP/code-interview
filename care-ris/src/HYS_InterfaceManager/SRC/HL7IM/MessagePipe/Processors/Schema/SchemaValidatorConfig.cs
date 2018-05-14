using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Messaging.Objects;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Processors.Schema
{
    public class SchemaValidatorConfig : ProcessorConfigBase
    {
        internal const string DEVICE_NAME = "XML Schema Validator";
        internal const string DEVICE_DESC = "XML Schema 1.0 validation mechanism provided by .Net 3.5";

        public SchemaValidatorConfig()
        {
            DeviceName = DEVICE_NAME;
            DeviceDescription = DEVICE_DESC;
            LoadDefaultConfiguration();
        }

        private void LoadDefaultConfiguration()
        {
            SchemaFileName = "Schema.xsd";
        }

        [XCData(true)]
        public string SchemaFileName { get; set; }
    }
}
