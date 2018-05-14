using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.IPMonitor.Policies;

namespace HYS.IM.IPMonitor
{
    public partial class ServiceList : Form
    {
        private string selectedText = "";
        private DataTable selectedServices = new DataTable();

        public DataTable SelectedServices
        {
            get
            {                
                return selectedServices;
            }
            set
            {
                selectedServices = value;
            }
        }

        public string SelectedText
        {
            get
            {
                return selectedText;
            }
            set
            {
                this.list.SelectionMode = SelectionMode.One;
                selectedText = value;
            }
        }

        public ServiceList()
        {
            InitializeComponent();
        }

        private void ServiceList_Load(object sender, EventArgs e)
        {
            this.CenterToParent();

            DataTable dt = Helper.GetServiceList();
            selectedServices.AcceptChanges();
            foreach (DataRow row in selectedServices.Rows)
            {
                foreach (DataRow row2 in dt.Rows)
                {
                    if (row["Value"].ToString() == row2["Value"].ToString())
                    {
                        row2.Delete();
                        break;
                    }
                }
            }
            dt.AcceptChanges();
            list.DisplayMember = "Display";
            list.ValueMember = "Value";
            list.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                list.SelectedIndex = -1;
            }
            else
            {
                if (this.list.SelectionMode == SelectionMode.One)
                {
                    list.SelectedValue = this.selectedText;
                }
                else
                {
                    list.SelectedIndex = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.list.SelectionMode == SelectionMode.One)
            {
                this.selectedText = this.list.SelectedValue.ToString();
                this.Close();
            }
            else
            {
                if (list.SelectedItems.Count > 0)
                {
                    foreach (DataRowView row in list.SelectedItems)
                    {
                        selectedServices.LoadDataRow(row.Row.ItemArray, false);
                    }
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
