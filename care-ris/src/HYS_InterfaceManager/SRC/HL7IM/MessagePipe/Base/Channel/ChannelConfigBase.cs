using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public abstract class ChannelConfigBase : XObject
    {
        [XCData(true)]
        public string DeviceName { get; set; }
        [XCData(true)]
        public string DeviceDescription { get; set; }

        private MessageEntryConfig _entryControl = new MessageEntryConfig();
        public MessageEntryConfig EntryControl
        {
            get { return _entryControl; }
            set { _entryControl = value; }
        }
    }
}
