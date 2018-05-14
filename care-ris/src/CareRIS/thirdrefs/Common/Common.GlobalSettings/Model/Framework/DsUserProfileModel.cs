using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class DsUserProfileModel : FrameworkBaseModel
    {
        public string ModuleID;
        public string RoleID;
        public string UserID;

        public DsUserProfileModel()
        {

        }

        public DsUserProfileModel(string szModuleID, string szRoleID, string szUserID)
        {
            this.ModuleID = szModuleID;
            this.RoleID = szRoleID;
            this.UserID = szUserID;
        }
    }
}
