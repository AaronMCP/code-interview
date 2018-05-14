using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class MessageTypeNotFoundException:HL7Exception
    {
        public MessageTypeNotFoundException(string Message)
            : base("Message type not found exception: " + Message)
        {
        }

        public MessageTypeNotFoundException(string Message, Exception innerException)
            : base("Message type not found exception: " + Message, innerException)
        {
        }

    }
}
