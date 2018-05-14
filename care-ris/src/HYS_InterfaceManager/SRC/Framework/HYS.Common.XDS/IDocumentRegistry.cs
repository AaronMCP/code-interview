using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Common.XDS
{
    [ServiceContract(Namespace = "urn:ihe:iti:xds-b:2007")]
    public interface IDocumentRegistry : IRegisterDocumentSet, IRegisterDocumentSetAsync, IRegistryStoredQuery
    {
    }
}
