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

    #region class definition for IntRangeValidaton
    /// <summary>
    ///  the inheritor class of Validation
    ///  this class use validate integer value range
    /// </summary>
    public class IntRangeValidaton : Validation
    {
        private int _minval;

        public int Minval
        {
            get { return _minval; }
            set { _minval = value; }
        }
        private int _maxval;

        public int Maxval
        {
            get { return _maxval; }
            set { _maxval = value; }
        }
        private int compareVal;

        public int CompareVal
        {
            get { return compareVal; }
            set { compareVal = value; }
        }

        /// <summary>
        /// constructor of IntRangeValidaton
        /// </summary>
        public IntRangeValidaton()
        { }

        /// <summary>
        /// constructor of IntRangeValidaton
        /// </summary>
        public IntRangeValidaton(int minval, int maxval)
        {
            this._minval = minval;
            this._maxval = maxval;
        }

        /// <summary>
        /// constructor of IntRangeValidaton
        /// </summary>
        public IntRangeValidaton(int minval, int maxval, int _compareval)
        {
            this._minval = minval;
            this._maxval = maxval;
            this.compareVal = _compareval;
        }

        /// <summary>
        ///  Implement for Validate method of Validation
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return (CompareVal >= Minval && CompareVal <= Maxval) ? true : false;
        }
    }

    #endregion
}
