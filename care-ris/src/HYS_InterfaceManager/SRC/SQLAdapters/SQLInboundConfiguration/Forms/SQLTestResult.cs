using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.DataAccess;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class SQLTestResult : Form
    {
        #region Local members
        //string connectionString;
        //string queryString;
        #endregion

        #region Constructor
        public SQLTestResult()
        {
            InitializeComponent();
        }
        #endregion

        #region Show Data
        public bool ShowDataResult(string connStr, string queryStr) {
             DataBase db = new DataBase(connStr);
             this.Cursor = Cursors.WaitCursor;
             if (!db.TestDBConnection())
             {
                 this.Cursor = Cursors.Default;
                 MessageBox.Show("Connect to data source failed!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                 this.Dispose();
                 return false;
             }
              
             DataSet ds = db.ExecuteQuery(queryStr);
             if (ds != null)
             {
                 this.Cursor = Cursors.Default;
                 DataTable table = ds.Tables[0];
                 table.DefaultView.AllowNew = false;
                 dataGridView.DataSource = table;
                 return true;
             }
             else
             {
                 this.Cursor = Cursors.Default;
                 MessageBox.Show("Query failed. " + ((db.LastError != null) ? db.LastError.Message : "")
                    , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                 this.Dispose();
                 return false;
             }
        }
        #endregion

        #region Close
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}