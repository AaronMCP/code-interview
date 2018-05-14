using System;
using System.Data;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Base
{
    public interface IOutboundAdapterService : IAdapterService
    {
        /// <summary>
        /// Outbound adapter receives query request from 3rd party data destination,
        /// and then invoke this event, specifying criteria and rule for this destination.
        /// AdapterService.exe then return a DataSet containing query result,
        /// according to this criteria and rule.
        /// 
        /// Notice: before invoking this event, IInboundRule should be registered into GC Gateway system.
        /// See interface IAdapterConfig definition.
        /// </summary>
        event DataRequestEventHanlder OnDataRequest;

        /// <summary>
        /// If necsessary, outbound adapter should set a process flag to processed data,
        /// after sending this data to data destination successfully.
        /// 
        /// Notice: before invoking this event, IInboundRule should be registered into GC Gateway system.
        /// See interface IAdapterConfig definition.
        /// </summary>
        event DataDischargeEventHanlder OnDataDischarge;
    }

    public delegate DataSet DataRequestEventHanlder(IOutboundRule rule, DataSet criteria);

    public delegate bool DataDischargeEventHanlder(string[] guidList);
}
