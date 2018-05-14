using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    /// <summary>
    /// For interfaces to generate customized db script (such as SP) into CS Broker database. 20090401
    /// </summary>
    public interface IRuleSupplier
    {
        string GetInstallDBScript();
        string GetUninstallDBScript();
    }
}
