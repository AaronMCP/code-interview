using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class DsRoleProfileModel : FrameworkBaseModel
    {
        public string ModuleID;
        public string RoleID;

        public DsRoleProfileModel()
        {

        }

        public DsRoleProfileModel(string szModuleID, string szRoleID)
        {
            this.ModuleID = szModuleID;
            this.RoleID = szRoleID;
        }
    }
}
