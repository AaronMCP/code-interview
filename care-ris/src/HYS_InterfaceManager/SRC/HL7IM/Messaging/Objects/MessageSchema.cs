using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageSchema : XObject
    {
        private Guid _id = Guid.Empty;
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _verion = "";
        public string Version
        {
            get { return _verion; }
            set { _verion = value; }
        }

        private string _location = "";
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
