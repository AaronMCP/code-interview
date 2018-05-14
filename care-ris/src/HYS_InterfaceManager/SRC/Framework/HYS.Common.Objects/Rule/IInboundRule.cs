using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public interface IInboundRule : IRule
    {
        /// <summary>
        /// Provide mapping items for this inbound adapter.
        /// </summary>
        /// <returns></returns>
        QueryResultItem[] GetQueryResultItems();
    }
}
