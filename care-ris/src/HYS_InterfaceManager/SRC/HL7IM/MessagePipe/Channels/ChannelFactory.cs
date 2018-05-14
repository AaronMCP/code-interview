using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Channels.SubscribePublish;
using HYS.MessageDevices.MessagePipe.Channels.RequestResponse;

namespace HYS.MessageDevices.MessagePipe.Channels
{
    public class ChannelFactory
    {
        public static string[] ChannelRegistry = new string[]{
            SubscribePublishChannelConfig.DEVICE_NAME,
            RequestResponseChannelConfig.DEVICE_NAME,
        };

        public static IChannel CreateChannel(string deviceName, ILog log)
        {
            try
            {
                switch (deviceName)
                {
                    case SubscribePublishChannelConfig.DEVICE_NAME: return new SubscribePublishChannelImpl();
                    case RequestResponseChannelConfig.DEVICE_NAME: return new RequestResponseChannelImpl();
                }

                return null;
            }
            catch (Exception e)
            {
                if (log != null) log.Write(e);
                return null;
            }
        }

        public static IChannelConfig CreateChannelConfig(string deviceName, ILog log)
        {
            try
            {
                switch (deviceName)
                {
                    case SubscribePublishChannelConfig.DEVICE_NAME: return new SubscribePublishChannelImplCfg();
                    case RequestResponseChannelConfig.DEVICE_NAME: return new RequestResponseChannelImplCfg();
                }

                return null;
            }
            catch (Exception e)
            {
                if (log != null) log.Write(e);
                return null;
            }
        }
    }
}
