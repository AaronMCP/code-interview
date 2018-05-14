using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Dicom
{
    public enum DVR
    {
        Unknown = 0,
        SL = 1,
        SS = 2,
        UL = 4,
        US = 8,
        FL = 16,
        FD = 32,
        AE = 64,
        AS = 128,
        CS = 256,
        DS = 512,
        IS = 1024,
        LO = 2048,
        LT = 4096,
        SH = 8192,
        ST = 16384,
        UT = 32768,
        OB = 65536,
        OW = 131072,
        OF = 262144,
        UN = 524288,
        TM = 1048576,
        DT = 2097152,
        DA = 4194304,
        AT = 8388608,
        PN = 16777216,
        UI = 33554432,
        SQ = 67108864,
    }
}
