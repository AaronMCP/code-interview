using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using MSG = HYS.IM.Messaging.Objects;
using HYS.Common.Xml;
using System.Xml;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Controler;
using HYS.IM.Messaging.Mapping.Transforming.UIHelper;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Forms
{
    public partial class FormTest : Form
    {
        private XmlTabControlControler _ctrlCSBMsg;

        public FormTest()
        {
            InitializeComponent();

            _ctrlCSBMsg = new XmlTabControlControler(this.tabControlCSBMsg,
                this.tabPageCSBMsgPlain, this.textBoxCSBMsg,
                this.tabPageCSBMsgTree, this.webBrowserCSBMsg);
        }

        private void buttonReadOrderMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string file = Path.Combine(Application.StartupPath, "SampleCSBMessages\\CSB_DATASET_ORDER.xml");
                _ctrlCSBMsg.Open(File.ReadAllText(file));
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this, "Read sample CSB DataSet Message failed. See the log file for details.\r\n\r\nMessage: " + err.Message, this.Text);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string xml = _ctrlCSBMsg.GetXmlString();
            if (string.IsNullOrEmpty(xml)) return;

            MSG.Message msg = XObjectManager.CreateObject<MSG.Message>(xml);
            if (msg == null)
            {
                MessageBox.Show(this, "Deserialize CSB DataSet Message failed. See the log file for details.", this.Text);
                return;
            }

            try
            {
                DataSet ds = new DataSet();
                XmlReadMode m = ds.ReadXml(XmlReader.Create(new StringReader(msg.Body)));
                this.dataGridViewDS.DataSource = ds.Tables[0];
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this, "Deserialize DataSet XML failed. See the log file for details.\r\n\r\nMessage: " + err.Message, this.Text);
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            DataTable tb = this.dataGridViewDS.DataSource as DataTable;
            if (tb == null) return;

            CSBrokerOutboundHelper.ReplaceValueInCSBrokerDataSet(tb, Program.Context);

            string dbcnn = Program.Context.ConfigMgr.Config.CSBrokerOLEDBConnectionString;
            string spName = string.Format("sp_{0}_Order", Program.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName);

            try
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (DataRow dr in tb.Rows)
                {
                    OleDbCommand cmd = new OleDbCommand(spName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DataColumn dc in tb.Columns)
                    {
                        string pname = string.Format("@{0}_{1}",
                            Program.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName,
                            dc.ColumnName);

                        OleDbParameter p = new OleDbParameter(pname, OleDbType.VarWChar);
                        p.Direction = ParameterDirection.Input;
                        p.Value = dr[dc];
                        cmd.Parameters.Add(p);
                    }

                    using (OleDbConnection conn = new OleDbConnection(dbcnn))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                MessageBox.Show(this, string.Format("Insert into database success. Row count: {0}", tb.Rows.Count), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this, string.Format("Insert into database failed.\r\nSP Name:{0}\r\n\r\nMessage: {1}", spName, err.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            string dbcnn = Program.Context.ConfigMgr.Config.CSBrokerOLEDBConnectionString;
            string sqlstr = string.Format("select top 100 * from {0}_DATAINDEX I " +
                "left outer join {0}_PATIENT P on (I.DATA_ID=P.DATA_ID) " +
                "left outer join {0}_ORDER O on (I.DATA_ID=O.DATA_ID) " +
                "order by P.DATA_DT desc",
                Program.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName);

            try
            {
                DataSet ds = new DataSet();
                this.Cursor = Cursors.WaitCursor;
                using (OleDbConnection conn = new OleDbConnection(dbcnn))
                {
                    conn.Open();
                    using (OleDbDataAdapter ad = new OleDbDataAdapter(sqlstr, conn))
                    {
                        ad.Fill(ds);
                    }
                    conn.Close();
                }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                {
                    MessageBox.Show(this, string.Format("Find no record in the interface.\r\n{0}", sqlstr),
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.dataGridViewDS.DataSource = ds.Tables[0];
                }
                
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this, string.Format("Query database failed.\r\n{0}\r\n\r\nMessage: {1}", sqlstr, err.Message), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
