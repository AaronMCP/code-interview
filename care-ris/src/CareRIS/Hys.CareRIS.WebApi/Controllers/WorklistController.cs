using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.WebApi.Services;
using Hys.CareRIS.WebApi.Utils;
using Kendo.DynamicLinq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Hys.CareRIS.WebApi.Controllers
{
    /// <summary>
    /// Worklist Web Api
    /// </summary>
    [RoutePrefix("api/v1/worklist")]
    public class WorklistController : ApiControllerBase
    {
        private readonly IWorklistService _worklistService;

        /// <summary>
        /// Worklist controller constructor
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="worklistService"></param>
        public WorklistController(IWorklistService worklistService)
        {
            _worklistService = worklistService;
        }

        /// <summary>
        /// kendo 工作列表
        /// </summary>
        /// <param name="query">kendo filter</param>
        /// <returns></returns>
        [HttpGet]
        [Route("advanced/search/works")]
        public async Task<IHttpActionResult> GetWorkList([FromUri]string query)
        {

            var microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            var criteria = JsonConvert.DeserializeObject<WorklistSearchCriteriaDto>(query, microsoftDateFormatSettings);

            if (criteria == null)
            {
                criteria = new WorklistSearchCriteriaDto
                {
                    Pagination = new PaginationDto
                    {
                        PageIndex = 1,
                        PageSize = Int32.MaxValue
                    }
                };
            }
            var user = base.CurrentUser();
            var result = await _worklistService.GetWorklist(criteria, user.UniqueID, user.Site, user.RoleName);
            return Ok(result);

        }
        /// <summary>
        /// Get worklist items by advanced criteria
        /// </summary>
        /// <param name="criteria">search criteria</param>
        /// <returns></returns>
        [HttpGet]
        [Route("advanced/search/result")]
        public async Task<IHttpActionResult> AdvancedSearch([FromUri]string query)
        {
            var criteria = JsonConvert.DeserializeObject<WorklistSearchCriteriaDto>(query);
            if (criteria == null)
            {
                criteria = new WorklistSearchCriteriaDto
                {
                    Pagination = new PaginationDto
                    {
                        PageIndex = 1,
                        PageSize = Int32.MaxValue
                    }
                };
            }
            var user = base.CurrentUser();
            var result = await _worklistService.SearchWorklist(criteria, user.UniqueID, user.Site, user.RoleName);
            return Ok<WorklistSearchResultDto>(result);
        }

        /// <summary>
        /// Get Search Criteria Shortcuts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("shortcuts")]
        public IHttpActionResult GetSearchCriteriaShortcuts()
        {
            var user = base.CurrentUser();
            var userID = user.UniqueID;
            var result = _worklistService.GetSearchCriteriaShortcuts(userID);
            return Ok(result);
        }

        /// <summary>
        /// Add Search Criteria Shortcut
        /// </summary>
        /// <param name="shortcutDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("shortcuts")]
        public IHttpActionResult AddSearchCriteriaShortcut([FromBody]SearchCriteriaShortcutDto shortcutDto)
        {
            if (String.IsNullOrEmpty(shortcutDto.UniqueID))
            {
                shortcutDto.UniqueID = Guid.NewGuid().ToString();
            }
            _worklistService.AddSearchCriteriaShortcut(shortcutDto);
            var shorcut = _worklistService.GetSearchCriteriaShortcut(shortcutDto.UniqueID);
            return Created("", shorcut);
        }

        /// <summary>
        /// Delete Search Criteria Shortcut
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("shortcuts/{id}")]
        public IHttpActionResult DeleteSearchCriteriaShortcut(string id)
        {
            var shortcut = _worklistService.GetSearchCriteriaShortcut(id);
            if (shortcut == null)
            {
                return NotFound();
            }
            _worklistService.DeleteSearchCriteriaShortcut(id);
            return Ok();
        }

        /// <summary>
        /// Set Detault Search Criteria Shortcut
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("shortcuts/default/{id}")]
        public IHttpActionResult SetDetaultSearchCriteriaShortcut(string id)
        {
            var shortcut = _worklistService.GetSearchCriteriaShortcut(id);
            if (shortcut == null)
            {
                return NotFound();
            }
            _worklistService.SetDetaultSearchCriteriaShortcut(id);
            return Ok();
        }
    }
}
