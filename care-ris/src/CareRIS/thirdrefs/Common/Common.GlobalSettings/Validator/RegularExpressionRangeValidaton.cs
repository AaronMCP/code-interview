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
    #region class definition for RegularExpressionRangeValidaton
    public class RegularExpressionRangeValidaton : Validation
    {
        private string szRegularExpression = string.Empty;

        /// <summary>
        ///  Property for Regular Expression
        /// </summary>
        public string RegularExpression
        {
            get { return szRegularExpression; }
            set { szRegularExpression = value; }
        }
        private string szCompareString = string.Empty;

        /// <summary>
        ///  Property for Compared String
        /// </summary>
        public string CompareString
        {
            get { return szCompareString; }
            set { szCompareString = value; }
        }

        /// <summary>
        /// constructor of RegularExpressionRangeValidaton
        /// </summary>
        public RegularExpressionRangeValidaton()
        { }
        /// <summary>
        /// constructor of RegularExpressionRangeValidaton
        /// </summary>
        public RegularExpressionRangeValidaton(string szCmp)
        {
            this.CompareString = szCmp;
        }

        /// <summary>
        /// constructor of RegularExpressionRangeValidaton
        /// </summary>
        public RegularExpressionRangeValidaton(string szRp, string szCmp)
        {
            this.CompareString = szCmp;
            this.RegularExpression = szRp;
        }

        /// <summary>
        ///  RegularExpressionRangeValidaton validate method
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            System.Text.RegularExpressions.Regex oRegex = new System.Text.RegularExpressions.Regex(this.RegularExpression);
            return oRegex.IsMatch(this.CompareString);
        }
    }
    #endregion
}
