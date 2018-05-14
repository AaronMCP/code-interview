using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.Unity;
using Hys.CrossCutting.Common.Utils;
using Newtonsoft.Json;

namespace Hys.CareRIS.WebApi.Security
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudiencesStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var container = UnityConfig.GetConfiguredContainer();
            var userService = container.Resolve<IUserManagementService>();
            var user = userService.GetUser(context.UserName);
            var result = Task.FromResult<object>(null);

            Func<string, Task> setError = (str) =>
            {
                context.SetError("invalid_grant", str);
                return result;
            };
            if (user == null)
            {
                return setError("用户名或密码错误！");
            }

            var c = new Cryptography("GCRIS2-20061025");
            var psd = c.Encrypt(context.Password);

            if (user.Password != psd)
            {
                return setError("用户名或密码错误！");
            }
            if (user.IsLocked.HasValue&&(bool)user.IsLocked)
            {
                return setError("用户已被锁定，请联系管理员!");
            }
            
            if (user.HasValidPeriod) {
                if (user.ValidStartDate != null && user.ValidEndDate != null)
                {
                    var nowtime = DateTime.Now;
                    if (nowtime < user.ValidStartDate || nowtime > user.ValidEndDate)
                    {
                        return setError("用户已失效，请联系管理员!");
                    }
                }
            }

            var identity = new ClaimsIdentity("JWT");
            var appConfig = new AppConfig();
            user.Domain = appConfig["Domain"];
            //user.Site = appConfig["Site"];
            user.Language = appConfig["DefaultLanguage"];
            user.PdfService = appConfig["PdfService"];
            var currentUser = JsonConvert.SerializeObject(user);
            identity.AddClaim(new Claim("user", currentUser));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            
            return result;
        }
    }
}