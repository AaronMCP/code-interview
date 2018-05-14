using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageRelationCollection : XCollection<MessageRelation>
    {
        public MessageRelationCollection()
            : base("Relation")
        {
        }
    }
}
