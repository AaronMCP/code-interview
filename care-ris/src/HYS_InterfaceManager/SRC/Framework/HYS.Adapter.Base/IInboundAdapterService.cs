using System;
using System.Data;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Base
{
    public interface IInboundAdapterService : IAdapterService
    {
        /// <summary>
        /// Add original dataset to Adapter Service.
        /// This dataset will be mapped and insert into database by Adapter Service.
        /// 
        /// Notice: before invoking this event, IInboundRule should be registered into GC Gateway system.
        /// See interface IAdapterConfig definition.
        /// </summary>
        /// <param name="data">Original dataset from data source</param>
        /// <returns>Return true when all the relative tables in the database are affected.</returns>
        event DataReceiveEventHandler OnDataReceived;
    }

    public delegate bool DataReceiveEventHandler(IInboundRule rule, DataSet data);
}
