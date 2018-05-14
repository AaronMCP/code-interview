using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Services
{
    public partial class BidirectionalService : AdapterService
    {
        public BidirectionalService()
        {
            BindInBoundAdapter();
        }

        private IBidirectionalAdapterService biAdapter;

        private void BindInBoundAdapter()
        {
            if (CheckAdapter()) return;

            biAdapter = adapter as IBidirectionalAdapterService;
            if (biAdapter == null)
            {
                Program.Log.Write(LogType.Error, "Cannot convert adapter instance into IBidirectionalAdapterService.");
            }
            else
            {
                Program.Log.Write("Adapter binded to bidirectional service.");
            }
        }
    }
}
