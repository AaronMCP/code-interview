using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Messaging.Queuing.RPC
{
    [ServiceContract(Name = "RequestResponse", Namespace = "http://www.carestreamhealth.com/xdsgateway")]
    public interface IRPCPullService
    {
        [OperationContract]
        bool ProcessMessage(string request, out string response);
    }
}
