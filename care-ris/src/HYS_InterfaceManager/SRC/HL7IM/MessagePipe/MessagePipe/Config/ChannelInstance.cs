using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Config
{
    public class ChannelInstance : XObject
    {
        public ChannelInstance()
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
        /// These option could be used to implement if-else logic in data flow inside message pipe by
        /// passing on the original message to next channel when the pervious channel process error.
        /// 
        /// Currently the logic of passing on a processed message to next channel 
        /// (which can faciliate a loop-while data flow)
        /// is not supported. It is assumed user can connect multiple message pipe to implement that.
        /// </summary>
        public bool PassOnOriginalMessageToNextChannelIfProcessingError { get; set; }
        //public bool PassOnOriginalMessageToNextChannelIfSendingError { get; set; }
        //public bool PassOnOriginalMessageToNextChannelIfProcessingSuccess { get; set; }

        /// <summary>
        /// Should serialized from class derived from ChannelConfigBase
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
