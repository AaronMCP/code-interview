using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace CommonGlobalSettings
{
    public class CoreBussinessModel
    {
        private RefOrderModel orderModel = null;
        private RefPatientModel patientModel = null;
        private List<RefReportModel> reportModels = new List<RefReportModel>();
        private List<RefRequisitionModel> requisitionModels = new List<RefRequisitionModel>();
        private string memo = string.Empty;

        public RefOrderModel OrderModel
        {
            get { return orderModel; }
            set { orderModel = value; }
        }

        public RefPatientModel PatientModel
        {
            get { return patientModel; }
            set { patientModel = value; }
        }

        public List<RefReportModel> ReportModels
        {
            get { return reportModels; }
            set { reportModels = value; }
        }

        public List<RefRequisitionModel> RequisitionModels
        {
            get { return requisitionModels; }
            set { requisitionModels = value; }
        }

        public string Memo
        {
            get { return this.memo; }
            set { this.memo = value; }
        }

        public XElement SerializeXML()
        {
            XElement element = new XElement("CoreBussiness");
            if (patientModel != null)
                element.Add(patientModel.SerializeXML());

            if (orderModel != null)
                element.Add(orderModel.SerializeXML());

            if (reportModels != null && reportModels.Count > 0)
            {
                foreach (var report in reportModels)
                {
                    element.Add(report.SerializeXML());
                }
            }

            if (RequisitionModels != null && RequisitionModels.Count > 0)
            {
                foreach (var requisition in RequisitionModels)
                {
                    element.Add(requisition.SerializeXML());
                }
            }

            element.Add(new XElement("Memo", this.memo));

            return element;
        }

        public void DeSerializeXML(XElement element)
        {

            var e = (from node in element.Descendants("Patient")
                     select node).Single();
            if (e != null)
            {
                this.patientModel = new RefPatientModel();
                this.patientModel.DeSerializeXML(e);
            }

            e = (from node in element.Descendants("Order")
                 select node).Single();
            if (e != null)
            {
                this.orderModel = new RefOrderModel();
                this.orderModel.DeSerializeXML(e);
            }

            var reportNodes = from node in element.Descendants("Report")
                              select node;
            foreach (var node in reportNodes)
            {
                RefReportModel rptModel = new RefReportModel();
                rptModel.DeSerializeXML(node);
                this.reportModels.Add(rptModel);
            }

            var requisitionNodes = from node in element.Descendants("Requisition")
                                   select node;
            foreach (var node in requisitionNodes)
            {
                RefRequisitionModel rqModel = new RefRequisitionModel();
                rqModel.DeSerializeXML(node);
                this.requisitionModels.Add(rqModel);
            }

            e = element.Element("Memo");
            this.memo = e == null ? "" : e.Value;
        }

    }
}
