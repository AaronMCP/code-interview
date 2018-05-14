using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Consts
{
    public class BizTableName
    {
        public const string Patient = "tRegPatient";
        public const string Order = "tRegOrder";
        public const string Procedure = "tRegProcedure";
        public const string Report = "tReport";
        public const string ReportFile = "tReportFile";
        public const string Requisition = "tRequisition";
        public const string ProcedureCode = "tProcedureCode";
    }

    public class ReferralConsts
    {
        public const string DateTimeFormatter = "yyyy-MM-dd HH:mm:ss";
    }

    public class RefEventTag
    {
        public const int ACKSent = 1 << 1;

        public const int Uploaded = 1 << 2;

        public const int Downloaded = 1 << 3;
    }

    public class SignAction
    {
        public const string Login = "Login";

        public const string SaveBooking = "SaveBooking";

        public const string SaveRegistration = "SaveRegistration";

        public const string SaveExam = "SaveExam";

        public const string CreateReport = "CreateReport";

        public const string SubmitReport = "SubmitReport";

        public const string RejectReport = "RejectReport";

        public const string ApproveReport = "ApproveReport";

        public const string SecondApproveReport = "SecondApproveReport";

    }
}
