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
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CommonGlobalSettings;

namespace Server.Utilities.Oam.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class DateTimeValidatorAttribute : ValidatorAttribute
    {
        public DateTimeValidatorAttribute()
        {
        }

        /// <summary>
        /// Checks if the specified value can be converted to a <see cref="DateTime" />.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <exception cref="OamException"><paramref name="value" /> cannot be converted to a <see cref="DateTime" />.</exception>
        public override void Validate(object value)
        {
            try
            {
                Convert.ToDateTime(value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new GCRISException(ex.Message);
            }
        }
    }
}
