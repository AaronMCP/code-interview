using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageHistoryCollection : XCollection<MessageHistory>
    {
        public MessageHistoryCollection()
            : base("History")
        {
        }

        public int GetRetryTimes(Guid entityID)
        {
            foreach (MessageHistory his in this)
            {
                if (his.EntityID == entityID) return his.RetryTimes;
            }
            return 0;
        }
    }
}
