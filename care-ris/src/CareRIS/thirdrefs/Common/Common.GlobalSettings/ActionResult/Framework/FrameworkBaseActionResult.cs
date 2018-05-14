using System;
using System.Collections.Generic;
using System.Text;

namespace Kodak.GCRIS.Common.ActionResult.Framework
{
    [System.Xml.Serialization.XmlInclude(typeof(DsPanelInfoActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(DsRoleProfileActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(DsSystemProfileActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(DsUserProfileActionResult))]
    public class FrameworkBaseActionResult : BaseActionResult
    {
    }
}
