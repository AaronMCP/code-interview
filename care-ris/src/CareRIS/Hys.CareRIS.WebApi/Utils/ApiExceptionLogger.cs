using Hys.Platform.CrossCutting.LogContract;
using System;
using System.Web.Http.ExceptionHandling;

namespace Hys.CareRIS.WebApi.Utils
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        private ICommonLog _Logger;

        public ApiExceptionLogger(ICommonLog logger) 
        {
            _Logger = logger;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _Logger.Log(LogLevel.Error, context.ExceptionContext.Exception.ToString());
        }
    }
}