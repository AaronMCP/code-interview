using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Services;
using Microsoft.Practices.Unity;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.Consultation.Application.Services.ServiceImpl;
using System.Reflection;
using AutoMapper;
using Hys.CrossCutting.Common.Extensions;
using System.Linq;
using WebApiContrib.Formatting.Jsonp;

namespace Hys.CareRIS.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.AddJsonpFormatter();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var container = UnityConfig.GetConfiguredContainer();
            configMapper();
            CacheData();
        }
        private void configMapper()
        {
            var careRisAsm = typeof(ConfigurationService).Assembly;
            var consultation = typeof(ConsultationService).Assembly;
            var asms = new Assembly[] { careRisAsm, consultation };

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(asms);
                attributeMap(cfg, asms);
            });
        }

        private void attributeMap(IMapperConfigurationExpression cfg, Assembly[] asms)
        {
            var autoMapType = typeof(AutoMapAttribute);
            asms.ForEach(asm =>
            {
                var types = asm.GetTypes().Where(t => t.IsDefined(autoMapType, false));
                var asmName = asm.FullName;
                types.ForEach(t =>
                {
                    cfg.CreateProfile(asmName + "." + t.Name.TrimEnd("Dto") + "Mapper", expression =>
                    {
                        var attr = (AutoMapAttribute[])t.GetCustomAttributes(autoMapType, false);
                        if (attr.Length > 0)
                        {
                            expression.CreateMap(t, attr[0].TargetType);
                            expression.CreateMap(attr[0].TargetType, t);
                        }
                    });
                });
            });
        }
        private static void CacheData()
        {
            var container = UnityConfig.GetConfiguredContainer();
            var configurationService = container.Resolve<IConsultationConfigurationService>();

            HttpContext.Current.Application["dic"] = new Dictionary<string, IEnumerable<ConsultationDictionaryDto>>(){
                {"en-us", configurationService.GetAllDictionaries("en-us")},
                {"zh-cn", configurationService.GetAllDictionaries("zh-cn")}
            };

            HttpContext.Current.Application["serviceType"] = new Dictionary<string, List<ServiceTypeDto>>(){
                {"en-us", configurationService.GetServiceType("en-us")},
                {"zh-cn", configurationService.GetServiceType("zh-cn")}
            };
            HttpContext.Current.Application["examModule"] = new Dictionary<string, IEnumerable<ExamModuleDto>>(){
                {"en-us", configurationService.GetDefaultExamModule("en-us")},
                {"zh-cn", configurationService.GetDefaultExamModule("zh-cn")}
            };
        }
    }
}
