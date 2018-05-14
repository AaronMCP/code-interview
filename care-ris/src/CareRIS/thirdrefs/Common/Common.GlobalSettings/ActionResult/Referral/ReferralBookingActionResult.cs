using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;


namespace Common.ActionResult.Referral
{
    public class ReferralBookingActionResult : ReferralBaseActionResult
    {
        private BookingRefModel _refBookModel = null;

        public BookingRefModel BookingModel
        {
            get
            {
                return _refBookModel;
            }
            set
            {
                _refBookModel = value;
            }
        }
    }
}
