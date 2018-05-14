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
    #region regular expression Validation decorator
    /// <summary>
    ///  definition for RegularExpressionValidationDecorator.
    ///  it is the derived class of ValidationDecorator<T>
    /// </summary>
    public class RegularExpressionValidationDecorator : ValidationDecorator
    {
        private RegularExpressionRangeValidaton m_obj=null;
        public RegularExpressionValidationDecorator(Validation oValidation,string szCmp)
        {
            this.ValidationObj = oValidation;
            m_obj = new RegularExpressionRangeValidaton(szCmp);
        }

        public RegularExpressionValidationDecorator(Validation oValidation, string szRp, string szCmp)
        {
            this.ValidationObj = oValidation;
            m_obj = new RegularExpressionRangeValidaton(szRp,szCmp);
        }

        public void SetCompareString(string szstr)
        {
            m_obj.CompareString = szstr;
        }
        public void SetRegularExpression(string szstr)
        {
            m_obj.RegularExpression = szstr;
        }

        public virtual bool Validate()
        {
            bool bRet = true;
            if (this.ValidationObj !=null)
                bRet = this.ValidationObj.Validate();

            if (true == bRet)
                bRet = m_obj.Validate();

            return bRet;
        }
    }
    #endregion
}
