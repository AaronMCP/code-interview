using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    public class PatientModel : PatientBaseModel
    {

        private string patientID = "";
        private string localName = "";
        private string englishName = "";
        private DateTime birthday;
        private string gender = "";
        private bool isVip = false;
        private string weight = "";
        private string telephone = "";
        private string address = "";
        private DateTime createDt;
        private string hisID = "";

        public PatientModel()
        {
            createDt = DateTime.Now;
            Birthday = new DateTime();
        }
        public string LocalName
        {
            set { localName = value; }
            get { return localName; }
        }

        public string EnglishName
        {
            set { englishName = value; }
            get { return englishName; }
        }

        public string PatientID
        {
            set { patientID = value; }
            get { return patientID; }
        }

        public DateTime Birthday
        {
            set { birthday = value; }
            get { return birthday; }
        }

        public string Gender
        {
            set { gender = value; }
            get { return gender; }
        }

        public bool IsVip
        {
            set { isVip = value; }
            get { return isVip; }
        }

        public string Weight
        {
            set { telephone = value; }
            get { return telephone; }
        }

        public string Telephone
        {
            set { weight = value; }
            get { return weight; }
        }

        public string Address
        {
            set { address = value; }
            get { return address; }
        }

        public string HISID
        {
            set { hisID = value; }
            get { return hisID; }
        }

        public DateTime CreateDt
        {
            set { createDt = value; }
            get { return createDt; }
        }
    }
}
