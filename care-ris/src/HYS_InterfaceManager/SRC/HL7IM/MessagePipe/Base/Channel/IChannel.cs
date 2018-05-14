using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public interface IChannel
    {
        bool Initialize(ChannelInitializationParameter parameter);
        ChannelProcessResult ProcessSubscriberMessage(MessagePackage message);
        ChannelProcessResult ProcessResponserMessage(MessagePackage request, out MessagePackage response);
        bool Uninitilize();
    }
}
