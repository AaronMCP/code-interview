using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UITest.MappingViewer;

namespace UITest
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            MListForm frm1 = new MListForm();
            frm1.Text = "RIS Fields";
            IItem iPatientID = frm1.AddItem(new MListItem("PatientID"));
            IItem iPatientName = frm1.AddItem(new MListItem("PatientName"));

            MListForm frm2 = new MListForm();
            frm2.Text = "Broker Fields";
            IItem iPID = frm2.AddItem(new MListItem("PID"));
            IItem iPN = frm2.AddItem(new MListItem("PN"));

            MListForm frm3 = new MListForm();
            frm3.Text = "DICOM Fields";
            IItem dPN = frm3.AddItem(new MListItem("(0010,0010)"));
            IItem dPID = frm3.AddItem(new MListItem("(0010,0020)"));
            IItem dOPN = frm3.AddItem(new MListItem("(0010,0030)"));

            panelCtrl = new MPanelControler(this.panel1);
            panelCtrl.RelationList.Add(new MRelation(iPatientID, iPID));
            panelCtrl.RelationList.Add(new MRelation(iPatientName, iPN));
            panelCtrl.RelationList.Add(new MRelation(iPID, dPID));
            panelCtrl.RelationList.Add(new MRelation(iPN, dOPN));
            panelCtrl.RelationList.Add(new MRelation(iPN, dPN));
            panelCtrl.AddList(frm1);
            panelCtrl.AddList(frm2);
            panelCtrl.AddList(frm3);
            
            //panelCtrl.Redraw();
            panelCtrl.Reposition();
        }

        private MPanelControler panelCtrl;

        private void buttonReposition_Click(object sender, EventArgs e)
        {
            panelCtrl.Reposition();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            panelCtrl.ClearList();
            panelCtrl.Redraw();
        }
    }
}