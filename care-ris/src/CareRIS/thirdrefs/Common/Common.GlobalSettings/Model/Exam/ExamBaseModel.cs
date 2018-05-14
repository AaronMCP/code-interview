using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ExamModel))]
    public abstract class ExamBaseModel : BaseModel
    {

    }
}