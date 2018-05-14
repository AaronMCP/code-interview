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
    #region class definition for DoubleRangeValidaton
    /// <summary>
    ///  the inheritor class of Validation
    ///  this class use validate double value range
    /// </summary>
    public class DoubleRangeValidation : Validation
    {
        private double minVal;

        public double MinVal
        {
            get { return minVal; }
            set { minVal = value; }
        }
        private double maxVal;

        public double MaxVal
        {
            get { return maxVal; }
            set { maxVal = value; }
        }

        private double compareVal = 0;

        public double CompareVal
        {
            get { return compareVal; }
            set { compareVal = value; }
        }

        /// <summary>
        /// constructor of DoubleRangeValidation
        /// </summary>
        public DoubleRangeValidation()
        {           
        }

        /// <summary>
        /// constructor of DoubleRangeValidation
        /// </summary>
        public DoubleRangeValidation(double _minval, double _maxval)
        {
            this.MinVal = _minval;
            this.MaxVal = _maxval;
        }
        /// <summary>
        /// constructor of DoubleRangeValidation
        /// </summary>
        public DoubleRangeValidation(double _minval, double _maxval, double _compareval)
        {
            this.MinVal = _minval;
            this.MaxVal = _maxval;
            this.CompareVal = _compareval;
        }

        /// <summary>
        ///  Implement for Validate method of Validation
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return (CompareVal >= MinVal && CompareVal <= MaxVal) ? true : false;
        }
    }
    #endregion
}
