using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OutboundDBInstall
{
    public partial class FConfigJoinNode : Form
    {
        private bool _isNew = true;

        public FConfigJoinNode()
        {
            InitializeComponent();

            this.comboBoxJoinType.DataSource = Enum.GetNames(typeof(JoinOperator));
        }

        public event SaveNodeEventHandler SaveNode;
        private void OnSaveNode()
        {
            if (null != SaveNode)
            {
                SaveNodeEventArgs e = new SaveNodeEventArgs();
                e.IsNew = this._isNew;
                e.Node = new MatchFieldNode();
                e.Node.JoinOperator = (JoinOperator)Enum.Parse(typeof(JoinOperator), this.comboBoxJoinType.Text);
                SaveNode(this, e);
            }
        }


        internal void ShowDialog(IWin32Window owner, MatchFieldNode matchFieldNode)
        {
            _isNew = false;
            this.comboBoxJoinType.Text = matchFieldNode.JoinOperator.ToString();

            this.ShowDialog(owner);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            OnSaveNode();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class SaveNodeEventArgs : EventArgs
    {
        public MatchFieldNode Node { get; set; }

        public bool IsNew { get; set; }
    }

    public delegate void SaveNodeEventHandler(object sender, SaveNodeEventArgs e);
}
