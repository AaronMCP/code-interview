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
    public class QCListControler : ListControler2
    {
        public QCListControler(ListView listViewElement)
            : base(false, listViewElement, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log)
        {
        }

        public override void RefreshList(IDicomMappingItem[] itemlist)
        {
            base.RefreshList(itemlist);

            foreach (ListViewItem item in _listViewElement.Items)
            {
                MWLQueryCriteriaItem qr = item.Tag as MWLQueryCriteriaItem;
                if (qr == null) continue;
                if (qr.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartTime)
                {
                    item.ForeColor = clrInvalid;
                    item.SubItems[3].Text = "";
                    item.SubItems[4].Text = "[Merged to Start Date]";
                }
            }
        }
        protected override void _listViewElement_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            base._listViewElement_ItemCheck(sender, e);

            ListViewItem item = this._listViewElement.Items[e.Index];
            MWLQueryCriteriaItem qr = item.Tag as MWLQueryCriteriaItem;
            if (qr == null) return;

            if (qr.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartTime)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }
        protected override void BeforeShowFormElement<MWLQueryCriteriaItem>(ButtonType type, FormElement2<MWLQueryCriteriaItem> form, MWLQueryCriteriaItem obj)
        {
            base.BeforeShowFormElement<MWLQueryCriteriaItem>(type, form, obj);

            form.checkBoxRedundancy.Visible = false;

            if (type == ButtonType.Edit)
            {
                if (obj.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartDate)
                {
                    form._tagControler.Enabled = false;
                }
            }
        }
    }
}
