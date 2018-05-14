using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonSCP_Click(object sender, EventArgs e)
        {
            FormSCP frm = new FormSCP(Program.ConfigMgt, Program.Log);
            frm.ShowDialog(this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Program.ConfigMgt.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQC_Click(object sender, EventArgs e)
        {
            FormQueryCriteria frm = new FormQueryCriteria();
            frm.ShowDialog(this);
        }

        private void buttonQR_Click(object sender, EventArgs e)
        {
            FormQueryResult frm = new FormQueryResult();
            frm.ShowDialog(this);
        }

        private void buttonService_Click(object sender, EventArgs e)
        {
            FormService frm = new FormService();
            frm.ShowDialog(this);
        }

        private void buttonIDGeneration_Click(object sender, EventArgs e)
        {
            FormAutoGenIDs frm = new FormAutoGenIDs();
            frm.ShowDialog(this);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //this.textBox1.Text = DHelper.GetMACAddress();
            //this.textBox1.Text = DHelper.GetMACAddressNumber().ToString();
        }

        //private decimal count;

        //private void buttonGUID_Click(object sender, EventArgs e)
        //{
        //    count = this.numericUpDownCount.Value;
        //    for (int i = 0; i < this.numericUpDownThread.Value; i++)
        //    {
        //        Thread thr1 = new Thread(GenerateGUID);
        //        thr1.Start();
        //    }

        //    //Thread thr2 = new Thread(GenerateGUID);
        //    //thr2.Start();

        //    //Thread thr3 = new Thread(GenerateGUID);
        //    //thr3.Start();

        //    //Thread thr4 = new Thread(GenerateGUID);
        //    //thr4.Start();
        //}

        //private void _AddItem(string guid, string thrID)
        //{
        //    ListViewItem item = new ListViewItem();
        //    item.Text = guid;
        //    item.SubItems.Add(guid.Length.ToString());
        //    item.SubItems.Add(thrID);
        //    this.listViewGUID.Items.Add(item);
        //    Application.DoEvents();
        //}

        //private delegate void AddItemHandler(string guid, string thrID);

        //private void AddItem(string guid, string thrID)
        //{
        //    AddItemHandler h = new AddItemHandler(_AddItem);
        //    this.Invoke(h, new object[] { guid, thrID });
        //}

        //private void GenerateGUID()
        //{
        //    //for (int i = 0; i < count; i++)
        //    //{
        //    //    //string guid = DHelper.GetDicomGUID("2147483647");
        //    //    string guid = DHelper.GetDicomGUID("500");
        //    //    AddItem(guid, Thread.CurrentThread.ManagedThreadId.ToString());
        //    //}

        //    List<string> list = new List<string>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        //string guid = DHelper.GetDicomGUID("2147483647");
        //        list.Add(DHelper.GetDicomGUID("500"));
        //    }
        //    foreach (string guid in list)
        //    {
        //        AddItem(guid, Thread.CurrentThread.ManagedThreadId.ToString());
        //    }
        //}

        //private void GenerateStepID()
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        string guid = HYS.DicomAdapter.MWLServer.Dicom.WorklistSCPHelper.GetRandomNumber();
        //        AddItem(guid, Thread.CurrentThread.ManagedThreadId.ToString());
        //    }
        //}

        //private void buttonStepD_Click(object sender, EventArgs e)
        //{
        //    count = this.numericUpDownCount.Value;
        //    for (int i = 0; i < this.numericUpDownThread.Value; i++)
        //    {
        //        Thread thr1 = new Thread(GenerateStepID);
        //        thr1.Start();
        //    }

        //    //Thread thr2 = new Thread(GenerateStepID);
        //    //thr2.Start();

        //    //Thread thr3 = new Thread(GenerateStepID);
        //    //thr3.Start();
        //}
    }
}