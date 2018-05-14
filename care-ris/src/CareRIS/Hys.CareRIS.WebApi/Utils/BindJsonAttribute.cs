using System;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;

namespace Hys.CareRIS.WebApi.Utils
{
    public class BindJsonAttribute : ActionFilterAttribute
    {
        Type _type;
        string _queryStringKey;
        public BindJsonAttribute(Type type, string queryStringKey)
        {
            _type = type;
            _queryStringKey = queryStringKey;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var json = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query)[_queryStringKey];
            if (json != null)
            {
                var serializer = new JavaScriptSerializer();
                actionContext.ActionArguments[_queryStringKey] = serializer.Deserialize(json, _type);
            }
        }
    }
}