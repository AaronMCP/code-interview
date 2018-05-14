using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(PatientModel))]
    public abstract class PatientBaseModel : BaseModel
    {

    }
}