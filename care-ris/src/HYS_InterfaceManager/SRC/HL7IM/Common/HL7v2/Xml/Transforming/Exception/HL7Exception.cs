using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class HL7Exception:ApplicationException
    {
        public HL7Exception(string Message)
            : base(Message)
        {
        }

        public HL7Exception(string Message, Exception innerException)
            : base(Message, innerException)
        {
        }
    }
}
