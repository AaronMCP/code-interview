using HYS.Common.Objects.Logging;
using HYS.SQLInboundAdapterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallExternalMethod
{
    public class ExternalClass : IThrPartyAccessApp
    {
        public void LoadMethod(ILogging log)
        {
            log.Write(LogType.Info, "start load method");
        }
    }
}
