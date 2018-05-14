using System;
using System.Text;
using System.Collections.Generic;

namespace HYS.Common.Dicom.Net
{
    public class Session
    {
        private string _calledAE;
        private string _callingIP;
        private string _callingAE;
        private int _assocID;

        public Session(string calledAE, string callingIP, string callingAE, int assocID)
        {
            _calledAE = calledAE;
            _callingIP = callingIP;
            _callingAE = callingAE;
            _assocID = assocID;
        }

        public int AssocID
        {
            get { return _assocID; }
        }
        public string CalledAE
        {
            get { return _calledAE; }
        }
        public string CallingIP
        {
            get { return _callingIP; }
        }
        public string CallingAE
        {
            get { return _callingAE; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AssocID:").Append(AssocID)
            .Append(" From:").Append(CallingAE.Trim())
            .Append("(").Append(CallingIP)
            .Append(") To:").Append(CalledAE.Trim());
            return sb.ToString();
        }
    }
}
