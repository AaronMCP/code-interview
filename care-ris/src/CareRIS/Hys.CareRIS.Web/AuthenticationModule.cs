using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Hys.CareRIS.Web
{
    public class AuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose()
        {

        }

        protected void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            var httpContext = ((HttpApplication)sender).Context;

            var path = httpContext.Request.Path;

            var noAuthentUrls = new string[] { "/signalr/" };

            foreach (var url in noAuthentUrls)
            {
                var noAuthentication = path.IndexOf(url, StringComparison.OrdinalIgnoreCase) > -1;

                if (noAuthentication)
                    httpContext.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }
        }

    }
}