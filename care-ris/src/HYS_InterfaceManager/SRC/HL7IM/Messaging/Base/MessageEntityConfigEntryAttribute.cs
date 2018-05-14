using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageEntityConfigEntryAttribute : EntryAttribute
    {
        public MessageEntityConfigEntryAttribute()
        {
        }
    }
}
