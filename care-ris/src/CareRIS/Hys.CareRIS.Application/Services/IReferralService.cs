using System;
using System.Collections.Generic;
using Hys.CrossCutting.Common.Interfaces;
using Hys.CareRIS.Application.Dtos.Referral;
using Hys.CareRIS.Application.Dtos;

namespace Hys.CareRIS.Application.Services
{
    public interface IReferralService : IDisposable
    {
        ReferralListSearchDto GetReferralList(ReferralListSearchCriteriaDto searchCriteria, string userSite);
        bool ReSend(string id, string userId);
        string GetProcedureID(string accNo, string procedureCode);
        List<SiteDto> GetTargetSites(string site);
        bool SendReferral(ManualReferralDto manualReferralDto, string domain, string site, string userId);
        bool GetCanReferral(string role);
        bool CancelReferral(string id, string userId, string localName);
    }
}
