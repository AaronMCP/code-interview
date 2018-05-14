using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace HYS.IM.Messaging.Queuing.RPC
{
    public class RPCPullService : IRPCPullService
    {
        public bool ProcessMessage(string request, out string response)
        {
            // these exception should be handled in programming/unit-test time,
            // therefore do not need to write to log.

            RPCServiceHost host = OperationContext.Current.Host as RPCServiceHost;
            if (host == null)
                throw new ArithmeticException("Cannot find RPCServiceHost in current operation context.");

            RPCPullReceiver receiver = host.Tag as RPCPullReceiver;
            if (receiver == null)
                throw new ArithmeticException("Cannot find RPCPullReceiver in current operation context.");

            return receiver.ProcessMessage(request, out response);
        }
    }
}
