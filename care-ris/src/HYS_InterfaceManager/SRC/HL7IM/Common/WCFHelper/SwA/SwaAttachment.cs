using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.WCFHelper.SwA
{
    public class SwaAttachment
    {
        public byte[] ContentBinary { get; set; }
        public string ContentType { get; set; }
        public string ContentId { get; set; }
    }
}
