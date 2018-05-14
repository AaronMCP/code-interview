using System.Linq;
using System.Web;
using System.Web.Http;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Services;
using WebApi.OutputCache.V2;
using WebApi.OutputCache.V2.TimeAttributes;
using System.Net.Http;
using Hys.CareRIS.WebApi.Utils;
using System;
using System.Configuration;
using System.Net;
using Hys.CrossCutting.Common.Utils;
using System.IdentityModel.Tokens;
using Hys.CareRIS.WebApi.Models;
using Hys.CareRIS.WebApi.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/v1/common")]
    [AutoInvalidateCacheOutput]
    public class CommonController : ApiControllerBase
    {
        private readonly ICommonService _procedureCodeService;
        private readonly IUserManagementService _userManagementService;
        private readonly IConfigurationService _configurationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedurecodeService"></param>
        /// <param name="userManagementService"></param>
        /// <param name="configurationService"></param>
        public CommonController(ICommonService procedurecodeService, IUserManagementService userManagementService, IConfigurationService configurationService)
        {
            _procedureCodeService = procedurecodeService;
            _userManagementService = userManagementService;
            _configurationService = configurationService;
        }

        /// <summary>
        /// Get procedurecodes data.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("procedurecodes")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetProcedureCodes()
        {
            var result = _procedureCodeService.GetProcedureCodes();
            return Ok(result);
        }

        /// <summary>
        /// Get ModalityTypes Data.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("modalitytypes")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetModalityTypes()
        {
            var modalityTypes = _procedureCodeService.GetModalityTypes();
            return Ok(modalityTypes);
        }


        /// <summary>
        /// Get BodyParts Data.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("bodyparts")]
        [CacheOutput(ServerTimeSpan = 36000)]
        public IHttpActionResult GetBodyParts()
        {
            var bodyparts = _procedureCodeService.GetBodyParts();
            return Ok(bodyparts);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("cache/elimination")]
        public void ClearCache()
        {
            // do nothing here, if someone called this method, all cache will be cleared in this controller
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("clientip")]
        public IHttpActionResult ClientIP()
        {
            return Ok(Request.GetClientIp());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("serverdate")]
        public IHttpActionResult ServerDate()
        {
            return Ok(DateTime.Now.ToString("yyyy-MM-dd"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("servertime")]
        public IHttpActionResult ServerTime()
        {
            return Ok(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("allsite")]
        public IHttpActionResult GetAllSite()
        {
            var allSites = _configurationService.GetAllSites();
            return Ok(allSites);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("clientid")]
        public IHttpActionResult GetClientId()
        {
            return Ok(base.ClientId);
        }

        [HttpGet]
        [Route("currentuser")]
        public IHttpActionResult GetCurrentUser()
        {
            var currUser = CurrentUser();
            return Ok(currUser);
        }

        [HttpGet, Route("refreshtoken")]
        public IHttpActionResult refreshToken()
        {
            var authHeader = Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization"));
            var bearJwtToken = authHeader.Value.FirstOrDefault();
            if (string.IsNullOrEmpty(bearJwtToken))
            {
                return BadRequest("Authorization required");
            }

            var arr = bearJwtToken.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length < 2)
            {
                return BadRequest("Invalid Token");
            }

            var jwtTokenStr = arr[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(jwtTokenStr) as JwtSecurityToken;
            
            var appConfig = new AppConfig();
            var audienceId = appConfig["clientId"];
            var issuer = appConfig["issuer"];
            var configExpire = appConfig["expireMinutes"];

            var claims = jwtToken.Claims;
            var notBefore = DateTime.Now;

            double expireMinutes = 0;
            if (!double.TryParse(configExpire, out expireMinutes))
            {
                expireMinutes = 30;
            }
            var expires = notBefore.AddMinutes(expireMinutes);

            Audience audience = AudiencesStore.FindAudience(audienceId);
            string symmetricKeyAsBase64 = audience.Base64Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var newToken = new JwtSecurityToken(issuer, audienceId, claims, notBefore, expires, signingKey);
            var jwt = tokenHandler.WriteToken(newToken);

            return Ok(jwt);
        }
    }
}
