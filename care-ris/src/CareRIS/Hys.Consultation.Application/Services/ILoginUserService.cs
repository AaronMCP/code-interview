using Hys.Consultation.Domain.Entities;
using Hys.CrossCutting.Common.Utils;

namespace Hys.Consultation.Application.Services
{
    public interface ILoginUserService
    {
        ServiceContext ServiceContext { get; set; }
        UserExtention LoginUser { get; }
        bool IsSystemAdmin { get; }
        string CurrentUserID { get; }
        string DefaultSiteID { get; }
    }
}