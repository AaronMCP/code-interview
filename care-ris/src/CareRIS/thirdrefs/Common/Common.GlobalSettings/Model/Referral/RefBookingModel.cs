using System;
using System.Collections.Generic;
using System.Text;

namespace Kodak.GCRIS.Common.Model.Referral
{
    /// <summary>
    /// Referral booking model
    /// </summary>
    [Serializable()]
    public class RefBookingModel : ReferralBaseModel
    {
        private RefPatientModel ptModel;
        private RefOrderModel orderModel;
        protected ReferralModel _refModel;
        private List<RefRequisitionModel> rqModels;

        private string _operatorId = string.Empty;
        private string _operatorName = string.Empty;

        public RefPatientModel PatientModel
        {
            get
            {
                if (ptModel == null)
                    return new RefPatientModel();
                return ptModel;
            }

            set
            {
                ptModel = value;
            }
        }

        public RefOrderModel OrderModel
        {
            get
            {
                if (orderModel == null)
                {
                    return new RefOrderModel();
                }
                return orderModel;
            }
            set
            {
                orderModel = value;
            }
        }

        public ReferralModel REFERRALMODEL
        {
            get
            {
                if (_refModel == null)
                {
                    return new ReferralModel();
                }
                return _refModel;
            }
            set
            {
                _refModel = value;
            }
        }

        public List<RefRequisitionModel> RequistionModelList
        {
            get
            {
                if (rqModels == null)
                {

                    rqModels = new List<RefRequisitionModel>();
                }
                return rqModels;
            }
            set { rqModels = value; }
        }

        public string OperatorId
        {
            get { return _operatorId; }
            set { _operatorId = value; }
        }

        public string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }
    }
}
