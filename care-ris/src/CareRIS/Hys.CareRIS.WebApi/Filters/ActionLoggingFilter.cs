using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using Hys.Platform.CrossCutting.LogContract;
using System;
using System.Web.Http.Filters;

namespace Hys.CareRIS.WebApi.Filters
{
    public class ActionLoggingFilter : ActionFilterAttribute
    {
        private ICommonLog _Logger;

        public ActionLoggingFilter(ICommonLog logger)
        {
            _Logger = logger;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            _Logger.Log(LogLevel.Info, String.Format("Executed action named {0} for request {1} by {2} from {3}.",
                actionContext.ActionDescriptor.ActionName,
                actionContext.Request.RequestUri,
                "User",
                actionContext.Request.GetClientIp()));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionContext = actionExecutedContext.ActionContext;

            _Logger.Log(LogLevel.Info, String.Format("Executing action named {0} for request {1} by {2} from {3}",
                actionContext.ActionDescriptor.ActionName,
                actionContext.Request.RequestUri,
                "User",
                actionContext.Request.GetClientIp()));
        }
    }
}