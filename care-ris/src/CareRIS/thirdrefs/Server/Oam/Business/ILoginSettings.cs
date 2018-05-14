using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.ActionResult.Framework;

namespace Server.Business.Oam
{
    public interface ILoginSettings
    {
         void GetLoginInfo(LoginSettingsActionResult result);
         void WriteXML(LoginSettingsActionResult result, string site);
         void ReadXML(LoginSettingsActionResult result, string site);
    }
}
