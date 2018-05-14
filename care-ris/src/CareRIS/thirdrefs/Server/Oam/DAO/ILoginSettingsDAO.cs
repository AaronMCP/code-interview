using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonGlobalSettings;
using Common.ActionResult.Framework;

namespace Server.DAO.Oam
{
    public interface ILoginSettingsDAO
    {
        string FullShowLoginBackground(ref string strSite);
        string GetXML(string site);
        bool WriteXML(string site, string xml);
        bool isWeekBeginFromMonday();
    }
}
