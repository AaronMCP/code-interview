using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace CommonGlobalSettings
{
    [Serializable()]
    public class SaveUserProfileModel : FrameworkBaseModel
    {
        public string RoleName;
        public string UserGUID;
        public DataSet DataSet;
        
        public SaveUserProfileModel()
        {

        }

        public SaveUserProfileModel(string szRoleName, string szUserGUID, DataSet dsDataSet)
        {
            this.RoleName = szRoleName;
            this.UserGUID = szUserGUID;
            this.DataSet = dsDataSet;
        }
    }
}
