using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public static class Contants
    {
        public static class ProfileKey
        {
            public static readonly string AccessSite = "AccessSite";
            public static readonly string BelongToSite = "BelongToSite";
            /// <summary>
            /// generate erno
            /// </summary>
            public static Int64 seed = Int64.Parse(DateTime.UtcNow.Subtract(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).TotalMilliseconds.ToString("0"));
            //C:\Windows\Temp ,AppDomain.CurrentDomain.BaseDirectory
            public static readonly string TEMP_PATH = @"C:\Windows\Temp";
        }

        public static class Broker
        {
            //broker for finish exam
            public static string FinishExamEventType = "41";
            public static string FinishExamStatus = "16";

             public static readonly string CreateRegistrationEventType = "10";
            public  static readonly string TransferBooking2RegExamStatus = "14";

            //Patient broker
            public static readonly string UpdatePatientEventType = "01";
            public static readonly string CreatePatientEventType = "00";

        }

        public static class Profile
        {
            public static string ReferralSiteOptions = "ReferralSiteOptions";
            public static string ReferralSiteOptionsSplit = "|";
        }
    }
}
