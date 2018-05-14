using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class Enums
    {
        public enum ActionCode
        {
            Create = 0,
            Update = 1,
            Delete = 2,
            BookingCreate=3,
            BookingUpdate=4,
            BookingDelete=5,
            FinishExam = 6,
            BookingToReg = 7
        }
        public enum Status
        {
            Booking = 10,
            Registered = 20,
            Examined = 50
        }

        /// <summary>
        /// referral status
        /// </summary>
        public enum ReferralStatus
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

        public enum Ref_Scope
        {
            MultiSite = 1,
            MultiDomain = 2
        }
        //ris Ref_Purpose
        public enum ReferralPurpose
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

        //ris DIRECTION
        public enum Direction
        {
            Out = 1,
            In = 0
        }

        public enum DelProcedureError
        {
            ExamCanNotDel=-1,
            OnlyOneCanNotDel=-2,
            HasBeenDeleted=-3
        }
        public enum DictionaryTag
        {
            AgeCompany = 6,
        }
    }
}
