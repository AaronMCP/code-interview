using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class InvalidFieldException:HL7Exception
    {
        public InvalidFieldException(string Message)
            : base("Invalid field exception: "+ Message)
        {
        }

        public InvalidFieldException(string Message, Exception innerException)
            : base("Invalid field exception: " + Message, innerException)
        {
        }

    }
}
