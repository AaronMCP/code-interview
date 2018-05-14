using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class SignHistoryModel : CommonBaseModel
    {

        public string Action
        {
            get;
            set;
        }

        public string RawData
        {
            get;
            set;
        }

        public string SignedData
        {
            get;
            set;
        }

        public int IsSigned
        {
            get;
            set;
        }

        public string SignedTimestamp
        {
            get;
            set;
        }

        public string CertSN
        {
            get;
            set;
        }

        public string OrderGuid
        {
            get;
            set;
        }

        public string ReportGuid
        {
            get;
            set;
        }

        public string Creater
        {
            get;
            set;
        }

        public string SignGuid
        {
            get;
            set;
        }

        public string UserGuid
        {
            get;
            set;
        }

        private bool _isApprovedRpt = false;
        public bool IsApproveReport
        {
            get { return _isApprovedRpt; }
            set { _isApprovedRpt = value; }
        }

        public string Comments
        {
            get;
            set;
        }
        
        public string PatientID { get; set; }
        public string LocalName { get; set; }
        public string AccNo { get; set; }
        public string CheckingItem { get; set; }
        public string IsPositive { get; set; }
        public string ClinicNo { get; set; }
        public string WYSText { get; set; }
        public string WYGText { get; set; }
        public string ExamDt { get; set; }
    }
}
