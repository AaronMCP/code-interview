using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class UserCertModel : OamBaseModel
    {

        public string UserGuid
        {
            get;
            set;
        }

        public string CertID
        {
            get;
            set;
        }

        public string CertSN
        {
            get;
            set;
        }

        public string CertBasicInfo
        {
            get;
            set;
        }

        public string CertInfo
        {
            get;
            set;
        }

        public int IsActive
        {
            get;
            set;
        }

        public object SignPic
        {
            get;
            set;
        }

        private DateTime expiryDate;
        public DateTime ExpiryDate
        {
            get
            {
                if (expiryDate == DateTime.MinValue && !string.IsNullOrEmpty(CertBasicInfo))
                {
                    XDocument xd = XDocument.Parse(CertBasicInfo);
                    DateTime dt;
                    DateTime.TryParse(xd.Root.Element("ValidateTo").Value, out dt);
                    expiryDate = dt;
                    return expiryDate;
                }
                else
                {
                    return expiryDate;
                }
            }
            set
            {
                expiryDate = value;
            }
        }
    }
}
