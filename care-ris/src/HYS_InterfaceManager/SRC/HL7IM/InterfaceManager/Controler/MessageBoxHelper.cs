using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.HL7IM.Manager.Controler
{
    public static class MessageBoxHelper
    {
        public static void ShowInformation(string strInfo)
        {
            MessageBox.Show(strInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorInfomation(string strInfo)
        {
            MessageBox.Show(strInfo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowConfirmBox(string strInfo)
        {
            if (MessageBox.Show(strInfo, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
