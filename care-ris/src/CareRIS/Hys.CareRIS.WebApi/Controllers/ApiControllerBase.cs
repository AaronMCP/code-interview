using System.Net;
using Hys.CrossCutting.Common.Utils;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Security;
using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Hys.CareRIS.Application.Dtos.UserManagement;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Base ApiController for all of the web api of CareRIS
    /// </summary>
    public class ApiControllerBase : ApiController
    {
        /// <summary>
        /// All the services shared for all web api controllers should be injectd into the this base api controller.
        /// </summary>
        public ApiControllerBase()
        {
            
        }

        /// <summary>
        /// Create Response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IHttpActionResult Response(object data)
        {
            return data == null ? (IHttpActionResult)NotFound() : Ok(data);
        }

        /// <summary>
        /// Get granted user
        /// </summary>
        /// <returns></returns>
        public UserDto CurrentUser()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims.Where(c => c.Type == "user").First();
            var user = JsonConvert.DeserializeObject<UserDto>(userClaims.Value);
            return user;
        }

        private static AppConfig _config;
        public AppConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new AppConfig();
                }
                return _config;
            }
        }

        private static string _clientId;
        public string ClientId
        {
            get
            {
                if (string.IsNullOrEmpty(_clientId))
                {
                    _clientId = Config["clientId"];
                }
                return _clientId;
            }
        }
    }
}