using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityInformation : XObject
    {
        private Guid _entityID;
        public Guid EntityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }

        private string _name = "";
        [XCData(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _deviceName = "";
        [XCData(true)]
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
