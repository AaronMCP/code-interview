using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class InvalidMessageTypeException:HL7Exception
    {
        public InvalidMessageTypeException(string Message)
            : base("Invalid message tpye exception: "+Message)
        {
        }

        public InvalidMessageTypeException(string Message, Exception innerException)
            : base("Invalid message tpye exception: " + Message, innerException)
        {
        }
        
    }
}
