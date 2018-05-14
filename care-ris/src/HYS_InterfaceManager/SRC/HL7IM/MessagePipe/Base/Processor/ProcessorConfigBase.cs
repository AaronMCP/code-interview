using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Base.Processor
{
    public abstract class ProcessorConfigBase : XObject
    {
        [XCData(true)]
        public string DeviceName { get; set; }
        [XCData(true)]
        public string DeviceDescription { get; set; }
    }
}
