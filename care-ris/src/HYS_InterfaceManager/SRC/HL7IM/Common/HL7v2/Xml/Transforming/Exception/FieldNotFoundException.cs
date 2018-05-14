using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class FieldNotFoundException:HL7Exception
    {
        public FieldNotFoundException(string Message)
            : base("Field not found exception: " + Message)
        {
        }

        public FieldNotFoundException(string Message, Exception innerException)
            : base("Field not found exception: " + Message, innerException)
        {
        }
    
    }
}
