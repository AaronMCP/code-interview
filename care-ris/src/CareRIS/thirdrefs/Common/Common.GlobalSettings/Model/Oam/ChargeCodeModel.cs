using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public class ChargeCodeModel : OamBaseModel
    {
        #region Member Variables

        private string code;
        private string description;
        private string type;
        private string unit;
        private string shortCutCode;
        private decimal price;
        private string site;

        #endregion

        #region Constructors
        public ChargeCodeModel()
        {
        }
        #endregion

        #region Public Properties

        public string Code
        {
            set
            {
                code = value;
            }
            get
            {
                return code;
            }
        }

        public string Site
        {
            set
            {
                site = value;
            }
            get
            {
                return site;
            }
        }

        public string Description
        {
            set
            {
                description = value;
            }
            get
            {
                return description;
            }
        }

        public string Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }

        public string Unit
        {
            set
            {
                unit = value;
            }
            get
            {
                return unit;
            }
        }

        public string ShortCutCode
        {
            set
            {
                shortCutCode = value;
            }
            get
            {
                return shortCutCode;
            }
        }

        public decimal Price
        {
            set
            {
                price = value;
            }
            get
            {
                return price;
            }
        }


        #endregion
    }
}
