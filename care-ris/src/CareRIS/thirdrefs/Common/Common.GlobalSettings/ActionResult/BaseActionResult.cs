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
/*     Author : Caron Zhao                                                  */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Common.ActionResult.Oam;
using Common.ActionResult.Exam;
using Common.ActionResult.Framework;
using Common.ActionResult.Referral;
namespace Common.ActionResult
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(LoginSettingsActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ArrayActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(DataSetActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(OamBaseActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ExamBaseActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(FrameworkBaseDataSetActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(CommonBaseActionResult))]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralBaseActionResult))]
    public class BaseActionResult
    {        
        protected bool      result = false;
        protected string    returnString = null;
        protected int       _retcode = 0;
        protected string    returnMessage = null;

        public BaseActionResult()
        {

        }

        public BaseActionResult(bool result)
        {
            this.result = result;
        }

        public BaseActionResult(bool result, string returnMessage)
        {
            this.result = result;
            this.returnMessage = returnMessage;
        }

        public bool Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }

        public string ReturnString 
        {
            get
            {
                return returnString;
            }
            set
            {
                returnString = value;
            }
        }

        public int recode 
        {
            get
            {
                return _retcode;
            }
            set
            {
                _retcode = value;
            }
        }

        public string ReturnMessage
        {
            get
            {
                return returnMessage;
            }
            set
            {
                returnMessage = value;
            }
        }
    }
}
