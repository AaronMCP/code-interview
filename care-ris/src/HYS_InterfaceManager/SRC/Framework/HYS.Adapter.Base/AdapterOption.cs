using System;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Base
{
    public class AdapterOption
    {
        public bool EnableCombination;
        public bool EnableDataProcess;
        public bool EnableGarbageCollection;
        public bool EnableLogConfig;

        public AdapterOption()
        {
        }
        public AdapterOption(DirectionType type)
        {
            if (type == DirectionType.INBOUND)
            {
                EnableDataProcess = true;
                EnableCombination = false;
                EnableGarbageCollection = true;
                EnableLogConfig = false;
            }
            else if (type == DirectionType.OUTBOUND)
            {
                EnableDataProcess = true;
                EnableCombination = true;
                EnableGarbageCollection = true;
                EnableLogConfig = false;
            }
            else
            {
                EnableDataProcess = false;
                EnableCombination = false;
                EnableGarbageCollection = false;
                EnableLogConfig = false;
            }
        }
    }
}
