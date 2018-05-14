using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Utilities.Oam
{
    public enum ScheduleType
    {
        IndividualDay,
        PeriodDay
    }

    public enum BulletinActionType
    {
        Add =0,
        Modify =1,
        View = 2,

        Create = 3,
        Submit = 4,
        Approve = 5,
        Publish = 6,
        Reject = 7
    }

    public enum BulletinState
    {
        Created =0,
        Submitted =1,
        Approved = 2,
        Published = 3,
        Rejected = 4
    }

    public enum WindowMode
    {
        Part = 1,
        Full = 2
    }

    public enum QualityScoringDBAction
    {
        Save =1,
        Update =2,
        SaveLinkExam = 3,
        UpdateLinkExam = 4
    }
}
