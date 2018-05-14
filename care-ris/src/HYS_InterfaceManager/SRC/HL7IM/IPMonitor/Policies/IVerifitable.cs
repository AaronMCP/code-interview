using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.IPMonitor.Policies
{
    public interface IVerifitable
    {
        bool Validation(string ServiceName);
    }
}
