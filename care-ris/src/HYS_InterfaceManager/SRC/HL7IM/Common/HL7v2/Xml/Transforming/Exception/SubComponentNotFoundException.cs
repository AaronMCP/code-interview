using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class SubComponentNotFoundException:HL7Exception
    {
        public SubComponentNotFoundException(string Message)
            : base("Subcomponent not found exception: " + Message)
        {
        }

        public SubComponentNotFoundException(string Message, Exception innerException)
            : base("Subcomponent not found exception: " + Message, innerException)
        {
        }
    }
}
