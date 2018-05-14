using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Management;
using HYS.IM.Messaging.Management.Scripts;
using HYS.IM.Messaging.Management.NTServices;

namespace HYS.IM.Messaging.Management.Service
{
    public class ManagementService : MarshalByRefObject, IManagementService
    {
        public ScriptTaskResult Execute(ScriptTask task)
        {
            ScriptTaskResult res = new ScriptTaskResult();

            if (task == null)
            {
                res.Success = false;
                res.Message = "No script to execute.";
                Program.Log.Write(res.Message);
                return res;
            }

            res.Message = ScriptMgt.ExecuteBatFile(task.File, task.Argument, task.WorkPath, Program.Log);
            res.Success = true;
            return res;
        }

        public bool SetServiceStatusAndStartStyle(string name, ServiceStatus status)
        {
            bool res = ServiceMgt.SetServiceStatus(name, status, Program.Log);

            if (res)
            {
                switch (status)
                {
                    case ServiceStatus.Running:
                        res = ServiceMgt.SetServiceStartStyle(name, ServiceMgt.Automatic, Program.Log);
                        break;
                    case ServiceStatus.Stopped:
                        res = ServiceMgt.SetServiceStartStyle(name, ServiceMgt.Manual, Program.Log);
                        break;
                }
            }

            return res;
        }
    }
}
