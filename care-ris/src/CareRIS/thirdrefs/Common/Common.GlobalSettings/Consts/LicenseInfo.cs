using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings
{
    public class LicenseInfo
    {

        ///        ClientNum WebClientNum ExpireTime Panel Function IISServerNum DemoExpireTime DemoPanel Demofunc    
        /// string     4           4          10       64    64        2            10           64       64

        public const int ClientNum_StartIndex = 0;
        public const int ClientNum_OffSet = 4;

        public const int WebClientNum_StartIndex = 4;
        public const int WebClientNum_OffSet = 4;

        public const int ExpireTime_StartIndex = 8;
        public const int ExpireTime_OffSet = 10;

        public const int Panel_StartIndex = 18;
        public const int Panel_OffSet = 64;

        public const int Function_StartIndex = 82;
        public const int Function_OffSet = 64;

        public const int IISServerNum_StartIndex = 146;
        public const int IISServerNum_OffSet = 2;

        public const int DemoExpireTime_StartIndex = 148;
        public const int DemoExpireTime_OffSet = 10;

        public const int DemoPanel_StartIndex = 158;
        public const int DemoPanel_OffSet = 64;

        public const int DemoFunction_StartIndex = 222;
        public const int DemoFunction_OffSet = 64;

        public static readonly DateTime DefaultExireTime = new DateTime(1980, 12, 10);
    }
}
