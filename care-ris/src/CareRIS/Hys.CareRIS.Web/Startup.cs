using Microsoft.Owin;
using Owin;
using System.Net;

[assembly: OwinStartup(typeof(Hys.CareRIS.Web.Startup))]
namespace Hys.CareRIS.Web
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ConfigureAuthentication(app);

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        public void ConfigureAuthentication(IAppBuilder app)
        {
        }
    }
}
