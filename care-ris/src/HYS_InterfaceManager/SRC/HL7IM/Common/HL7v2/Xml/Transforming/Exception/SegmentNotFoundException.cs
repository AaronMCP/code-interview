using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml.Transforming
{
    public class SegmentNotFoundException:HL7Exception
    {
        public SegmentNotFoundException(string Message)
            : base("Segment not found exception: " + Message)
        {
        }

        public SegmentNotFoundException(string Message, Exception innerException)
            : base("Segment not found exception: " + Message, innerException)
        {
        }
    }
}
