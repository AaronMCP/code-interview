using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class MInterfaceConfigSection : XObject
    {
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

        private string _configFileName = "";
        [XCData(true)]
        public string ConfigFileName
        {
            get { return _configFileName; }
            set { _configFileName = value; }
        }

        private XCollection<MInterfaceConfigItem> _items = new XCollection<MInterfaceConfigItem>();
        public XCollection<MInterfaceConfigItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
