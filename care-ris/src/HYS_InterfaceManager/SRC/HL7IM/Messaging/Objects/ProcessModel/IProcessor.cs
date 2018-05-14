using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Messaging.Objects.ProcessModel
{
    /// <summary>
    /// single or multiple inputs, single or multiple output
    /// </summary>
    public interface IProcessor
    {
        ProcesserInfo Info { get;}

        MessagePackage[] Process(params MessagePackage[] inputs);

        event PreProcessEventHanlder Processing;
        event PostProcessEventHanlder Processed;
    }

    public delegate void PreProcessEventHanlder(ProcesserInfo desc, MessagePackage[] inputs, out bool cancel);
    public delegate void PostProcessEventHanlder(ProcesserInfo desc, MessagePackage[] inputs, MessagePackage[] outputs, bool success);
}
