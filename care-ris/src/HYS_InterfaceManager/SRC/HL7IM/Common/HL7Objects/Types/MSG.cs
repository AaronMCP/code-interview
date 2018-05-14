using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.HL7Objects.Types
{
    public class MSG : XObject
    {
        public string MessageCode;
        public string TriggerEvent;
        public string MessageStructure;
    }
}
