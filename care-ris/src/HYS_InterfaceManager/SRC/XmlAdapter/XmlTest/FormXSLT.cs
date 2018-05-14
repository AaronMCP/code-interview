using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.XmlAdapter.Common.Objects;

namespace XmlTest
{
    public partial class FormXSLT : Form
    {
        public FormXSLT()
        {
            InitializeComponent();
        }

        private IEControler _ieCtrlGC;
        private IEControler _ieCtrlIn;
        private IEControler _ieCtrlOut;
        private XMLTransformer _transformer;
        private XMLTransformer _transformerOut;

        private void checkBoxHTML_CheckedChanged(object sender, EventArgs e)
        {
            _ieCtrlGC.XML = !checkBoxHTML.Checked;
            _ieCtrlGC.RefreshText();
        }
        private void buttonOutput_Click(object sender, EventArgs e)
        {
            string str = "";
            if (_transformerOut == null) return;
            if (_transformerOut.TransformString(this.textBoxGCXml.Text, ref str))
            {
                this.textBoxOutXML.Text = str;
            }
            else
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
            }
        }
        private void buttonInput_Click(object sender, EventArgs e)
        {
            string str = "";
            if (_transformer == null) return;
            if (_transformer.TransformString(this.textBoxInXML.Text, ref str))
            {
                this.textBoxGCXml.Text = str;
            }
            else
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _ieCtrlGC = new IEControler(this, this.textBoxGCXml, this.checkBoxGC, true);
            _ieCtrlIn = new IEControler(this.panelIn, this.textBoxInXML, this.checkBoxIn, true);
            _ieCtrlOut = new IEControler(this.panelOut, this.textBoxOutXML, this.checkBoxOut, true);

            _transformer = XMLTransformer.CreateFromFile(Program.XSLFileName);
            if (_transformer == null)
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
            }

            _transformerOut = XMLTransformer.CreateFromFile(Program.XSLFileNameOut);
            if (_transformerOut == null)
            {
                MessageBox.Show(XMLTransformer.LastErrorInfor);
            }

            using (StreamReader sr = File.OpenText(Program.XMLFileName))
            {
                this.textBoxInXML.Text = sr.ReadToEnd();
            }
        }
    }
}