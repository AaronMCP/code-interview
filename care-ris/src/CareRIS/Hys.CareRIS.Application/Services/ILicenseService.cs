using System;
using Hys.CareRIS.Application.Dtos;

namespace Hys.CareRIS.Application.Services
{
    public interface ILicenseService : IDisposable
    {
        LicenseDataDto GetLicenseData();
    }
}
