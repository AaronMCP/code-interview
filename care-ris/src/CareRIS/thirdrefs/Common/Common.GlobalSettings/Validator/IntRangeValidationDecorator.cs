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
    #region integer range Validation decorator
    /// <summary>
    ///  definition for RegularExpressionValidationDecorator.
    ///  it is the derived class of ValidationDecorator<T>
    /// </summary>
    public class IntRangeValidationDecorator : ValidationDecorator
    {
        private IntRangeValidaton m_obj = null;
        public IntRangeValidationDecorator(Validation oValidation, int minval, int maxval, int iCmp)
        {
            this.ValidationObj = oValidation;
            m_obj = new IntRangeValidaton(minval, maxval, iCmp);
        }
        public virtual bool Validate()
        {
            bool bRet = true;
            if (this.ValidationObj != null)
                bRet = this.ValidationObj.Validate();

            if (true == bRet)
                bRet = m_obj.Validate();

            return bRet;
        }
    }
    #endregion
}
