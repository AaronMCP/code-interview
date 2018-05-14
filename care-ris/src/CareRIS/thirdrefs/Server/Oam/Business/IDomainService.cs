using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;
using CommonGlobalSettings;

namespace Server.Business.Oam
{
    public interface IDomainService
    {
        int DomainList(BaseActionResult bar);
        int SyncDomainSiteList(BaseActionResult bar);
        int AddDomain(DataTable dtDomain, BaseActionResult bar);
        int ModifyDomain(DataTable dtDomain, BaseActionResult bar);
        int DelDomain(string strDomain, BaseActionResult bar);
        int AddSite(DataTable dtSite, BaseActionResult bar);
        int ModifySite(DataTable dtSite, BaseActionResult bar);
        DataSet GetSiteProfileDataSet(string domainName, string siteName);
        bool EditSiteProfile(SystemModel model, string domainName, string siteName);
        bool AddSiteProfile(string domainName, string siteName, string fieldName, string moduleID);
        bool DeleteSiteProfile(string domainName, string siteName, string fieldName, string moduleID);
    }
}
