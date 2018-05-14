using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.XDS
{
    public class XdsActionIdentifications
    {
        public const string NameSpace = "urn:ihe:iti:xds-b:2007";

        public const string ProvideAndRegisterDocumentSetAction = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b";
        public const string ProvideAndRegisterDocumentSetActionReply = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-bResponse";

        public const string RegisterDocumentSetAction = "urn:ihe:iti:2007:RegisterDocumentSet-b";
        public const string RegisterDocumentSetActionReply = "urn:ihe:iti:2007:RegisterDocumentSet-bResponse";

        /// <summary>
        /// asynchronous mode of register document set, this message is sent from document registry to document repository
        /// after the disconnection of previous register document set message exchange between repository and registry
        /// </summary>
        public const string RegisterDocumentSetAsyncAction = "urn:ihe:iti:2007:DocumentSource_RegisterDocumentSet-bAsyncResponse";
        public const string RegisterDocumentSetAsyncActionReply = "*";

        public const string RegistryStoredQueryAction = "urn:ihe:iti:2007:RegistryStoredQuery";
        public const string RegistryStoredQueryActionReply = "urn:ihe:iti:2007:RegistryStoredQueryResponse";

        public const string RetrieveDocumentSetAction = "urn:ihe:iti:2007:RetrieveDocumentSet";
        public const string RetrieveDocumentSetActionReply = "urn:ihe:iti:2007:RetrieveDocumentSetResponse";
    }
}
