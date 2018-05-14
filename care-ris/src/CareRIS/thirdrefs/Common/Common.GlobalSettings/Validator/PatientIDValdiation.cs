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

namespace CommonGlobalSettings
{
    #region validation class PID
    public class PatientIDValdiation
    {
        private RegularExpressionValidationDecorator oregex = null;
        private string szRp = "^[^|;%'\"\\ ]+$";
        public PatientIDValdiation()
        {
            oregex = new RegularExpressionValidationDecorator(null, szRp, string.Empty);
        }

        public PatientIDValdiation(string szcompare)
        {
            oregex = new RegularExpressionValidationDecorator(null, szRp, szcompare);
        }
        public void SetCompareString(string szstr)
        {
            oregex.SetCompareString(szstr);
        }
        public void SetRegularExpression(string szstr)
        {
            oregex.SetRegularExpression(szstr);
        }
        public virtual bool Validate()
        {
            return this.oregex.Validate();
        }
    }
    #endregion
}
