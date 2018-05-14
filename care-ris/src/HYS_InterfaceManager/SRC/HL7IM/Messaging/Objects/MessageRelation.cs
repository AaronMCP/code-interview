using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageRelation : XObject
    {
        private Guid _referenceID = Guid.Empty;
        public Guid ReferenceID
        {
            get { return _referenceID; }
            set { _referenceID = value; }
        }

        private MessageRelationType _type;
        public MessageRelationType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
