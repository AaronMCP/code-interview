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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            MListControler listCtrl1 = new MListControler(this.listBox1);
            IItem itemPatientID = listCtrl1.AddItem(new MListItem("PatientID"));
            IItem itemPatientName = listCtrl1.AddItem(new MListItem("PatientName"));

            MListControler listCtrl2 = new MListControler(this.listBox2);
            IItem itemPID = listCtrl2.AddItem(new MListItem("PID"));
            IItem itemPName = listCtrl2.AddItem(new MListItem("PName"));

            MListControler listCtrl3 = new MListControler(this.listBox3);
            IItem item0020 = listCtrl3.AddItem(new MListItem("(0010,0020)"));
            IItem item0010 = listCtrl3.AddItem(new MListItem("(0010,0010)"));

            //listCtrl1.Items[0].MapTarget = listCtrl2.Items[1];
            //listCtrl2.Items[1].MapTarget = listCtrl3.Items[0];
            
            _panelCtrl = new MPanelControler(this.panel1);
            _panelCtrl.AddList(listCtrl1);
            _panelCtrl.AddList(listCtrl2);
            _panelCtrl.AddList(listCtrl3);

            _panelCtrl.RelationList.Add(new MRelation(itemPatientID, itemPName));
            _panelCtrl.RelationList.Add(new MRelation(itemPName, new IItem[] { item0010, item0020 }));

            listCtrl1.RefreshList();
            listCtrl2.RefreshList();
            listCtrl3.RefreshList();
            _panelCtrl.Redraw();
        }

        private MPanelControler _panelCtrl;
    }
}