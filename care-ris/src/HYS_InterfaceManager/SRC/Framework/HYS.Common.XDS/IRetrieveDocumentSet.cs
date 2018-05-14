using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    /// <summary>
    /// IHE XDS Retrieve Document Set transaction
    /// </summary>
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IRetrieveDocumentSet
    {
        [OperationContract(Action = XdsActionIdentifications.RetrieveDocumentSetAction,
                           ReplyAction = XdsActionIdentifications.RetrieveDocumentSetActionReply)]
        System.ServiceModel.Channels.Message RetrieveDocumentSet(System.ServiceModel.Channels.Message input);
    }
}
