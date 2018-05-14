using System.Web.Http;
using Hys.CrossCutting.Common.Interfaces;
using Hys.CareRIS.Application.Services;
using WebApi.OutputCache.V2;
using Hys.CareRIS.Application.Dtos.Referral;
using Newtonsoft.Json;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Referral Web Api
    /// </summary>
    [RoutePrefix("api/v1/referral")]
    [AutoInvalidateCacheOutput]
    public class ReferralController : ApiControllerBase
    {
        private readonly IReferralService _referralService;

        /// <summary>
        /// Registration controller constructor
        /// </summary>
        /// <param name="referralService"></param>
        public ReferralController(IReferralService referralService)
        {
            _referralService = referralService;
        }

        /// <summary>
        /// Get referral list data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("referrallist")]
        public IHttpActionResult GetPatients([FromUri]string query)
        {
            var searchCriteria = JsonConvert.DeserializeObject<ReferralListSearchCriteriaDto>(query);
            var user = base.CurrentUser();
            var result = _referralService.GetReferralList(searchCriteria, user.Site);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("resend/{id}")]
        public IHttpActionResult ReSend(string id)
        {
            var currentUser = base.CurrentUser();
            var result = _referralService.ReSend(id, currentUser.UniqueID);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("procedureid")]
        public IHttpActionResult GetProcedureID(string accno, string procedurecode)
        {
            var result = _referralService.GetProcedureID(accno, procedurecode);
            return Ok(result);
        }

        [HttpGet]
        [Route("targetsites")]
        public IHttpActionResult GetTargetSites()
        {
            var user = base.CurrentUser();
            var result = _referralService.GetTargetSites(user.Site);
            return Ok(result);
        }

        [HttpPost]
        [Route("sendreferral")]
        public IHttpActionResult SendReferral([FromBody]ManualReferralDto manualReferralDto)
        {
            var currUser = base.CurrentUser();
            var result = _referralService.SendReferral(manualReferralDto, currUser.Domain, currUser.Site, currUser.UniqueID);
            return Ok(result);
        }

        [HttpGet]
        [Route("canreferral")]
        public IHttpActionResult GetCanReferral()
        {
            var user = base.CurrentUser();
            var result = _referralService.GetCanReferral(user.RoleName);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cancelreferral/{id}")]
        public IHttpActionResult CancelReferral(string id)
        {
            var user = base.CurrentUser();
            var result = _referralService.CancelReferral(id, user.UniqueID, user.LocalName);
            return Ok(result);
        }
    }
}
