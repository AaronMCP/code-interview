using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    internal class StringConsts
    {
        #region Message Section
        public const string WorkTimeNameEmpty = "WorkTimeEmpty";
        public const string WorkTimeNameEmptyDefaultValue = "The Work Time Name can not be empty.";

        public const string WorkTimeNameTooLong = "WorkTimeTooLong";
        public const string WorkTimeNameTooLongDefaultValue = "The Work Time Name is too long.";

        public const string BeginTimeEmpty = "BeginTimeEmpty";
        public const string BeginTimeEmptyDefaultValue = "The begin time can not be empty.";

        public const string EndTimeEmpty = "EndTimeEmpty";
        public const string EndTimeEmptyDefaultValue = "The end time can not be empty.";

        public const string EndTimeLessBeginTime = "EndTimeLessBeginTime";
        public const string EndTimeLessBeginTimeDefaultValue = "The EndTime must be great than the BeginTime.";

        public const string ScheduleTypeEmpty = "ScheduleTypeEmpty";
        public const string ScheduleTypeEmptyDefaultValue = "The Schedule Type can not be empty.";

        public const string SelectModalityPrompt = "SelectModalityPrompt";
        public const string SelectModalityPromptDefaultValue = "Please Select modality.";

        public const string InputSchedulesPrompt = "InputSchedulesPrompt";
        public const string InputSchedulesPromptDefaultValue = "Please input the schedules.";

        public const string WorkTimeOverlap = "WorkTimeOverlap";
        public const string WorkTimeOverlapDefaultValue = "The work time has overlapped.";
        #endregion
    }
}
