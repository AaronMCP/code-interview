using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.SQLInboundAdapterConfiguration.Controls;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class ShowConnectionStatement : Form
    {
        #region Local members
        ActiveMode parentControl;
        #endregion

        #region Constructor
        public ShowConnectionStatement(ActiveMode control)
        {
            InitializeComponent();
            this.parentControl = control;
        }
        #endregion

        #region Controls events
        public void TextContent(string content) {
            this.txtConnection.Text = content;
        }
        #endregion

        #region OK and Cancel
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            parentControl.ConnectionConfig(this.txtConnection.Text);
            this.Close(); ;
        }
        #endregion
    }
}