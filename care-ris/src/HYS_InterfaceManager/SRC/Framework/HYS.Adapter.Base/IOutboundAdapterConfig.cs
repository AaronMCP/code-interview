using System;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Base
{
    public interface IOutboundAdapterConfig : IAdapterConfig
    {
        /// <summary>
        /// Register outbound rule into GC Gateway system.
        /// GC Gateway will use this rule to create storage procedures to perform data mapping and data access.
        /// </summary>
        IOutboundRule[] GetRules();
    }
}
