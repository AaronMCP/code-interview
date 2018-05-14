using System;
using System.Configuration;
using System.ServiceModel;
using Hys.CrossCutting.Common.Utils;
using Hys.CareRIS.Application.AuditService;
using Hys.CareRIS.Application.Dtos;
using Hys.Platform.Application;
using Hys.Platform.CrossCutting.LogContract;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class LicenseService : DisposableServiceBase, ILicenseService
    {
        private readonly ICommonLog _logger;
        public LicenseService(ICommonLog logger)
        {
            _logger = logger;
        }

        public LicenseDataDto GetLicenseData()
        {
            try
            {
                var auditServer = ConfigurationManager.AppSettings["AuditServer"];
                var devID = ConfigurationManager.AppSettings["DevID"];
                if (devID == "Test")
                {
                    return new LicenseDataDto
                    {
                        IsSuccessed = true,
                        RisEnabled = true,
                        ConsultationEnabled = true,
                        MaxOnlineUserCount = 99999
                    };
                }
                using (var client = new ServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(auditServer)))
                {
                    var result = client.GetRisLicense(devID);
                    if (result.isSuccessful)
                    {
                        var data = new Cryptography("GCRIS2_Terrence").DeEncrypt(result.risData);
                        var maxOnlie = data.Substring(4, 4);
                        var expiredDate = data.Substring(8, 10);
                        var isExpired = expiredDate != "1980-12-10" && DateTime.Now >= DateTime.Parse(data.Substring(8, 10));
                        var dataPanel = data.Substring(18, 64);
                        var risEnabled = dataPanel.Substring(61, 1);
                        var consultationEnabled = dataPanel.Substring(62, 1);
                        return new LicenseDataDto
                        {
                            IsSuccessed = true,
                            IsExpired = isExpired,
                            MaxOnlineUserCount = int.Parse(maxOnlie),
                            ConsultationEnabled = consultationEnabled != "0",
                            RisEnabled = risEnabled != "0",
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "connect to license server failed: \r\n" + exception);
            }

            return new LicenseDataDto
            {
                IsSuccessed = false,
                ErrorMessage = "Get License failed."
            };
        }
    }
}