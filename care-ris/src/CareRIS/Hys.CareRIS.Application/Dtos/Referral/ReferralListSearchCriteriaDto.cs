using System;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.CareRIS.Application.Dtos.Referral
{

    public enum SearchType
    {
        All = 0,
        Unsend = 1
    }

    public class ReferralListSearchCriteriaDto : Pagination
    {
        public SearchType[] StatusList { get; set; }
    }
}
