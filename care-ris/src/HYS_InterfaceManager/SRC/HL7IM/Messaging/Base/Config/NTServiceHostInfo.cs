using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class NTServiceHostInfo : XObject
    {
        private string _serviceName = "";
        [XCData(true)]
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        private string _serviceDescription = "";
        [XCData(true)]
        public string ServiceDescription
        {
            get { return _serviceDescription; }
            set { _serviceDescription = value; }
        }

        private string _servicePath = "";
        [XCData(true)]
        public string ServicePath
        {
            get { return _servicePath; }
            set { _servicePath = value; }
        }
    }
}
