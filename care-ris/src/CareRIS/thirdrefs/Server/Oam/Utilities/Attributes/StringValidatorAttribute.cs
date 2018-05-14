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
using System.Text.RegularExpressions;
using CommonGlobalSettings;

namespace Server.Utilities.Oam.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class StringValidatorAttribute : ValidatorAttribute
    {
        private bool allowEmpty = true;
        private string expression;
        private string expressionErrorMessage;

        public StringValidatorAttribute()
        {

        }

        public StringValidatorAttribute(string expression)
        {
            this.expression = expression;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an empty string or null
        /// should be a considered a valid value.
        /// </summary>
        /// <value>
        /// true if an empty string or null should be considered a valid value; otherwise, false.
        /// The default is true.
        /// </value>
        public bool AllowEmpty
        {
            get
            {
                return allowEmpty; 
            }
            set
            { 
                allowEmpty = value; 
            }
        }

        /// <summary>
        /// Gets or sets a regular expression.The string will be validated to
        /// determine if it matches the expression.
        /// </summary>
        /// <value><see cref="System.Text.RegularExpressions"/></value>
        public string Expression
        {
            get 
            { 
                return expression; 
            }
            set 
            { 
                expression = value;
            }
        }

        /// <summary>
        /// An optional error message that can be used to better describe the
        /// regular expression error.
        /// </summary>
        public string ExpressionErrorMessage
        {
            get 
            { 
                return this.expressionErrorMessage;
            }
            set
            { 
                this.expressionErrorMessage = value;
            }
        }

        /// <summary>
        /// Checks if the specified value adheres to the rules defined by the 
        /// properties of the <see cref="StringValidatorAttribute" />.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <exception cref="OamException"><paramref name="value" /> is an empty string value and <see cref="AllowEmpty" /> is set to false.</exception>
        public override void Validate(object value)
        {
            string valueString;

            try
            {
                valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new GCRISException(ex.Message);
            }

            if (!AllowEmpty && StringUtils.IsNullOrEmpty(valueString))
            {
                throw new GCRISException("An empty value is not allowed.");
            }

            if (null != StringUtils.ConvertEmptyToNull(Expression))
            {
                if (!Regex.IsMatch(Convert.ToString(value), Expression))
                {
                    string msg = string.Format("String {0} does not match expression {1}.", value, Expression);
                    if (null != this.ExpressionErrorMessage && string.Empty != this.ExpressionErrorMessage)
                    {
                        msg = this.ExpressionErrorMessage;
                    }
                    throw new GCRISException(msg);
                }
            }
        }
    }
}
