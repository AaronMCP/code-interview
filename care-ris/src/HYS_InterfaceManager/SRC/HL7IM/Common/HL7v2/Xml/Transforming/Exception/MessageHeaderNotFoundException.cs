using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class MessageHeaderNotFoundException:HL7Exception
    {
        public MessageHeaderNotFoundException(string Message)
            : base("Message header not found exception: " + Message)
        {
        }

        public MessageHeaderNotFoundException(string Message, Exception innerException)
            : base("Message header not found exception: " + Message, innerException)
        {
        }
    }
}
