using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class SendingRefContext : ReferralBaseModel
    {
        public List<string> DomainAndSite
        {
            get;
            set;
        }

        public string OrderId
        {
            get;
            set;
        }

        public string Memo
        {
            get;
            set;
        }

        public string OperatorName
        {
            get;
            set;
        }

        public bool ReportConsult
        {
            get;
            set;
        }

        public string ReferralId
        {
            get;
            set;
        }

        public int Scope
        {
            get;
            set;
        }

        public string ActionSite
        {
            get;
            set;
        }

        public string RefApplication
        {
            get;
            set;
        }

        public string RefReport
        {
            get;
            set;
        }
    }
}
