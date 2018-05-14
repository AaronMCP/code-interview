using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.XDS
{
    public class XdsRegistryError
    {
        public string CodeContext { get; set; }

        public string ErrorCode { get; set; }

        public string Severity { get; set; }

        public string Location { get; set; }
    }
}
