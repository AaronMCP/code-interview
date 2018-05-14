using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects.Entity;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class MInterfaceMonitorPage : XObject
    {
        public MInterfaceMonitorPage()
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

        private string _configFileName = "";
        [XCData(true)]
        public string ConfigFileName
        {
            get { return _configFileName; }
            set { _configFileName = value; }
        }

        private MInterfaceMonitorPageType _type;
        public MInterfaceMonitorPageType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
