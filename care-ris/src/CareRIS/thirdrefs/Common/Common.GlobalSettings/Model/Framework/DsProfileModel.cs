using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class DsSystemProfileModel : FrameworkBaseModel
    {
        public string ModuleID;

        public DsSystemProfileModel()
        {

        }

        public DsSystemProfileModel(string szModuleID)
        {
            this.ModuleID = szModuleID;
        }
    }

    [Serializable]
    public class DsSiteProfileModel : FrameworkBaseModel
    {
        public string ModuleID;
        public string SiteName;

        public DsSiteProfileModel()
        {

        }

        public DsSiteProfileModel(string szModuleID,string szSiteName)
        {
            this.ModuleID = szModuleID;
            this.SiteName = szSiteName;
        }
    }
}
