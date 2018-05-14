using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HYS.Adapter.Monitor.Controls
{
    public partial class UIDataGridView : DataGridView
    {
        private bool _Frozen = false;
        public bool Frozen
        {
            get { return _Frozen; }
            set
            {
                _Frozen = value;
                if (_Frozen)
                {
                    FrozenRow = this.CurrentCell.RowIndex;
                    FrozenColumn = this.CurrentCell.ColumnIndex;
                    for (int i = 0; i < this.ColumnCount; i++)
                        this.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                else
                {
                    FrozenRow = -1;
                    FrozenColumn = -1;
                    for (int i = 0; i < this.ColumnCount; i++)
                        this.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }

            }
        }

        private int _FrozenRow = -1;
        public int FrozenRow
        {
            get { return _FrozenRow; }
            set { _FrozenRow = value; }
        }

        private int _FrozenColumn = -1;
        public int FrozenColumn
        {
            get { return _FrozenColumn; }
            set { _FrozenColumn = value; }
        }

        protected override bool SetCurrentCellAddressCore(int columnIndex, int rowIndex, bool setAnchorCellAddress, bool validateCurrentCell, bool throughMouseClick)
        {
          try
            {
                if (!Frozen || FrozenRow < 0 || FrozenColumn < 0)
                {
                    return base.SetCurrentCellAddressCore(columnIndex, rowIndex, setAnchorCellAddress, validateCurrentCell, throughMouseClick);
                }


                if (rowIndex != FrozenRow)
                {
                    columnIndex = FrozenColumn;
                    rowIndex = FrozenRow;
                }
                else {
                    FrozenColumn = columnIndex;
                    FrozenRow = rowIndex;
                }
                return base.SetCurrentCellAddressCore(columnIndex, rowIndex, setAnchorCellAddress, validateCurrentCell, throughMouseClick);
            }
            catch (Exception ex)
            {
                Program.Log.Write(ex);
                return false;
            }
        }

        protected override void SetSelectedCellCore(int columnIndex, int rowIndex, bool selected)
        {
            try
            {
                if (!Frozen || FrozenRow < 0 || FrozenColumn < 0)
                {
                    base.SetSelectedCellCore(columnIndex, rowIndex, selected);
                    return;
                }

                if (selected)
                {
                    if (rowIndex != FrozenRow)
                        base.SetSelectedCellCore(FrozenColumn, FrozenRow, selected);
                    else
                        base.SetSelectedCellCore(columnIndex, rowIndex, selected);
                }
                else
                {
                    base.SetSelectedCellCore(columnIndex, rowIndex, selected);
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(ex);
            }
        }

    }
}
