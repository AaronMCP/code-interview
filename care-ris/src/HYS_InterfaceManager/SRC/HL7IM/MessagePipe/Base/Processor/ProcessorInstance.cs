using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public class ProcessorInstance : XObject
    {
        public ProcessorInstance()
        {
            Enable = true;
        }

        public bool Enable { get; set; }

        [XCData(true)]
        public string Name { get; set; }
        [XCData(true)]
        public string Description { get; set; }
        [XCData(true)]
        public string DeviceName { get; set; }

        /// <summary>
        /// Should serialized from class derived from ProcessorConfigBase
        /// </summary>
        [XRawXmlString(true)]
        public string Setting { get; set; }

        //for plugin use in the future
        //
        //[XCData(true)]
        //public string TypeName { get; set; }
        //[XCData(true)]
        //public string AssemablyLocation { get; set; }
    }
}
