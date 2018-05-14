using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.WCFHelper;

namespace HYS.IM.Common.WCFHelper
{
    public class SoapMessageBase
    {
        public SoapEnvelopeVersion SoapEnvelopeVersion { get; set; }
        public WSAddressingVersion WSAddressingVersion { get; set; }
        public string SoapEnvelopeBodyContent { get; set; }
    }
}
