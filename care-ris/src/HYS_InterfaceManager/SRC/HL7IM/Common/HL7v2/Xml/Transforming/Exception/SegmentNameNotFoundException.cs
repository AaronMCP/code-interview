using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class SegmentNameNotFoundException:HL7Exception
    {
        public SegmentNameNotFoundException(string Message)
            : base("Segment name not found exception: " + Message)
        {
        }

        public SegmentNameNotFoundException(string Message, Exception innerException)
            : base("Segment name not found exception: " + Message, innerException)
        {
        }
    }
}
