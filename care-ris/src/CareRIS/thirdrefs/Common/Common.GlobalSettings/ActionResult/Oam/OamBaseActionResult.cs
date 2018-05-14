using System;
using System.Collections.Generic;
using System.Text;
using Common.ActionResult;

namespace Common.ActionResult.Oam
{
    [System.Xml.Serialization.XmlInclude(typeof(AddRoleActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ACRCodeDataTableActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ResourceActionResult))]
    public class OamBaseActionResult : BaseActionResult
    {
    }
}
