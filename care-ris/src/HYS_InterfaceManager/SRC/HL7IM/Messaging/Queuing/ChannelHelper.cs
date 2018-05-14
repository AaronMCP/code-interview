using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Messaging.Queuing.RPC;
using HYS.IM.Messaging.Queuing.MSMQ;

namespace HYS.IM.Messaging.Queuing
{
    public class ChannelHelper
    {
        public static void GenerateLPCChannel(PushChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.LPCConfig = new LPCChannelConfig();
            cfg.LPCConfig.SenderEntityID = cfg.SenderEntityID;
            cfg.LPCConfig.ReceiverEntityID = cfg.ReceiverEntityID;

            cfg.MSMQConfig = null;
        }

        public static void GenerateLPCChannel(PullChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.LPCConfig = new LPCChannelConfig();
            cfg.LPCConfig.SenderEntityID = cfg.SenderEntityID;
            cfg.LPCConfig.ReceiverEntityID = cfg.ReceiverEntityID;

            cfg.RPCConfig = null;
        }

        public static void GenerateMSMQChannel(PushChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.MSMQConfig = new MSMQChannelConfig();
            cfg.MSMQConfig.SenderParameter.MSMQ.Path =
                cfg.MSMQConfig.ReceiverParameter.MSMQ.Path = @".\private$\" + cfg.SenderEntityName + "_" + cfg.ReceiverEntityName;

            // In workgroup mode, we only can create a private queue, and set permissions for Everyone so that NT service can access it.
            // Public queue can only be created in domain mode.

                //cfg.MSMQConfig.ReceiverParameter.MSMQ.Path = @".\" + cfg.SenderEntityName + "_" + cfg.ReceiverEntityName;

            // WinForm Host is running on login user permission, NT Service Host is running on SYSTEM user permissin.
            // Private queue can only be accessed by login user as default, we have to configure queue security setting manually before NT Service Host can access it.
            // Public queue can accessed by SYSTEM user as default, therefore we use public queue to build the push channel as default.  20080820

            cfg.LPCConfig = null;
        }

        public static void GenerateWCFNamedPipeChannel(PullChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.RPCConfig = new RPCChannelConfig();
            cfg.RPCConfig.URI = string.Format("net.pipe://localhost/{0}_{1}", cfg.SenderEntityName, cfg.ReceiverEntityName);

            cfg.LPCConfig = null;
        }

        public static void GenerateWCFTcpChannel(PullChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.RPCConfig = new RPCChannelConfig();
            cfg.RPCConfig.URI = string.Format("net.tcp://localhost:{0}/{1}_{2}", GeneratePortNumber(), cfg.SenderEntityName, cfg.ReceiverEntityName);

            cfg.LPCConfig = null;
        }

        public static void GenerateWCFSoapChannel(PullChannelConfig cfg)
        {
            if (cfg == null) return;

            cfg.RPCConfig = new RPCChannelConfig();
            cfg.RPCConfig.URI = string.Format("http://localhost:{0}/{1}_{2}", GeneratePortNumber(), cfg.SenderEntityName, cfg.ReceiverEntityName);

            cfg.LPCConfig = null;
        }

        private static string GeneratePortNumber()
        {
            Random m = new Random(DateTime.Now.Millisecond);
            return m.Next(9000, 10000).ToString();
        }
    }
}
