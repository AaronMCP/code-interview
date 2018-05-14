using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    // currently not necessary
    //[ServiceContract(Name = "XDSGWSOAPAdatperContract", Namespace = "http://www.carestreamhealth.com")] 
    [ServiceContract]
    public interface IAbstractContract
    {
        [OperationContract(Action = "*")]       // for client use
        Message SendMessage(Message request);

        [OperationContract(Action = "ProcessMessage", ReplyAction = "*")]      // for server use
        Message ProcessMessage(Message request);
    }
}
