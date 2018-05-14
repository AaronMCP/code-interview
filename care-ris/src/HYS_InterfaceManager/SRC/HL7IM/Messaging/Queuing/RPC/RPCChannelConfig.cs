using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.RPC
{
    public class RPCChannelConfig : XObject
    {
        private string _uri = "";
        [XCData(true)]
        public string URI
        {
            get { return _uri; }
            set { _uri = value; }
        }
    }
}
