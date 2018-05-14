using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public class RefNonBookingModel :ReferralBaseModel
    {
        private ReferralModel refModel;
        private RefEventModel refEventModel;

        public RefEventModel EventModel
        {
            get
            {
                if (refEventModel == null)
                    return new RefEventModel();
                return refEventModel;
            }
            set 
            {
                refEventModel = value;
            }
        }

        public ReferralModel RefModel
        {
            get
            {
                if (refModel == null)
                    return new ReferralModel();
                return refModel;
            }
            set 
            { 
                refModel = value; 
            }
        }
    }
}
