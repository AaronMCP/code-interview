using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(SearchCriteria))]
    [System.Xml.Serialization.XmlInclude(typeof(GraphicArrivalFlag))]
    [System.Xml.Serialization.XmlInclude(typeof(TimePeriod))]
    [System.Xml.Serialization.XmlInclude(typeof(ModalityTypeMaxAssignCount))]
    [System.Xml.Serialization.XmlInclude(typeof(SpecialCriteria))]
    [System.Xml.Serialization.XmlInclude(typeof(AssignmentLog))]
    [System.Xml.Serialization.XmlInclude(typeof(UnlockReport))]
    public class SearchCriteriaBaseModel : BaseModel
    {
    }
}
