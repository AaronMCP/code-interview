using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Config;

namespace HYS.MessageDevices.MessagePipe.Controler
{
    public class ChannelControler
    {
        public readonly IChannel ChannelImpl;
        public readonly ChannelInstance ChannelConfig;

        public ChannelControler(IChannel chn, ChannelInstance chnCfg)
        {
            ChannelImpl = chn;
            ChannelConfig = chnCfg;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", ChannelConfig.Name, ChannelConfig.DeviceName);
        }
    }
}
