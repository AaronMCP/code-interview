using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQParameter : XObject
    {
        private string _path = "";
        [XCData(true)]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
    }
}
