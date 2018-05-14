using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public interface ICodePageIndicator
    {
        string CodePageName { get; }
        int CodePageCode { get; }
    }
}
