using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Referral booking model
    /// </summary>
    [Serializable()]
    public class BookingRefModel : ReferralBaseModel
    {
        protected ReferralModel _refModel;

        private string _operatorId = string.Empty;
        private string _operatorName = string.Empty;
        private CoreBussinessModel _corBizModel = new CoreBussinessModel();

        public RefPatientModel PatientModel
        {
            get
            {
                return _corBizModel.PatientModel;
            }

            set
            {
                _corBizModel.PatientModel = value;
            }
        }

        public RefOrderModel OrderModel
        {
            get
            {
                return _corBizModel.OrderModel;
            }
            set
            {
                _corBizModel.OrderModel = value;
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
                return _corBizModel.RequisitionModels;

            }
            set { _corBizModel.RequisitionModels = value; }
        }

        public CoreBussinessModel CorBizModel
        {
            get { return _corBizModel; }
            set { _corBizModel = value; }
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
