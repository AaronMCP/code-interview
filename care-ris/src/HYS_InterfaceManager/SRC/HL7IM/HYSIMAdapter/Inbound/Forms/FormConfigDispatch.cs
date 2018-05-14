using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Config;
using HYS.Common.Objects.Rule;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
{
    public partial class FormConfigDispatch : Form
    {
        private MessageDispatchConfig _config;

        public FormConfigDispatch(MessageDispatchConfig cfg)
        {
            _config = cfg;
            InitializeComponent();

            InitComboxTable();

            LoadSetting();
        }

        private void InitComboxTable()
        {
           this.comboBoxTable.DataSource =  Enum.GetNames(typeof(GWDataDBTable));
        }

        private void LoadSetting()
        {
            switch (_config.Model)
            {
                case MessageDispatchModel.Publish: this.radioButtonPublish.Checked = true; break;
                case MessageDispatchModel.Request: this.radioButtonRequest.Checked = true; break;
                case MessageDispatchModel.Custom: this.radioButtonCustom.Checked = true; break;
            }

            if (MessageDispatchModel.Custom == _config.Model)
            {
                this.comboBoxTable.Text = _config.TableName;
                comboBoxTable_SelectedIndexChanged(this.comboBoxTable, EventArgs.Empty);

                this.comboBoxField.Text = _config.FieldName;

                this.textBoxValueSubscriber.Text = _config.ValueSubscriber;
                this.textBoxValueResponser.Text = _config.ValueResponser;
            }
        }

        private bool SaveSetting()
        {
            if (this.radioButtonCustom.Checked)
            {
                if ((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), this.comboBoxTable.Text) != GWDataDBTable.None)
                {
                    _config.TableName = this.comboBoxTable.Text;
                    _config.FieldName = this.comboBoxField.Text;
                    _config.ValueSubscriber = this.textBoxValueSubscriber.Text;
                    _config.ValueResponser = this.textBoxValueResponser.Text;
                }
                else
                {
                    MessageBox.Show(this,
                        "Please select database table and field.",
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.comboBoxTable.Focus();
                    return false;
                }
            }

            if (this.radioButtonPublish.Checked) _config.Model = MessageDispatchModel.Publish;
            else if (this.radioButtonRequest.Checked) _config.Model = MessageDispatchModel.Request;
            else if (this.radioButtonCustom.Checked) _config.Model = MessageDispatchModel.Custom;

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxField.Items.Clear();
            GWDataDBTable table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), this.comboBoxTable.Text);

            if (GWDataDBTable.None != table)
            {
                GWDataDBField[] fields = GWDataDBField.GetFields(table);

                this.comboBoxField.Items.Clear();
                foreach (GWDataDBField field in fields)
                {
                    if (field.FieldName.ToUpper() == GWDataDBField.i_IndexGuid.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.i_DataDateTime.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.i_PROCESS_FLAG.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                        continue;
                    if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                        continue;

                    this.comboBoxField.Items.Add(field.FieldName);
                }
                this.comboBoxField.SelectedIndex = 0;
            }     
        }
    }
}
