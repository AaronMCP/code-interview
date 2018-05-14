using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class UserModel : OamBaseModel
    {
        private string userGuid = "";
        private string loginName = "";
        private string localName = "";
        private string displayName = "";
        private string englishName = "";
        private string password = "";
        private string roleName = "";
        private string department = "";
        private string title = "";
        private string address = "";
        private string telephone = "";
        private string comments = "";
        private string addNewRole = "";
        private string deleteOldRole = "";
        private string domainLoginName = "";
        private int isSetExpireDate = 0;
        private object sign = null;
        private DateTime startDate ;
        private DateTime endDate ;
        private bool changedPasswordChecked = false;
        private bool isLoginNameChanged = false;
        private bool isDisplayNameChanged = false;
        private bool isDomainLoginNameChanged = false;
        private bool recoverDeletedUser = false; 
        private bool addSameDisplayNameUser = false;
        private DataSet dsSaveUserProfile = null;
        private DataSet dsUserDetails = null;
        private string domain = "";
        private string _iKeySN = string.Empty;
        private string mobile = "";
        private string email = "";

        public string UserGuid
        {
            get
            {
                return userGuid;
            }
            set
            {
                userGuid = value;
            }
        }

        public object Sign
        {
            get
            {
                return sign;
            }
            set
            {
                sign = value;
            }
        }

        public string LoginName
        {
            get
            {
                return loginName;
            }
            set
            {
                loginName = value;
            }
        }

        public string LocalName
        {
            get
            {
                return localName;
            }
            set
            {
                localName = value;
            }
        }

        public string EnglishName
        {
            get
            {
                return englishName;
            }
            set
            {
                englishName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public string Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string Telephone
        {
            get
            {
                return telephone;
            }
            set
            {
                telephone = value;
            }
        }

        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        public string DomainLoginName
        {
            get
            {
                return domainLoginName;
            }
            set
            {
                domainLoginName = value;
            }
        }

        public string AddNewRole
        {
            get
            {
                return addNewRole;
            }
            set
            {
                addNewRole = value;
            }
        }

        public string DeleteOldRole
        {
            get
            {
                return deleteOldRole;
            }
            set
            {
                deleteOldRole = value;
            }
        }

        public bool ChangedPasswordChecked
        {
            get
            {
                return changedPasswordChecked;
            }
            set
            {
                changedPasswordChecked = value;
            }
        }

        public bool LoginNameChanged
        {
            get
            {
                return isLoginNameChanged;
            }
            set
            {
                isLoginNameChanged = value;
            }
        }

        public bool DisplayNameChanged
        {
            get
            {
                return isDisplayNameChanged;
            }
            set
            {
                isDisplayNameChanged = value;
            }
        }

        public bool DomainLoginNameChanged
        {
            get
            {
                return isDomainLoginNameChanged;
            }
            set
            {
                isDomainLoginNameChanged = value;
            }
        }


        public DataSet SaveUserProfile
        {
            get
            {
                return dsSaveUserProfile;
            }
            set
            {
                dsSaveUserProfile = value;
            }
        }

        public DataSet UserDetails
        {
            get
            {
                return dsUserDetails;
            }
            set
            {
                dsUserDetails = value;
            }
        }
        public int IsSetExpireDate
        {
            get
            {
                return isSetExpireDate;
            }
            set
            {
                isSetExpireDate = value;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        public bool RecoverDeletedUser
        {
            get
            {
                return recoverDeletedUser;
            }
            set
            {
                recoverDeletedUser = value;
            }

        }

        public bool AddSameDisplayNameUser
        {
            get
            {
                return addSameDisplayNameUser;
            }
            set
            {
                addSameDisplayNameUser = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                domain = value;
            }

        }

        public string iKeySN
        {
            get
            {
                return _iKeySN;
            }
            set
            {
                _iKeySN = value;
            }
        }

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        #region Modified by Blue for RC603.2 - US16932, 06/03/2014
        private bool _isLocked = false;

        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        #endregion
    }
}
