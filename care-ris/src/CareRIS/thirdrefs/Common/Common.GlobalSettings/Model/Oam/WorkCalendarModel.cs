using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class SpecialDay : OamBaseModel
    {
        #region Member Variables

        private DateTime date;
        private int dateType;
        private string description;
        private string modality;
        private string site;

        #endregion

        #region Constructors
        public SpecialDay()
        {
        }
        #endregion

        #region Public Properties

        public DateTime Date
        {
            set
            {
                date = value;
            }
            get
            {
                return date;
            }
        }

        public int DateType
        {
            set
            {
                dateType = value;
            }
            get
            {
                return dateType;
            }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Modality
        {
            get { return modality; }
            set { modality = value; }
        }

        public string Site
        {
            get { return site; }
            set { site = value; }
        }
        #endregion
    }

    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(SpecialDay))]
    public class SpecialDayCollection : OamBaseModel
    {
        private List<SpecialDay> items = null;

        public SpecialDayCollection()
        {
            items = new List<SpecialDay>();
        }

        //[XmlArrayItem(typeof(SpecialDay))]
        public List<SpecialDay> SpecialDays
        {
            get { return items; }
            set { items = value; }
        }
    }
}
