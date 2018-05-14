using Hys.CareRIS.WebApi.Filters;
using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Hys.CareRIS.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // IoC configuration
            var container = UnityConfig.GetConfiguredContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            config.DependencyResolver = new UnityResolver(container);

            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());

            // register exception logger
            config.Services.Add(typeof(IExceptionLogger), container.Resolve<ApiExceptionLogger>());

            // register action logging filter
            config.Filters.Add(container.Resolve<ActionLoggingFilter>());

            // register exception handler.
            config.Filters.Add(new ExceptionHandlingAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            // conert result to camlCase
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            // all the datetime seserialization should be local time, here we did not concern utc time for now
            // if we do not add this handling, the datetime will be serialized as utc time, but in everywhere we use them as local time
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling
                = DateTimeZoneHandling.Local;
        }
    }
}
