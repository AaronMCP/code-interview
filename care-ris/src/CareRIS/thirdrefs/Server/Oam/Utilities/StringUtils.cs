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
using System.Collections.Specialized;

namespace Server.Utilities.Oam
{
    public sealed class StringUtils
    {
        private StringUtils()
        {

        }

        /// <summary>
        /// Determines whether the last character of the given <see cref="string" />
        /// matches the specified character.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="c">The character.</param>
        /// <returns>
        /// <see langword="true" /> if the last character of <paramref name="value" />
        /// matches <paramref name="c" />; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
        public static bool EndsWith(string value, char c)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            int stringLength = value.Length;
            if ((stringLength != 0) && (value[stringLength - 1] == c))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Indicates whether or not the specified <see cref="string" /> is 
        /// <see langword="null" /> or an <see cref="string.Empty" /> string.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="value" /> is <see langword="null" />
        /// or an empty string (""); otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsNullOrEmpty(string value)
        {
            return (value == null || value.Trim().Length == 0);
        }

        /// <summary>
        /// Converts an empty string ("") to <see langword="null" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// <see langword="null" /> if <paramref name="value" /> is an empty 
        /// string ("") or <see langword="null" />; otherwise, <paramref name="value" />.
        /// </returns>
        public static string ConvertEmptyToNull(string value)
        {
            if (!IsNullOrEmpty(value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Converts <see langword="null" /> to an empty string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// An empty string if <paramref name="value" /> is <see langword="null" />;
        /// otherwise, <paramref name="value" />.
        /// </returns>
        public static string ConvertNullToEmpty(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }

        /// <summary>
        /// Concatenates a specified separator <see cref="string" /> between each 
        /// element of a specified <see cref="StringCollection" />, yielding a 
        /// single concatenated string.
        /// </summary>
        /// <param name="separator">A <see cref="string" />.</param>
        /// <param name="value">A <see cref="StringCollection" />.</param>
        /// <returns>
        /// A <see cref="string" /> consisting of the elements of <paramref name="value" /> 
        /// interspersed with the separator string.
        /// </returns>
        /// <remarks>
        /// <para>
        /// For example if <paramref name="separator" /> is ", " and the elements 
        /// of <paramref name="value" /> are "apple", "orange", "grape", and "pear", 
        /// <see cref="Join(string, StringCollection)" /> returns "apple, orange, 
        /// grape, pear".
        /// </para>
        /// <para>
        /// If <paramref name="separator" /> is <see langword="null" />, an empty 
        /// string (<see cref="string.Empty" />) is used instead.
        /// </para>
        /// </remarks>
        public static string Join(string separator, StringCollection value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (separator == null)
            {
                separator = string.Empty;
            }

            // create with size equal to number of elements in collection
            string[] elements = new string[value.Count];

            // copy elements in collection to array
            value.CopyTo(elements, 0);

            // concatenate specified separator between each elements 
            return string.Join(separator, elements);
        }

        /// <summary>
        /// Creates a shallow copy of the specified <see cref="StringCollection" />.
        /// </summary>
        /// <param name="stringCollection">The <see cref="StringCollection" /> that should be copied.</param>
        /// <returns>
        /// A shallow copy of the specified <see cref="StringCollection" />.
        /// </returns>
        public static StringCollection Clone(StringCollection stringCollection)
        {
            string[] strings = new string[stringCollection.Count];
            stringCollection.CopyTo(strings, 0);
            StringCollection clone = new StringCollection();
            clone.AddRange(strings);
            return clone;
        }
    }
}
