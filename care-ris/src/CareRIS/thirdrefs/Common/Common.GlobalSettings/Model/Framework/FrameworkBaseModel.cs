using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(DsSystemProfileModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DsRoleProfileModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DsUserProfileModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SaveUserProfileModel))]
    [System.Xml.Serialization.XmlInclude(typeof(DsSiteProfileModel))]
    public abstract class FrameworkBaseModel : BaseModel
    {
    }
}
