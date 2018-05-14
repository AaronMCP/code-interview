using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hys.CareRIS.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
#if DEBUG
            BundleConfig.RegisterBundles(BundleTable.Bundles);
#endif
        }

        protected void Application_Error()
        {
        }
    }
}
