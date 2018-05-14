using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public interface IOutboundRule : IRule
    {
        /// <summary>
        /// Provide query result mapping items for this outbound adapter.
        /// </summary>
        /// <returns></returns>
        QueryResultItem[] GetQueryResultItems();
        /// <summary>
        /// Provide query criteria mapping items from this outbound adapter.
        /// </summary>
        /// <returns></returns>
        QueryCriteriaItem[] GetQueryCriteriaItems();
        /// <summary>
        /// Specify the query criteria type of this outbound adapter.
        /// </summary>
        /// <returns></returns>
        QueryCriteriaRuleType QueryCriteriaRuleType { get; }
        /// <summary>
        /// Whether to set process flag to 1(processed) after the data record is selected.
        /// </summary>
        /// <returns></returns>
        bool AutoUpdateProcessFlag { get; }
        /// <summary>
        /// Whether to select data record where process flag is 1(processed).
        /// </summary>
        /// <returns></returns>
        bool CheckProcessFlag { get; }
        /// <summary>
        /// 20080326
        /// Max record count of each query. This limitation is for performance consideration. 
        /// If MaxRecordCount less than 0, it means no limitation.  
        /// </summary>
        int MaxRecordCount { get; }
    }
}
