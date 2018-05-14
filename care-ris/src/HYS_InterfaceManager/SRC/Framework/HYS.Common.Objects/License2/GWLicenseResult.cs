using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.License2
{
    public class GWLicenseResult
    {
        public GWLicenseResult()
        {
            License = GWLicenseHelper.GetDefaultLicense();
            Success = true;

        }


        public readonly GWLicense License;
        public readonly bool Success;

    }
}
