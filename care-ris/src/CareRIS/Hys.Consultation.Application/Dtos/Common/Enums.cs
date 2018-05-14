using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public enum ConsultationRequestStatus
    {
        All = 0,
        Applied = 1,
        Accepted = 2,
        Completed = 3,
        Cancelled = 4,
        Rejected = 5,
        Consulting = 6,
        Reconsider = 7,
        Terminate = 8,
        Deleted = 9
    }

    public enum PatientCaseStatus
    {
        All = -1,
        NotApply = 0,
        Applied = 1,
        Deleted = 2
    }

    public enum ConsultationTimeRange
    {
        None = 0,
        Morning = 1,
        Afternoon = 2,
        Night = 3,
    }

    public enum ShortcutCategory
    {
        All = 0,
        RequestSearchDoctor = 1,
        RequestSearchCenter = 2,
        RequestSearchExpert = 3
    }

    public enum Gender
    {
        Unspecified = 0,
        Male = 1,
        Female = 2,
    }

    public enum HospitalDefaultType
    {
        Hospital = 0,
        Expert = 1
    }

    public enum SearchType
    {
        All = 0,
        RequestSearchDoctor = 1,
        RequestSearchCenter = 2,
        Expert = 3
    }

    public enum NotifyEvent
    {
        DoctorSendRequest = 0,
        ConsolutionAdminAssignToExpert = 1,
        ConsolutionAdminAcceptedNotifyDoctor = 2,
        ConsolutionAdminDeclinedRequest = 3,
        ConsolutionAdminForceEndRequest = 4,
        DoctorCancelRequest = 5,
        ConsolutionReportUpdated = 6,
    }

    public enum NotifyType
    {
        Sms,
        Im,
    }

    public enum ConsultantType
    {
        Hospital = 0,
        Expert = 1
    }

    public enum ReceiveType
    {
        Center = 0,
        Expert = 1,
    }

    public enum SysConfigModule
    {
        System = 0,
        Meeting = 1,
        VNC = 2
    }
}
