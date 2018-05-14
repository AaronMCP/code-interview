using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(FtpModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SignHistoryModel))]
    public class CommonBaseModel : BaseModel
    {
    }


    public class ChargeItem : CommonBaseModel
    {
        #region Member Variables

        private string _ItemIndex;
        private string _Code;
        private string _Description;
        private int _Count;
        private Decimal _Price;
        private int _Action;
        private string _Operator;
        private string _Owner;
        private string _unit;
        private string _Optional;
        private string _Status;
        #endregion



        #region Public Properties

        public string ItemIndex
        {
            set
            {
                _ItemIndex = value;
            }
            get
            {
                return _ItemIndex;
            }
        }

        public string Code
        {
            set
            {
                _Code = value;
            }
            get
            {
                return _Code;
            }
        }

        public string Description
        {
            set
            {
                _Description = value;
            }
            get
            {
                return _Description;
            }
        }

        public int Count
        {
            set
            {
                _Count = value;
            }
            get
            {
                return _Count;
            }
        }

        public Decimal Price
        {
            set
            {
                _Price = value;
            }
            get
            {
                return _Price;
            }
        }

        public int Action
        {
            set
            {
                _Action = value;
            }
            get
            {
                return _Action;
            }
        }

        public string Operator
        {
            set
            {
                _Operator = value;
            }
            get
            {
                return _Operator;
            }
        }

        public string Unit
        {
            set
            {
                _unit = value;
            }
            get
            {
                return _unit;
            }
        }

        public string Owner
        {
            set
            {
                _Owner = value;
            }
            get
            {
                return _Owner;
            }
        }

        public string Optional
        {
            set
            {
                _Optional = value;
            }
            get
            {
                return _Optional;
            }
        }
        public string Status
        {
            set
            {
                _Status = value;
            }
            get
            {
                return _Status;
            }
        }

        #endregion
    }

    public class ChargeResult : ChargeItem
    {
        private int _Status;
        private string _Reason;
        public int Status
        {
            set
            {
                _Status = value;
            }
            get
            {
                return _Status;
            }
        }



        public string Reason
        {
            set
            {
                _Reason = value;
            }
            get
            {
                return _Reason;
            }
        }
    }

    public class CheckingItem : CommonBaseModel
    {

        private string _ItemIndex;
        public string ItemIndex
        {
            set
            {
                _ItemIndex = value;
            }
            get
            {
                return _ItemIndex;
            }
        }

        private string _ModalityType;
        public string ModalityType
        {
            set
            {
                _ModalityType = value;
            }
            get
            {
                return _ModalityType;
            }
        }

        private string _Modality;
        public string Modality
        {
            set
            {
                _Modality = value;
            }
            get
            {
                return _Modality;
            }
        }

        private string _ProcedureCode;
        public string ProcedureCode
        {
            set
            {
                _ProcedureCode = value;
            }
            get
            {
                return _ProcedureCode;
            }
        }

        private string _Description;
        public string Description
        {
            set
            {
                _Description = value;
            }
            get
            {
                return _Description;
            }
        }
        private string _Charge;
        public string Charge
        {
            set
            {
                _Charge = value;
            }
            get
            {
                return _Charge;
            }
        }


    }

    public class ChargeInfo : CommonBaseModel
    {
        #region Member Variables

        private string _HisID;
        private string _RISPatientID;
        private string _LocalName;
        private string _PatientType;
        private string _InhospitalNbr;
        private string _InhospitalRegion;
        private string _ChargeType;
        private string _HISAccNo;
        private string _RISAccNo;


        #endregion



        #region Public Properties

        public string HisID
        {
            set
            {
                _HisID = value;
            }
            get
            {
                return _HisID;
            }
        }

        public string RISPatientID
        {
            set
            {
                _RISPatientID = value;
            }
            get
            {
                return _RISPatientID;
            }
        }

        public string LocalName
        {
            set
            {
                _LocalName = value;
            }
            get
            {
                return _LocalName;
            }
        }

        public string PatientType
        {
            set
            {
                _PatientType = value;
            }
            get
            {
                return _PatientType;
            }
        }


        public string InhospitalNbr
        {
            set
            {
                _InhospitalNbr = value;
            }
            get
            {
                return _InhospitalNbr;
            }
        }



        public string InhospitalRegion
        {
            set
            {
                _InhospitalRegion = value;
            }
            get
            {
                return _InhospitalRegion;
            }
        }
        public string ChargeType
        {
            set
            {
                _ChargeType = value;
            }
            get
            {
                return _ChargeType;
            }
        }
        public string HISAccNo
        {
            set
            {
                _HISAccNo = value;
            }
            get
            {
                return _HISAccNo;
            }
        }
        public string RISAccNo
        {
            set
            {
                _RISAccNo = value;
            }
            get
            {
                return _RISAccNo;
            }
        }

        #endregion
    }
}
