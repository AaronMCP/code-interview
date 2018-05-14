using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    /// <summary>
    /// IHE XDS Register Document Set transaction
    /// </summary>
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IRegisterDocumentSet
    {
        [OperationContract(Action = XdsActionIdentifications.RegisterDocumentSetAction,
                           ReplyAction = XdsActionIdentifications.RegisterDocumentSetActionReply)]
        System.ServiceModel.Channels.Message RegisterDocumentSet(System.ServiceModel.Channels.Message input);
    }
}
