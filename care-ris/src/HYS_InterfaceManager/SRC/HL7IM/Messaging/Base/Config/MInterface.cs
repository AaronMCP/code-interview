using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects.Entity;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class MInterface : XObject
    {
        public MInterface()
        {
            _id = EntityDictionary.GetRandomNumber();
        }

        private string _id;
        [XCData(true)]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        [XCData(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private XCollection<NTServiceHostInfo> _hosts = new XCollection<NTServiceHostInfo>();
        public XCollection<NTServiceHostInfo> Hosts
        {
            get { return _hosts; }
            set { _hosts = value; }
        }

        private XCollection<MInterfaceConfigPage> _configPages = new XCollection<MInterfaceConfigPage>();
        public XCollection<MInterfaceConfigPage> ConfigPages
        {
            get { return _configPages; }
            set { _configPages = value; }
        }

        private XCollection<MInterfaceMonitorPage> _monitorPages = new XCollection<MInterfaceMonitorPage>();
        public XCollection<MInterfaceMonitorPage> MonitorPages
        {
            get { return _monitorPages; }
            set { _monitorPages = value; }
        }
    }
}
