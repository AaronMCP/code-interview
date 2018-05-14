using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(OrderModel))]
    public abstract class OrderBaseModel : BaseModel
    {

    }
}