using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class InvalidDelimetersException:HL7Exception
    {
        public InvalidDelimetersException(string Message)
            : base("Invalid delimeter exception: "+Message)
        {
        }

        public InvalidDelimetersException(string Message, Exception innerException)
            : base("Invalid delimeter exception: " + Message, innerException)
        {
        }
    }
}
