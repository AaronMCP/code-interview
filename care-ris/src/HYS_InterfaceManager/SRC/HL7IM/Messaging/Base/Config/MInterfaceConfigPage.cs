using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects.Entity;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class MInterfaceConfigPage : XObject
    {
        public MInterfaceConfigPage()
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

        private MInterfaceConfigPageType _type;
        public MInterfaceConfigPageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _configFileName = "";
        [XCData(true)]
        public string ConfigFileName
        {
            get { return _configFileName; }
            set { _configFileName = value; }
        }

        private XCollection<MInterfaceConfigSection> _sections = new XCollection<MInterfaceConfigSection>();
        public XCollection<MInterfaceConfigSection> Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }
    }
}
