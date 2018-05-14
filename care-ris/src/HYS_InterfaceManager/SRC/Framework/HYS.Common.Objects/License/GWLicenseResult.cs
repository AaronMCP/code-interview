using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicenseResult
    {
        public GWLicenseResult(uint value)
        {
            Value = value;
            Success = (value == 0);
        }
        public GWLicenseResult(uint value, byte[] array)
        {
            Value = value;
            Success = (value == 0);

            if (Success)
            {
                License = GWLicenseHelper.GetDefaultLicense();
                if (!GWLicenseHelper.LoadBytes(License, array)) Success = false;
            }
        }

        public readonly GWLicense License;
        public readonly bool Success;
        public readonly uint Value;
    }
}
