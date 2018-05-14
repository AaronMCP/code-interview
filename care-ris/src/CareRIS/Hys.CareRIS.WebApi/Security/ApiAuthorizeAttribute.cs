using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Hys.CareRIS.WebApi.Security
{
    /// <summary>
    /// API Authrorization.
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        // For now it is just inherited from AuthorizeAttribute, 
        // As all API controllers will need add the Authorize attribute, 
        // create this empty Authorize attribute, 
        // the purpose is to make change easier if we want to add some custom authorization behaviors in future.
    }
}