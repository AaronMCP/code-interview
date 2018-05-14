using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Base.Processor;

namespace HYS.MessageDevices.MessagePipe.Processors.Forms
{
    public partial class FormProcessor : Form
    {
        private ConfigurationInitializationParameter _param;
       
        private ProcessorInstance _Processor = new ProcessorInstance();
        public ProcessorInstance Processor
        {
            get { return _Processor; }
            set { }
        }
       
        public FormProcessor(ConfigurationInitializationParameter _configparam,ProcessorInstance _Pi)
        {
            InitializeComponent();
            _param = _configparam;
            _Processor.Name = _Pi.Name;
            _Processor.DeviceName = _Pi.DeviceName;
            _Processor.Description = _Pi.Description;
            _Processor.Setting = _Pi.Setting;

            LoadProcessorType();
        }

        private void LoadProcessorType()
        {
            cmBoxProcessorType.Items.Clear();
            foreach(string name in ProcessorFactory.ProcessorRegistry)
            {
                cmBoxProcessorType.Items.Add(name);
            }

            if (cmBoxProcessorType.Items.Count >0)
            cmBoxProcessorType.SelectedIndex = 0;
        }

        private void FormProcessor_Load(object sender, EventArgs e)
        {
            LoadProcessor();
        }

        private void LoadProcessor()
        {
            tBoxProcessorName.Text = _Processor.Name;
            cmBoxProcessorType.Text = _Processor.DeviceName;
            tBoxDescription.Text = _Processor.Description;

            LoadSetting();
            
            
        }

        private void LoadSetting()
        {
            rtBoxSetting.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("Processor Name: "+_Processor.Setting);
            rtBoxSetting.Text = sb.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tBoxProcessorName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Processor Name can not be empty, please define a name for this processor.");
                tBoxProcessorName.Focus();
                return;
            }
            
            if (cmBoxProcessorType.Text.Equals(""))
            {
                MessageBox.Show("Please select the processor type!");
                return;
            }

            _Processor.Name = tBoxProcessorName.Text.Trim();
            _Processor.DeviceName = cmBoxProcessorType.Text;
            _Processor.Description = tBoxDescription.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (_Processor.Setting == null || _Processor.Setting.Equals(""))
            {
                CreatConfig();
            }
            else
            {
                EditConfig();
            }
        }

        private void CreatConfig()
        {
            if (this.cmBoxProcessorType.SelectedItem == null) return;
            string ProcessorType = this.cmBoxProcessorType.SelectedItem.ToString();

            IProcessorConfig cfg = ProcessorFactory.CreateProcessorConfig(ProcessorType, _param.Log);
            if (cfg == null) return;
                        
            if (!cfg.Initialize(_param)) return;

            string cfgXml = "";
            if (cfg.CreateConfig(this, out cfgXml))
            {
                _Processor.Setting = cfgXml.ToString();
                rtBoxSetting.Clear();
                rtBoxSetting.Text = _Processor.Setting;

            }
        }

        private void EditConfig()
        {
            if (this.cmBoxProcessorType.SelectedItem == null) return;
            string ProcessorType = this.cmBoxProcessorType.SelectedItem.ToString();

            IProcessorConfig cfg = ProcessorFactory.CreateProcessorConfig(ProcessorType, _param.Log);
            if (cfg == null) return;
                        
            if (!cfg.Initialize(_param)) return;

            string cfgXml = _Processor.Setting;
            if (cfg.EditConfig(this, ref cfgXml))
            {
                _Processor.Setting = cfgXml;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All configuration will be cancelled, continue?","Confirmation",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
