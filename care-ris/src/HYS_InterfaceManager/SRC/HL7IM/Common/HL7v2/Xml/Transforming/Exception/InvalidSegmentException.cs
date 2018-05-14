using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class InvalidSegmentException:HL7Exception
    {
        public InvalidSegmentException(string Message)
            : base("Invalid segment exception: " + Message)
        {
        }

        public InvalidSegmentException(string Message, Exception innerException)
            : base("Invalid segment exception: " + Message, innerException)
        {
        }
    }
}
