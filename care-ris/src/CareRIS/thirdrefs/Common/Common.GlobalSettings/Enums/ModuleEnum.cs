#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Andy Bu                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CommonGlobalSettings
{
    #region ModuleEnum definition
    //
    // Summary:
    //     ModuleEnum definition    
    //
    public enum ModuleEnum : int
    {
        /// <summary>
        /// Framework common module
        /// of client layer
        /// </summary>
        Framework_Common = 0x0000,
        /// <summary>
        /// Framework Client Layer
        /// </summary>
        /// <remarks>Framework Client Layer</remarks>
        Framework_Client = 0x0100,
        /// <summary>
        /// Framework Web Service Layer
        /// </summary>
        Framework_WS = 0x0101,
        /// <summary>
        /// Framework Business Facade Layer
        /// </summary>
        Framework_BF = 0x0102,
        /// <summary>
        /// Framework Data Access Layer
        /// </summary>
        Framework_DA = 0x0103,
        /// <summary>
        /// ExamApplication Client Layer
        /// </summary>
        ExamApplication_Client = 0x0200,
        /// <summary>
        /// ExamApplication Web Service Layer
        /// </summary>
        ExamApplication_WS = 0x0201,
        /// <summary>
        /// ExamApplication Business Facade Layer
        /// </summary>
        ExamApplication_BF = 0x0202,
        /// <summary>
        /// ExamApplication Data Access Layer
        /// </summary>
        ExamApplication_DA = 0x0203,
        /// <summary>
        /// Register Client Layer
        /// </summary>
        Register_Client = 0x0300,
        /// <summary>
        /// Register Web Service Layer
        /// </summary>
        Register_WS = 0x0301,
        /// <summary>
        /// Register Business Facade Layer
        /// </summary>
        Register_BF = 0x0302,
        /// <summary>
        /// Register Data Access Layer
        /// </summary>
        Register_DA = 0x0303,
        /// <summary>
        /// Report Client Layer
        /// </summary>
        Report_Client = 0x0400,
        /// <summary>
        /// Report Web Service Layer
        /// </summary>
        Report_WS = 0x0401,
        /// <summary>
        /// Report Business Facade Layer
        /// </summary>
        Report_BF = 0x0402,
        /// <summary>
        /// Report Data Access Layer
        /// </summary>
        Report_DA = 0x0403,
        /// <summary>
        /// Teaching Client Layer
        /// </summary>
        Teaching_Client = 0x0500,
        /// <summary>
        /// Teaching Web Service Layer
        /// </summary>
        Teaching_WS = 0x0501,
        /// <summary>
        /// Teaching Business Facade Layer
        /// </summary>
        Teaching_BF = 0x0502,
        /// <summary>
        /// Teaching Data Access Layer
        /// </summary>
        Teaching_DA = 0x0503,
        /// <summary>
        /// Exam Client Layer
        /// </summary>
        Exam_Client = 0x0600,
        /// <summary>
        /// Exam Web Service Layer 
        /// </summary>
        Exam_WS = 0x0601,
        /// <summary>
        /// Exam Business Facade Layer
        /// </summary>
        Exam_BF = 0x0602,
        /// <summary>
        /// Exam Data Access Layer
        /// </summary>
        Exam_DA = 0x0603,
        /// <summary>
        /// Statistic Client Layer
        /// </summary>
        Statistic_Client = 0x0700,
        /// <summary>
        /// Statistic Web Service Layer
        /// </summary>
        Statistic_WS = 0x0701,
        /// <summary>
        /// Statistic Business Facade Layer
        /// </summary>
        Statistic_BF = 0x0702,
        /// <summary>
        /// Statistic Data Access Layer
        /// </summary>
        Statistic_DA = 0x0703,
        /// <summary>
        /// Oam Client Layer
        /// </summary>
        Oam_Client = 0x0800,
        /// <summary>
        /// Oam Web Service Layer
        /// </summary>
        Oam_WS = 0x0801,
        /// <summary>
        /// Oam Business Facade Layer
        /// </summary>
        Oam_BF = 0x0802,
        /// <summary>
        /// Oam Data Access Layer
        /// </summary>
        Oam_DA = 0x0803,
        /// <summary>
        /// Web Access Client Layer
        /// </summary>
        WebAccess_Client = 0x0900,
        /// <summary>
        /// Web Access Web Service Layer
        /// </summary>
        WebAccess_WS = 0x0901,
        /// <summary>
        /// Web Access Business Facade Layer
        /// </summary>
        WebAccess_BF = 0x0902,
        /// <summary>
        /// Web Access Data Access Layer
        /// </summary>
        WebAccess_DA = 0x0903,
        /// <summary>
        /// LogServer for client
        /// </summary>
        Log_Client = 0x0A00,
        /// <summary>
        /// LogServer for Server side
        /// </summary>
        Log_Server = 0x0B00,
        /// <summary>
        /// Templates for Client side
        /// </summary>
        Templates_Client = 0x0C00,
        /// <summary>
        /// Web Access Web Service Layer
        /// </summary>
        Templates_WS = 0x0C01,
        /// <summary>
        /// Web Access Business Facade Layer
        /// </summary>
        Templates_BF = 0x0C02,
        /// <summary>
        /// Data Access Layer
        /// </summary>
        Templates_DA = 0x0C03,
        /// <summary>
        /// QualityControl Client Layer
        /// </summary>
        QualityControl_Client = 0x0D00,
        /// <summary>
        /// QualityControl Web Service Layer
        /// </summary>
        QualityControl_WS = 0x0D01,
        /// <summary>
        /// QualityControl Business Facade Layer
        /// </summary>
        QualityControl_BF = 0x0D02,
        /// <summary>
        /// QualityControl Data Access Layer
        /// </summary>
        QualityControl_DA = 0x0D03,
        /// <summary>
        /// Integration PACS image in RIS
        /// </summary>
        Integration_Client = 0x0E00,
        /// <summary>
        /// Backup files with ftp
        /// </summary>
        Bacup_Tool = 0x0F00,
        /// <summary>
        /// RIS Gate Server
        /// </summary>
        RISGateServer = 0x49,
        /// <summary>
        /// Modality Server
        /// </summary>
        ModalityServer = 0x3e,
        /// <summary>
        /// Paris Gate Server
        /// </summary>
        ParisGateServer = 0x3d,
        /// <summary>
        /// Common Module.
        /// It is not business module
        /// </summary>
        CommonModule = 0x3f,
        /// <summary>
        /// Referral Client Layer
        /// </summary>
        Referral_Client = 0x0204,
        /// <summary>
        /// Referral Web Service Layer
        /// </summary>
        Referral_WS = 0x0205,
        /// <summary>
        /// Referral Business Facade Layer
        /// </summary>
        Referral_BF = 0x0206,
        /// <summary>
        /// Referral Data Access Layer
        /// </summary>
        Referral_DA = 0x0207,
        /// <summary>
        /// Referral Client Layer
        /// </summary>
        DataCenter_Client = 0x0F10,
        /// <summary>
        /// Referral Web Service Layer
        /// </summary>
        DataCenter_WS = 0x0F11,
        /// <summary>
        /// Referral Business Facade Layer
        /// </summary>
        DataCenter_BF = 0x0F12,
        /// <summary>
        /// Referral Data Access Layer
        /// </summary>
        DataCenter_DA = 0x0F13,
 
    }
    #endregion

    #region GCRIS EmptyString Exception
    /// <summary>
    ///  validate if string is empty.
    ///  if so, throw the exception
    /// </summary>
    public class GCRISEmtpyStringException : ApplicationException
    {
        public GCRISEmtpyStringException() : base() { }
        public GCRISEmtpyStringException(string szstr) : base(szstr) { }
        public GCRISEmtpyStringException(SerializationInfo oinfo, StreamingContext ocontext) : base(oinfo, ocontext) { }
        public GCRISEmtpyStringException(String szstr, Exception ex) : base(szstr, ex) { }
    }
    #endregion
}
