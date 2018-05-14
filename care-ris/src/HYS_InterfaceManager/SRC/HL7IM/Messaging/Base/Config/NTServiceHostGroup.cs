using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    // To simply ISMP for G3 release by exposing/maintaining duplicated NT Service Host of File Outbound
    public class NTServiceHostGroup  : XObject
    {
        private string _groupName = "";
        [XCData(true)]
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        private string _defaultNTServiceHostName = "";
        [XCData(true)]
        public string DefaultNTServiceHostName
        {
            get { return _defaultNTServiceHostName; }
            set { _defaultNTServiceHostName = value; }
        }

        private XCollection<NTServiceHostInformation> _ntServiceHostList = new XCollection<NTServiceHostInformation>();
        public XCollection<NTServiceHostInformation> NTServiceHostList
        {
            get { return _ntServiceHostList; }
            set { _ntServiceHostList = value; }
        }

        public NTServiceHostInformation FindNTServiceHostByName(string name)
        {
            if (name == null) return null;
            foreach (NTServiceHostInformation i in NTServiceHostList)
            {
                if (i.ServiceName == name) return i;
            }
            return null;
        }
    }
}
