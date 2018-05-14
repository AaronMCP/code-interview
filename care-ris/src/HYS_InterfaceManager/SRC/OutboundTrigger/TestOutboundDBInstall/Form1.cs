using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using OutboundDBInstall;
using HYS.Common.Objects.Rule;

namespace TestOutboundDBInstall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string t = Path.GetFileNameWithoutExtension("xxxyy");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// Members
            //System.Reflection.MemberInfo[] mis = typeof(IOChannelSettings.ConflictTreatMethodEnum).GetMembers();
            //this.listBox1.Items.Add("ConflictTreatMethodEnum Members:");
            //foreach (System.Reflection.MemberInfo mi in mis)
            //{
            //    this.listBox1.Items.Add("Name:"+mi.Name +" MemberType:" +mi.MemberType.ToString());
            //}

            //this.listBox1.Items.Add("");
            //// Fields
            //System.Reflection.FieldInfo[] fis = typeof(IOChannelSettings.ConflictTreatMethodEnum).GetFields();
            //this.listBox1.Items.Add("\nConflictTreatMethodEnum Fields:");
            //foreach (System.Reflection.FieldInfo mi in fis)
            //{
            //    this.listBox1.Items.Add("Name:" + mi.Name + " MemberType:" + mi.MemberType.ToString() + " IsLiteral:" + mi.IsLiteral.ToString());
            //}

            //this.listBox1.Items.Add("");
            //// Propertys
            //System.Reflection.PropertyInfo[] pis = typeof(IOChannelSettings.ConflictTreatMethodEnum).GetProperties();
            //this.listBox1.Items.Add("\nConflictTreatMethodEnum Propertys:");
            //foreach (System.Reflection.PropertyInfo mi in pis)
            //{
            //    this.listBox1.Items.Add("Name:" + mi.Name + "    MemberType:" + mi.MemberType.ToString());
            //}

            //this.listBox1.Items.Add("");
            //// Methods
            //System.Reflection.MethodInfo[] mdis = typeof(IOChannelSettings.ConflictTreatMethodEnum).GetMethods();
            //this.listBox1.Items.Add("\nConflictTreatMethodEnum Methods:");
            //foreach (System.Reflection.MethodInfo mi in mdis)
            //{
            //    this.listBox1.Items.Add("Name:" + mi.Name + "    MemberType:" + mi.MemberType.ToString());
            //}


            
        }

        private void btLoadData_Click(object sender, EventArgs e)
        {
            ////this.Column2.DataSource = HYS.Common.Objects.Rule.GWDataDBTable.Index;
            //this.Column3.DataSource = HYS.Common.Objects.Rule.GWDataDBField.GetFields(GWDataDBTable.Index);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //OutboundDBConfigMgt.Config.OutboundConfig.IName = "out";
            //IOChannel ch = new IOChannel();
            //ch.InboundConfig.IName = "in";

            ////this.richTextBox1.Text = OutboundDBConfigMgt.Config.OutboundConfig.View_Outbound_InstallScript(ch);
            //this.richTextBox1.Text = OutboundDBConfigMgt.Config.OutboundConfig.Procedure_IsRedundant_InstallScript(ch);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {

        }
    }
}