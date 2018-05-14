using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class UnableToGetStringException:HL7Exception
    {
        public UnableToGetStringException(string Message)
            : base("Unable get string exception: " + Message)
        {
        }

        public UnableToGetStringException(string Message, Exception innerException)
            : base("Unable get string exception: " + Message, innerException)
        {
        }
    }
}
