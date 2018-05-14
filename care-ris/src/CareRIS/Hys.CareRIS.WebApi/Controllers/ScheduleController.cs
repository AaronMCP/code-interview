using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hys.CareRIS.WebApi.Controllers
{
    [RoutePrefix("api/v1/schedule")]
    public class ScheduleController : ApiControllerBase
    {
        private IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [Route("timeslice"), HttpPost]
        public async Task<IHttpActionResult> AddTimeSlice([FromBody]NewTimeSlice timeslice)
        {
            var currUser = CurrentUser();
            var result = await _scheduleService.AddModalityTimeSlice(timeslice.ModalityType,
                timeslice.Modality, timeslice.StartTime, timeslice.EndTime,
                timeslice.Description, timeslice.Amount, currUser.Domain, timeslice.DateTypes,
                timeslice.AvailableDate, timeslice.Interval);
            return Ok(result);
        }

        [Route("timeslice"), HttpGet]
        public async Task<IHttpActionResult> SearchTimeSlice(string modality, int dateType)
        {
            var result = await _scheduleService.SearchTimeSlice(modality, dateType);
            return Ok(result);
        }

        [Route("timeslice/{sliceId}"), HttpPut]
        public async Task<IHttpActionResult> ModifyTimeSlice(string sliceId, [FromBody]TimeSliceModifier modifier)
        {
            var result = await _scheduleService.ModifyTimeSlice(sliceId, modifier.StartTime, modifier.EndTime, modifier.Description, modifier.Amount);
            return Ok(result);
        }

        [Route("timeslice/del"), HttpPost]
        public async Task<IHttpActionResult> DelTimeSlice(string[] sliceIds)
        {
            var result = await _scheduleService.DelTimeSlice(sliceIds);
            return Ok(result);
        }

        [Route("timeslicecopy"), HttpPost]
        public async Task<IHttpActionResult> CopyTimeSlice([FromBody]TimeSliceCopier copyTarget)
        {
            var currentUser = CurrentUser();
            var result = await _scheduleService.CopyTimeSlice(copyTarget.SliceIds, currentUser.Domain, copyTarget.AvailableDate, copyTarget.Modality, copyTarget.DateType);
            return Ok(result);
        }

        [Route("timesliceshare"), HttpPut]
        public async Task<IHttpActionResult> ShareTimeSlice([FromBody]TimesliceSharerRequest shareRequest)
        {
            var result = await _scheduleService.ShareTimeSlice(shareRequest.SliceIds, shareRequest.Sharers);
            return Ok(result);
        }

        [Route("timesliceshare/{sliceId}"), HttpGet]
        public async Task<IHttpActionResult> GetSharedTimeslice(string sliceId)
        {
            var result = await _scheduleService.GetSharedTimeslice(sliceId);
            return Ok(result);
        }
    }
}