using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    /// <summary>
    /// IHE XDS ProvideAndRegisterDocumentSet transaction
    /// </summary>
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IProvideAndRegisterDocumentSet
    {
        [OperationContract(Action = XdsActionIdentifications.ProvideAndRegisterDocumentSetAction,
                           ReplyAction = XdsActionIdentifications.ProvideAndRegisterDocumentSetActionReply)]
        System.ServiceModel.Channels.Message ProvideAndRegisterDocumentSet(System.ServiceModel.Channels.Message input);
    }
}
