using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Registry
{
    /// <summary>
    /// The message body XML schemas defined by XObject classes in this file are used in PIX prototype only.
    /// Do not need to use these classes in XDS Gateway 1.1 deliverables.
    /// </summary>

    public class PatientRegisterRequest : XObject
    {
        public string PatientID { get; set; }
        public string Issuer { get; set; }
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
    }

    public class PatientUpdateRequest : XObject
    {
        public string PatientID { get; set; }
        public string Issuer { get; set; }
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
    }

    public class PatientMergeRequest : XObject
    {
        public string PatientID { get; set; }
        public string Issuer { get; set; }
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
        public string PriorPaitentID { get; set; }
        public string PriorIsser { get; set; }
    }

    public class PatientIdentifier : XObject
    {
        public string PatientID { get; set; }
        public string Issuer { get; set; }
    }

    public class PatientUpdateNotification : XObject
    {
        private XCollection<PatientIdentifier> _patientIdentifiers = new XCollection<PatientIdentifier>();
        public XCollection<PatientIdentifier> PatientIdentifiers
        {
            get { return _patientIdentifiers; }
            set { _patientIdentifiers = value; }
        }

        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
    }

    public class PIXQueryRequest : XObject
    {
        private PatientIdentifier _queryCriteria = new PatientIdentifier();
        public PatientIdentifier QueryCriteria
        {
            get { return _queryCriteria; }
            set { _queryCriteria = value; }
        }

        private XCollection<PatientIdentifier> _desireIssuers = new XCollection<PatientIdentifier>();
        public XCollection<PatientIdentifier> DesireIssuers
        {
            get { return _desireIssuers; }
            set { _desireIssuers = value; }
        }

        public bool IsDesireIssuer(string issuer)
        {
            if (DesireIssuers == null || DesireIssuers.Count < 1) return true;
            foreach (PatientIdentifier pi in DesireIssuers)
            {
                if (pi.Issuer == issuer) return true;
            }
            return false;
        }
    }

    public class PIXQueryResponse : PIXQueryRequest
    {
        private XCollection<PatientIdentifier> _queryResult = new XCollection<PatientIdentifier>();
        public XCollection<PatientIdentifier> QueryResult
        {
            get { return _queryResult; }
            set { _queryResult = value; }
        }

        public PIXQueryResponse()
        {
        }
        public PIXQueryResponse(PIXQueryRequest req)
        {
            this.QueryCriteria.Issuer = req.QueryCriteria.Issuer;
            this.QueryCriteria.PatientID = req.QueryCriteria.PatientID;

            foreach (PatientIdentifier pi in req.DesireIssuers)
            {
                PatientIdentifier npi = new PatientIdentifier();
                npi.Issuer = pi.Issuer;
                npi.PatientID = pi.PatientID;
                this.DesireIssuers.Add(npi);
            }
        }
    }

    public class PatientDemographic : XObject
    {
        public string PatientID { get; set; }
        public string Issuer { get; set; }
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
    }

    public class PDQQueryRequest : XObject
    {
        private PatientIdentifier _identifier = new PatientIdentifier();
        public PatientIdentifier Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        // Currently only support 1 desire issuer,
        // because not clear the meaning/different of "PID Issuer" and "Patient Demographics Source Domain"
        // in IHE ITI TF Vol2 M2/M3 and HL7 v2.5 3.3.57
        public string DesireIssuer { get; set; }        

        public bool IsDesireIssuer(string issuer)
        {
            if (DesireIssuer == null || DesireIssuer.Length < 1) return true;
            return DesireIssuer == issuer;
        }

        // Currently only PID and issuer as query criteria
        public string PatientName { get; set; }
        public string PatientSex { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientAddress { get; set; }
    }

    public class PDQQueryResponse : PDQQueryRequest
    {
        private XCollection<PatientDemographic> _queryResult = new XCollection<PatientDemographic>();
        public XCollection<PatientDemographic> QueryResult
        {
            get { return _queryResult; }
            set { _queryResult = value; }
        }

        public PDQQueryResponse()
        {
        }
        public PDQQueryResponse(PDQQueryRequest req)
        {
            this.Identifier.Issuer = req.Identifier.Issuer;
            this.Identifier.PatientID = req.Identifier.PatientID;
            this.DesireIssuer = req.DesireIssuer;
            this.PatientName = req.PatientName;
            this.PatientSex = req.PatientSex;
            this.PatientBirthDate = req.PatientBirthDate;
            this.PatientAddress = req.PatientAddress;
        }
    }
}
