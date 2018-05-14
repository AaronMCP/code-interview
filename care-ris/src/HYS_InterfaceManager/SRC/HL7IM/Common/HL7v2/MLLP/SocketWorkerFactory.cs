using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketWorkerFactory
    {
        public static string[] SocketWorkerTypeRegistry = new string[] 
        {
            SocketWorker.DEVICE_NAME,
            SocketWorkerWithLongConnection.DEVICE_NAME,
            //SocketWorkerWithBreakPointSupport.DEVICE_NAME,
            SocketWorkerNoMLLP.DEVICE_NAME,
            SocketWorkerWithLongConnectionNoMLLP.DEVICE_NAME,
        };

        internal static ISocketWorker Create(int id, Socket socket, SocketServer server)
        {
            if (socket == null || server == null || server.Config == null) return null;
            switch (server.Config.SocketWorkerType)
            {
                default: return null;
                case SocketWorker.DEVICE_NAME: return new SocketWorker(id, socket, server);
                case SocketWorkerWithLongConnection.DEVICE_NAME: return new SocketWorkerWithLongConnection(id, socket, server);
                case SocketWorkerWithLongConnection2.DEVICE_NAME: return new SocketWorkerWithLongConnection2(id, socket, server);
                //case SocketWorkerWithBreakPointSupport.DEVICE_NAME: return new SocketWorkerWithBreakPointSupport(id, socket, server);
                case SocketWorkerNoMLLP.DEVICE_NAME: return new SocketWorkerNoMLLP(id, socket, server);
                case SocketWorkerWithLongConnectionNoMLLP.DEVICE_NAME: return new SocketWorkerWithLongConnectionNoMLLP(id, socket, server);
            }
        }
    }
}
