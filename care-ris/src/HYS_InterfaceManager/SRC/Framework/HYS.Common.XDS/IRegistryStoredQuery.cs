using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    /// <summary>
    /// IHE XDS Registy Stored Query transaction
    /// </summary>
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IRegistryStoredQuery
    {
        [OperationContract(Action = XdsActionIdentifications.RegistryStoredQueryAction,
                           ReplyAction = XdsActionIdentifications.RegistryStoredQueryActionReply)]
        System.ServiceModel.Channels.Message RegistryStoredQuery(System.ServiceModel.Channels.Message input);
    }
}
