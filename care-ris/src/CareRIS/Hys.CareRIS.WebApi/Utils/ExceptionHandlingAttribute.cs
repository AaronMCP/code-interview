using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Hys.CareRIS.WebApi.Utils
{
    /// <summary>
    /// Handle uncatched exception.
    /// The WebApi default behavior will pass the detail exception information to client, include stack trace, 
    /// which is very heavy, and may contain sensitive information.
    /// Here generate the simple and necessary information to client.
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is OperationCanceledException || context.Exception is HttpResponseException)
                {
                    // ignore canceled operation
                }
                else
                {
                    var exceptionData = new ExceptionData
                    {
                        Name = context.Exception.GetType().Name,
                        Message = context.Exception.Message
                    };

                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new ObjectContent<ExceptionData>(exceptionData, JsonFormatter),
                        ReasonPhrase = context.Exception.GetType().Name
                    });
                }
            }
        }

        private JsonMediaTypeFormatter _JsonFormatter;
        private JsonMediaTypeFormatter JsonFormatter
        {
            get
            {
                if (_JsonFormatter == null)
                {
                    _JsonFormatter = new JsonMediaTypeFormatter();
                    _JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                return _JsonFormatter;
            }
        }

        // A simple class for generate response with json content.
        public class ExceptionData
        {
            public string Name { get; set; }
            public string Message { get; set; }
        }
    }
}