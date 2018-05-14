using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{

    //10----NoCheckIn(booking)
    //20----Registered
    //23----CheckIn
    //25----Repeatshot(重拍)
    //30----Examing(正在检查)
    //40----Cancel
    //50----Examination
    //100----Draft
    //105----Reject
    //110----Submit
    //120----FirstApprove
    //130----SecondApprove
    public enum RP_Status
    {
        Unknown = 0,
        NoCheck = 10,
        Registered = 20,
        CheckIn=23,
        Repeatshot = 25,
        Examing = 30,
        RPCancel = 40,
        Examination = 50,
        Draft = 100,
        Reject = 105,
        Submit = 110,
        FirstApprove = 120,
        SecondApprove = 130
    }

    /// <summary>
    /// referral status
    /// </summary>
    public enum Ref_Status
    {
        Created = 0,
        /// <summary>
        /// Send
        /// </summary>
        Sending = 1,

        Sent = 3,

        Arrived = 5,

        SentFailed = 8,

        //Noaccept = 9,
        /// <summary>
        /// Accept = 10
        /// </summary>
        Accept = 10,

        /// <summary>
        /// Cancel = 15
        /// </summary>
        Cancelling = 12,

        Canceled = 15,

        CancelFailed = 18,

        /// <summary>
        /// Reject
        /// </summary>
        Rejecting = 20,

        Rejected = 22,

        RejectFailed = 25,

        /// <summary>
        /// Finish
        /// </summary>
        Finishing = 28,

        Finished = 30,

        FinishFailed = 32
    }

    /// <summary>
    /// Referral purpose
    /// </summary>
    public enum Ref_Purpose
    {
        /// <summary>
        /// Examine = 20
        /// </summary>
        Examine = 20,
        /// <summary>
        /// WriteReport = 50
        /// </summary>
        WriteReport = 50,
        /// <summary>
        /// ApproveReport = 110
        /// </summary>
        ApproveReport = 110,
        /// <summary>
        /// Consult
        /// </summary>
        Consult = 200
    }

    public enum Ref_Report
    {
        NonReferral = 0,
        ReferralReport = 1,
        ConsultReport = 2
    }

    /// <summary>
    /// Enum Direction
    /// </summary>
    public class DIRECTION
    {
        public const int OUT = 1;
        public const int IN = 0;
    }

    /// <summary>
    /// Referral event types
    /// </summary>
    public enum Ref_EventType
    {
        /// <summary>
        /// Send Referral out
        /// </summary>
        SendReferral = 1,
        /// <summary>
        /// Reject referral
        /// </summary>
        RejectReferral = 2,
        /// <summary>
        /// Feedback referral status
        /// </summary>
        AcceptReferral = 3,
        /// <summary>
        /// Feedback proceudure stauts
        /// </summary>
        RPStatusFeedBack = 4,
        /// <summary>
        /// publish report
        /// </summary>
        PublishReport = 5,
        /// <summary>
        /// Download files
        /// </summary>
        DownloadFile = 6,
        /// <summary>
        /// Auto Referral
        /// </summary>
        AutoReferral = 7,
        /// <summary>
        /// Move report back
        /// </summary>
        MoveReportBack = 8,
        /// <summary>
        /// Update GetReportDomain
        /// </summary>
        UpdateGetReportDomain = 9,
        /// <summary>
        /// Cancel Referral
        /// </summary>
        CancelReferral = 10,
        /// <summary>
        /// Send ReportSnapEvent
        /// </summary>
        SendReportSnapshot = 11,

        ACK = 12,

        FinishReferral = 13,

        WaitAck = 14,

        REFStatusFeedback = 15,

        AutoDelayFinishReferral = 16
    }

    public enum Ref_MSG
    {
        RefBooking = 10,
        RefReport = 11
    }

    /// <summary>
    /// Status of charge
    /// </summary>
    public enum Charge_Status
    {
        Unknow = -1,

        UnConfirm = 0,
        Confirmed = 1,
        ConfirmFailed = 2,

        UnDeduct = 10,
        Deducted = 11,
        DeductFailed = 12,

        UnRefund = 20,
        Refunded = 21,
        RefundFailed = 22,

        UnCancel = 30,
        Canceled = 31,
        CancelFailed = 32
    }


    /// <summary>
    /// Action of Charge
    /// </summary>
    public enum Charge_Action
    {
        Unknow = -1,
        Deduct = 1,
        Confirm = 2,
        Refund = 3,
        Cancel = 4
    }


    /// <summary>
    /// indicate bulletin which group to be sent.
    /// </summary>
    public enum BulletinGroupType
    {
        /// <summary>
        /// by dept
        /// </summary>
        Department = 1,
        /// <summary>
        /// By Role
        /// </summary>
        Role = 2,
        /// <summary>
        /// By user
        /// </summary>
        Person = 3
    }

    public enum CalendarDateType
    {
        /// <summary>
        /// 1
        /// </summary>
        WorkingDay = 1,
        /// <summary>
        /// 2
        /// </summary>
        Weekend = 2,
        /// <summary>
        /// 3
        /// </summary>
        Holiday = 3,
        /// <summary>
        /// Modality maintence
        /// </summary>
        MaintenceDay = 4,
        /// <summary>
        /// 5
        /// </summary>
        Monday = 5,
        /// <summary>
        /// 6
        /// </summary>
        Tuesday = 6,
        /// <summary>
        /// 7
        /// </summary>
        Wednesday = 7,
        /// <summary>
        /// 8
        /// </summary>
        Thursday = 8,
        /// <summary>
        /// 9
        /// </summary>
        Friday = 9,
        /// <summary>
        /// 10
        /// </summary>
        Saturday = 10,
        /// <summary>
        /// 11
        /// </summary>
        Sunday = 11
    }

    public enum DoctorStatus
    {
        RejectReport = 0,
        Normal = 1
    }

    public enum AssignmentType
    {
        None = 0,
        UnwrittenReport = 1,
        UnapprovedReport = 2
    }

    public enum PreferType
    {
        None = 0,
        ReportType = 1,
        PatientType = 2,
        ModalityType = 3,
        BodyCategory = 4,
        ExamSystem = 5,
        FromSite = 6,
        ReportDoctorGroup = 7
    }

    public enum OperationType
    {
        AutoAssignment = 0,
        ManualAssignment = 1,
        AutoCallback = 2
    }

    public enum FunctionName
    {
        NewRegNBooking,
        NewCheckingItem,
        Save,
        PrintBarCode,
        Refresh,
        PrintNotification,
        OpenNCloseRequisition,
        PrintRequisition,
        OpenGraphic,
        ExamHistory,
        Submit,
        Approve,
        FinishNConfirmExam,
        AmendRegistration,
        Queue,
        Previous,
        Next,
        Referral
    }

    public enum Ref_Scope
    {
        MultiSite = 1,
        MultiDomain = 2
    }

    public enum ShareTargetType
    {
       Site = 1,
       Department = 2
    }
}
