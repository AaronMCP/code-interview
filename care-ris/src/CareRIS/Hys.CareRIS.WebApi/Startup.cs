using Hys.CareRIS.WebApi.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.IO;
using Microsoft.AspNet.Identity;
using Hys.CrossCutting.Common.Utils;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Configuration;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.Consultation.Application.Services.ServiceImpl;
using AutoMapper;
using System.Reflection;
using Hys.CrossCutting.Common.Extensions;

[assembly: OwinStartup(typeof(Hys.CareRIS.WebApi.Startup))]

namespace Hys.CareRIS.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuthentication(IAppBuilder app)
        {
            var appConfig = new AppConfig();
            var configExpire = appConfig["expireMinutes"];

            double expireMinutes = 0;
            if (!double.TryParse(configExpire, out expireMinutes))
            {
                expireMinutes = 30;
            }

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(expireMinutes),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(appConfig["issuer"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            var secret = TextEncodings.Base64Url.Decode(appConfig["base64Secret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { appConfig["clientId"] },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(appConfig["issuer"], secret)
                    },
                    Provider = new OAuthBearerAuthenticationProvider
                    {
                        OnValidateIdentity = context =>
                        {
                            return Task.FromResult<object>(null);
                        }
                    }
                });

            PinyinUtil.Initialize();
        }
    }
}