using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings.HippaName
{
    public struct ActionCode
    {
        public const string Create = "C";
        public const string Read = "R";
        public const string Update = "U";
        public const string Delete = "D";
        public const string MergeVisit = "MV";
        public const string MergePatient = "MP";
        public const string Separate = "S";
        public const string Submit = "Sub";
        public const string Reject = "Rej";
        public const string Approved = "App";
        public const string SecondApproved = "SecondApp";
        public const string Rebuild = "Reb";
        public const string MergeOrder = "MRO";
        public const string MoveOrder = "MVO";
        public const string MoveProcedure = "M";
        public const string Quash = "QU";
        public const string Resume = "RE";
        public const string Scoring = "Scoring";
        public const string CancelSubmit = "CancelSub";
        public const string RelatePatient = "RP";
    }
    public struct EventTypeCode
    {
        public const string Login = "Login";
        public const string Logout = "Logout";
        public const string StartExam = "StartExam";
        public const string FinishExam = "FinishExam";
        public const string CancelExam = "CancelExam";
        public const string UpdateExam = "Update Procedure";
        public const string QuashOrder = "Quash Order";
        public const string ResumeOrder = "Resume Order";
        public const string ScoreReport = "Score Report";
        public const string ScoreImage = "Score Image";
    }
}
