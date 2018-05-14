using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class WorklistSearchCriteriaDto
    {
        public WorklistSearchCriteriaDto() 
        {
            PatientTypes = new List<string>();
            Statuses = new List<int>();
            ModalityTypes = new List<string>();
            Modalities = new List<string>();
            CreateTimeRanges = new List<TimeRangeDto>();
            ExamineTimeRanges = new List<TimeRangeDto>();
            AccessSites = new List<string>();
            Results = new List<string>();
        }

        // patient info
        public string PatientName { get; set; }
        public string PatientNo { get; set; }

        // order info
        public string AccNo { get; set; }
        public List<string> PatientTypes { get; set; }
        public DateTime? CreateStartDate { get; set; }
        public DateTime? CreateEndDate { get; set; }
        public List<TimeRangeDto> CreateTimeRanges { get; set; }

        // procedure info
        public List<int> Statuses { get; set; }
        public List<string> ModalityTypes { get; set; }
        public List<string> Modalities { get; set; }
        public DateTime? ExamineStartDate { get; set; }
        public DateTime? ExamineEndDate { get; set; }
        public List<TimeRangeDto> ExamineTimeRanges { get; set; }

        public List<string> AccessSites { get; set; }
        public List<string> Results { get; set; }
        public PaginationDto Pagination { get; set; }

        // additional properties for saving shortcut
        public string CreateTimeType { get; set; }
        public string ExamineTimeType { get; set; }
        public int? CreateDays { get; set; }
        public int? ExamineDays { get; set; }
    }

    public class TimeRangeDto 
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public TimeSpan Start
        {
            get { return StartTime.HasValue ? StartTime.Value.TimeOfDay : DateTime.Today.TimeOfDay; }
        }

        public TimeSpan End
        {
            get { return EndTime.HasValue ? EndTime.Value.TimeOfDay : DateTime.Today.AddSeconds(-1).TimeOfDay; }
        }
    }
}
