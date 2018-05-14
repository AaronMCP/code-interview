using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom;
using System.Threading;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormAutoGenIDs : Form
    {
        public FormAutoGenIDs()
        {
            InitializeComponent();
        }

        private decimal count;
        private int maxLenGUID;
        private int maxLenStepID;

        private void _AddItem(string guid, string thrID, string timeSpent)
        {
            ListViewItem item = new ListViewItem();
            item.Text = guid;
            item.SubItems.Add(guid.Length.ToString());
            item.SubItems.Add(thrID);
            item.SubItems.Add(timeSpent);
            this.listViewID.Items.Add(item);
            Application.DoEvents();
        }
        private delegate void AddItemHandler(string guid, string thrID, string timeSpent);
        private void AddItem(string guid, string thrID, string timeSpent)
        {
            AddItemHandler h = new AddItemHandler(_AddItem);
            this.Invoke(h, new object[] { guid, thrID, timeSpent });
        }

        private void Initialize()
        {
            count = this.numericUpDownCount.Value;
            maxLenGUID = (int)this.numericUpDownMaxLenGUID.Value;
            maxLenStepID = (int)this.numericUpDownMaxLenStepID.Value;
        }
        private void GenerateGUID()
        {
            List<string> list = new List<string>();
            DateTime dtBegin = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                //string guid = DHelper.GetDicomGUID("2147483647");
                list.Add(DHelper.GetDicomGUID("500", maxLenGUID));
            }
            DateTime dtEnd = DateTime.Now;
            TimeSpan dtSpan = dtEnd.Subtract(dtBegin);
            double average = (double)dtSpan.TotalMilliseconds / (double)count;
            foreach (string guid in list)
            {
                AddItem(guid, Thread.CurrentThread.ManagedThreadId.ToString(), average.ToString());
            }
        }
        private void GenerateStepID()
        {
            List<string> list = new List<string>();
            DateTime dtBegin = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                list.Add(WorklistSCPHelper.GetRandomNumber(maxLenStepID));
            }
            DateTime dtEnd = DateTime.Now;
            TimeSpan dtSpan = dtEnd.Subtract(dtBegin);
            double average = (double)dtSpan.TotalMilliseconds / (double)count;
            foreach (string guid in list)
            {
                AddItem(guid, Thread.CurrentThread.ManagedThreadId.ToString(), average.ToString());
            }
        }
        private void FindDuplicatedID()
        {
            Dictionary<string, List<ListViewItem>> dic = new Dictionary<string, List<ListViewItem>>();
            foreach (ListViewItem i in this.listViewID.Items)
            {
                string guid = i.Text;
                if (dic.ContainsKey(guid))
                {
                    dic[guid].Add(i);
                }
                else
                {
                    List<ListViewItem> l = new List<ListViewItem>();
                    dic.Add(guid, l);
                    l.Add(i);
                }
            }

            int duplicatedIDCount = 0;
            int maxDisplayDuplicateIDCount = 10;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, List<ListViewItem>> p in dic)
            {
                if (p.Value.Count > 1)
                {
                    duplicatedIDCount++;
                    if (duplicatedIDCount <= maxDisplayDuplicateIDCount)
                    {
                        sb.Append("Duplicated ID: ").Append(p.Key)
                          .Append(" Times: ").AppendLine(p.Value.Count.ToString())
                          .Append(" Thread IDs: ");
                        foreach (ListViewItem i in p.Value)
                        {
                            sb.Append(i.SubItems[2].Text).Append(";");
                        }
                        sb.AppendLine();
                    }
                }
            }
            sb.AppendLine(duplicatedIDCount > maxDisplayDuplicateIDCount ? "There are more..." : "");
            dic.Clear();

            if (duplicatedIDCount < 1)
            {
                MessageBox.Show(this,
                    "Do not find any duplicated ID.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this,
                    "Find " + duplicatedIDCount.ToString() + " duplicated ID(s) as the following: \r\n\r\n" + sb.ToString(),
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void FormAutoGenIDs_Load(object sender, EventArgs e)
        {
            this.textBoxMACAddress.Text = DHelper.GetMACAddressNumber().ToString();
        }
        private void buttonGUID_Click(object sender, EventArgs e)
        {
            Initialize();
            for (int i = 0; i < this.numericUpDownThread.Value; i++)
            {
                Thread thr = new Thread(GenerateGUID);
                thr.Start();
            }
        }
        private void buttonStepID_Click(object sender, EventArgs e)
        {
            Initialize();
            for (int i = 0; i < this.numericUpDownThread.Value; i++)
            {
                Thread thr = new Thread(GenerateStepID);
                thr.Start();
            }
        }
        private void buttonClearList_Click(object sender, EventArgs e)
        {
            this.listViewID.Items.Clear();
        }
        private void buttonValidate_Click(object sender, EventArgs e)
        {
            FindDuplicatedID();
        }   
    }
}