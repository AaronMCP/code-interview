using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Base
{
    public interface IAdapterAgent
    {
        string FileName { get; }
        Type Type { get;}
        AdapterEntryAttributeBase Attribute { get; }
        IAdapterBase Instance { get; }
    }
}
