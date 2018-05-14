using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Translation;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormCode : Form
    {
        public FormCode()
        {
            InitializeComponent();
            mgt.OnChange += new CodeStatusChangeEventHandler(mgt_OnChange);
        }

        private bool lockText;

        private string originalText = "";

        private CodeStatusMgt mgt = new CodeStatusMgt();

        private void mgt_OnChange(CodeStatus from, CodeStatus to)
        {
            if (from is InitCode)
            {
                originalText = this.textBoxWord.Text;
            }

            if (to is InitCode)
            {
                this.buttonBIG5_GB.Enabled = true;
                this.buttonBIG5_GBK.Enabled = true;
                this.buttonGB_BIG5.Enabled = true;
                this.buttonGB_GBK.Enabled = true;
                this.buttonGBK_BIG5.Enabled = true;
                this.buttonGBK_GB.Enabled = true;
            }
            else if (to is GBCode)
            {
                this.buttonBIG5_GB.Enabled = false;
                this.buttonBIG5_GBK.Enabled = false;
                this.buttonGB_BIG5.Enabled = true;
                this.buttonGB_GBK.Enabled = true;
                this.buttonGBK_BIG5.Enabled = false;
                this.buttonGBK_GB.Enabled = false;
            }
            else if (to is GBKCode)
            {
                this.buttonBIG5_GB.Enabled = false;
                this.buttonBIG5_GBK.Enabled = false;
                this.buttonGB_BIG5.Enabled = false;
                this.buttonGB_GBK.Enabled = false;
                this.buttonGBK_BIG5.Enabled = true;
                this.buttonGBK_GB.Enabled = true;
            }
            else if (to is BIG5Code)
            {
                this.buttonBIG5_GB.Enabled = true;
                this.buttonBIG5_GBK.Enabled = true;
                this.buttonGB_BIG5.Enabled = false;
                this.buttonGB_GBK.Enabled = false;
                this.buttonGBK_BIG5.Enabled = false;
                this.buttonGBK_GB.Enabled = false;
            }
        }

        private void checkBoxWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxWord.WordWrap = this.checkBoxWrap.Checked;
        }

        private void textBoxWord_TextChanged(object sender, EventArgs e)
        {
            if (lockText) return;
            mgt.Reset();
            this.Text = DateTime.Now.Ticks.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.Reset();
            this.textBoxWord.Text = originalText;
            lockText = false;
        }

        private void buttonGB_GBK_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.GB_GBK(); 
            this.textBoxWord.Text = ChineseCode.GB2GBK(this.textBoxWord.Text);
            lockText = false;
        }

        private void buttonGB_BIG5_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.GB_BIG5();
            this.textBoxWord.Text = ChineseCode.GB2BIG5(this.textBoxWord.Text);
            lockText = false;
        }

        private void buttonGBK_GB_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.GBK_GB(); 
            this.textBoxWord.Text = ChineseCode.GBK2GB(this.textBoxWord.Text);
            lockText = false;
        }

        private void buttonGBK_BIG5_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.GBK_BIG5();
            this.textBoxWord.Text = ChineseCode.GBK2BIG5(this.textBoxWord.Text);
            lockText = false;
        }

        private void buttonBIG5_GB_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.BIG5_GB();
            this.textBoxWord.Text = ChineseCode.BIG52GB(this.textBoxWord.Text);
            lockText = false;
        }

        private void buttonBIG5_GBK_Click(object sender, EventArgs e)
        {
            lockText = true;
            mgt.BIG5_GBK();
            this.textBoxWord.Text = ChineseCode.BIG52GBK(this.textBoxWord.Text);
            lockText = false;
        }
    }
}