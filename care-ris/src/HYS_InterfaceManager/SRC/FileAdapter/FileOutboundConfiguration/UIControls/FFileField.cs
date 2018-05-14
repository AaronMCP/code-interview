using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.FileOutboundAdapterConfiguration.Forms;

namespace HYS.FileAdapter.FileOutboundAdapterConfiguration.UIControls
{
    public partial class FFileField : UserControl
    {
        public FFileField()
        {
            InitializeComponent();
            _ThrPartyDBParamterExOut = new ThrPartyDBParamterExOut();
            UpdateForm(true);
        }

        ThrPartyDBParamterExOut _ThrPartyDBParamterExOut;

        public FFileField(ThrPartyDBParamterExOut Param)
        {
            InitializeComponent();
            _ThrPartyDBParamterExOut = Param;
            UpdateForm(true);
        }

        /// <summary>
        /// bUpdate = true : Load _ThrPartyDBParamterExOut to Form
        /// bUpdate = false: Save Form to _ThrPartyDBParamterExOut
        /// </summary>
        /// <param name="bUpdate"></param>
        public void UpdateForm(bool bUpdate)
        {
            if (bUpdate)
            {
                this.tbFilePrefix.Text = _ThrPartyDBParamterExOut.FileFieldParam.FilePrefix;
                this.tbFileSuffix.Text = _ThrPartyDBParamterExOut.FileFieldParam.FileSuffix;
                this.tbFilePath.Text = _ThrPartyDBParamterExOut.FileFieldParam.FilePath;
                this.tbFileDTFormat.Text = _ThrPartyDBParamterExOut.FileFieldParam.FileDtFormat;
                this.tbTemplate.Text = _ThrPartyDBParamterExOut.FileFieldTemplate;
            }
            else
            {
                //throw exception when template is invalid
                XCollection<GWDataDBField> fields = ParseTemplate();
                
                _ThrPartyDBParamterExOut.FileFields.Clear();
                foreach (GWDataDBField f in fields)
                    _ThrPartyDBParamterExOut.FileFields.Add(f);

                _ThrPartyDBParamterExOut.FileFieldParam.FilePrefix = this.tbFilePrefix.Text;
                _ThrPartyDBParamterExOut.FileFieldParam.FileSuffix = this.tbFileSuffix.Text;
                _ThrPartyDBParamterExOut.FileFieldParam.FilePath= this.tbFilePath.Text;
                _ThrPartyDBParamterExOut.FileFieldParam.FileDtFormat = this.tbFileDTFormat.Text;
                _ThrPartyDBParamterExOut.FileFieldTemplate = this.tbTemplate.Text;

            }
        }

        private XCollection<GWDataDBField> ParseTemplate()
        {
            XCollection<GWDataDBField> fields = new XCollection<GWDataDBField>();

            string s, sub;
            s = tbTemplate.Text;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                sub = "[%" + f.GetFullFieldName().Replace(".", "_") + "%]";
                if (s.IndexOf(sub) >= 0)
                    fields.Add(f);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                sub = "[%" + f.GetFullFieldName().Replace(".", "_") + "%]";
                if (s.IndexOf(sub) >= 0)
                    fields.Add(f);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                sub = "[%" + f.GetFullFieldName().Replace(".", "_") + "%]";
                if (s.IndexOf(sub) >= 0)
                    fields.Add(f);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Report))
            {
                sub = "[%" + f.GetFullFieldName().Replace(".", "_") + "%]";
                if (s.IndexOf(sub) >= 0)
                    fields.Add(f);
            }
            
            return fields;
        }

        public ThrPartyDBParamterExOut CurrThrPartyDBParamterExOut
        {
            get
            {
                UpdateForm(false);
                return _ThrPartyDBParamterExOut;
            }
            set
            {
                _ThrPartyDBParamterExOut = value;
                UpdateForm(true);
            }
        }

       

        private void btSelFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select Folder";
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                this.tbFilePath.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void btSelFileFields_Click(object sender, EventArgs e)
        {
            XCollection<GWDataDBField> Fields = new XCollection<GWDataDBField>();
            try
            {
                Fields = ParseTemplate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FSelFields F = new FSelFields();
            if (F.ShowDialog(this, Fields) == DialogResult.OK)
            {
                foreach (GWDataDBField f in Fields)
                {
                    this.tbTemplate.Text += "[%" + f.GetFullFieldName().Replace(".","_") +"%]\r\n\r\n";
                }
            }
        }

        
    }
}
