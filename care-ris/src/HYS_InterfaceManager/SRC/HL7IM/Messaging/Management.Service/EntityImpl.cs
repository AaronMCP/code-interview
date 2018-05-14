using System;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Collections.Generic;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Management;
using HYS.IM.Messaging.Management.Config;

namespace HYS.IM.Messaging.Management.Service
{
    [MessageEntityEntry("XDS Gateway Management Service", DirectionTypes.Unknown, InteractionTypes.Unknown, "XDS Gateway Management Service")]
    public class EntityImpl : IMessageEntity
    {
        #region IMessageEntity Members

        public bool Start()
        {
            try
            {
                int port = Program.ConfigMgt.Config.RemotingPort;
                if (port > 30808) port = 10808;
                int maxPort = port + 10;

                for (; port <= maxPort; port++)
                {
                    Program.Log.Write("Trying to start remoting service on port: " + port);
                    if (StartChannel(port)) return true;
                }

                Program.Log.Write(LogType.Error, "Failed to start remoting service on ports " + port + " - " + maxPort);
                return false;
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                return false;
            }
        }

        private bool StartChannel(int port)
        {
            try
            {
                Program.Log.Write("Starting remoting service...");

                HttpChannel chn = new HttpChannel(port);
                ChannelServices.RegisterChannel(chn, false);

                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(ManagementService),
                    ServiceConfig.ServiceName,
                    WellKnownObjectMode.SingleCall);

                string url = string.Format("http://localhost:{0}/{1}",
                    port, ServiceConfig.ServiceName);
                Program.Log.Write("Remoting service started at " + url);

                Program.ConfigMgt.Config.RemotingPort = port;
                Program.ConfigMgt.Config.RemotingUrl = url;
                
                if (!Program.ConfigMgt.Save())
                {
                    Program.Log.Write(LogType.Error, "Save configuration failed.");
                    Program.Log.Write(Program.ConfigMgt.LastError);
                }

                return true;
            }
            catch (System.Net.Sockets.SocketException)
            {
                return false;
            }
        }

        public bool Stop()
        {
            return true;
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            Program.PreLoading(arg);
            return true;
        }

        public EntityConfigBase GetConfiguration()
        {
            return new EntityConfigBase();
        }

        public bool Uninitalize()
        {
            Program.BeforeExit();
            return true;
        }

        #endregion
    }
}
