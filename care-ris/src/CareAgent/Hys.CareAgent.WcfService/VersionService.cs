#region

using System.Configuration;
using Hys.CareAgent.WcfService.Contract;

#endregion

namespace Hys.CareAgent.WcfService
{
    public class VersionService : IVersionService
    {
        private static readonly string Version;

        static VersionService()
        {
            Version = ConfigurationManager.AppSettings["Version"];
        }

        public string Get()
        {
            return Version;
        }
    }
}