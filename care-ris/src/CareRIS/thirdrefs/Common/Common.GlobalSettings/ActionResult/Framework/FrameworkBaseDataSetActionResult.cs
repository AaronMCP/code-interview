using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ActionResult.Framework
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(DsPanelInfoActionResult))]
    public class FrameworkBaseDataSetActionResult : DataSetActionResult
    {
    }
}
