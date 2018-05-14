using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Config.Controls
{
    public interface IConfigControl
    {
        bool LoadConfig();
        bool SaveConfig();
    }
}
