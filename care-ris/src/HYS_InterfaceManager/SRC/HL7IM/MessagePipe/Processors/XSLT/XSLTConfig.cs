using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Common.Xml;


namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    public class XSLTConfig : ProcessorConfigBase
    {
        internal const string DEVICE_NAME = "XSLT";
        internal const string DEVICE_DESC = "Transfer XML format with defined XSLT file";

        public XSLTConfig()
        {
            DeviceName = DEVICE_NAME;
            DeviceDescription = DEVICE_DESC;
            LoadDefaultConfiguration();
        }

        private void LoadDefaultConfiguration()
        {
            XSLTFileName = "XSLT.xsl";
        }

        [XCData(true)]
        public string XSLTFileName { get; set; }
    }
}
