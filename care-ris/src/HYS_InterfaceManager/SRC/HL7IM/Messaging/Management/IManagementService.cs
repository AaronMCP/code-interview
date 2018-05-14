using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Management.Scripts;
using HYS.IM.Messaging.Management.NTServices;

namespace HYS.IM.Messaging.Management
{
    public interface IManagementService
    {
        ScriptTaskResult Execute(ScriptTask task);
        bool SetServiceStatusAndStartStyle(string name, ServiceStatus status);
    }
}
