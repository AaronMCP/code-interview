using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ActionResult
{
    [System.Xml.Serialization.XmlInclude(typeof(FTPActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(BackupActionResult))]
    public class CommonBaseActionResult : BaseActionResult
    {
    }
}
