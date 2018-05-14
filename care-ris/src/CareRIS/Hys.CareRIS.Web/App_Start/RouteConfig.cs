using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hys.CareRIS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Main",
                url: "careris/{lang}/{id}",
                defaults: new { controller = "Home", action = "Index", lang = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{lang}/{id}",
                defaults: new { controller = "Account", action = "LogOn", lang = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}
