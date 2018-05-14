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
using LogServer;


namespace Server.Utilities.LogFacility
{
    /// <summary>
    /// the derived class from LogManager.<br></br>
    /// use to Filter log in Server Module.<br></br>
    /// </summary>
    public class LogManagerForServer : LogManager
    {
        private string loginName = "not set";

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string hospitalName = "not set";

        public string HospitalName
        {
            get { return hospitalName; }
            set { hospitalName = value; }
        }
        private string _szClientLoglevelKey, _szModuleNumber;
        public LogManagerForServer(string szClientLoglevelKey, string szModuleNumber)
        {
            _szClientLoglevelKey = szClientLoglevelKey;
            _szModuleNumber = szModuleNumber;
        }

        private int iDefaultLogLevel = -1;

        /// <summary>
        /// Property of Default Log Level.<br></br>
        /// </summary>
        public int DefaultLogLevel
        {
            get
            {
                if (iDefaultLogLevel == -1)
                {
                    // get value from tsystemprofiile
                    string szLoglevel = ConfigDicMgr.Instance.GetConfigDicValue(_szClientLoglevelKey, _szModuleNumber);
                    try
                    {
                        iDefaultLogLevel = int.Parse(szLoglevel);
                    }
                    catch
                    {
                        iDefaultLogLevel = 4;
                    }
                    
                }
                return iDefaultLogLevel;
            }
            //set { iDefaultLogLevel = value; }
        }
        /// <summary>
        ///   Debug Log Info.<br></br>
        /// </summary>
        /// <param name="lModule">Module Number</param>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public override void Debug(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            int level = 1;
            if (level >= this.DefaultLogLevel)
            {
                base.LoginName = LoginName;
                base.HospitalName = hospitalName;
                base.Debug(lModule, szModuleInstanceName, lCode, szDescription, szExtension, szSourceFile, lLineNo);
            }
        }

        /// <summary>
        ///   Inform Log Info.<br></br>
        /// </summary>
        /// <param name="lModule">Module Number</param>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public override void Info(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            int level = 2;
            if (level >= this.DefaultLogLevel)
            {
                base.LoginName = LoginName;
                base.HospitalName = hospitalName;
                base.Info(lModule, szModuleInstanceName, lCode, szDescription, szExtension, szSourceFile, lLineNo);
            }
        }

        /// <summary>
        ///   Warning Log Info.<br></br>
        /// </summary>
        /// <param name="lModule">Module Number</param>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public override void Warn(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            int level = 3;
            if (level >= this.DefaultLogLevel)
            {
                base.LoginName = LoginName;
                base.HospitalName = hospitalName;
                base.Warn(lModule, szModuleInstanceName, lCode, szDescription, szExtension, szSourceFile, lLineNo);
            }
        }

        /// <summary>
        ///   Error Log Info.<br></br>
        /// </summary>
        /// <param name="lModule">Module Number</param>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public override void Error(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            int level = 4;
            if (level >= this.DefaultLogLevel)
            {
                base.LoginName = LoginName;
                base.HospitalName = hospitalName;
                base.Error(lModule, szModuleInstanceName, lCode, szDescription, szExtension, szSourceFile, lLineNo);
            }
        }

        /// <summary>
        ///   Fatal Log Info.<br></br>
        /// </summary>
        /// <param name="lModule">Module Number</param>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public override void Fatal(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            int level = 5;
            if (level >= this.DefaultLogLevel)
            {
                base.LoginName = LoginName;
                base.HospitalName = hospitalName;
                base.Fatal(lModule, szModuleInstanceName, lCode, szDescription, szExtension, szSourceFile, lLineNo);
            }
        }


    }
}
