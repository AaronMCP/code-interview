using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    /// <summary>
    /// TERequisition object  mapped table 'tERequisition'.
    /// </summary>
    [Serializable()]
    public class ERequisitionModel : ReferralBaseModel
    {
        #region Member Variables
		
		protected string _id;
		protected string _eRNo  = string.Empty;
		protected string _patientID  = string.Empty;
		protected string _patientName  = string.Empty;
		protected string _inHospitalNo  = string.Empty;
		protected string _clinicNo  = string.Empty;
		protected string _bedNo  = string.Empty;
		protected string _currentAge  = string.Empty;
		protected string _gender  = string.Empty;
		protected DateTime? _patientBirthday ;
		protected string _modalityType  = string.Empty;
		protected string _procedureCode  = string.Empty;
		protected string _examSystem  = string.Empty;
		protected string _applyDoctor  = string.Empty;
		protected string _applyDept  = string.Empty;
		protected string _applyDeptNo  = string.Empty;
		protected string _observation  = string.Empty;
		protected string _healthHistory  = string.Empty;
		protected string _comments  = string.Empty;
		protected string _address  = string.Empty;
		protected string _telephone  = string.Empty;
		protected DateTime? _workedDate ;
		protected string _status  = string.Empty;
		protected string _isBooking  = string.Empty;
		protected DateTime? _applyDate ;
		protected string _description  = string.Empty;
		protected string _domain  = string.Empty;
		protected string _referralId  = string.Empty;

		#endregion

		#region Constructors

		public ERequisitionModel() { }		

		#endregion

		#region Public Properties

		[DataField("Guid")]
		public string Guid
		{
			get {return _id;}
			set
			{
				if ( value != null && value.Length > 128)
					throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
				_id = value;
			}
		}

		[DataField("ERNo")]
		public string ERNo
		{
			get { return _eRNo; }
			set
			{				
				_eRNo = value;
			}
		}

		[DataField("PatientID")]
		public string PatientID
		{
			get { return _patientID; }
			set
			{				
				_patientID = value;
			}
		}

		[DataField("PatientName")]
		public string PatientName
		{
			get { return _patientName; }
			set
			{				
				_patientName = value;
			}
		}

		[DataField("InHospitalNo")]
		public string InHospitalNo
		{
			get { return _inHospitalNo; }
			set
			{				
				_inHospitalNo = value;
			}
		}

		[DataField("ClinicNo")]
		public string ClinicNo
		{
			get { return _clinicNo; }
			set
			{				
				_clinicNo = value;
			}
		}

		[DataField("BedNo")]
		public string BedNo
		{
			get { return _bedNo; }
			set
			{				
				_bedNo = value;
			}
		}

		[DataField("CurrentAge")]
		public string CurrentAge
		{
			get { return _currentAge; }
			set
			{				
				_currentAge = value;
			}
		}

		[DataField("Gender")]
		public string Gender
		{
			get { return _gender; }
			set
			{				
				_gender = value;
			}
		}

		[DataField("PatientBirthday")]
		public DateTime? PatientBirthday
		{
			get { return _patientBirthday; }
			set { _patientBirthday = value; }
		}

		[DataField("ModalityType")]
		public string ModalityType
		{
			get { return _modalityType; }
			set
			{				
				_modalityType = value;
			}
		}

		[DataField("ProcedureCode")]
		public string ProcedureCode
		{
			get { return _procedureCode; }
			set
			{				
				_procedureCode = value;
			}
		}

		[DataField("ExamSystem")]
		public string ExamSystem
		{
			get { return _examSystem; }
			set
			{				
				_examSystem = value;
			}
		}

		[DataField("ApplyDoctor")]
		public string ApplyDoctor
		{
			get { return _applyDoctor; }
			set
			{				
				_applyDoctor = value;
			}
		}

		[DataField("ApplyDept")]
		public string ApplyDept
		{
			get { return _applyDept; }
			set
			{				
				_applyDept = value;
			}
		}

		[DataField("ApplyDeptNo")]
		public string ApplyDeptNo
		{
			get { return _applyDeptNo; }
			set
			{				
				_applyDeptNo = value;
			}
		}

		[DataField("Observation")]
		public string Observation
		{
			get { return _observation; }
			set
			{				
				_observation = value;
			}
		}

		[DataField("HealthHistory")]
		public string HealthHistory
		{
			get { return _healthHistory; }
			set
			{				
				_healthHistory = value;
			}
		}

		[DataField("Comments")]
		public string Comments
		{
			get { return _comments; }
			set
			{				
				_comments = value;
			}
		}

		[DataField("Address")]
		public string Address
		{
			get { return _address; }
			set
			{				
				_address = value;
			}
		}

		[DataField("Telephone")]
		public string Telephone
		{
			get { return _telephone; }
			set
			{				
				_telephone = value;
			}
		}

		[DataField("WorkedDate")]
		public DateTime? WorkedDate
		{
			get { return _workedDate; }
			set { _workedDate = value; }
		}

		[DataField("Status")]
		public string Status
		{
			get { return _status; }
			set
			{				
				_status = value;
			}
		}

		[DataField("IsBooking")]
		public string IsBooking
		{
			get { return _isBooking; }
			set
			{				
				_isBooking = value;
			}
		}

		[DataField("ApplyDate")]
		public DateTime? ApplyDate
		{
			get { return _applyDate; }
			set { _applyDate = value; }
		}

		[DataField("Description")]
		public string Description
		{
			get { return _description; }
			set
			{				
				_description = value;
			}
		}

		[DataField("Domain")]
		public string Domain
		{
			get { return _domain; }
			set
			{				
				_domain = value;
			}
		}

		[DataField("ReferralId")]
		public string ReferralId
		{
			get { return _referralId; }
			set
			{				
				_referralId = value;
			}
		}		

		#endregion
    }
}
