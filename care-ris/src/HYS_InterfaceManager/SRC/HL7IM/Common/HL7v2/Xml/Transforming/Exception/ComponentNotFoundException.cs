using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class ComponentNotFoundException:HL7Exception
    {
        public ComponentNotFoundException(string Message)
            : base("Component not found exception: " + Message)
        {
        }

        public ComponentNotFoundException(string Message, Exception innerException)
            : base("Component not found exception: "+Message, innerException)
        {
        }
    }
}
