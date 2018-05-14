using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public class ChargeModel : OamBaseModel
    {
        #region Member Variables

        private string chargeGuid;
        private string orderGuid;
        private string code;
        private string description;
        private int amount;
        private Decimal price;
        private string unit;
        private int confirm;
        private string confirmer;
        private DateTime? confirmDt;
        private string confirmReason;
        private int deduct;
        private string deducter;
        private DateTime? deductDt;
        private string deductReason;
        private int refund;
        private string refunder;
        private DateTime? refundDt;
        private string refundReason;
        private int cancel;
        private string canceler;
        private DateTime? cancelDt;
        private string cancelReason;
        private int lastAction;
        private int lastStatus;
        private string optional;
        #endregion

        #region Constructors
        public ChargeModel()
        {
            deduct = 10;// not pay
            refund = 20; // not refund
            cancel = 30; // not cancel
        }
        #endregion

        #region Public Properties

        public string ChargeGuid
        {
            set
            {
                chargeGuid = value;
            }
            get
            {
                return chargeGuid;
            }
        }

        public string OrderGuid
        {
            set
            {
                orderGuid = value;
            }
            get
            {
                return orderGuid;
            }
        }

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

        public int Amount
        {
            set
            {
                amount = value;
            }
            get
            {
                return amount;
            }
        }

        public Decimal Price
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

        public int Confirm
        {
            set
            {
                confirm = value;
            }
            get
            {
                return confirm;
            }
        }

        public string Confirmer
        {
            set
            {
                confirmer = value;
            }
            get
            {
                return confirmer;
            }
        }

        public DateTime? ConfirmDt
        {
            set
            {
                confirmDt = value;
            }
            get
            {
                return confirmDt;
            }
        }

        public string ConfirmReason
        {
            set
            {
                confirmReason = value;
            }
            get
            {
                return confirmReason;
            }
        }

        public int Deduct
        {
            set
            {
                deduct = value;
            }
            get
            {
                return deduct;
            }
        }

        public string Deducter
        {
            set
            {
                deducter = value;
            }
            get
            {
                return deducter;
            }
        }

        public DateTime? DeductDt
        {
            set
            {
                deductDt = value;
            }
            get
            {
                return deductDt;
            }
        }

        public string DeductReason
        {
            set
            {
                deductReason = value;
            }
            get
            {
                return deductReason;
            }
        }

        public int Refund
        {
            set
            {
                refund = value;
            }
            get
            {
                return refund;
            }
        }

        public string Refunder
        {
            set
            {
                refunder = value;
            }
            get
            {
                return refunder;
            }
        }

        public DateTime? RefundDt
        {
            set
            {
                refundDt = value;
            }
            get
            {
                return refundDt;
            }
        }

        public string RefundReason
        {
            set
            {
                refundReason = value;
            }
            get
            {
                return refundReason;
            }
        }

        public int Cancel
        {
            set
            {
                cancel = value;
            }
            get
            {
                return cancel;
            }
        }

        public string Canceler
        {
            set
            {
                canceler = value;
            }
            get
            {
                return canceler;
            }
        }

        public DateTime? CancelDt
        {
            set
            {
                cancelDt = value;
            }
            get
            {
                return cancelDt;
            }
        }

        public string CancelReason
        {
            set
            {
                cancelReason = value;
            }
            get
            {
                return cancelReason;
            }
        }

        public int LastAction
        {
            set
            {
                lastAction = value;
            }
            get
            {
                return lastAction;
            }
        }

        public int LastStatus
        {
            set
            {
                lastStatus = value;
            }
            get
            {
                return lastStatus;
            }
        }

        public string Optional
        {
            set
            {
                optional = value;
            }
            get
            {
                return optional;
            }
        }

        #endregion
    }
}
