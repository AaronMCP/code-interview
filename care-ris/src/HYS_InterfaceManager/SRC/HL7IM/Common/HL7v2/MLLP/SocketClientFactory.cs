using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketClientFactory
    {
        public static string[] SocketClientTypeRegistry = new string[] 
        {
            SocketClient.DEVICE_NAME,
            SocketClientWithLongConnection.DEVICE_NAME,
            SocketClientNoMLLP.DEVICE_NAME,
            SocketClientWithLongConnectionNoMLLP.DEVICE_NAME,
        };

        public static IClient Create(SocketClientConfig cfg)
        {
            if (cfg == null) return null;
            switch (cfg.SocketClientType)
            {
                default: return null;
                case SocketClient.DEVICE_NAME: return new SocketClient(cfg);
                case SocketClientWithLongConnection.DEVICE_NAME: return new SocketClientWithLongConnection(cfg);
                case SocketClientNoMLLP.DEVICE_NAME: return new SocketClientNoMLLP(cfg);
                case SocketClientWithLongConnectionNoMLLP.DEVICE_NAME: return new SocketClientWithLongConnectionNoMLLP(cfg);
            }
        }
    }
}
