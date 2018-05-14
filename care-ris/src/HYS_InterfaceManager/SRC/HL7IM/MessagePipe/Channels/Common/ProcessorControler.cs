using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.MessageDevices.MessagePipe.Base.Channel;

namespace HYS.MessageDevices.MessagePipe.Channels.Common
{
    public class ProcessorControler
    {
        public readonly IProcessor ProcessorImpl;
        public readonly ProcessorInstance ProcessorConfig;

        public ProcessorControler(IProcessor proc, ProcessorInstance procCfg)
        {
            ProcessorImpl = proc;
            ProcessorConfig = procCfg;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", ProcessorConfig.Name, ProcessorConfig.DeviceName);
        }
    }
}
