using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Newtonsoft.Json;

namespace Hys.CareRIS.Web.Security
{
    public class Misc
    {
        public const string CookieKeyApiToken = "ApiToken";

        public static string GetLang(HttpContextBase httpContext,RouteData routeData)
        {
            var routeLang = routeData.Values["lang"];
            string lang;
            if (routeLang == null)
            {

                var cookieLang = httpContext.GetOwinContext().Request.Cookies[CookieKeyApiToken];
                if (cookieLang != null)
                {
                    dynamic apiToken = JsonConvert.DeserializeObject<dynamic>(cookieLang);
                    lang = apiToken.language;
                }
                else
                {
                    lang = ConfigurationManager.AppSettings["DefaultLanguage"];
                }
            }
            else
            {
                lang = routeLang.ToString();
            }
            return lang;
        }
    }
}