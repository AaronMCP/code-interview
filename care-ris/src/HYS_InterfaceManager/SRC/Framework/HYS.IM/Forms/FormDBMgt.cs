using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Properties;
using HYS.Common.DataAccess;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.IM;

namespace HYS.IM
{
    public partial class FormDBMgt : Form
    {
        public FormDBMgt()
        {
            InitializeComponent();

            recDMgt.AttachDataGrid(this.dataGrid1);
            recIMgt.AttachDataGrid(this.dataGrid2);

            this.textBox1.Text = Program.DataDB.ConnectionString;
        }

        private DeviceRecManager recDMgt = new DeviceRecManager(Program.ConfigDB);
        private InterfaceRecManager recIMgt = new InterfaceRecManager(Program.ConfigDB);

        private void button1_Click(object sender, EventArgs e)
        {
            if (recDMgt.Create())
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (recDMgt.Drop())
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (recIMgt.Create())
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (recIMgt.Drop())
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!recDMgt.RefreshDataGrid())
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!recIMgt.RefreshDataGrid())
            {
                MessageBox.Show("failed\n\r" + Program.ConfigDB.LastError);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase(this.textBox1.Text);
            db.OnError += new DataAccessExceptionHanlder(db_OnError);
            MessageBox.Show(db.TestDBConnection().ToString());
        }

        private void db_OnError(string cnn, string sql, Exception err)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Connection String:" + cnn).AppendLine();
            sb.AppendLine("SQL Statement:" + sql).AppendLine();
            sb.AppendLine("Exception:" + err).AppendLine();
            MessageBox.Show(sb.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase(this.textBox1.Text);
            db.OnError += new DataAccessExceptionHanlder(db_OnError);
            this.dataGrid3.DataSource = db.ExecuteQuery(this.textBox2.Text);
        }

        private Dictionary<string, string> GetCurrentRow()
        {
            DataSet ds = this.dataGrid3.DataSource as DataSet;
            if (ds.Tables.Count < 1) return null;

            DataRow dr = null;
            int index = this.dataGrid3.CurrentRowIndex;
            if(ds.Tables[0].Rows.Count > 0) dr = ds.Tables[0].Rows[index];

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                string val = dr == null ? "" : dr[dc] as string;
                dic.Add(dc.Caption, val);
            }
            return dic;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = GetCurrentRow();
            if (dic == null) return;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            foreach (KeyValuePair<string, string> p in dic)
            {
                sb.Append(p.Key).Append(",");
            }
            string str = sb.ToString().TrimEnd(',');
            str += (" FROM <table>");

            FormDBScript frm = new FormDBScript(str);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            if (frm.Clear) this.textBox2.Text = frm.Script;
            else this.textBox2.Text += frm.Script;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = GetCurrentRow();
            if (dic == null) return;

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO <table> (");
            foreach (KeyValuePair<string, string> p in dic)
            {
                sb.Append(p.Key).Append(",");
            }
            string str = sb.ToString().TrimEnd(',');
            sb = new StringBuilder();
            sb.Append(str).Append(") VALUES (");
            foreach (KeyValuePair<string, string> p in dic)
            {
                sb.Append("'").Append(p.Value).Append("',");
            }
            str = sb.ToString().TrimEnd(',');
            str += ")";

            FormDBScript frm = new FormDBScript(str);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            if (frm.Clear) this.textBox2.Text = frm.Script;
            else this.textBox2.Text += frm.Script;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = GetCurrentRow();
            if (dic == null) return;

            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE <table> SET ");
            foreach (KeyValuePair<string, string> p in dic)
            {
                sb.Append(p.Key).Append("='").Append(p.Value).Append("',");
            }
            string str = sb.ToString().TrimEnd(',');
            str += " WHERE";

            FormDBScript frm = new FormDBScript(str);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            if (frm.Clear) this.textBox2.Text = frm.Script;
            else this.textBox2.Text += frm.Script;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //decimal d = 10;
            //MessageBox.Show(((int)d).ToString(this.textBox2.Text));
            //return;

            FormDBScript frm = new FormDBScript("");
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            if (frm.Clear) this.textBox2.Text = frm.Script;
            else this.textBox2.Text += frm.Script;
        }
    }
}