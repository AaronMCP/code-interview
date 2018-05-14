using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;

namespace OutboundDBInstall
{
    public partial class formFilter : Form
    {
        public formFilter()
        {
            InitializeComponent();
            
        }
        FilterFieldList _ffList;
        int CurrentSubFilterList;
        int CurrentFilterField;

        #region Load Field
        private void LoadFields()
        {
            this.tViewField.Nodes.Clear();


            GWDataDBField[] Index = GWDataDBField.GetFields(GWDataDBTable.Index);
            GWDataDBField[] Patient = GWDataDBField.GetFields(GWDataDBTable.Patient);
            GWDataDBField[] Order = GWDataDBField.GetFields(GWDataDBTable.Order);
            GWDataDBField[] Report = GWDataDBField.GetFields(GWDataDBTable.Report);

            tViewField.Nodes.Add("Index");
            foreach (GWDataDBField item in Index)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.i_IndexGuid.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.i_DataDateTime.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.i_PROCESS_FLAG.FieldName.ToUpper())
                    continue;

                if (item.FieldName.ToUpper().Equals(GWDataDBField.i_EventType.FieldName.ToUpper()))
                {
                    continue;
                }
               this.tViewField.Nodes[0].Nodes.Add(item.FieldName);
               
               
            }

            tViewField.Nodes.Add("Patient");
            foreach (GWDataDBField item in Patient)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                    continue;
                this.tViewField.Nodes[1].Nodes.Add(item.FieldName);

            }

            tViewField.Nodes.Add("Order");
            foreach (GWDataDBField item in Order)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                    continue;

                this.tViewField.Nodes[2].Nodes.Add(item.FieldName);
            }

            tViewField.Nodes.Add("Report");
            foreach (GWDataDBField item in Report)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                    continue;
                this.tViewField.Nodes[3].Nodes.Add(item.FieldName);
            }

            tViewField.ExpandAll();
        }



        #endregion

        public DialogResult ShowDialog(IWin32Window owner, FilterFieldList ffList)
        {
            _ffList = ffList;
            return base.ShowDialog(owner);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void formFilter_Load(object sender, EventArgs e)
        {
            LoadFields();
            LoadFilterField();
        }

        private void LoadFilterField()
        {
            tViewFilter.Nodes.Clear();
            btnAnd.Enabled = false;
            btnOr.Enabled = false;
            BtnDel.Enabled = false;
            btnApplyFilter.Enabled = false;
            cbBoxLogic.Enabled = false;
            tBoxLogicValue.Enabled = false;
            cMenuStripFilter.Enabled = false;

          if (_ffList == null )
          {
              return;
          }

            if(_ffList.Count<1)
            {                
                return;
            }

          for (int i = 0; i < _ffList.Count;i++ )
          {

              tViewFilter.Nodes.Add((i+1).ToString() + "(AND)");
              SubFilterFieldList sffl = (SubFilterFieldList)_ffList[i];
              for (int j = 0; j < sffl.Count;j++ )
              {
                  FilterField FF = (FilterField)sffl[j];
                  if (j < (sffl.Count - 1))
                  {
                      tViewFilter.Nodes[i].Nodes.Add(FF.Table + "." + FF.Field + " " + FF.Logic + " " + FF.LogicValue +" OR");
                  }
                  else
                  {
                      tViewFilter.Nodes[i].Nodes.Add(FF.Table + "." + FF.Field + " " + FF.Logic + " " + FF.LogicValue);
                  }
              }

          }

          CurrentSubFilterList = -1;
          CurrentFilterField = -1;

          tViewFilter.ExpandAll();

        }

        private void tViewFilter_AfterSelect(object sender, TreeViewEventArgs e)
        {         

            if (tViewFilter.SelectedNode.Parent == null)
            {
                CurrentSubFilterList = tViewFilter.SelectedNode.Index;
                cbBoxLogic.Enabled = false;
                cbBoxLogic.Text = "";
                tBoxLogicValue.Text = "";
                tBoxLogicValue.Enabled = false;
                btnApplyFilter.Enabled = false;

            }
            else
            {
                CurrentSubFilterList = tViewFilter.SelectedNode.Parent.Index;
                CurrentFilterField = tViewFilter.SelectedNode.Index;

                SubFilterFieldList sffl = (SubFilterFieldList)_ffList[CurrentSubFilterList];
                FilterField ff = (FilterField)sffl[CurrentFilterField];
                cbBoxLogic.Text = ff.Logic;
                tBoxLogicValue.Text = ff.LogicValue;

                cbBoxLogic.Enabled = true;
                tBoxLogicValue.Enabled = true;
                btnApplyFilter.Enabled = true;

            }

            cMenuStripFilter.Enabled = true;
            BtnDel.Enabled = true;

            btnAnd.Enabled = false;
            btnOr.Enabled = false;
        }

        private void tViewField_AfterSelect(object sender, TreeViewEventArgs e)
        {          
          
            if (tViewField.SelectedNode.Parent != null)
            {
                btnAnd.Enabled = true;
                if (tViewFilter.Nodes.Count < 1)
                {                   
                    btnOr.Enabled = false;
                }
                else
                {
                    btnOr.Enabled =true;
                }
            }

            BtnDel.Enabled = false;
            cMenuStripFilter.Enabled = false;
       
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            if (tViewField.SelectedNode.Parent == null)
            {
                return;
            }

            FilterField ff = new FilterField();
            ff.Table = tViewField.SelectedNode.Parent.Text;
            ff.Field = tViewField.SelectedNode.Text;
           
            SubFilterFieldList sffList = new SubFilterFieldList();
            sffList.Add(ff);

            _ffList.Add(sffList);
            LoadFilterField();
          
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            if ((CurrentSubFilterList<0) || (CurrentFilterField<0) )
            {
                return;
            }

            if(CurrentSubFilterList> tViewFilter.Nodes.Count)
            {
                return;
            }
          
            if (CurrentFilterField> tViewFilter.Nodes[CurrentSubFilterList].Nodes.Count)
            {
                return;
            }

            if (cbBoxLogic.Items.IndexOf(cbBoxLogic.Text)<0)
            {
                MessageBox.Show("Invalid Logic Operator, Please Select Logic Operator From List!","Error");
                return;
            }

            SubFilterFieldList sffList = (SubFilterFieldList)_ffList[CurrentSubFilterList];
            FilterField ff = (FilterField)sffList[CurrentFilterField];
            ff.Logic = cbBoxLogic.Text;
            ff.LogicValue = tBoxLogicValue.Text;

            /*
            if (CurrentFilterField == tViewField.Nodes[CurrentSubFilterList].Nodes.Count - 1)
            {
                tViewFilter.Nodes[CurrentSubFilterList].Nodes[CurrentFilterField].Text = ff.Table + "." + ff.Field + " " + ff.Logic + " " + ff.LogicValue ;
            }
            else
            {
                tViewFilter.Nodes[CurrentSubFilterList].Nodes[CurrentFilterField].Text = ff.Table + "." + ff.Field + " " + ff.Logic + " " + ff.LogicValue + "  (OR)";
            }
             * */
            LoadFilterField();
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            if (tViewField.SelectedNode.Parent == null)
            {
                return;
            }

            if (tViewFilter.Nodes.Count<1)
            {
                btnOr.Enabled = false;
                return;
            }

            FilterField ff = new FilterField();
            ff.Table = tViewField.SelectedNode.Parent.Text;
            ff.Field = tViewField.SelectedNode.Text;
            ff.Logic = "=";
            CurrentSubFilterList = tViewFilter.Nodes.Count - 1;

            SubFilterFieldList sffList = (SubFilterFieldList)_ffList[CurrentSubFilterList];
            sffList.Add(ff);

            tViewFilter.Nodes[CurrentSubFilterList].Nodes.Add(ff.Table + "." + ff.Field + " " + ff.Logic + " " + ff.LogicValue);
            tViewFilter.ExpandAll();
           // tViewFilter.Nodes[CurrentSubFilterList].Nodes[CurrentFilterField].Checked = true; 
        }

        private void DeleteFilter_Click(object sender, EventArgs e)
        {
            if (CurrentSubFilterList<0 || CurrentSubFilterList>= tViewFilter.Nodes.Count)
            {
                return;
            }

           

            if (MessageBox.Show("Are you sure delete this filter?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tViewFilter.SelectedNode.Parent != null)
                {
                    SubFilterFieldList sffl = (SubFilterFieldList)_ffList[CurrentSubFilterList];
                    FilterField ff =(FilterField)sffl[CurrentFilterField];
                    sffl.Remove(ff);
                    if (tViewFilter.SelectedNode.Parent.Nodes.Count <2)
                    {
                        _ffList.Remove(sffl);
                    }
                }else
                {
                    SubFilterFieldList sffl = (SubFilterFieldList)_ffList[CurrentSubFilterList];
                    _ffList.Remove(sffl);
                }

                LoadFilterField();
            }
        }

        private void cMenuStripFilter_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tViewField_Click(object sender, EventArgs e)
        {
            if (tViewField.SelectedNode ==  null)
            {
                return;
            }

            if (tViewField.SelectedNode.Parent != null)
            {
                btnAnd.Enabled = true;
                if (tViewFilter.Nodes.Count < 1)
                {
                    btnOr.Enabled = false;
                }
                else
                {
                    btnOr.Enabled = true;
                }
            }

            BtnDel.Enabled = false;
            cMenuStripFilter.Enabled = false;
        }

        private void tViewFilter_Click(object sender, EventArgs e)
        {

            if (tViewFilter.SelectedNode == null) return;

            if (tViewFilter.SelectedNode.Parent == null)
            {
                CurrentSubFilterList = tViewFilter.SelectedNode.Index;
                cbBoxLogic.Enabled = false;
                cbBoxLogic.Text = "";
                tBoxLogicValue.Text = "";
                tBoxLogicValue.Enabled = false;
                btnApplyFilter.Enabled = false;

            }
            else
            {
                CurrentSubFilterList = tViewFilter.SelectedNode.Parent.Index;
                CurrentFilterField = tViewFilter.SelectedNode.Index;

                SubFilterFieldList sffl = (SubFilterFieldList)_ffList[CurrentSubFilterList];
                FilterField ff = (FilterField)sffl[CurrentFilterField];
                cbBoxLogic.Text = ff.Logic;
                tBoxLogicValue.Text = ff.LogicValue;

                cbBoxLogic.Enabled = true;
                tBoxLogicValue.Enabled = true;
                btnApplyFilter.Enabled = true;

            }

            cMenuStripFilter.Enabled = true;
            BtnDel.Enabled = true;

            btnAnd.Enabled = false;
            btnOr.Enabled = false;
        }


    }
}
