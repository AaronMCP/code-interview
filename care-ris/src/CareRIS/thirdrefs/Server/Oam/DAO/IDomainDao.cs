/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*                        Author : Bruce Deng
/****************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;
using CommonGlobalSettings;
namespace Server.DAO.Oam
{   
    public interface IDomainDao
    {
        bool DomainList(DataSet ds, ref string strError);
        bool AddDomain(DataTable dtDomain, ref string strError);
        bool ModifyDomain(DataTable dtDomain, ref string strError);
        bool DelDomain(string strDomain, ref string strError);
        bool AddSite(DataTable dtSite, ref string strError);
        bool ModifySite(DataTable dtSite, ref string strError);
        bool SyncDomainSiteList(DataSet dtSite, ref string strError);
        DataSet GetSiteProfileDataSet(string domainName, string siteName);
        bool EditSiteProfile(SystemModel model, string domainName, string siteName);
        bool AddSiteProfile(string domainName, string siteName, string fieldName, string moduleID);
        bool DeleteSiteProfile(string domainName, string siteName, string fieldName, string moduleID);
    }
}
