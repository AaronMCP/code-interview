using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public class QRListControler : ListControler2
    {
        public QRListControler(ListView listViewElement)
            : base(true, listViewElement, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log)
        {
        }

        public bool EnsurePrimaryKey = false;
        private bool bPrimaryKeyWarning = true;

        public override void ClearAll()
        {
            bPrimaryKeyWarning = false;
            base.ClearAll();
            bPrimaryKeyWarning = true;
        }
        public override void RefreshList(IDicomMappingItem[] itemlist)
        {
            base.RefreshList(itemlist);

            foreach (ListViewItem item in _listViewElement.Items)
            {
                MWLQueryResultItem qr = item.Tag as MWLQueryResultItem;
                if (qr == null) continue;
                if ((qr.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID && Program.ConfigMgt.Config.AutoGenerateSTDUID) ||     //RIS2.0 is not responsible for creating study instnace uid
                     (qr.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID && Program.ConfigMgt.Config.AutoGenerateRPID) ||
                     (qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID && Program.ConfigMgt.Config.AutoGenerateSPSPID))
                {
                    //item.ForeColor = clrAuto;
                    //item.SubItems[3].Text = "";
                    item.SubItems[4].Text = "[Auto]";
                }
                else if ((qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationAETitle && Program.ConfigMgt.Config.LookupAETitleByIPInModalityListForScheduledStationAETitle) ||
                        (qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationName && Program.ConfigMgt.Config.LookupDescriptionByIPInModalityListForScheduledStationName))
                {
                    item.SubItems[4].Text = "[Lookup]";
                }
                else if (qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepSequence ||
                        qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProtocolCodeSequence)
                {
                    item.ForeColor = clrInvalid;
                }
            }
        }
        protected override void _listViewElement_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base._listViewElement_ItemCheck(sender, e);
            
            ListViewItem item = this._listViewElement.Items[e.Index];
            MWLQueryResultItem qr = item.Tag as MWLQueryResultItem;
            if( qr == null ) return;

            if (EnsurePrimaryKey && qr.TargetField == Program.ConfigMgt.Config.PrimaryKeyColumnName)
            {
                if (bPrimaryKeyWarning && e.NewValue != CheckState.Checked)
                {
                    MessageBox.Show(_listViewElement, "This element has been marked in the merging rule and should always be enabled. \r\n\r\n" +
                        "If you want to modify the element mapping rule, please click the \"Edit\" button.\r\n" +
                        "If you want to modify the merging rule, please click the \"Advance\" button.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                e.NewValue = CheckState.Checked;
            }

            if (//qr.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID ||
                //qr.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID ||
                //qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID ||
                qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProtocolCodeSequence ||
                qr.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepSequence)
            {
                e.NewValue = CheckState.Checked;
            }
        }
        protected override void BeforeShowFormElement<MWLQueryResultItem>(ButtonType type, FormElement2<MWLQueryResultItem> form, MWLQueryResultItem obj)
        {
            base.BeforeShowFormElement<MWLQueryResultItem>(type, form, obj);

            form.labelDirection.Text = "<--";

            if (type == ButtonType.Add || type == ButtonType.AddChild)
            {
                form.checkBoxRedundancy.Visible = false;
            }
            else if (type == ButtonType.Edit)
            {
                bool isAuto = false;
                bool isLookup = false;

                if (obj.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID)
                {
                    isAuto = true;
                    form.checkBoxRedundancy.Checked = Program.ConfigMgt.Config.AutoGenerateRPID;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID)
                {
                    isAuto = true;
                    form.checkBoxRedundancy.Checked = Program.ConfigMgt.Config.AutoGenerateSPSPID;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID)
                {
                    isAuto = true;
                    form.checkBoxRedundancy.Checked = Program.ConfigMgt.Config.AutoGenerateSTDUID;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationAETitle)
                {
                    isLookup = true;
                    form.checkBoxRedundancy.Text = "Lookup AE title in the modality list by calling IP.";
                    form.checkBoxRedundancy.Checked = Program.ConfigMgt.Config.LookupAETitleByIPInModalityListForScheduledStationAETitle;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationName)
                {
                    isLookup = true;
                    form.checkBoxRedundancy.Text = "Lookup description in the modality list by calling IP.";
                    form.checkBoxRedundancy.Checked = Program.ConfigMgt.Config.LookupDescriptionByIPInModalityListForScheduledStationName;
                }

                if (isAuto)
                {
                    form.checkBoxRedundancy.Text = "Auto generate if GC Gateway Field is empty.";
                }

                if (isAuto || isLookup)
                {
                    form.checkBoxRedundancy.CheckedChanged += delegate(Object o, EventArgs e)
                    {
                        if (form.checkBoxRedundancy.Checked)
                        {
                            form.checkBoxFixValue.Checked = false;
                            form.checkBoxLUT.Checked = false;
                        }
                    };
                    form.checkBoxFixValue.CheckedChanged += delegate(Object o, EventArgs e)
                    {
                        if (form.checkBoxFixValue.Checked)
                        {
                            form.checkBoxRedundancy.Checked = false;
                        }
                    };
                    form.checkBoxLUT.CheckedChanged += delegate(Object o, EventArgs e)
                    {
                        if (form.checkBoxLUT.Checked)
                        {
                            form.checkBoxRedundancy.Checked = false;
                        }
                    };
                }
                else
                {
                    form.checkBoxRedundancy.Visible = false;
                }
            }
        }
        protected override void AfterShowFormElement<MWLQueryResultItem>(ButtonType type, FormElement2<MWLQueryResultItem> form, MWLQueryResultItem obj)
        {
            base.AfterShowFormElement<MWLQueryResultItem>(type, form, obj);

            if (type == ButtonType.Edit)
            {
                if (obj.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID)
                {
                    Program.ConfigMgt.Config.AutoGenerateRPID = form.checkBoxRedundancy.Checked;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID)
                {
                    Program.ConfigMgt.Config.AutoGenerateSPSPID = form.checkBoxRedundancy.Checked;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID)
                {
                    Program.ConfigMgt.Config.AutoGenerateSTDUID = form.checkBoxRedundancy.Checked;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationAETitle)
                {
                    Program.ConfigMgt.Config.LookupAETitleByIPInModalityListForScheduledStationAETitle = form.checkBoxRedundancy.Checked;
                }
                else if (obj.TargetField == WorklistSCPHelper.DataColumns.ScheduledStationName)
                {
                    Program.ConfigMgt.Config.LookupDescriptionByIPInModalityListForScheduledStationName = form.checkBoxRedundancy.Checked;
                }
            }
        }
    }
}
