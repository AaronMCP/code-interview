using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using System.Data;

namespace Hys.CommonControls
{
    /// <summary>
    /// NOTE: Please be sure that you have set DisplayMember before you bind data source to ListBox or the Text property will display incorrect value.
    /// </summary>
    public class CSListBox : RadListBox
    {
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadListBox";
            }
            set
            {
                base.ThemeClassName = value;
            }
        }

        #region Public Functions
        /// <summary>
        /// Finds the index of the first item in the list box that matches the specified string. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int FindStringExact(string text)
        {
            int index = -1;
            RadListBoxItem item = this.FindItemExact(text);
            if (item != null)
            {
                index = this.Items.IndexOf(item);
            }
            return index;
        }
        #endregion

    }
}
