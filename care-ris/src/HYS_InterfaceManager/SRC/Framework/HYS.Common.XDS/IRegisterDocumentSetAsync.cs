using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    /// <summary>
    /// IHE XDS Register Document Set transaction in asynchronous mode
    /// this service shall be implemented by a document repository actor
    /// and will be called by a document registry actor
    /// </summary>
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IRegisterDocumentSetAsync
    {
        [OperationContract(Action = XdsActionIdentifications.RegisterDocumentSetAsyncAction,
                           ReplyAction = XdsActionIdentifications.RegisterDocumentSetAsyncActionReply)]
        System.ServiceModel.Channels.Message RegisterDocumentSetAsync(System.ServiceModel.Channels.Message input);
    }
}
