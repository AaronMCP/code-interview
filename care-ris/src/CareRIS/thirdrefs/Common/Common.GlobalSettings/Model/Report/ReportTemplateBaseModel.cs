using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ReportTemplateModel))]
    [System.Xml.Serialization.XmlInclude(typeof(PhraseTemplateModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ExaminTemplateModel))]
    public abstract class ReportTemplateBaseModel:BaseModel
    {
        
    }
}
